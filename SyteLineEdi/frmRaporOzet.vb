Public Class frmRaporOzet

    #Region "Fields"

    Public dtRapor As DataTable
    Public sBaslik As String
    Public sKagitAdi As String
    Public sTip As String = ""

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

End Class