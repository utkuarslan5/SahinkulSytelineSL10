Public Class frmSatinalmaSiparisMektubu

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Dim dt As New DataTable
    Dim dtTemp As New DataTable
    Dim sQuery As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click
        Try
            sQuery = " SELECT * FROM TR_POSIP WHERE 1=1 "

            If cbAktif.Checked Then
                sQuery &= " AND order_date >= " & sTirnakEkle(dtmSiparis.Value.ToString("yyyy-MM-dd"))
            End If

            If txtTedarikci1.Text <> "" Then
                If txtTedarikci2.Text = "" Then
                    sQuery &= " AND vend_num = " & sTirnakEkle(txtTedarikci1.Text)
                Else
                    sQuery &= " AND vend_num BETWEEN " & sTirnakEkle(txtTedarikci1.Text) &
                        " AND " & sTirnakEkle(txtTedarikci2.Text)
                End If
            End If

            If txtMalzeme1.Text <> "" Then
                If txtMalzeme2.Text = "" Then
                    sQuery &= " AND item = " & sTirnakEkle(txtMalzeme1.Text)
                Else
                    sQuery &= " AND item BETWEEN " & sTirnakEkle(txtMalzeme1.Text) &
                        " AND " & sTirnakEkle(txtMalzeme2.Text)
                End If
            End If

            If cbPlanci1.Text <> "" Then
                If cbPlanci2.Text = "" Then
                    sQuery &= " AND plan_code = " & sTirnakEkle(cbPlanci1.Text)
                Else
                    sQuery &= " AND plan_code BETWEEN " & sTirnakEkle(cbPlanci1.Text) &
                        " AND " & sTirnakEkle(cbPlanci2.Text)
                End If
            End If

            If cbSiparis1.Text <> "" Then
                If cbSiparis2.Text = "" Then
                    sQuery &= " AND po_num = " & sTirnakEkle(cbSiparis1.Text)
                Else
                    sQuery &= " AND po_num BETWEEN " & sTirnakEkle(cbSiparis1.Text) &
                        " AND " & sTirnakEkle(cbSiparis2.Text)
                End If
            End If

            If rbIlkBasim.Checked Then
                sQuery &= " AND Uf_Print IS NULL"
            ElseIf rbYenidenBasim.Checked Then
                sQuery &= " AND Uf_Print = 'P'"
            End If

            If rbAcilmis.Checked Then
                sQuery &= " AND stat ='O'"
            ElseIf rbPlanli.Checked Then
                sQuery &= " AND stat = 'P'"
            End If

            dt = db.RunSql(sQuery)
            GridEX1.DataSource = dt

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnYazdır_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYazdır.Click
        If (rbIlkBasim.Checked) Then
            Dim po_num, po_line, query As String

            For Each row As Janus.Windows.GridEX.GridEXRow In GridEX1.GetRows

                po_num = row.Cells("po_num").Value

                po_line = row.Cells("po_line").Value

                query = "UPDATE poitem " &
                            " SET Uf_Print = " & sTirnakEkle("P") &
                                    ", stat='O' " &
                            " WHERE po_num = " & sTirnakEkle(po_num) &
                                " AND po_line = " & sTirnakEkle(po_line)

                db.RunSql(query)

                query = "UPDATE po " &
                            " SET stat='O' " &
                            " WHERE po_num = " & sTirnakEkle(po_num)

                db.RunSql(query)

                query = "UPDATE job " &
                            " SET Uf_Print = " & sTirnakEkle("P") &
                            " WHERE job = " & sTirnakEkle(po_num)

                db.RunSql(query)

            Next

        End If

        dt = GridEX1.DataSource

        If dt.Rows(0).Item("lang_code") = "TRK" Then
            RaporCagir("TR_POSIP.rpt", , , , , , , dt)
        Else
            RaporCagir("TR_POSIP_ENG.rpt", , , , , , , dt)
        End If

        ' RaporCagir("TR_POSIP.rpt", , , , , , , dt)

    End Sub

    Private Sub cbAktif_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAktif.CheckedChanged
        dtmSiparis.Enabled = cbAktif.Checked
    End Sub

    Private Sub frmSatinalmaSiparisMektubu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtmSiparis.Value = DateTime.Now

        sQuery = "SELECT DISTINCT plan_code FROM TR_POSIP WHERE plan_code IS NOT NULL "
        dt = db.RunSql(sQuery)
        cbPlanci1.DataSource = dt
        cbPlanci1.DisplayMember = "plan_code"
        cbPlanci1.SelectedIndex = -1

        dt = db.RunSql(sQuery)
        cbPlanci2.DataSource = dt
        cbPlanci2.DisplayMember = "plan_code"
        cbPlanci2.SelectedIndex = -1

        sQuery = "SELECT DISTINCT po_num FROM TR_POSIP"
        dt = db.RunSql(sQuery)
        cbSiparis1.DataSource = dt
        cbSiparis1.DisplayMember = "po_num"
        cbSiparis1.SelectedIndex = -1

        dt = db.RunSql(sQuery)
        cbSiparis2.DataSource = dt
        cbSiparis2.DisplayMember = "po_num"
        cbSiparis2.SelectedIndex = -1
    End Sub

    Private Sub txtMalzeme1_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMalzeme1.ButtonClick
        Try
            Me.Cursor = Cursors.WaitCursor
            sQuery = "SELECT DISTINCT item AS Malzeme , description as Tanım FROM TR_POSIP"
            FindFormCagir(sQuery, "Malzeme", "Tanım", txtMalzeme1.Text, "")

        Catch ex As Exception
            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    Private Sub txtMalzeme2_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMalzeme2.ButtonClick
        Try
            Me.Cursor = Cursors.WaitCursor
            sQuery = "SELECT DISTINCT item AS Malzeme , description as Tanım FROM TR_POSIP"
            FindFormCagir(sQuery, "Malzeme", "Tanım", txtMalzeme2.Text, "")

        Catch ex As Exception
            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    Private Sub txtSiparis1_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            sQuery = "SELECT DISTINCT vend_num AS Tedarikçi , name as Tanım FROM TR_POSIP"
            FindFormCagir(sQuery, "Tedarikçi", "Tanım", txtTedarikci2.Text, txtTedarikciAdi2.Text)

        Catch ex As Exception
            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    Private Sub txtSiparis2_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            sQuery = "SELECT DISTINCT vend_num AS Tedarikçi , name as Tanım FROM TR_POSIP"
            FindFormCagir(sQuery, "Tedarikçi", "Tanım", txtTedarikci2.Text, txtTedarikciAdi2.Text)

        Catch ex As Exception
            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    Private Sub txtTedarikci1_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTedarikci1.ButtonClick
        Try
            Me.Cursor = Cursors.WaitCursor
            sQuery = "SELECT DISTINCT vend_num AS Tedarikçi , name as Tanım FROM TR_POSIP"
            FindFormCagir(sQuery, "Tedarikçi", "Tanım", txtTedarikci1.Text, txtTedarikciAdi1.Text)

        Catch ex As Exception
            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    Private Sub txtTedarikci2_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTedarikci2.ButtonClick
        Try
            Me.Cursor = Cursors.WaitCursor
            sQuery = "SELECT DISTINCT vend_num AS Tedarikçi , name as Tanım FROM TR_POSIP"
            FindFormCagir(sQuery, "Tedarikçi", "Tanım", txtTedarikci2.Text, txtTedarikciAdi2.Text)

        Catch ex As Exception
            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    #End Region 'Methods

End Class