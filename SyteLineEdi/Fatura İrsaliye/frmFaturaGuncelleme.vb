Public Class frmFaturaGuncelleme

    #Region "Fields"

    Dim com As String = String.Empty
    Dim db As New Core.Data(My.Settings.ConnectionString.ToString)
    Dim dt As New DataTable

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnGuncelle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuncelle.Click
        Try

            Dim sorgu = " EXEC TR_Fatura_Guncel_Fiyat '" & txtMusteri1.Text & "'"

            db.RunSql(sorgu)
            MessageBox.Show("İşlem tamamlandı.")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtMusteri1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMusteri1.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim sorgu As String
            sorgu = "SELECT Distinct Cust_Num as Müşteri,Name as Tanim " & _
                            " From CustAddr" & _
                            " Where cust_seq=0"

            FindFormCagir(sorgu, "Müşteri", "Tanim", txtMusteri1.Text, txtMusteriAdi1.Text)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    #End Region 'Methods

End Class