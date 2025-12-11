Public Class frmDeliveryNoteBasimi

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Dim dt As New DataTable
    Dim dtTemp As New DataTable
    Dim dtTemp1 As New DataTable
    Dim dtTemp2 As New DataTable
    Dim sSQL As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click
        sSQL = "SELECT DISTINCT cust, name, shpno, irsno, shipto, kapi ,  pickno, s.cust_seq, s.itnbr " & _
                " FROM   shppack s " & _
                " Left Join custaddr c " & _
                    " On c.cust_num=s.cust And c.cust_seq=s.cust_seq" & _
                " WHERE  1=1 and pickno<>0"

        If (txtIrsaliyeNo1.Text <> "") And (txtIrsaliyeNo2.Text <> "") Then
            sSQL = sSQL & " AND rtrim(ltrim(s.shpno))<=" & sTirnakEkle(txtIrsaliyeNo2.Text) & _
                            " AND rtrim(ltrim(s.shpno))>=" & sTirnakEkle(txtIrsaliyeNo1.Text)
        ElseIf (txtIrsaliyeNo1.Text <> "") Then
            sSQL = sSQL & " AND rtrim(ltrim(s.shpno)) =" & sTirnakEkle(txtIrsaliyeNo1.Text)
        End If

        If (txtMusteriNo.Text <> "") Then
            sSQL = sSQL & " AND cust_num=" & sTirnakEkle(txtMusteriNo.Text)
        End If

        'If Not cbYenidenBasim.Checked Then
        '    sSQL = sSQL & " AND etkdrm <> 'E'"
        'Else
        '    sSQL = sSQL & " AND etkdrm = 'E'"
        'End If

        dt = db.RunSql(sSQL)

        GridEX1.DataSource = dt

        GridEX1.ColumnAutoResize = True

        For Each col As Janus.Windows.GridEX.GridEXColumn In GridEX1.RootTable.Columns

            col.AutoSize()

        Next
    End Sub

    Private Sub btnYazdir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYazdir.Click
        Dim sSQL As String
        Dim sPath As String

        Dim sFormul As String

        Dim k As Integer
        ' /  nFltInd,nFltCount

        Dim sDHCDTX As String
        Dim sCrdTim As String

        Dim nKapAdedi As Double

        Dim nBrtAgr As Double

        Dim sMItmKod As String = ""
        Dim sMItmDesc As String = ""
        Dim sKontrat As String = ""
        Dim sKanban As String = ""
        Dim sMAMBKOD As String = ""
        Dim sMKKOD As String = ""
        Dim sMPKOD As String = ""
        Dim sMSPKOD As String = ""
        Dim sMKPKOD As String = ""
        Dim sAdres1 As String = ""
        Dim sAdres2 As String = ""
        Dim sAdres3 As String = ""
        Dim sSehir As String = ""
        Dim sUlke As String = ""
        Dim sPlaka As String = ""
        Dim sTeslimTar As String = ""
        Dim sCarrDesc As String = ""
        Dim sCarrAdres1 As String = ""
        Dim sCarrAdres2 As String = ""
        Dim sCarrAdres3 As String = ""
        Dim sCarrCity As String = ""

        Dim nIrsaliyeNo As Integer
        Dim sRanNo As String
        Dim sPusNo As String

        Try

            sSQL = " DELETE FROM ANSDELIVERY"

            dbAccess.RunSql(sSQL)

            Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

            Dim row As Janus.Windows.GridEX.GridEXRow

            checkedRows = Me.GridEX1.GetCheckedRows()

            If checkedRows.Length = 0 Then

                MessageBox.Show("Lütfen  Seçim Yapınız!")

                Exit Sub

            Else

                For Each row In checkedRows

                    nIrsaliyeNo = row.Cells("Shpno").Text

                    Exit For

                Next

            End If

            sSQL = "SELECT DISTINCT s.shpno, s.pickno, s.shipto, s.cust, " & _
                        " s.itnbr, s.kapi, p.ttime, s.sevktar,sum(s.shipmik) As shipmik ," & _
                        " s.stkob, " & " sum(s.ksay) As ksay, s.kmik, sum(s.psay)As Psay , " & _
                        " s.ambkod,sum(s.brtagr)As brtagr, " & _
                        " Case When Substring(s.pusno,1,2)='FO' Or Substring(s.pusno,1,2)='EO' " & _
                                " Then  Cast(s.pickno  As nvarchar(7)) " & _
                                " Else  s.pusno  End Pusno ," & _
                        "  p.dunsid, p.USERF2, p.USERF3, " & _
                        " ETKADR1,ETKADR2, min(s.crddte)As crddte ,min(s.crdtim) As crdtim , " & _
                        " min(s.ilkkutuno)As ilkkutuno , min(s.ilkpaletno)As ilkpaletno ,  s.pkmik," & _
                        " Max(s.Plaka) As Plaka, Max(s.Carrier) As Carrier " & _
                        " FROM SHPPACK s" & _
                        " Left Join PLANTPRM p  " & _
                             " On s.cust = p.canb " & _
                              " AND   s.shipto = p.B9CD " & _
                              " And   s.Cust_seq=p.cust_seq " & _
                        " WHERE s.cust = p.canb " & _
                            "AND   s.shipto = p.B9CD "

            sSQL = sSQL & "AND   s.shpno = " & nIrsaliyeNo & _
                        " group by  s.shpno, s.pickno, s.shipto, s.cust, s.itnbr, " & _
                                " s.kapi, p.ttime, s.sevktar, s.stkob,  s.kmik, " & _
                                " s.ambkod, s.pusno, p.dunsid, p.USERF2, p.USERF3, " & _
                                " ETKADR1,ETKADR2, s.pkmik " & _
                        " order by Itnbr,pusno "

            nBrtAgr = 0
            nKapAdedi = 0

            dt = db.RunSql(sSQL)

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                For Each rowBaslik As DataRow In dt.Rows

                    sAdres1 = sLookup(" ETKSUPNM ", "EDIPRM", " 1=1")

                    sAdres2 = sLookup(" ETKADR ", "EDIPRM", " 1=1")
                    sAdres3 = ""
                    sSehir = ""
                    sUlke = ""

                    sPlaka = rowBaslik.Item("PLAKA").ToString

                    sDHCDTX = rowBaslik.Item("Carrier").ToString

                    sSQL = " SELECT Description, Uf_NakliyeciAdresi " & _
                                " FROM shipcode" & _
                                " WHERE ship_code=" & sTirnakEkle(sDHCDTX)

                    dtTemp = db.RunSql(sSQL)

                    If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                        sCarrDesc = dtTemp.Rows(0)("Description").ToString.Replace(",", "")
                        ' Carrier description

                        sCarrAdres1 = Copy(dtTemp.Rows(0)("Uf_NakliyeciAdresi").ToString.Replace(",", ""), 0, 35)
                        ' Carrier adres 1

                        sCarrAdres2 = Copy(dtTemp.Rows(0)("Uf_NakliyeciAdresi").ToString.Replace(",", ""), 35, 35)
                        ' Carrier adres 2

                        sCarrAdres3 = Copy(dtTemp.Rows(0)("Uf_NakliyeciAdresi").ToString.Replace(",", ""), 70, 35)
                        ' Carrier adres 3

                        sCarrCity = ""
                        ' Carrier city
                    End If

                    sMItmKod = ""
                    sMItmDesc = ""
                    sKontrat = ""
                    sKanban = ""

                    MusteriMalzemeBilgileri(sMItmKod, sMItmDesc, sKontrat, sKanban, _
                                            rowBaslik.Item("Cust").ToString, _
                                            rowBaslik.Item("Shipto").ToString, _
                                            rowBaslik.Item("kapi").ToString, _
                                            rowBaslik.Item("itnbr").ToString)

                    sMAMBKOD = ""
                    sMKKOD = ""
                    sMPKOD = ""
                    sMSPKOD = ""
                    sMKPKOD = ""

                    MusteriAmbalajBilgileri(sMAMBKOD, sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, _
                                            rowBaslik.Item("itnbr").ToString, _
                                            rowBaslik.Item("ambkod").ToString)

                    nBrtAgr = nBrtAgr + rowBaslik.Item("BRTAGR").ToString

                    nKapAdedi = nKapAdedi + rowBaslik.Item("PSAY").ToString
                    '
                    ' if pos('%', Trim(rowBaslik.item('PUSNO').tostring)) = 0 then
                    ' sPusNoSon := ''
                    ' else if pos('%', Trim(rowBaslik.item('PUSNO').tostring)) = 1 then
                    ' sPusNoSon := trim(copy(rowBaslik.item('PUSNO').tostring, 1, 25))
                    ' else
                    ' sPusNoSon := copy(rowBaslik.item('PUSNO').tostring, pos('%', Trim(rowBaslik.item('PUSNO').tostring))+1, 22);
                    '

                    sTeslimTar = DateAdd(DateInterval.Day, CInt(IIf(rowBaslik.Item("TTime").ToString = "", 0, rowBaslik.Item("TTime").ToString)), CDate(rowBaslik.Item("SEVKTAR").ToString))

                    sCrdTim = sLookup("MAX(CRDTIM)", "SHPPACK", " SHPNO=" & rowBaslik.Item("SHPNO").ToString)

                    sSQL = "SELECT Itnbr,pltno,pusno," & _
                                " min(ETKSERINO) As ILKKUTUNO , Max(ETKSERINO) As SONKUTUNO ,Count(Pusno) AS KSAY " & _
                                " FROM ETIKETDTY" & _
                                " WHERE  pickno = " & rowBaslik.Item("Pickno").ToString & _
                                    " AND itnbr = " & sTirnakEkle(rowBaslik.Item("itnbr").ToString) & _
                                    " And Pusno=" & sTirnakEkle(IIf(rowBaslik.Item("Pusno").ToString = "", rowBaslik.Item("Pickno").ToString, rowBaslik.Item("Pusno").ToString)) & _
                                    " And KutuEtk=1" & _
                                " group by Itnbr,pltno,pusno " & _
                                " order by Pltno,Itnbr "

                    k = 1

                    dtTemp = db.RunSql(sSQL)

                    If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                        For Each rowDetay As DataRow In dtTemp.Rows

                            sRanNo = rowDetay.Item("pusno").ToString.Trim()

                            If sRanNo.Split("%").Length > 1 Then

                                sPusNo = sRanNo.Split("%")(0).ToString

                                sRanNo = sRanNo.Split("%")(1).ToString

                            Else

                                sPusNo = sRanNo

                                sRanNo = ""

                            End If

                            sSQL = " INSERT INTO ANSDELIVERY " & _
                                    "(  SHPNO, PICKNO, SHIPTO, KAPI, ITNBR, DUNSNO, " & _
                                    " SEVKTAR, TESLIMTAR, KONTNO, KANBANNO, REFADI, ITEM, " & _
                                    " SHIPMIK, STKOB, AMBTAN, KSAY, KMIK, BRTAGR, " & _
                                    " PLAKANO, PUSNO, PUSNOSON, PSAY, ILKPALETNO, SONPALETNO," & _
                                    " ILKKUTUNO, SONKUTUNO, KAPADEDI, " & _
                                    " ADDR1, ADDR2, ADDR3, CONTACT, TELEPHONE, " & _
                                    " ETKADR1, ETKADR2, CRDDTE, CRDTIM, " & _
                                    " CarrDesc, CarrAdres1, CarrAdres2, CarrAdres3, CarrCity " & _
                                    " ) VALUES (" & _
                                    rowBaslik.Item("SHPNO").ToString & ", " & _
                                    rowBaslik.Item("PiCKNO").ToString & ", " & _
                                    sTirnakEkle(rowBaslik.Item("SHiPTO").ToString) & ", " & _
                                    sTirnakEkle(rowBaslik.Item("KAPi").ToString) & ", " & _
                                    sTirnakEkle(rowBaslik.Item("iTNBR").ToString) & ", " & _
                                    sTirnakEkle(rowBaslik.Item("DUNSiD").ToString) & ", " & _
                                    sTarih(rowBaslik.Item("SEVKTAR").ToString) & ", " & _
                                    sTarih(sTeslimTar) & ", " & _
                                    sTirnakEkle(sKontrat) & ", " & _
                                    sTirnakEkle(sKanban) & ", " & _
                                    sTirnakEkle(sMItmDesc) & ", " & _
                                    sTirnakEkle(sMItmKod) & ", " & _
                                    rowBaslik.Item("SHiPMiK").ToString & ", " & _
                                    sTirnakEkle(rowBaslik.Item("STKOB").ToString) & ", " & _
                                    sTirnakEkle(sMAMBKOD) & ", " & _
                                    rowDetay.Item("KSAY").ToString & ", " & _
                                    rowBaslik.Item("KMiK").ToString & ", " & _
                                    rowBaslik.Item("BRTAGR").ToString & ", " & _
                                    sTirnakEkle(sPlaka) & ", " & _
                                    sTirnakEkle(sPusNo) & ", " & _
                                    sTirnakEkle(sRanNo) & ", 1 , " & _
                                    rowDetay.Item("PLTNO").ToString & ", "

                            If k = 1 Then
                                sSQL = sSQL & "0,"
                                k = 0
                            Else
                                sSQL = sSQL & "1,"
                            End If

                            sSQL = sSQL & rowDetay.Item("ILKKUTUNO").ToString & ", " & _
                            rowDetay.Item("KSAY").ToString & ", " & "1, " & _
                            sTirnakEkle(sAdres1) & ", " & _
                            sTirnakEkle(sAdres2) & ", " & _
                            sTirnakEkle(sAdres3 & sSehir & sUlke) & ", " & _
                            sTirnakEkle(rowBaslik.Item("USERF2").ToString) & ", " & _
                            sTirnakEkle(rowBaslik.Item("USERF3").ToString) & ", " & _
                            sTirnakEkle(rowBaslik.Item("ETKADR1").ToString) & ", " & _
                            sTirnakEkle(rowBaslik.Item("ETKADR2").ToString) & ", " & _
                           sTarih(rowBaslik.Item("CRDDTE").ToString) & ", " & _
                           sTirnakEkle(sCrdTim) & ", " & _
                           sTirnakEkle(sCarrDesc) & ", " & _
                           sTirnakEkle(sCarrAdres1) & ", " & _
                           sTirnakEkle(sCarrAdres2) & ", " & _
                           sTirnakEkle(sCarrAdres3) & ", " & _
                           sTirnakEkle(sCarrCity) & _
                           ")"

                            dbAccess.RunSql(sSQL)

                        Next

                    End If

                Next

            End If

            sSQL = "UPDATE ANSDELIVERY " & _
                        " SET brtagr = " & Convert.ToString(Convert.ToInt64(nBrtAgr)) & _
                        ", KAPADEDI = " & Convert.ToString(nKapAdedi) + " "

            dbAccess.RunSql(sSQL)

            sFormul = " {ANSDELIVERY.SHIPMIK}<>0 "

            sPath = Environment.CurrentDirectory

            RaporCagir("RDelivery.rpt", sFormul)

            GridEX1.ExpandGroups()

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged, GridEX1.Click, GridEX1.DoubleClick
        GridSec(GridEX1, "shpno", sender, e)
    End Sub

    Private Sub txtMusteriNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMusteriNo.ButtonClick
        Try
            Dim ssorgu As String

            Me.Cursor = Cursors.WaitCursor

            ssorgu = "SELECT Distinct Cust_Num as Taraf,Name as Tanim " & _
                            " From CustAddr"

            FindFormCagir(ssorgu, "Taraf", "Tanim", txtMusteriNo.Text, txtTanim.Text)

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    #End Region 'Methods

End Class