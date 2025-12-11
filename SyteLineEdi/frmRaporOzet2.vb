Public Class frmRaporOzet2

#Region "Fields"

    Public dtRapor As DataTable
    Public sBaslik As String
    Public sKagitAdi As String
    Public sTip As String = ""
    Public sKopyaSayisi As Double
#End Region 'Fields

#Region "Methods"

    Private Sub frmRaporOzet_Load(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub

#End Region 'Methods


    Private Sub btnYazdir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYazdir.Click

        reportDocument2.PrintToPrinter(1, False, 0, 0)

    End Sub
End Class