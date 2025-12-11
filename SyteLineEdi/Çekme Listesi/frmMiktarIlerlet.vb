Imports System.Data.SqlClient

Public Class frmMiktarIlerlet

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString.ToString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Dim dt As New DataTable
    Dim sStr As String = String.Empty

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Close()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtItem.Text = ""
        txtQty.Text = ""
        '

        '
        'txtYer2.Text = ""
        txtLot.Text = ""
        txtItem.Focus()
        txtItem.Enabled = True
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Try

            If Not kontrol(, Me) Then
                Exit Sub
            End If

            Dim cmd As String

            cmd = "select qty_on_hand from lot_loc where item = '" & txtItem.Text & "' AND loc='" & cmbBoxCikisYeri.Text & "'"
            dt = db.RunSql(cmd)

            Try
                If (txtQty.Text > dt.Rows(0)(0)) Then
                    MessageBox.Show("Eldeki miktar: " & Convert.ToInt32(dt.Rows(0)(0)) & vbNewLine & "Bu miktardan fazlası ilerletilemez!")
                    Exit Sub
                End If
            Catch ex As Exception
                MessageBox.Show("Hata oluştu")
                Exit Sub
            End Try

            Dim p As New ArrayList

            p.Add(New SqlParameter("@item", txtItem.Text))
            p.Add(New SqlParameter("@loc", txtYer2.Text))
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

            cmd = "EXEC dbo.TR_Miktar_Ilerlet @Item = '" & txtItem.Text & "', @loc1 = '" & cmbBoxCikisYeri.Text + "', @lot1 = '" & _
                txtLot.Text & "', @loc2 = '" & txtYer2.Text & "', @qty ='" & txtQty.Text & "' "

            db.RunSql(cmd, True)

            txtItem.Text = ""
            txtLot.Text = ""
            txtQty.Text = ""
            txtYer2.Text = ""
            cmbBoxCikisYeri.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtItem.KeyDown, txtQty.KeyDown
        Dim Malzeme As String = String.Empty

        Dim lot As String = String.Empty

        If e.KeyCode = Keys.Enter Then

            Cursor.Current = Cursors.WaitCursor

            Try
                If txtItem.Text.Trim.Length > 0 Then

                    If txtItem.Text.Contains("%") Then

                        Malzeme = txtItem.Text.Split("%")(0)
                        lot = txtItem.Text.Split("%")(1)
                        txtItem.Text = Malzeme
                        txtLot.Text = lot

                        sStr = "SELECT loc FROM lot_loc WHERE item = '" & _
                            txtItem.Text & "' " & "And lot = '" & txtLot.Text & "' And qty_on_hand > 0 AND whse='" & My.Settings.DefaultWhse & "'"
                        cmbBoxCikisYeri.DisplayMember = "loc"
                        dt = db.RunSql(sStr)

                        cmbBoxCikisYeri.DataSource = dt

                        cmbBoxCikisYeri.Focus()

                    End If

                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
    End Sub

    #End Region 'Methods

End Class