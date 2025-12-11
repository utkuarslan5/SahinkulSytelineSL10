Public Class frmTatilGunleri

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dt As New DataTable
    Dim dtTatil As New DataTable
    Dim sSql As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnTatilGunuEkle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTatilGunuEkle.Click
        dtTatil.Rows.Add(New Object() {dtmTatilGunu.SelectionStart})

        GridEX1.DataSource = dtTatil
    End Sub

    Private Sub frmTatilGunleri_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtmTatilGunu.SetDate(Now)

        dtTatil.Columns.Add(New DataColumn("Tatil", Type.GetType("System.String")))
    End Sub

    #End Region 'Methods

End Class