Imports System.IO

Public Class frmRaporGor

    #Region "Fields"

    Private Shared parametre As Integer

    Private ad() As String
    Private DosyaAd As String
    Private ilkHal As String
    Private Klasor() As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub ac()
        Dim sDizin As String
        '
        fldDizin.Description = "Klasörü Seçiniz! "
        '
        'DialogResult = fldDizin.ShowDialog(Me)
        '
        fldDizin.SelectedPath = "c:\"

        If fldDizin.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
            If IO.Directory.Exists(fldDizin.SelectedPath) = True Then
                '
                sDizin = fldDizin.SelectedPath.ToString
                txtDosyaYol.Text = sDizin

            Else
                '
                MessageBox.Show("Böyle Bir Dizin Bulunamadý!", " Error", _
                MessageBoxButtons.OK, MessageBoxIcon.Error)
                '
            End If
            '
        End If
    End Sub

    Private Sub btnAc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAc.Click
        ac()
        parametre = 1
    End Sub

    Private Sub btnListele_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListele.Click
        lstListe.Items.Clear()
        PictureBox1.Visible = True
        PictureBox1.Refresh()

        If (Directory.Exists(txtDosyaYol.Text)) Then
            parametre = 1
        End If

        If parametre <> 1 Then
            Dim deger As String
            Dim raporAd As String = txtDosyaYol.Text
            deger = txtDosyaYol.Text.Split("\").Length - 1
            raporAd = txtDosyaYol.Text.Split("\")(deger)
            Dim KlasorUstYol As String
            KlasorUstYol = Mid(txtDosyaYol.Text, 1, txtDosyaYol.Text.Length - raporAd.Length)
            doldur(KlasorUstYol, raporAd)
            If lstListe.Items.Count.ToString < 1 Then
                MsgBox("Böyle Bir Dosya Yada Klasör Bulunamadý", MsgBoxStyle.OkOnly, "Hata")
            End If
        Else
            doldur(txtDosyaYol.Text, "*")
            parametre = 0
        End If

        PictureBox1.Visible = False
        PictureBox1.Refresh()
    End Sub

    Private Sub btnSil_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSil.Click
        lstListe.Items.Clear()
        txtDosyaYol.Text = ""
        CrystalReportViewer1.ReportSource = Nothing
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub doldur(ByVal klasorYol As String, ByVal raporAd As String)
        Try

            Dim KlasorIlkYol As String

            PictureBox1.Refresh()

            If (Directory.Exists(klasorYol)) Then
                'ad = Directory.GetFileSystemEntries(klasorYol, raporAd + ".rpt")
                ad = Directory.GetFiles(klasorYol)
                For Each yaz As String In ad
                    If (yaz.EndsWith("rpt")) Then
                        PictureBox1.Refresh()
                        Me.Refresh()
                        ilkHal = yaz
                        'yaz.Split(".")(0).ToString()
                        Dim deger As Integer
                        deger = yaz.Split("\").Length - 1
                        yaz = yaz.Split("\")(deger)

                        KlasorIlkYol = Mid(ilkHal, 1, ilkHal.Length - yaz.Length)

                        lstListe.Items.Add(yaz)
                        lstListe.Refresh()
                    End If
                Next

                ' Klasor = Directory.GetDirectories(klasorYol)
                'For Each KlasorDizin As String In Klasor
                '    PictureBox1.Refresh()
                '  doldur(KlasorDizin, raporAd)

                '    Application.DoEvents() '************************'
                'Next

            Else
                DosyaAd = klasorYol
                ' MsgBox("Böyle Bir Klasör Bulunamadý", MsgBoxStyle.OkOnly, "Hata")
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PictureBox1.Visible = False
        parametre = 0
        txtDosyaYol.Text = My.Settings.ReportsPath
        'C:\Program Files\Ekip MAPICS\SAP_MENU_KURULUM\SAP_Raporlar
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim faturaAd As String
        Dim deger1, deger2 As Integer

        faturaAd = lstListe.Items(lstListe.SelectedIndex).ToString

        deger1 = faturaAd.IndexOf(" ", 1, faturaAd.Length - 1)
        deger2 = faturaAd.Length - 80

        'ToolTip1.SetToolTip(lstListe, faturaAd.Substring(80, deger2).ToString)

        CrystalReportViewer1.ReportSource = faturaAd.Substring(80, deger2) + faturaAd.Substring(0, deger1)

        CrystalReportViewer1.Show()
    End Sub

    Private Sub lstListe_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstListe.SelectedIndexChanged
        Dim faturaAd As String
        Dim deger1, deger2 As Integer

        faturaAd = lstListe.Items(lstListe.SelectedIndex).ToString

        'deger1 = faturaAd.IndexOf(" ", 1, faturaAd.Length - 1)
        'deger2 = faturaAd.Length - 80

        'ToolTip1.SetToolTip(lstListe, faturaAd.Substring(80, deger2).ToString)

        CrystalReportViewer1.ReportSource = txtDosyaYol.Text & "\" & faturaAd

        CrystalReportViewer1.Show()
    End Sub

    Private Sub txtDosyaYol_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDosyaYol.KeyDown
        If e.KeyData = Keys.Enter Then
            lstListe.Items.Clear()
            PictureBox1.Visible = True

            If (Directory.Exists(txtDosyaYol.Text)) Then
                parametre = 1
            End If

            If parametre <> 1 Then

                Dim deger As String
                Dim raporAd As String = txtDosyaYol.Text
                deger = txtDosyaYol.Text.Split("\").Length - 1
                raporAd = txtDosyaYol.Text.Split("\")(deger)

                Dim KlasorUstYol As String
                KlasorUstYol = Mid(txtDosyaYol.Text, 1, txtDosyaYol.Text.Length - raporAd.Length)
                doldur(KlasorUstYol, raporAd)
                If lstListe.Items.Count.ToString < 1 Then
                    MsgBox("Böyle Bir Dosya Yada Klasör Bulunamadý", MsgBoxStyle.OkOnly, "Hata")
                End If
            Else
                doldur(txtDosyaYol.Text, "*")
                parametre = 0
            End If
            PictureBox1.Visible = False
        End If
    End Sub

    Private Sub txtDosyaYol_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDosyaYol.TextChanged
    End Sub

    #End Region 'Methods

End Class