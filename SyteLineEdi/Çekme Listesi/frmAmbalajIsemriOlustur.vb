Imports System.Data.SqlClient

Public Class frmAmbalajIsemriOlustur

    #Region "Fields"

    Dim bSorgu As Boolean
    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Dim ds As DataSet
    Dim dt As New DataTable
    Dim sQuery As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnCheckedAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckedAll.Click
        GridEX1.CheckAllRecords()
    End Sub

    Private Sub btnisemriolustur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnisemriolustur.Click
        Try
            Dim param As New ArrayList
            'Dim paramout As New ArrayList

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Dim serino As String = ""

            Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

            checkedRows = Me.GridEX1.GetCheckedRows()

            Dim row As Janus.Windows.GridEX.GridEXRow

            If checkedRows.Length > 0 Then

                For Each row In checkedRows

                    serino = "MA" & SeriNoAl("JOB").ToString.PadLeft(8, "0")

                    param.Clear()

                    param.Add(New SqlClient.SqlParameter("@Item", row.Cells("URUN").Value.ToString))

                    Dim p As New SqlClient.SqlParameter("@Job", serino.ToString)
                    param.Add(p)

                    Dim p1 As New SqlClient.SqlParameter("@Suffix", 0)
                    param.Add(p1)

                    Dim p2 As New SqlClient.SqlParameter("@RefNum", "")
                    param.Add(p2)

                    Dim p3 As New SqlClient.SqlParameter("@FromWorkbench", 0)
                    param.Add(p3)

                    Dim p4 As New SqlClient.SqlParameter("@ReqdQty", row.Cells("MIKTAR").Value.ToString)
                    param.Add(p4)

                    Dim p5 As New SqlClient.SqlParameter("@Whse", row.Cells("AMBAR").Value.ToString)
                    param.Add(p5)

                    Dim p6 As New SqlClient.SqlParameter("@CopyCurrentBOM", 1)
                    param.Add(p6)

                    Dim p7 As New SqlClient.SqlParameter("@Infobar", 0)
                    'p6.Direction = ParameterDirection.Output
                    param.Add(p7)

                    'Dim p7 As New SqlClient.SqlParameter("@Infobar", DBNull.Value)
                    'p7.Direction = ParameterDirection.Output
                    'param.Add(p7)

                    'Dim p5 As New SqlClient.SqlParameter("@DueDate", DBNull.Value)
                    'param.Add(p5)

                    'Dim p6 As New SqlClient.SqlParameter("@ReqdQty", 0)
                    'param.Add(p6)

                    'Dim p7 As New SqlClient.SqlParameter("@RefType", DBNull.Value)
                    'param.Add(p7)

                    'Dim p8 As New SqlClient.SqlParameter("@RefLineSuf", 0)
                    'param.Add(p8)

                    'Dim p9 As New SqlClient.SqlParameter("@RefRelease", 0)
                    'param.Add(p9)

                    'Dim p10 As New SqlClient.SqlParameter("@RefSeq", 0)
                    'param.Add(p10)

                    'Dim p12 As New SqlClient.SqlParameter("@CopyCurrentBOM", 0)
                    'param.Add(p12)

                    'Dim p13 As New SqlClient.SqlParameter("@CopyIndentedBOM ", 0)
                    'param.Add(p13)

                    'Dim p14 As New SqlClient.SqlParameter("@SessionID", DBNull.Valu)
                    'param.Add(p14)

                    'Dim p5 As New SqlClient.SqlParameter("@Infobar", 0)
                    'p5.Direction = ParameterDirection.Output
                    'param.Add(p5)

                    'sQuery = " Exec FirmJobSp_TUR2 '" & row.Cells("URUN").Value.ToString & "','" & serino & _
                    '         "',0,'',0,null,null," & row.Cells("MIKTAR").Value.ToString & ",'F',0,0,0,'MAIN',1,0,null,null"

                    'db.RunSp(sQuery, True)

                    db.RunSp("[FirmJobSp_TUR2]", param)

                    Threading.Thread.Sleep(1000)

                    sQuery = "Select * from JOB Where ITEM='" & row.Cells("URUN").Value.ToString & "'" & _
                             " AND JOB='" & serino & "'"

                    dt = db.RunSql(sQuery, "JOB").Tables(0)

                    If dt.Rows.Count > 0 Then

                        sQuery = "Update TRCEKLIST Set JOBSTAT=1, JOBNUMBER='" & serino & "' Where CEKLIST='" & _
                        row.Cells("CEKLIST").Value.ToString & "'" & " AND ADAITX='" & _
                        row.Cells("URUN").Value.ToString & "'"

                        db.RunSql(sQuery, True)

                    End If
                Next
                btnSorgula_Click(sender, AcceptButton)
            Else
                MessageBox.Show("Lütfen Listeden, Ýþ emri vereceðiniz bir kayýt seçiniz!", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            '
            System.Windows.Forms.Cursor.Current = Cursors.Default

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            System.Windows.Forms.Cursor.Current = Cursors.Default

        End Try
    End Sub

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click
        Dim sDate As String

        sDate = dtmTeslimTarihi.Value.ToString("1yyMMdd")

        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        If txtMusteri2.Text <> "" AndAlso txtMusteri2.Text < txtMusteri1.Text Then

            MessageBox.Show("Lütfen Ýkinci Müþteri Numarasýný Ýlkinden Büyük Seçiniz!", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Exit Sub

        End If

        Try

            sQuery = " SELECT CEKLIST,C6CANB as MusteriNo,CUSNM as MusteriAd,C6B9CD as TeslAlan,C6F1CD as KAPI,  " & _
                     " ADAITX as URUN,ITDSC as TANIM,ADA3cd as AMBAR,CEKDATE AS cekmetarihi," & _
                     " sum(ADAQQT) as MIKTAR,JOBSTAT,JOBNUMBER " & _
                     " FROM  TRCEKLIST  " & _
                     " Left Join Item " & _
                                    " On Item.Item=ADAITX " & _
                     " WHERE JOBSTAT is null and ADAQQT > 0 " & _
                        " And ADAITX <> isnull(Uf_AmbalajsizMalzemeKodu,'')"

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

            If CheckBox1.Checked Then
                sQuery = sQuery & "    and CEKDATE=" & CDate(dtmTeslimTarihi.Value).ToString("1yyMMdd")
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

            sQuery = sQuery & " GROUP BY CEKLIST,C6CANB,CUSNM,C6B9CD,C6F1CD,ADAITX, " & _
                                " ITDSC,ADA3cd,CEKDATE,JOBSTAT,JOBNUMBER "

            sQuery = sQuery & " Order By c6canb, C6B9CD, C6f1cd, CEKDATE "

            dt = db.RunSql(sQuery)

            If Not (dt Is Nothing) AndAlso _
                            dt.Rows.Count > 0 Then

                bSorgu = True

                GridEX1.DataSource = dt
                GridEX1.DataMember = "CEKLIST"
                'GridEX1.DataMember = "Cekme"

                bSorgu = False

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

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            dtmTeslimTarihi.Enabled = True
        Else
            dtmTeslimTarihi.Enabled = False
        End If
    End Sub

    Private Sub frmCLYenidenBasim_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            txtKullanici.Text = kullaniciadi

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

    Private Sub GridEX1_CurrentCellChanging(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.CurrentCellChangingEventArgs) Handles GridEX1.CurrentCellChanging
        Dim sQuery As String

        Dim dtJob As New DataTable

        If bSorgu = False Then
            sQuery = "Select i.item, i.job,j.item" & _
                    " from item i" & _
                    " inner Join jobmatl j " & _
                      " On j.job=i.job " & _
                    " Where i.item=" & sTirnakEkle(e.Row.Cells("Urun").Text)

            dtJob = db.RunSql(sQuery)

            If Not dtJob Is Nothing AndAlso dtJob.Rows.Count = 0 Then

                MsgBox(e.Row.Cells("Urun").Text & " Nolu Ürün Seçilemez. Ürün Aðacý Eksik!", MsgBoxStyle.OkOnly, "Ekip Mapics")

                e.Cancel = True

            End If

        End If
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