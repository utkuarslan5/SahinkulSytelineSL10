Public Class frmFaturaBirimFiyatGuncelleme

    #Region "Fields"

    Dim com As String = String.Empty
    Dim db As New Core.Data(My.Settings.ConnectionString.ToString)
    Dim dt As New DataTable

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnOlustur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOlustur.Click
        Try
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            For Each row As Janus.Windows.GridEX.GridEXRow In grdMain.GetRows()
                com = "UPDATE SHPDTY SET PRICE = " & row.Cells(4).Value &
                    " WHERE ITNBR ='" & row.Cells(1).Value & "' AND SHPNO = '" & row.Cells(0).Value & "'"

                db.RunSql(com)

                com = "INSERT INTO ShpDty_Update (item, DESCRIPTION, Price, UpdatedPrice, UserName)  VALUES " &
                    "('" & row.Cells(1).Value & "', '" & row.Cells(2).Value & "', " &
                        row.Cells(3).Value & ", " & row.Cells(4).Value & ", '" &
                        KullaniciAdi & "')"
                db.RunSql(com)

            Next
            MessageBox.Show("Kaydedildi.")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click
        Try
            com = "SELECT s.SHPNO, i.item, i.description, s.PRICE AS UnitPrice, s.PRICE AS UnitPrice2 FROM SHPDTY s " & _
                  "LEFT JOIN item i ON s.ITNBR = i.item WHERE SHPNO = '" & cmbBoxIrsaliye.Text & "'"

            grdMain.DataSource = db.RunSql(com)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmFaturaBirimFiyatGuncelleme_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            com = "SELECT DISTINCT SHPNO FROM SHPDTY WHERE DO_NUM IS NULL ORDER BY SHPNO DESC"
            dt = db.RunSql(com)
            cmbBoxIrsaliye.DataSource = dt
            cmbBoxIrsaliye.ValueMember = "SHPNO"

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub grdMain_CellEdited(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdMain.CellEdited
    End Sub

    #End Region 'Methods

End Class