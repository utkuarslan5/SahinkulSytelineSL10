Imports Janus.Windows.GridEX

Public Class frmCekmeListesi

    #Region "Fields"

    Dim bAmbalaj As Boolean
    Dim bSorgu As Boolean
    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Dim dt As New DataTable
    Dim sQuery As String

    #End Region 'Fields

    #Region "Methods"

    Public Sub AmbalajDegistir()
        Dim AmbKod, Malzeme As String

        Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

        checkedRows = Me.GridEX1.GetCheckedRows()

        Dim row As Janus.Windows.GridEX.GridEXRow

        For i As Integer = 0 To GridEX1.RowCount - 1

            row = GridEX1.GetRow(i)

            If row.RowType <> RowType.GroupHeader And row.Cells(0).Value = True Then

                AmbKod = row.Cells("AmbKod").Text

                Malzeme = row.Cells("ADAITX").Text

                sQuery = " Select KMIK, PKSIR, PKMIK" & _
                            " From Itmpack" & _
                            " Where AMBKOD = " & sTirnakEkle(AmbKod.Trim) & _
                            " AND ITNBR = " & sTirnakEkle(Malzeme)

                dt = db.RunSql(sQuery)

                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                    GridEX1.Update()

                    row.BeginEdit()

                    row.Cells("KMIK").Text = dt.Rows(0)("KMIK").ToString
                    row.Cells("KMIK").Value = dt.Rows(0)("KMIK").ToString

                    row.Cells("PKSIR").Text = dt.Rows(0)("PKSIR").ToString
                    row.Cells("PKSIR").Value = dt.Rows(0)("PKSIR").ToString

                    row.Cells("PKMIK").Text = dt.Rows(0)("PKMIK").ToString
                    row.Cells("PKMIK").Value = dt.Rows(0)("PKMIK").ToString

                    row.Cells("AMBKOD").Text = AmbKod
                    row.Cells("AMBKOD").Value = AmbKod

                    row.EndEdit()

                    GridEX1.Update()

                    GridEX1.Update()

                End If

            End If

        Next

        Hesapla()
    End Sub

    Public Sub Hesapla()
        Dim row As Janus.Windows.GridEX.GridEXRow

        For i As Integer = 0 To GridEX1.RowCount - 1

            row = GridEX1.GetRow(i)

            If row.RowType <> RowType.GroupHeader Then
                row.BeginEdit()
                Dim isevkmik As Integer

                If row.Cells("SevkMik").Text <> "" Then

                    isevkmik = row.Cells("SevkMik").Text

                Else

                    isevkmik = 0

                End If

                If isevkmik <> 0 Then

                    If row.Cells("KMIK").Text <> "0" And row.Cells("KMIK").Text <> "" Then

                        row.Cells("KUTUADET").Text = CStr(CInt(row.Cells("SevkMik").Text) / CInt(row.Cells("KMIK").Text))

                        row.Cells("KUTUADET").Text = CInt(row.Cells("KUTUADET").Text)

                        row.Cells("KUTUADET").Value = CInt(row.Cells("KUTUADET").Text)

                    End If

                End If

                If CDec(row.Cells("KUTUADET").Text) - CInt(row.Cells("KUTUADET").Text) <> 0 Then

                    row.Cells("DURUM").Text = "50"
                    row.Cells("DURUM").Value = "50"

                Else

                    row.Cells("DURUM").Text = "00"
                    row.Cells("DURUM").Value = "00"

                End If

                row.EndEdit()

            End If

        Next i
    End Sub

    Private Sub AddConditionalFormatting()
        'Imports Janus.Windows.GridEX is used in this module

        'Adding a condition using Discontinued field

        Dim fc As GridEXFormatCondition

        ''adding a format condition to use font bold when OnSale field is true
        'fc = New GridEXFormatCondition(GridEX1.RootTable.Columns("ADIIST"), ConditionOperator.GreaterThan, "50")

        'fc.FormatStyle.FontBold = TriState.True

        'fc.FormatStyle.ForeColor = Color.Red

        'GridEX1.RootTable.FormatConditions.Add(fc)

        fc = New GridEXFormatCondition(GridEX1.RootTable.Columns("DURUM"), ConditionOperator.GreaterThanOrEqualTo, "50")

        fc.FormatStyle.FontBold = TriState.True

        fc.FormatStyle.ForeColor = Color.Blue

        GridEX1.RootTable.FormatConditions.Add(fc)

        fc = New GridEXFormatCondition(GridEX1.RootTable.Columns("KMIK"), ConditionOperator.Equal, "0")

        'setting Format style properties to be applied to all the rows
        'that met this condition
        fc.FormatStyle.FontStrikeout = TriState.True

        fc.FormatStyle.ForeColor = Color.Red

        GridEX1.RootTable.FormatConditions.Add(fc)

        fc = New GridEXFormatCondition(GridEX1.RootTable.Columns("packed"), ConditionOperator.Equal, "1")

        'setting Format style properties to be applied to all the rows
        'that met this condition
        fc.FormatStyle.FontStrikeout = TriState.True

        fc.FormatStyle.ForeColor = Color.Red

        GridEX1.RootTable.FormatConditions.Add(fc)
    End Sub

    Private Sub btnOlustur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOlustur.Click
        'Dim sFileName, sLine As String

        Dim bCreate As Boolean

        Dim iKdz93x As Integer

        Dim PSay As Double = 0

        Dim HSay As Double = 0

        Dim SSay As Double = 0

        Dim KSay As Double = 0

        'Dim objWriter As System.IO.StreamWriter

        Try

            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

            sQuery = " Delete From TRCEKLIST"

            dbAccess.RunSql(sQuery, True)

            Dim sGroup As String = ""
            Dim sMalzeme As String = ""

            'sFileName = "C:\Edi\" & Now.Date.ToString("yyyyMMddhhss") & "Picklist" & ".txt"

            'objWriter = New System.IO.StreamWriter(sFileName)

            If Not chkDegisken.Checked Then

                AmbalajDegistir()

            End If

            checkedRows = Me.GridEX1.GetCheckedRows()

            If checkedRows.Length = 0 Then

                MessageBox.Show("Lütfen  Seçim Yapýnýz!")

            Else

                If MessageBox.Show(" Çekme Listesi Oluþturulsun mu?", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    bCreate = True

                Else

                    bCreate = False

                    Exit Sub

                End If

                Dim row As Janus.Windows.GridEX.GridEXRow

                Dim sKontrolAmbalaj As String = ""
                Dim iVadeFarkli As Integer = 0
                Dim sVadeKosullari As String = ""

                For Each row In checkedRows

                    If Not sVadeKosullari.Contains(row.Cells("terms_code").Text) Then

                        sVadeKosullari = IIf(sVadeKosullari = "", "", sVadeKosullari & ",") & row.Cells("terms_code").Text

                        iVadeFarkli += 1

                    End If

                    If Not sKontrolAmbalaj.Contains(row.Cells("ADAITX").Text) Then

                        sKontrolAmbalaj = IIf(sKontrolAmbalaj = "", row.Cells("ADAITX").Text & "-" & _
                                                    row.Cells("Ambkod").Text, sKontrolAmbalaj & "," & row.Cells("ADAITX").Text & "-" & _
                                                    row.Cells("Ambkod").Text)

                    Else

                        If Not sKontrolAmbalaj.Contains(row.Cells("ADAITX").Text & "-" & _
                                                     row.Cells("Ambkod").Text) Then

                            MsgBox(row.Cells("ADAITX").Text & " Nolu  Malzeme Ýçin Ambalaj Uyuþmazlýðý Mevcut.Lütfen Kotrol Ediniz...!", MsgBoxStyle.OkOnly, "Ekip Mapics")

                            Exit Sub

                        End If

                    End If

                Next

                If iVadeFarkli > 1 Then

                    MessageBox.Show("Farklý Vade Koþullarýna Ait Çekme Listeleri Seçili!")

                    Exit Sub

                End If

                Try

                    db.BeginTransaction()

                    For Each row In checkedRows

                        If nLookup("Boyut", _
                                   " Tr_Ambalaj_Boyutlari", _
                                   " Ambkod=" & sTirnakEkle(row.Cells("Ambkod").Text) & _
                                   " And Itnbr=" & sTirnakEkle(row.Cells("ADAITX").Text)) = 0 Then

                            Throw New Exception(row.Cells("ADAITX").Text & " Nolu Malzemenin " & _
                                            row.Cells("Ambkod").Text & " Nolu " & "Ambalajýnýn Bilgileri Eksik(Boyut, En, Yükseklik V.b)! Lütfen Kontrol Ediniz...")

                        End If

                        If sLookup("packed", "coitem", " co_num=" & sTirnakEkle(row.Cells("ADCVNB").Text) & _
                                                            " And co_line=" & row.Cells("ADFCNB").Text) = "1" Then

                            Throw New Exception("Sipariþlerden  eþ zamanlý çekilen var! Yeniden Seçiniz...")

                        End If

                        sQuery = " Update  MBADREP" & _
                                    " Set packed='1' " & _
                                    " Where ADCVNB='" & row.Cells("ADCVNB").Text & "'" & _
                                        " And ADAITX='" & row.Cells("ADAITX").Text & "'" & _
                                        " And ADFCNB=" & row.Cells("ADFCNB").Text

                        db.RunSql(sQuery, True)

                    Next

                    '    db.CommitTransaction()

                    'Catch ex As Exception

                    '    db.RollbackTransaction()

                    '    Throw ex

                    'End Try

                    For Each row In checkedRows

                        If row.RowType <> RowType.GroupHeader Then

                            If row.Cells("packed").Text = "1" Then

                                MsgBox(" Bu Sipariþ Seçilemez. Açýk Çekme Listesi Mevcut!", MsgBoxStyle.OkOnly, "Ekip Mapics")

                                Exit Sub

                            End If

                            sQuery = " Select * " & _
                                        " From Trceklist" & _
                                        " Where (Sevkno=0 Or Sevkno is null) " & _
                                            " And Adcvnb=" & sTirnakEkle(row.Cells("ADCVNB").Text) & _
                                            " And Adfcnb=" & row.Cells("ADFCNB").Text & _
                                            " And Durum<>'X'"

                            dt = db.RunSql(sQuery)

                            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                                MsgBox(" Bu Sipariþ Seçilemez. Açýk Çekme Listesi Mevcut! Çekme No..:" & _
                                        dt.Rows(0).Item("CEKLIST").ToString, _
                                        MsgBoxStyle.OkOnly, "Ekip Mapics")

                                Exit Sub

                            End If

                            If row.Cells("KMIK").Text = "0" Then

                                MsgBox(" Bu Sipariþ Seçilemez. Ambalaj bilgilerinde sorun var!", MsgBoxStyle.OkOnly, "Ekip Mapics")

                                sQuery = " Update  MBADREP" & _
                                        " Set packed='0' " & _
                                        " Where ADCVNB='" & row.Cells("ADCVNB").Text & "'" & _
                                            " And ADAITX='" & row.Cells("ADAITX").Text & "'" & _
                                            " And ADFCNB=" & row.Cells("ADFCNB").Text

                                db.RunSql(sQuery, True)

                                Exit Sub

                            End If

                            If row.Cells("SevkMik").Text <> "" Then

                                If row.Cells("SevkMik").Text <> 0 Then

                                    If row.Cells("KMIK").Text <> "0" And row.Cells("KMIK").Text <> "" Then

                                        row.Cells("KUTUADET").Text = CStr(CInt(row.Cells("SevkMik").Text) / CInt(row.Cells("KMIK").Text))

                                    Else

                                        row.Cells("KUTUADET").Text = 0

                                    End If

                                End If

                            End If

                            If row.Cells("SevkMik").Text > row.Cells("ADAQQT").Value Then

                                MsgBox(" Sevk Miktarý Bakiye den büyük olamaz !", MsgBoxStyle.OkOnly, "Ekip Mapics")

                                row.Cells("SevkMik").Text = row.Cells("ADAQQT").Value

                                Exit Sub

                            End If

                            'If CDec(row.Cells("KUTUADET").Text) - CInt(row.Cells("KUTUADET").Text) <> 0 Then

                            '    MsgBox(" Bu Sipariþ Seçilemez. Bakiye, Kutu Ýçi Miktarla Uyuþmuyor. Bakiye Düzenlemesi Yapýn !", MsgBoxStyle.OkOnly, "Ekip Mapics")

                            '    row.Cells("KUTUADET").Text = "0"

                            '    sQuery = " Update  MBADREP" & _
                            '            " Set packed='0' " & _
                            '            " Where ADCVNB='" & row.Cells("ADCVNB").Text & "'" & _
                            '                " And ADAITX='" & row.Cells("ADAITX").Text & "'" & _
                            '                " And ADFCNB=" & row.Cells("ADFCNB").Text

                            '    db.RunSql(sQuery, True)

                            '    Exit Sub

                            'Else

                            row.Cells("KUTUADET").Text = CInt(row.Cells("KUTUADET").Text)

                            'End If

                            '************* Palet Sayýsý Hesaplama ********************

                            If row.Cells("PKMIK").Text <> "0" And row.Cells("PKSIR").Text <> "0" Then

                                If CDec(row.Cells("KUTUADET").Text / row.Cells("PKSIR").Text) = Math.Truncate(row.Cells("KUTUADET").Text / row.Cells("PKSIR").Text) Then

                                    HSay = 0

                                    KSay = row.Cells("KUTUADET").Text

                                    If Math.Truncate(KSay / row.Cells("PKMIK").Text) = CDec(KSay / row.Cells("PKMIK").Text) Then

                                        PSay = Math.Truncate(KSay / row.Cells("PKMIK").Text)

                                    Else

                                        PSay = Math.Truncate(KSay / row.Cells("PKMIK").Text)

                                        PSay = PSay + 1

                                    End If

                                Else

                                    HSay = row.Cells("KUTUADET").Text - ((Math.Truncate(row.Cells("KUTUADET").Text / row.Cells("PKSIR").Text) * row.Cells("PKSIR").Text))

                                    KSay = row.Cells("KUTUADET").Text - HSay

                                    If Math.Truncate(KSay / row.Cells("PKMIK").Text) = CDec(KSay / row.Cells("PKMIK").Text) Then

                                        PSay = Math.Truncate(KSay / row.Cells("PKMIK").Text)

                                    Else

                                        PSay = Math.Truncate(KSay / row.Cells("PKMIK").Text)

                                        PSay = PSay + 1

                                    End If

                                End If

                            End If

                        End If

                        If bCreate = True Then

                            If row.Cells("Grp").Text <> sGroup Then

                                iKdz93x = SeriNoAl("PICK")

                                sGroup = row.Cells("Grp").Text

                                sMalzeme = row.Cells("ADAITX").Text

                                'sLine = "Picklist Number:" & iKdz93x & vbTab & "Group: " & sGroup

                                'objWriter.WriteLine(sLine)

                            ElseIf rdbMalzeme.Checked And row.Cells("ADAITX").Text <> sMalzeme Then

                                iKdz93x = SeriNoAl("PICK")

                                sGroup = row.Cells("Grp").Text

                                sMalzeme = row.Cells("ADAITX").Text

                                'sLine = "Picklist Number:" & iKdz93x & vbTab & "Group: " & sGroup

                                'objWriter.WriteLine(sLine)

                            End If

                            'row.Cells(1).Text

                            Dim sPusNo As String

                            sPusNo = IIf(Copy(row.Cells("CJCBTX").Text, 0, 2) = "EO" Or _
                                        Copy(row.Cells("CJCBTX").Text, 0, 2) = "FO", _
                                        "", _
                                        row.Cells("CJCBTX").Text)

                            sQuery = " INSERT INTO TRCEKLIST" & _
                                        " (CEKLIST, C6CANB, CUSNM, C6B9CD, C6F1CD, CJCBTX ," & _
                                        " CDAFYV, ADCVNB, ADAITX, ITDSC, ITEM,REFADI, MOHTQ ," & _
                                        " ADDZVA, ADAQQT, ADBJDT, KMIK, C6FNST, ADIIST, DEF1CD ," & _
                                        " ADA3CD ,CEKDATE,ADFCNB,KANBANNO,Ambkod,DURUM, CRDUSR ," & _
                                        " KSAY, PSAY, HKSAY, PKSAY, PKSIR,L3PREF) " & _
                                        " VALUES(" & iKdz93x & "," & _
                                        sTirnakEkle(row.Cells("C6CANB").Text) & "," & _
                                        sTirnakEkle(row.Cells("CUSNM").Text) & "," & _
                                        sTirnakEkle(row.Cells("C6B9CD").Text) & "," & _
                                        sTirnakEkle(row.Cells("C6F1CD").Text) & "," & _
                                        sTirnakEkle(sPusNo) & "," & _
                                        sTirnakEkle(row.Cells("CDAFYV").Text) & "," & _
                                        sTirnakEkle(row.Cells("ADCVNB").Text) & "," & _
                                        sTirnakEkle(row.Cells("ADAITX").Text) & "," & _
                                        sTirnakEkle(row.Cells("ITDSC").Text) & "," & _
                                        sTirnakEkle(row.Cells("ITEM").Text) & "," & _
                                        sTirnakEkle(row.Cells("REFADI").Text) & "," & _
                                        row.Cells("MOHTQ").Text.Replace(",", "") & "," & _
                                        row.Cells("ADDZVA").Text.Replace(",", "") & "," & _
                                        row.Cells("SevkMik").Text.Replace(",", "") & "," & _
                                        CDate(row.Cells("ADBJDT").Text).Date.ToString("1yyMMdd") & "," & _
                                        row.Cells("KMIK").Text.Replace(",", "") & "," & _
                                        sTirnakEkle(row.Cells("C6FNST").Text) & "," & _
                                        sTirnakEkle(row.Cells("ADIIST").Text) & "," & _
                                        sTirnakEkle(row.Cells("DEF1CD").Text) & "," & _
                                        sTirnakEkle(row.Cells("ADA3CD").Text) & "," & _
                                        sTirnakEkle(Now.Date.ToString("1yyMMdd")) & "," & _
                                        row.Cells("ADFCNB").Text & "," & _
                                        sTirnakEkle(row.Cells("KANBAN").Text) & "," & _
                                        sTirnakEkle(row.Cells("Ambkod").Text) & "," & _
                                        "'0'," & _
                                        sTirnakEkle(KullaniciAdi) & "," & _
                                        row.Cells("KUTUADET").Text & "," & _
                                        PSay & "," & _
                                        HSay & "," & _
                                        row.Cells("PKMIK").Text & "," & _
                                        row.Cells("PKSIR").Text & "," & _
                                        sTirnakEkle(row.Cells("L3PREF").Text) & _
                                        ")"

                            'Durum Alaný için
                            '0  Oluþturuldu
                            '1  Yazdýrýldý
                            '2  Sevk Edildi

                            db.RunSql(sQuery, True)

                            'QueriesTableAdapter1.InsertQuery(iKdz93x, row.Cells("C6CANB").Text, _
                            'row.Cells("CUSNM").Text, row.Cells("C6B9CD").Text, _
                            'row.Cells("C6F1CD").Text, row.Cells("CJCBTX").Text, _
                            'row.Cells("CDAFYV").Text, row.Cells("ADCVNB").Text, _
                            'row.Cells("ADAITX").Text, row.Cells("ITDSC").Text, _
                            'row.Cells("ITEM").Text, row.Cells("REFADI").Text, _
                            'row.Cells("MOHTQ").Text.Replace(",", ""), row.Cells("ADDZVA").Text.Replace(",", ""), _
                            'row.Cells("ADAQQT").Text.Replace(",", ""), _
                            'CDate(row.Cells("ADBJDT").Text).Date.ToString("1yyMMdd"), _
                            'row.Cells("KMIK").Text.Replace(",", ""), row.Cells("C6FNST").Text, _
                            'row.Cells("ADIIST").Text, row.Cells("DEF1CD").Text, _
                            'row.Cells("ADA3CD").Text, Now.Date.ToString("1yyMMdd"), _
                            'row.Cells("ADFCNB").Text, _
                            'row.Cells("KANBAN").Text, row.Cells("Ambkod").Text)
                            If db.Result.ReturnValue = True Then

                                sQuery = " Update  MBADREP" & _
                                            " Set packed='1' " & _
                                            ", qty_packed=" & iKdz93x & _
                                            " Where ADCVNB=" & sTirnakEkle(row.Cells("ADCVNB").Text) & _
                                                " And ADAITX=" & sTirnakEkle(row.Cells("ADAITX").Text) & _
                                                " And ADFCNB=" & row.Cells("ADFCNB").Text

                                db.RunSql(sQuery, True)

                            Else

                                sQuery = " Delete " & _
                                        " From TRCEKLIST" & _
                                        " Where CEKLIST=" & iKdz93x

                                db.RunSql(sQuery, True)

                                sQuery = " Update  MBADREP" & _
                                            " Set packed='0' " & _
                                            ", qty_packed=0" & _
                                            " Where qty_packed=" & iKdz93x

                                db.RunSql(sQuery, True)

                            End If

                        End If



                        'sQuery = " INSERT INTO  TRCEKLIST" & _
                        '            " (CEKLIST, C6CANB, CUSNM, C6B9CD, C6F1CD, CJCBTX, " & _
                        '            " CDAFYV, ADCVNB, ADAITX, ITDSC, ITEM,REFADI, MOHTQ, " & _
                        '            " ADDZVA, ADAQQT, ADBJDT, KMIK, C6FNST, ADIIST, DEF1CD , ADA3CD ,CEKDATE,ADFCNB ) " & _
                        '            " VALUES(" & iKdz93x & "," & row.Cells("C6CANB").Text & "," & _
                        '        "'" & row.Cells("CUSNM").Text & "','" & row.Cells("C6B9CD").Text & "'," & _
                        '        "'" & row.Cells("C6F1CD").Text & "','" & row.Cells("CJCBTX").Text & "'," & _
                        '        "'" & row.Cells("CDAFYV").Text & "','" & row.Cells("ADCVNB").Text & "'," & _
                        '        "'" & row.Cells("ADAITX").Text & "','" & row.Cells("ITDSC").Text & "'," & _
                        '        "'" & row.Cells("ITEM").Text & "','" & row.Cells("REFADI").Text & "'," & row.Cells("MOHTQ").Text.Replace(",", "") & "," & _
                        '        row.Cells("ADDZVA").Text.Replace(",", "") & "," & row.Cells("SEVKMIK").Text.Replace(",", "") & "," & _
                        '        CDate(row.Cells("ADBJDT").Text.Substring(6, 2) & "." & row.Cells("ADBJDT").Text.Substring(4, 2) & "." & row.Cells("ADBJDT").Text.Substring(0, 4)).Date.ToString("1yyMMdd") & _
                        '        "," & row.Cells("KMIK").Text.Replace(",", "") & "," & _
                        '        "'" & row.Cells("C6FNST").Text & "','" & row.Cells("ADIIST").Text & "'," & _
                        '        "'" & row.Cells("DEF1CD").Text & "','" & row.Cells("ADA3CD").Text & "'," & _
                        '        "'" & Now.Date.ToString("1yyMMdd") & "'," & row.Cells("ADFCNB").Text & ")"

                        'db.RunSql(sQuery, True)

                        'sQuery = " INSERT INTO TRCEKLIST" & _
                        '            " (CEKLIST, C6CANB, CUSNM, C6B9CD, C6F1CD, CJCBTX, " & _
                        '            " CDAFYV, ADCVNB, ADAITX, ITDSC, ITEM,REFADI, MOHTQ, " & _
                        '            " ADDZVA, ADAQQT, ADBJDT, KMIK, C6FNST, ADIIST, DEF1CD , ADA3CD ,CEKDATE,ADFCNB,HNDCODE,KANBANNO,AMBKOD ) " & _
                        '        " VALUES(" & iKdz93x & "," & row.Cells("C6CANB").Text & "," & _
                        '        "'" & row.Cells("CUSNM").Text & "','" & row.Cells("C6B9CD").Text & "'," & _
                        '        "'" & row.Cells("C6F1CD").Text & "','" & row.Cells("CJCBTX").Text & "'," & _
                        '        "'" & row.Cells("CDAFYV").Text & "','" & row.Cells("ADCVNB").Text & "'," & _
                        '        "'" & row.Cells("ADAITX").Text & "','" & row.Cells("ITDSC").Text & "'," & _
                        '        "'" & row.Cells("ITEM").Text & "','" & row.Cells("REFADI").Text & "'," & row.Cells("MOHTQ").Text.Replace(",", "") & "," & _
                        '        row.Cells("ADDZVA").Text.Replace(",", "") & "," & row.Cells("ADAQQT").Text.Replace(",", "") & "," & _
                        '        CDate(row.Cells("ADBJDT").Text.Substring(6, 2) & "." & row.Cells("ADBJDT").Text.Substring(4, 2) & "." & row.Cells("ADBJDT").Text.Substring(0, 4)).Date.ToString("1yyMMdd") & _
                        '        "," & row.Cells("KMIK").Text.Replace(",", "") & "," & _
                        '        "'" & row.Cells("C6FNST").Text & "','" & row.Cells("ADIIST").Text & "'," & _
                        '        "'" & row.Cells("DEF1CD").Text & "','" & row.Cells("ADA3CD").Text & "'," & _
                        '        "'" & Now.Date.ToString("1yyMMdd") & "'," & row.Cells("ADFCNB").Text & "," & _
                        '        "'" & row.Cells("HNDCODE").Text & "'," & _
                        '        "'" & row.Cells("KANBAN").Text & "','" & row.Cells("Ambkod").Text & "')"

                        'dbAccess.RunSql(sQuery, True)

                    Next

                    'objWriter.Close()

                    'If bCreate = False Then

                    '    sQuery = " Update Mbkdcpp" & _
                    '                " Set Kdz93x=" & iKdz93x & _
                    '                " Where Kdaenb='" & My.Settings.Sirket & "'"

                    '    db.RunSql(sQuery, True)

                    'End If

                    '
                    'rpt.SetLogonInfo(My.Settings.AccessConnection.Split(";")(0).Split("=")(1), "c:\EDI\Sevkiyat.mdb", "", "")
                    '

                    'frmRapor.sRapor = AppDomain.CurrentDomain.SetupInformation.ApplicationBase & "\rptCEKILISTESI.rpt"

                    ''
                    'frmRapor.ShowDialog()

                    ''


                    MessageBox.Show(iKdz93x & " nolu çekme listesi oluþturuldu...", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    sQuery = "UPDATE coitem SET Uf_Secim = 0 WHERE qty_packed = " & iKdz93x
                    db.RunSql(sQuery)

                    db.CommitTransaction()

                Catch ex As Exception

                    db.RollbackTransaction()

                    Throw ex

                End Try

                btnSorgula_Click(sender, e)

                System.Windows.Forms.Cursor.Current = Cursors.Default

            End If

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnSelectall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckedAll.Click
        GridEX1.CheckAllRecords()

        Try
            For Each row As Janus.Windows.GridEX.GridEXRow In GridEX1.GetCheckedRows
                sQuery = "UPDATE coitem SET Uf_Secim = 1 WHERE co_num = " & sTirnakEkle(row.Cells("ADCVNB").Text) & " AND co_line = " & row.Cells("ADFCNB").Text
                db.RunSql(sQuery)
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click
        Dim sQuery, sDate As String

        Dim ds As DataSet

        sDate = dtmTeslimTarihi.Value.ToString("1yyMMdd")

        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        If txtMusteri1.Text = "" Then

            MessageBox.Show("Lütfen Bir Müþteri Seçiniz!", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Exit Sub

        End If

        If txtMusteri2.Text <> "" AndAlso txtMusteri2.Text < txtMusteri1.Text Then

            MessageBox.Show("Lütfen Ýkinci Müþteri Numarasýný Ýlkinden Büyük Seçiniz!", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Exit Sub

        End If

        Try

            sQuery = "SELECT *" & _
                        " From CekmeListesiMst" & _
                        " Where ADBJDT <= " & sDate & _
                        IIf(rdbTumu.Checked, " And 1=1", " And packed=0")

            If txtMusteri1.Text <> "" Then

                If txtMusteri2.Text = "" Then

                    sQuery = sQuery & "    and c6canb='" & txtMusteri1.Text & "'"

                Else

                    sQuery = sQuery & "    and c6canb>='" & txtMusteri1.Text & "'" & _
                                        " and c6canb<='" & txtMusteri2.Text & "'"

                End If

            End If

            If txtAmbar1.Text <> "" Then

                sQuery = sQuery & "    and ada3cd='" & txtAmbar1.Text & "'"

            End If

            If txtTeslimAlan1.Text <> "" Then

                If txtTeslimAlan2.Text = "" Then

                    sQuery = sQuery & "    and C6B9CD='" & txtTeslimAlan1.Text & "'"

                Else

                    sQuery = sQuery & "    and C6B9CD>='" & txtTeslimAlan1.Text & "'" & _
                                        " and C6B9CD<='" & txtTeslimAlan2.Text & "'"
                End If

            End If

            If txtKapi1.Text <> "" Then

                If txtKapi2.Text = "" Then

                    sQuery = sQuery & "    and C6f1cd='" & txtKapi1.Text & "'"

                Else

                    sQuery = sQuery & "    and C6f1cd>='" & txtKapi1.Text & "'" & _
                                        "    and C6f1cd<='" & txtKapi2.Text & "'"

                End If

            End If

            If txtMalzeme1.Text <> "" Then

                If txtMalzeme2.Text = "" Then

                    sQuery = sQuery & "    and ADAITX='" & txtMalzeme1.Text & "'"

                Else

                    sQuery = sQuery & "    and ADAITX>='" & txtMalzeme1.Text & "'" & _
                                        "    and ADAITX<='" & txtMalzeme2.Text & "'"

                End If

            End If

            If txtPusno.Text <> "" Then

                sQuery = sQuery & "    and CJCBTX like '" & txtPusno.Text & "%'"

            End If

            If CheckBox1.Checked Then

                sQuery = sQuery & "    and ADBJDT<=" & sTarih(dtmTeslimTarihi.Value)

            End If

            sQuery = sQuery & " Order By c6canb, C6B9CD, C6f1cd, ADBJDT "

            ds = db.RunSql(sQuery, "Cekme")

            If Not (ds Is Nothing) AndAlso _
                            ds.Tables.Count > 0 AndAlso _
                            ds.Tables("Cekme").Rows.Count > 0 Then

                bSorgu = True

                GridEX1.DataSource = ds

                GridEX1.DataMember = "Cekme"

                bSorgu = False

                Dim row As Janus.Windows.GridEX.GridEXRow

                For i As Integer = 0 To GridEX1.RowCount - 1

                    row = GridEX1.GetRow(i)

                    If row.RowType <> RowType.GroupHeader Then

                        Dim isevkmik As Integer

                        If row.Cells("SevkMik").Text <> "" Then

                            isevkmik = row.Cells("SevkMik").Text

                        Else

                            isevkmik = 0

                        End If

                        If isevkmik <> 0 Then

                            If row.Cells("KMIK").Text <> "0" And row.Cells("KMIK").Text <> "" Then

                                row.Cells("KUTUADET").Text = CStr(CInt(row.Cells("SevkMik").Text) / CInt(row.Cells("KMIK").Text))

                                row.Cells("KUTUADET").Text = CInt(row.Cells("KUTUADET").Text)

                            End If

                        End If

                        If CDec(row.Cells("KUTUADET").Text) - CInt(row.Cells("KUTUADET").Text) <> 0 Then

                            row.Cells("DURUM").Text = "50"

                        Else

                            row.Cells("DURUM").Text = "00"

                        End If

                    End If

                Next i

                AddConditionalFormatting()

            Else

                'data yoktur

                'MessageBox.Show(" Kayýt Bulunamadý... ", " Ekip Mapics ", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, False)

                GridEX1.DataSource = Nothing

            End If

            Duzenle(GridEX1, False)

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        Windows.Forms.Cursor.Current = Cursors.Arrow
    End Sub

    Private Sub btnUnCheckedAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnCheckedAll.Click

        Try
            For Each row As Janus.Windows.GridEX.GridEXRow In GridEX1.GetCheckedRows
                sQuery = "UPDATE coitem SET Uf_Secim = 0 WHERE co_num = " & sTirnakEkle(row.Cells("ADCVNB").Text) & " AND co_line = " & row.Cells("ADFCNB").Text
                db.RunSql(sQuery)
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        GridEX1.UnCheckAllRecords()

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            dtmTeslimTarihi.Enabled = True
        Else
            dtmTeslimTarihi.Enabled = False
        End If
    End Sub

    Private Sub frmCekmeListesi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtAmbar1.Text = VarsayilanAmbar

        'sQuery = " Select Ambkod, Ambtan  " & _
        '        " From  ITMPACK "

        'dsAmbalaj = db.RunSql(sQuery, "Ambalaj")

        'If Not (dsAmbalaj Is Nothing) AndAlso _
        '                dsAmbalaj.Tables.Count > 0 AndAlso _
        '                dsAmbalaj.Tables("Ambalaj").Rows.Count > 0 Then

        '    GridEX1.DropDowns(0).SetDataBinding(dsAmbalaj, "Ambalaj")

        AddConditionalFormatting()

        'End If

        Dim fc As GridEXFormatCondition

        fc = New GridEXFormatCondition(GridEX1.RootTable.Columns("Ambkod"), ConditionOperator.Equal, True)

        'setting Format style properties to be applied to all the rows
        'that met this condition
        fc.FormatStyle.ForeColor = Color.Red
        GridEX1.RootTable.FormatConditions.Add(fc)
    End Sub

    Private Sub GridEX1_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.ColumnButtonClick
        Dim AmbKod, Malzeme As String

        Dim grid As Janus.Windows.GridEX.GridEX = CType(sender, Janus.Windows.GridEX.GridEX)
        Dim index As Integer

        index = grid.SelectedItems.Item(0).GetRow.RowIndex

        If e.Column.ButtonText = "AmbDegistir" Then

            AmbKod = grid.SelectedItems.Item(0).GetRow.Cells("AmbKod").Text

            Malzeme = grid.SelectedItems.Item(0).GetRow.Cells("ADAITX").Text

            sQuery = " Select KMIK, PKSIR, PKMIK" & _
                        " From Itmpack" & _
                        " Where AMBKOD = " & sTirnakEkle(AmbKod.Trim) & _
                        " AND ITNBR = " & sTirnakEkle(Malzeme)

            dt = db.RunSql(sQuery)

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                GridEX1.Update()
                grid.SelectedItems.Item(0).GetRow.BeginEdit()

                grid.SelectedItems.Item(0).GetRow.Cells("KMIK").Text = dt.Rows(0)("KMIK").ToString
                grid.SelectedItems.Item(0).GetRow.Cells("KMIK").Value = dt.Rows(0)("KMIK").ToString

                grid.SelectedItems.Item(0).GetRow.Cells("PKSIR").Text = dt.Rows(0)("PKSIR").ToString
                grid.SelectedItems.Item(0).GetRow.Cells("PKSIR").Value = dt.Rows(0)("PKSIR").ToString

                grid.SelectedItems.Item(0).GetRow.Cells("PKMIK").Text = dt.Rows(0)("PKMIK").ToString
                grid.SelectedItems.Item(0).GetRow.Cells("PKMIK").Value = dt.Rows(0)("PKMIK").ToString

                grid.SelectedItems.Item(0).GetRow.Cells("AMBKOD").Text = AmbKod
                grid.SelectedItems.Item(0).GetRow.Cells("AMBKOD").Value = AmbKod

                grid.SelectedItems.Item(0).GetRow.EndEdit()

                GridEX1.Update()

                Hesapla()

                GridEX1.Update()

            End If

        End If
    End Sub

    Private Sub GridEX1_CurrentCellChanging(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.CurrentCellChangingEventArgs) Handles GridEX1.CurrentCellChanging
        Dim sQuery As String

        Dim dsAmbar As DataSet

        If bSorgu = False Then

            If e.Row.RowType <> RowType.GroupHeader Then

                e.Row.BeginEdit()

                If e.Row.Cells("SevkMik").Text <> "" Then

                    If e.Row.Cells("SevkMik").Text <> 0 Then

                        If e.Row.Cells("KMIK").Text <> "0" And e.Row.Cells("KMIK").Text <> "" Then

                            e.Row.Cells("KUTUADET").Text = CStr(CInt(e.Row.Cells("SevkMik").Text) / CInt(e.Row.Cells("KMIK").Text))

                            e.Row.Cells("KUTUADET").Text = CInt(e.Row.Cells("KUTUADET").Text)

                            e.Row.Cells("KUTUADET").Value = CInt(e.Row.Cells("KUTUADET").Text)

                        Else

                            e.Row.Cells("KUTUADET").Text = 0

                            e.Row.Cells("KUTUADET").Value = 0

                        End If

                    End If

                End If

                If e.Row.Cells("SevkMik").Value <> 0 Then

                    If e.Row.Cells("KMIK").Text <> "0" And e.Row.Cells("KMIK").Text <> "" Then

                        e.Row.Cells("KUTUADET").Text = CStr(CInt(e.Row.Cells("SevkMik").Text) / CInt(e.Row.Cells("KMIK").Text))

                        e.Row.Cells("KUTUADET").Text = CInt(e.Row.Cells("KUTUADET").Text)

                        e.Row.Cells("KUTUADET").Value = CInt(e.Row.Cells("KUTUADET").Text)

                    Else

                        e.Row.Cells("KMIK").Text = "0"

                        e.Row.Cells("KMIK").Value = "0"

                    End If

                End If

                'If CDec(e.Row.Cells("KUTUADET").Text) - CInt(e.Row.Cells("KUTUADET").Text) <> 0 Then

                '    MsgBox(" Bu Sipariþ Seçilemez. Bakiye, Kutu Ýçi Miktarla Uyuþmuyor. Bakiye Düzenlemesi Yapýn !", MsgBoxStyle.OkOnly, "Ekip Mapics")

                '    e.Row.Cells("KUTUADET").Text = "10"

                '    e.Row.Cells("KUTUADET").Value = "10"

                '    e.Cancel = True

                'End If

                If e.Row.Cells("SevkMik").Text > e.Row.Cells("ADAQQT").Value Then

                    MsgBox(" Sevk Miktarý Bakiye den büyük olamaz !", MsgBoxStyle.OkOnly, "Ekip Mapics")

                    e.Row.Cells("SevkMik").Text = CInt(e.Row.Cells("ADAQQT").Value)

                    e.Row.Cells("SevkMik").Value = CInt(e.Row.Cells("ADAQQT").Value)

                    e.Cancel = True

                End If

                'If Math.Truncate(e.Row.Cells("SevkMik").Text / e.Row.Cells("KMIK").Value) * e.Row.Cells("KMIK").Value <> e.Row.Cells("SevkMik").Value Then

                '    MsgBox(" Sevk Miktarý Kutu Ýçi Miktarýn Katý olmak zorunda!", MsgBoxStyle.OkOnly, "Ekip Mapics")

                '    e.Row.Cells("SevkMik").Text = CInt(e.Row.Cells("ADAQQT").Value)

                '    e.Row.Cells("SevkMik").Value = CInt(e.Row.Cells("ADAQQT").Value)

                '    'e.Cancel = True

                'End If

                e.Row.EndEdit()

                If e.Row.Cells("packed").Text = "1" Then

                    MsgBox(" Bu Sipariþ Seçilemez. Açýk Çekme Listesi Mevcut!", MsgBoxStyle.OkOnly, "Ekip Mapics")

                    e.Cancel = True

                End If

                If e.Row.Cells("KMIK").Text = "0" Then

                    MsgBox(" Bu Sipariþ Seçilemez. Ambalaj bilgilerinde sorun var!", MsgBoxStyle.OkOnly, "Ekip Mapics")

                    e.Cancel = True

                End If

                sQuery = " Select Ambkod, Ambtan  " & _
                                " From ITMPACK " & _
                                " Where Itnbr ='" & e.Row.Cells("ADAITX").Text & "'"

                dsAmbar = db.RunSql(sQuery, "Ambar")

                If Not (dsAmbar Is Nothing) AndAlso _
                                dsAmbar.Tables.Count > 0 AndAlso _
                                dsAmbar.Tables("Ambar").Rows.Count > 0 Then

                    GridEX1.DropDowns(0).SetDataBinding(dsAmbar, "Ambar")

                    AddConditionalFormatting()

                End If

            End If

        End If
    End Sub

    Private Sub MalzemeSec(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMalzeme1.ButtonClick, txtMalzeme2.ButtonClick
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            Dim txt As Janus.Windows.GridEX.EditControls.EditBox = CType(sender, Janus.Windows.GridEX.EditControls.EditBox)

            ssorgu = "SELECT Distinct Item as Malzeme,Description as Tanim " & _
                            " From Item" & _
                            " Order By 1"

            FindFormCagir(ssorgu, "Malzeme", "Tanim", txt.Text, "")

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
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

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    Private Sub txtMusteri1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMusteri1.ButtonClick
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct Cust_Num as Müþteri,Name as Tanim " & _
                            " From CustAddr" & _
                            " Where cust_seq=0"

            FindFormCagir(ssorgu, "Müþteri", "Tanim", txtMusteri1.Text, txtMusteriAdi1.Text)

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

            ssorgu = "SELECT Distinct Cust_Num as Müþteri,Name as Tanim " & _
                            " From CustAddr" & _
                            " Where cust_seq=0"

            FindFormCagir(ssorgu, "Müþteri", "Tanim", txtMusteri2.Text, txtMusteriAdi2.Text)

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    #End Region 'Methods

    Private Sub GridEX1_CellValueChanged(sender As System.Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellValueChanged

        Dim rowGrd As Janus.Windows.GridEX.GridEXRow

        rowGrd = GridEX1.SelectedItems(0).GetRow

        Try
            If rowGrd.Cells("C6CANB").Text = "M000165" Then
                Exit Sub
            End If

            If rowGrd.IsChecked = True Then

                sQuery = "exec Tr_Coitem_Fifo " & sTirnakEkle(rowGrd.Cells("ADCVNB").Text) & "," & rowGrd.Cells("ADFCNB").Text
                dt = db.RunSql(sQuery)

                If dt.Rows.Count > 0 Then

                    If (dt.Rows(0)(0) <> rowGrd.Cells("ADCVNB").Text OrElse dt.Rows(0)(1) <> rowGrd.Cells("ADFCNB").Text) AndAlso dt.Rows(0)(2) <> rowGrd.Cells("ADBJDT").Text Then

                        sQuery = "SELECT ISNULL(Uf_Secim,0) FROM coitem WHERE co_num = " & sTirnakEkle(rowGrd.Cells("ADCVNB").Text) & " AND co_line = " & rowGrd.Cells("ADFCNB").Text
                        Dim dtTemp As DataTable
                        dtTemp = db.RunSql(sQuery)

                        If Not dtTemp.Rows(0)(0) = True Then

                            MessageBox.Show("Daha eski tarihli sipariþ mevcut." & vbNewLine & _
                               "Sipariþ Numarasý : " & dt.Rows(0)(0) & " ve Sipariþ Kalemi : " & dt.Rows(0)(1) & " olan kayýt seçilmeli.")

                            sQuery = "UPDATE coitem SET Uf_Secim = 0 WHERE co_num = " & sTirnakEkle(rowGrd.Cells("ADCVNB").Text) & " AND co_line = " & rowGrd.Cells("ADFCNB").Text
                            db.RunSql(sQuery)

                            rowGrd.IsChecked = False

                        End If


                        Exit Sub

                    End If
                End If

                sQuery = "UPDATE coitem SET Uf_Secim = 1 WHERE co_num = " & sTirnakEkle(rowGrd.Cells("ADCVNB").Text) & " AND co_line = " & rowGrd.Cells("ADFCNB").Text
                db.RunSql(sQuery)

            Else

                sQuery = "SELECT * FROM coitem WHERE co_num = " & sTirnakEkle(rowGrd.Cells("ADCVNB").Text) & " AND co_line = " & rowGrd.Cells("ADFCNB").Text & " AND Uf_Secim = 1"
                dt = db.RunSql(sQuery)

                If dt.Rows.Count > 0 Then
                    sQuery = "UPDATE coitem SET Uf_Secim = 0 WHERE co_num = " & sTirnakEkle(rowGrd.Cells("ADCVNB").Text) & " AND co_line = " & rowGrd.Cells("ADFCNB").Text
                    db.RunSql(sQuery)
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmCekmeListesi_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        If GridEX1.GetCheckedRows.Length > 0 Then
            Try
                For Each row As Janus.Windows.GridEX.GridEXRow In GridEX1.GetCheckedRows
                    sQuery = "UPDATE coitem SET Uf_Secim = 0 WHERE co_num = " & sTirnakEkle(row.Cells("ADCVNB").Text) & " AND co_line = " & row.Cells("ADFCNB").Text
                    db.RunSql(sQuery)
                Next

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

    End Sub
End Class