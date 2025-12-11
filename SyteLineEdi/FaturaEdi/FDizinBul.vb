Option Strict Off
Option Explicit On

Friend Class FDizinBul
    Inherits System.Windows.Forms.Form

    #Region "Methods"

    Private Sub CancelButton_Renamed_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CancelButton_Renamed.Click
        Me.Close()
    End Sub

    Private Sub Drive1_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Drive1.SelectedIndexChanged
        On Error Resume Next
        Dir1.Path = Drive1.Drive
    End Sub

    Private Sub OKButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OKButton.Click
        FAna.Txt_DosyaYol.Text = Dir1.Path
        Me.Close()
    End Sub

    #End Region 'Methods

End Class