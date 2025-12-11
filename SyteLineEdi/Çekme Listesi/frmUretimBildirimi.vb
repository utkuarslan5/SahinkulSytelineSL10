Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Globalization
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms

Public Class frmUretimBildirimi

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString.ToString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Dim dMiktar As Decimal
    Dim dt As New DataTable
    Dim dtOper As New DataTable
    Dim sBeforeOper As String
    Dim sNextOper As String
    Dim sStr As String = String.Empty

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Try

            If txtYer.Text = String.Empty Then Throw New Exception("Yer Tanýmsýz")
            If txtItem.Text = String.Empty Then Throw New Exception("Malzeme Tanýmsýz")
            If txtQty.Text > dMiktar Then Throw New Exception("Miktar Gerekenden Büyük")

            Dim p As New ArrayList

            p.Add(New SqlParameter("@item", txtItem.Text))
            p.Add(New SqlParameter("@loc", txtYer.Text))
            p.Add(New SqlParameter("@lot", txtLot.Text))
            ' p.Add(New SqlParameter("@ReturnMessage", txtLot.Text))

            Dim p2 As New SqlClient.SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 4000)

            p2.Direction = ParameterDirection.Output
            p.Add(p2)

            db.RunSp("TR_Stok_Kontrol", p, 1, False)
            If Not (IsDBNull(p2.Value) Or p2.Value Is Nothing) Then
                MessageBox.Show(p2.Value.ToString)
                Return
            End If

            sStr = " Exec Tr_UretimBildirimi " &
                        "@Job=" & sTirnakEkle(txtJob.Text) & "," &
                        "@Oper_num=" & cmbOperNo.Text & "," &
                        "@Loc=" & sTirnakEkle(txtYer.Text) & "," &
                        "@Lot=" & sTirnakEkle(txtLot.Text) & "," &
                        "@qty_complete=" & txtQty.Text & "," &
                        "@NextOper=" & sNextOper

            db.RunSql(sStr)

            'btnTemizle_Click(Me, e)
            txtItem.Text = String.Empty
            txtJob.Text = String.Empty
            txtYer.Text = String.Empty
            txtLot.Text = String.Empty
            cmbOperNo.Text = String.Empty
            cmbOperNo.Value = String.Empty
            txtQty.Text = 0
            cmbOperNo.DataSource = Nothing
            sBeforeOper = String.Empty
            sNextOper = String.Empty
            dMiktar = 0

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnTemizle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTemizle.Click
        txtItem.Text = String.Empty
        txtJob.Text = String.Empty
        txtYer.Text = String.Empty
        txtLot.Text = String.Empty
        cmbOperNo.Text = String.Empty
        cmbOperNo.Value = String.Empty
        txtQty.Text = 0
        cmbOperNo.DataSource = Nothing
        sBeforeOper = String.Empty
        sNextOper = String.Empty
        dMiktar = 0
    End Sub

    Private Sub cmbOperNo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOperNo.ValueChanged, cmbOperNo.Leave
        Dim sFilter As String
        Dim foundRows() As DataRow = Nothing

        txtYer.Text = cmbOperNo.Value

        If dtOper.Rows.Count > 0 Then
            sFilter = "job=" & sTirnakEkle(txtJob.Text) & " And " & " Oper_num=" & IIf(cmbOperNo.Text = "", 0, cmbOperNo.Text)
            foundRows = dtOper.Select(sFilter)
            If foundRows.Length > 0 Then
                sBeforeOper = foundRows(0).ItemArray(6).ToString
                sNextOper = foundRows(0).ItemArray(7).ToString
                dMiktar = foundRows(0).ItemArray(5).ToString
                txtYer.Text = foundRows(0).ItemArray(8).ToString
            Else
                sBeforeOper = String.Empty
                sNextOper = String.Empty
                dMiktar = 0
                txtYer.Text = String.Empty
            End If
        Else
            sBeforeOper = String.Empty
            sNextOper = String.Empty
            dMiktar = 0
            txtYer.Text = String.Empty
        End If
    End Sub

    Private Sub txtItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtItem.KeyDown
        Dim Malzeme As String = String.Empty

        Dim Isemri As String = String.Empty

        If e.KeyCode = Keys.Enter Then

            Cursor.Current = Cursors.WaitCursor

            Try
                If txtItem.Text.Trim.Length > 0 Then

                    If txtItem.Text.Contains("%") Then

                        Malzeme = txtItem.Text.Split("%")(0)
                        Isemri = txtItem.Text.Split("%")(1)
                        txtItem.Text = Malzeme
                        txtJob.Text = Isemri
                        txtLot.Text = Isemri
                        sStr = "Exec Tr_Edi_Uretim_Bildirimi_Load @Job=" & sTirnakEkle(Isemri) & ""

                        dtOper = db.RunSql(sStr)

                        cmbOperNo.DataSource = dtOper

                        cmbOperNo.Focus()

                    End If

                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
    End Sub

    #End Region 'Methods

End Class