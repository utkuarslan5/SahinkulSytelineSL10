Public Class frmSerbestEtiketBasimi

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim ds As New DataSet

    'Implements RefreshMalzemeSevkEtme
    Dim dt As DataTable
    Dim Row As Janus.Windows.GridEX.GridEXRow
    Dim sQuery As String

    #End Region 'Fields

    #Region "Methods"

    Public Function RandomNumber() As String
        Dim a As New Random
        Return a.Next
    End Function

    Private Sub btnTemizle_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles btnTemizle.Click
        Try

            txtBoxKopya.Text = String.Empty
            txtBoxLot.Text = String.Empty
            txtBoxMalzemeTanim.Text = String.Empty
            txtBoxMalzemeNo.Text = String.Empty

            txtBoxMalzemeNo.Focus()

        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")

        End Try
    End Sub

    Private Sub btnYazdir_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles btnYazdir.Click
        Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection) 'Access connection

        Try

            If Not kontrol(, GroupBox1) Then
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            Dim sStr As String

            If MsgBox("Yazdırma işlemi yapılsın mı ?", MsgBoxStyle.YesNo, "Onay") = MsgBoxResult.Yes Then

                Cursor = Cursors.WaitCursor

                Dim SEISLEMNO As String = RandomNumber()
                Dim IRSALIYE_NO As String = String.Empty
                Dim SATICI_KODU As String = String.Empty
                Dim SATICI_ADI As String = String.Empty
                Dim MALZEME_KODU As String = txtBoxMalzemeNo.Text
                Dim MALZEME_TANIMI As String = txtBoxMalzemeTanim.Text
                Dim MIKTAR As Decimal = txtBoxEtiketMiktari.Text
                Dim LOT_NO As String = txtBoxLot.Text
                Dim TARIH As String = DateTime.Now.ToString("yyyy-MM-dd")

                db.BeginTransaction()
                dbAccess.BeginTransaction()

                For i As Integer = 1 To txtBoxKopya.Text

                    sStr = " insert into LABELDB " & _
                           " ( " & _
                           "  SEISLEMNO, IRSALIYENO,  " & _
                           "  SATICIKODU, SATICIADI, " & _
                           "  MALZEMEKODU, MALZEMETANIMI, " & _
                           "  MIKTAR, LOT_NO, TARIH " & _
                           " ) " & _
                           " values " & _
                           " ( " & _
                           "  '" & SEISLEMNO & "', '" & IRSALIYE_NO & "', " & _
                           "  '" & SATICI_KODU & "', '" & SATICI_ADI & "', " & _
                           "  '" & MALZEME_KODU & "', '" & MALZEME_TANIMI & "', " & _
                           "  " & MIKTAR & ", '" & LOT_NO & "', " & _
                           "  '" & TARIH & "'" & _
                           " ) "

                    dbAccess.RunSql(sStr, True)
                Next

                If dbAccess.Transaction Then
                    dbAccess.CommitTransaction()
                End If

                sStr = " SELECT LABELDB.* FROM LABELDB " & _
                       " WHERE LABELDB.[SEISLEMNO]='" & SEISLEMNO & "'"

                AccessLogOnDatabase("Malzeme_Etiketi.rpt", dbAccess.RunSql(sStr))

                frmRaporOzet.CrystalReportViewer1.ReportSource = frmRaporOzet.reportDocument1
                frmRaporOzet.CrystalReportViewer1.ShowRefreshButton = False
                frmRaporOzet.ShowDialog()

                sStr = "DELETE FROM LABELDB WHERE SEISLEMNO='" & SEISLEMNO & "'"

                dbAccess.RunSql(sStr, True)

                If db.Transaction Then
                    db.CommitTransaction()
                End If

            End If
        Catch ex As Exception
            If dbAccess.Transaction Then
                dbAccess.RollbackTransaction()
            End If
            If db.Transaction Then
                db.RollbackTransaction()
            End If
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")
        Finally
            Cursor = Cursors.Arrow

        End Try
    End Sub

    Private Sub EdtSaticiKodu_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles txtBoxMalzemeNo.ButtonClick
        Try

            sQuery = " SELECT item [Malzeme No], description  [Malzeme Adı] FROM item"

            FindFormCagir(sQuery, "Malzeme No", "Malzeme Adı", txtBoxMalzemeNo.Text, txtBoxMalzemeTanim.Text)

        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")

        End Try
    End Sub

    Private Sub frmSatinAlmaGirisEtiketi_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("tr-TR")
            'Bolgesel Ayarlar Opsiyonu
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.PercentDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.PercentGroupSeparator = ","

        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")

        End Try
    End Sub

    #End Region 'Methods

End Class