Imports System
Imports System.IO
Imports System.Collections
Imports Microsoft.Office.Interop
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Janus.Windows.GridEX

Public Class frmKonsinye

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim ds As DataSet
    Dim dt As New DataTable
    Dim sQuery As String = ""
    Dim HataVar As Boolean = False

    Dim mainFolder As String = ""
    Dim filepath As String = ""


    Private Sub btnDizin_Click(sender As System.Object, e As System.EventArgs) Handles btnDizin.Click

        'Dim openDir As FolderBrowserDialog = New FolderBrowserDialog
        '
        OpenDir.Description = " Görüntülemek İstediğiniz Dizini Seçiniz! "
        '
        'DialogResult = openDir.ShowDialog(Me) 

        sQuery = " Select EDIDIR From EdiPrm"

        ds = db.RunSql(sQuery, "EdiPrm")

        If Not ds Is Nothing Then

            If ds.Tables("EdiPrm").Rows.Count > 0 Then

                txtPath.Text = Trim(ds.Tables("EdiPrm").Rows(0).Item("EDIDIR").ToString)

                If Not Directory.Exists(txtPath.Text) Then

                    txtPath.Text = "\\10.0.100.97\Ekip Mapics\GELEN_EDI"

                End If
            Else
                txtPath.Text = "\\10.0.100.97\Ekip Mapics\GELEN_EDI"

            End If

        Else

            txtPath.Text = "\\10.0.100.97\Ekip Mapics\GELEN_EDI"
        End If

        'txtPath.Text = "C:\Edi\GELENEDI"

        'OpenFileDialog1.InitialDirectory = "C:\EDI\GELENEDI"
        mainFolder = txtPath.Text
        OpenFileDialog1.InitialDirectory = txtPath.Text
        OpenFileDialog1.Filter = "DESADV (*DESADV*.*)|*DESADV*.*"


        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then

            'filepath = OpenFileDialog1.FileName
            filepath = System.IO.Path.GetFileName(OpenFileDialog1.FileName)

            txtPath.Text = mainFolder & "\" & filepath

            DosyaListele(txtPath.Text)

        End If
        '
        'OpenDir.SelectedPath = txtPath.Text
        '
        'If OpenDir.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
        '    '
        '    If IO.Directory.Exists(OpenDir.SelectedPath) = True Then
        '        '
        '        txtPath.Text = OpenDir.SelectedPath.ToString
        '        '
        '        DosyaListele(OpenDir.SelectedPath.ToString)
        '        '
        '    Else
        '        '
        '        MessageBox.Show("Böyle Bir Dizin Bulunamadı!", " Error", _
        '        MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        '
        '    End If
        '    '
        'End If
    End Sub

    Private Sub AddConditionalFormatting()
        'Imports Janus.Windows.GridEX is used in this module

        'Adding a condition using Discontinued field

        Dim fc As GridEXFormatCondition

        fc = New GridEXFormatCondition(GridEX1.RootTable.Columns("stat"), ConditionOperator.Equal, "0")

        'setting Format style properties to be applied to all the rows
        'that met this condition
        'fc.FormatStyle.FontStrikeout = TriState.True
        fc.FormatStyle.ForeColor = Color.Red

        GridEX1.RootTable.FormatConditions.Add(fc)


    End Sub

    Private Sub DosyaListele(ByVal sDizin As String)

        Dim file As String
        Dim sODID As String
        Dim sMTyp As String
        Dim sVer As String
        Dim sRel As String
        Dim sMCRef As String
        Dim nLine As Integer
        Dim sDte As String = ""
        Dim sFrmt As String = ""
        Dim nCount As Integer
        Dim strLine1 As String
        Dim sPlnt As String
        Dim sNADATyp As String
        Dim sNADCN As String
        Dim sMRef As String

        file = sDizin

        'sDizin = "C:\Edi"
        'If Not Directory.Exists(sDizin) Then
        '    MessageBox.Show(sDizin & " klasörü bulunmamaktadır.")
        '    Exit Sub
        'Else
        '    txtPath.Text = sDizin
        'End If


        'Dim files() As String = Directory.GetFiles(sDizin)

        Dim ds As New DataSet()

        Dim table As New DataTable("Dosya")

        Dim objStreamReader As StreamReader

        Dim strLine, sSaticiNo, sTarih, sTip, sMesajNo As String

        Dim sMesajTipi, sDosyaTarih, sDosyaSaat As String

        Dim i As Integer

        sMesajNo = ""

        sSaticiNo = ""

        sTarih = ""

        sTip = ""

        table.Columns.Add(New DataColumn("MsgNo", Type.GetType("System.String")))

        table.Columns.Add(New DataColumn("DocNo", Type.GetType("System.String")))

        table.Columns.Add(New DataColumn("DocDate", Type.GetType("System.String")))

        table.Columns.Add(New DataColumn("plant", Type.GetType("System.String")))

        table.Columns.Add(New DataColumn("gate", Type.GetType("System.String")))

        table.Columns.Add(New DataColumn("customer", Type.GetType("System.String")))

        table.Columns.Add(New DataColumn("cust_seq", Type.GetType("System.Int32")))

        table.Columns.Add(New DataColumn("CustItem", Type.GetType("System.String")))

        table.Columns.Add(New DataColumn("Qty", Type.GetType("System.Decimal")))

        table.Columns.Add(New DataColumn("Item", Type.GetType("System.String")))

        table.Columns.Add(New DataColumn("qty_on_hand", Type.GetType("System.Decimal")))

        table.Columns.Add(New DataColumn("stat", Type.GetType("System.String")))

        table.Columns.Add(New DataColumn("result", Type.GetType("System.String")))


        ds.Tables.Add(table)

        'For Each file In files

        Try

            Dim s As New IO.FileInfo(file)

            'sDosyaTarih = s.CreationTime.ToString("dd/MM/yyyy")

            'sDosyaSaat = s.CreationTime.ToString("HH:mm")

            'sODID = ""
            'sPlnt = ""
            'sMTyp = ""
            Dim msgNo As String = ""
            Dim docNo As String = ""
            Dim docDate As String = ""
            Dim custItem As String = ""
            Dim qty As Decimal = 0
            Dim qty_on_hand As Decimal = 0
            Dim item As String = ""
            Dim stat As String = ""
            Dim customer As String = ""
            Dim gate As String = ""
            Dim plant As String = ""
            Dim cust_seq As Integer
            'sVer = ""
            'sRel = ""
            'sMRef = ""
            'sMCRef = ""
            nLine = 1

            objStreamReader = New StreamReader(file)

            strLine = objStreamReader.ReadLine

            Do While Not strLine Is Nothing

                If strLine = "" Then Exit Do

                sTip = Copy(strLine, 1 - 1, 4).TrimEnd()


                If sTip = "UNH" Then

                    msgNo = Copy(strLine, 5 - 1, 10).TrimEnd()


                    'For Each row As Janus.Windows.GridEX.GridEXRow In GridEX1.GetRows
                    sQuery = "select * from KONSWRK WHERE MsgNo = '" & msgNo & "'"
                    dt = db.RunSql(sQuery)

                    If dt.Rows.Count > 0 Then
                        MessageBox.Show("Bu mesaj daha önceden işlenmiştir.")
                        objStreamReader.Close()
                        Exit Sub
                    End If

                    'Next

                    'sVer = Copy(strLine, 27 - 1, 3).TrimEnd()

                    'sRel = Copy(strLine, 31 - 1, 3).TrimEnd()

                    'If strLine.Length > 44 Then

                    '    sMCRef = Copy(strLine, 45 - 1, 35).TrimEnd()

                    'End If

                ElseIf sTip = "BGM" Then

                    docNo = LTrim(Copy(strLine, 21, 40)).TrimEnd()

                ElseIf sTip = "DTM" Then

                    If Copy(strLine, 5 - 1, 3).TrimEnd() = "137" Then

                        docDate = Copy(strLine, 9 - 1, 12).TrimEnd()

                    End If

                ElseIf sTip = "NAD" Then

                    If Copy(strLine, 5 - 1, 2).TrimEnd() = "CN" Then

                        plant = Copy(strLine, 8 - 1, 35).TrimEnd().ToString.Split(":")(0)

                        If plant <> "" Then
                            sQuery = "select TOP 1 CANB,cust_seq FROM PLANTPRM_konsinye WHERE B9CD = " & sTirnakEkle(plant)
                            dt = db.RunSql(sQuery)

                            If dt.Rows.Count > 0 Then
                                customer = dt.Rows(0)("CANB")
                                cust_seq = dt.Rows(0)("cust_seq")
                            End If
                        End If

                    End If


                ElseIf sTip = "LOC" Then

                    If Copy(strLine, 5 - 1, 2).TrimEnd() = "11" Then
                        gate = Copy(strLine, 8 - 1, 25).TrimEnd()
                    End If


                ElseIf sTip = "LIN" Or sTip = "LIN0" Then

                    custItem = Copy(strLine, 16 - 1, 30).TrimEnd()


                    sQuery = "select top 1 item  from itemcust where cust_item= '" & custItem & "'"
                    dt = db.RunSql(sQuery)

                    If dt.Rows.Count > 0 Then
                        item = dt.Rows(0)(0)
                    Else
                        item = ""
                    End If

                    sQuery = "select top 1 qty_on_hand  from itemloc where whse = 'GEFC' and loc='GEFCO' and item= '" & item & "'"
                    dt = db.RunSql(sQuery)

                    If dt.Rows.Count > 0 Then
                        qty_on_hand = dt.Rows(0)(0)
                    Else
                        qty_on_hand = 0
                    End If



                ElseIf sTip = "QTY" Or sTip = "QTY0" Then

                    If Copy(strLine, 5 - 1, 3).TrimEnd() = "12" Or Copy(strLine, 7 - 1, 3).TrimEnd() = "12" Then

                        qty = CDec(Copy(strLine, 9 - 1, 15).TrimEnd())

                        If qty_on_hand < qty Then
                            stat = "0"
                        Else
                            stat = "1"
                        End If


                        table.Rows.Add(New Object() {msgNo, docNo, docDate, plant, gate, customer, cust_seq, custItem, qty, item, qty_on_hand, stat, ""})

                        AddConditionalFormatting()

                    End If

                End If


                nLine = nLine + 1

                strLine = objStreamReader.ReadLine

            Loop


            objStreamReader.Close()



            'file.Split("\")(4).ToString
            'If txtPlant.Text.Trim = "" Then
            '    table.Rows.Add(New Object() {Path.GetFileName(file) _
            ', sDosyaTarih, sDosyaSaat, ConvertEDIDate(sFrmt, sDte, "BA"), sMesajTipi, sODID, sPlnt})
            'ElseIf txtPlant.Text.Trim = sPlnt.Trim Then


            'End If


            'table.Rows.Add(New Object() {file.Split(".")(0).Split("\")(file.Split(".")(0).Split("\").Length - 1) & "." & file.Split(".")(1) _
            '                            , sDosyaTarih, sDosyaSaat, ConvertEDIDate(sFrmt, sDte, "BA"), sMesajTipi, sODID, sPlnt})

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        'Next

        Me.GridEX1.DataSource = ds

        Me.GridEX1.DataMember = "Dosya"

        Me.GridEX1.AutoSizeColumns()

    End Sub

    Private Sub btnMesajAl_Click(sender As System.Object, e As System.EventArgs) Handles btnMesajAl.Click
        Try
            Dim param As New ArrayList
            Dim paramout As New ArrayList
            Dim hatali As Boolean = False

            For Each row As Janus.Windows.GridEX.GridEXRow In GridEX1.GetRows
                sQuery = "delete from KONSWRK where MsgNo ='" & row.Cells("MsgNo").Text & "' and stat <> '2'"
                db.RunSql(sQuery)
                Exit For
            Next

            Dim Gun As String = Date.Now.ToString("yyyy-MM-dd HH:mm:ss.fff").ToString

            For Each row As Janus.Windows.GridEX.GridEXRow In GridEX1.GetRows
                If row.Cells("stat").Text = "0" Then

                    sQuery = "INSERT INTO KONSWRK(UserId,MsgNo,DocNo,DocDate,CustItem,Qty,Item,Stat,Result,CrdDate,Plant,Gate,Customer,Cust_Seq) " &
                       " VALUES('" & KullaniciAdi & "','" & row.Cells("MsgNo").Text & "','" & row.Cells("DocNo").Text & "','" & row.Cells("DocDate").Text &
                       "','" & row.Cells("CustItem").Text & "'," & row.Cells("Qty").Text & ",'" & row.Cells("Item").Text & "',0,'Stok Yetersiz.','" & Gun &
                       "','" & row.Cells("Plant").Text & "','" & row.Cells("Gate").Text & "','" & row.Cells("Customer").Text & "'," & row.Cells("cust_seq").Text & ")"
                    db.RunSql(sQuery)

                End If
            Next

            For Each row As Janus.Windows.GridEX.GridEXRow In GridEX1.GetRows
                'Try
                If row.Cells("stat").Text = "1" Then

                    Dim trans_type As String = 2


                    Dim MaxTransNum As Integer
                    Dim dtTmp As New DataTable

                    sQuery = " Select isnull(max(trans_num),0)+1 from dcitem "
                    dtTmp = db.RunSql(sQuery)
                    If Not dtTmp Is Nothing AndAlso dtTmp.Rows.Count > 0 Then
                        MaxTransNum = CInt(dtTmp.Rows(0)(0).ToString)
                    End If

                    param.Clear()
                    param.Add(New SqlClient.SqlParameter("@trans_num", MaxTransNum))
                    param.Add(New SqlClient.SqlParameter("@trans_date", Gun))
                    param.Add(New SqlClient.SqlParameter("@trans_type", trans_type))
                    param.Add(New SqlClient.SqlParameter("@item", row.Cells("Item").Text))
                    param.Add(New SqlClient.SqlParameter("@loc", "GEFCO"))
                    param.Add(New SqlClient.SqlParameter("@lot", "1"))
                    param.Add(New SqlClient.SqlParameter("@count_qty ", row.Cells("Qty").Text))
                    param.Add(New SqlClient.SqlParameter("@whse ", "GEFC"))
                    param.Add(New SqlClient.SqlParameter("@siteref ", "Default"))
                    param.Add(New SqlClient.SqlParameter("@emp_num ", "EDI"))
                    param.Add(New SqlClient.SqlParameter("@document_num ", row.Cells("MsgNo").Text))

                    'Dim p10 As New SqlClient.SqlParameter("@rowsAffected", SqlDbType.Int)
                    'p10.Direction = ParameterDirection.Output
                    'param.Add(p10)

                    Dim p5 As New SqlClient.SqlParameter("@pResult", SqlDbType.NVarChar, 500)
                    p5.Direction = ParameterDirection.Output
                    param.Add(p5)

                    row.BeginEdit()

                    'db.RunSp("[TRM_Dcitem_Insert_Post_DocNum_SL9]", param, 1, False)
                    If db.RunSp("[TRM_Dcitem_Insert_Post_DocNum]", param, 1, False) = True Then

                        HataVar = True
                        hatali = True
                    Else

                        If Not IsDBNull(p5.Value) Then

                            HataVar = True

                            row.Cells("stat").Value = "1"
                            row.Cells("stat").Text = "1"

                            row.Cells("result").Value = p5.Value
                            row.Cells("result").Text = p5.Value

                            row.EndEdit()

                            'MessageBox.Show("İşlem başarısız.")

                            sQuery = "INSERT INTO KONSWRK(UserId,MsgNo,DocNo,DocDate,CustItem,Qty,Item,Stat,Result,CrdDate,Plant,Gate,Customer,Cust_Seq) " &
                                " VALUES('" & KullaniciAdi & "','" & row.Cells("MsgNo").Text & "','" & row.Cells("DocNo").Text & "','" & row.Cells("DocDate").Text &
                                "','" & row.Cells("CustItem").Text & "'," & row.Cells("Qty").Text & ",'" & row.Cells("Item").Text & "',1,'" & p5.Value & " ','" & Gun &
                                "','" & row.Cells("Plant").Text & "','" & row.Cells("Gate").Text & "','" & row.Cells("Customer").Text & "'," & row.Cells("cust_seq").Text & ")"
                            db.RunSql(sQuery)


                        Else
                            HataVar = False

                            row.Cells("stat").Value = "2"
                            row.Cells("stat").Text = "2"

                            row.Cells("result").Value = "İşlem Tamamlandı."
                            row.Cells("result").Text = "İşlem Tamamlandı."

                            row.EndEdit()
                            'MessageBox.Show("İşlem Tamamlandı.")



                            Dim plant As String = ""
                            plant = row.Cells("Plant").Text

                            If row.Cells("Customer").Text = "  12115" Then
                                plant = ""
                            Else

                            End If

                            sQuery = "INSERT INTO KONSWRK(UserId,MsgNo,DocNo,DocDate,CustItem,Qty,Item,Stat,Result,CrdDate,Plant,Gate,Customer,Cust_Seq) " &
                                " VALUES('" & KullaniciAdi & "','" & row.Cells("MsgNo").Text & "','" & row.Cells("DocNo").Text & "','" & row.Cells("DocDate").Text &
                                "','" & row.Cells("CustItem").Text & "'," & row.Cells("Qty").Text & ",'" & row.Cells("Item").Text & "',2,'İşlem Tamamlandı','" & Gun &
                                "','" & row.Cells("Plant").Text & "','" & row.Cells("Gate").Text & "','" & row.Cells("Customer").Text & "'," & row.Cells("cust_seq").Text & ")"
                            db.RunSql(sQuery)


                            If sLookup("1", "OFFITEMBL", "CANBK=" & sTirnakEkle(row.Cells("Customer").Text.TrimEnd) &
                                   " And B9CDK=" & sTirnakEkle(plant) &
                                    " And GATE=" & sTirnakEkle(row.Cells("Gate").Text.TrimEnd) &
                                   " And AITXK=" & sTirnakEkle(row.Cells("Item").Text.TrimEnd)) = "1" Then

                                sQuery = " Update OFFITEMBL " &
                                            " Set UF_OFF_KUMREC = UF_OFF_KUMREC + " & row.Cells("Qty").Text &
                                            " Where CANBK=" & sTirnakEkle(row.Cells("Customer").Text) &
                                            " And B9CDK=" & sTirnakEkle(plant) &
                                            " And GATE=" & sTirnakEkle(row.Cells("Gate").Text) &
                                            " And AITXK=" & sTirnakEkle(row.Cells("Item").Text)

                                db.RunSql(sQuery)

                            Else
                                '" AND AENBK=" & sTirnakEkle(sCustSeq) & _



                                sQuery = " insert into OFFITEMBL(CANBK, B9CDK, GATE, AITXK, DDARQK, ASNKUM, [USER], TAR, UTIME,AENBK,UF_OFF_KUMREC)" &
                              " Values (" &
                              sTirnakEkle(row.Cells("Customer").Text) & "," &
                              sTirnakEkle(plant) & "," &
                              sTirnakEkle(row.Cells("Gate").Text) & "," &
                              sTirnakEkle(row.Cells("Item").Text) & "," &
                              "0" & "," &
                              "0" & "," &
                              sTirnakEkle(KullaniciAdi) & "," &
                              sTirnakEkle(Now.ToString("yyyy-MM-dd")) & "," &
                              Now.ToString("HHmmss") & "," &
                              row.Cells("cust_seq").Text & "," &
                              row.Cells("Qty").Text & ")"

                                db.RunSql(sQuery)

                            End If


                        End If

                        sQuery = "DELETE FROM dcitem where trans_num = " & MaxTransNum
                        db.RunSql(sQuery)


                    End If

                End If

            Next


            If Not hatali And txtPath.Text <> "" Then

                Dim sBckdir As String

                sBckdir = sLookup("EDIBCKDIR", "EDIPRM", " SIRKET='Default'")

                'sBckdir = "C:\Edi\BACKUP"

                If sBckdir <> "" Then

                    If Not System.IO.Directory.Exists(sBckdir) Then

                        System.IO.Directory.CreateDirectory(sBckdir)

                    End If

                    If System.IO.File.Exists(sBckdir & "\" & filepath) Then

                        System.IO.File.Delete(sBckdir & "\" & filepath)

                    End If

                    System.IO.File.Move(mainFolder & "\" & filepath, sBckdir & "\" & filepath)
                    txtPath.Text = ""
                End If



            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnListele_Click(sender As System.Object, e As System.EventArgs) Handles btnListele.Click
        Try
            sQuery = "SELECT MsgNo,DocNo,DocDate,CustItem,Qty,Item,(select qty_on_hand from itemwhse  where whse = 'GEFC' and itemwhse.item= KONSWRK.item) as qty_on_hand," &
                " case when (select qty_on_hand from itemwhse  where whse = 'GEFC' and itemwhse.item= KONSWRK.item) >= Qty then '1' else '0' end as stat," &
                "  Result FROM KONSWRK with (nolock) WHERE stat in ('0','1') AND UserId = '" & KullaniciAdi & "'"
            dt = db.RunSql(sQuery)
            GridEX1.DataSource = Nothing
            GridEX1.DataSource = dt

            AddConditionalFormatting()
            GridEX1.AutoSizeColumns()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmKonsinye_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


    End Sub

    Private Sub btnMesajSil_Click(sender As System.Object, e As System.EventArgs) Handles btnMesajSil.Click

        If MessageBox.Show("Hatalı kayıtları silmek istediğinizden emin misiniz?", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Try
                Dim mesajNo As String = ""

                For Each row As Janus.Windows.GridEX.GridEXRow In GridEX1.GetRows

                    mesajNo = row.Cells("MsgNo").Text

                    sQuery = "DELETE FROM KONSWRK WHERE MsgNo = '" & mesajNo & "' AND stat <> '2' "
                    db.RunSql(sQuery)

                Next

                btnListele_Click(sender, e)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If


    End Sub

    Private Sub btnKapat_Click(sender As System.Object, e As System.EventArgs) Handles btnKapat.Click
        Me.Close()
    End Sub
End Class