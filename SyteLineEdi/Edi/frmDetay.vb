Imports Janus.Windows.GridEX

Public Class frmDetay

    Public dt As DataTable
    Private sQuery As String
    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim flag As Boolean = False
    Public iptal As Boolean = False
    Public picklist As String
    Dim sSQL As String

    Private Sub frmDetay_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        GridEX1.DataSource = dt
        iptal = False
    End Sub

    Private Sub btnTamam_Click(sender As System.Object, e As System.EventArgs) Handles btnTamam.Click
        Try
            Cursor = Cursors.WaitCursor

            'Dim dtKontrol As New DataTable
            'dtKontrol = GridEX1.DataSource
          

            For Each dr As GridEXRow In GridEX1.GetRows

                If dr.Cells("PKOD").Text = "" And dr.Cells("PSAY").Text <> 0 Then
                    MessageBox.Show(dr.Cells("PICKNO").Text & " nolu çekmeye ait " & dr.Cells("ITNBR").Text & " malzemesinin palet kodu tanımsız." & vbNewLine & _
                                    "Palet Sayısı Giremezsiniz.")
                    'dr.Cells("PSAY").Text = 0
                    Exit Sub
                End If

                If dr.Cells("KPKOD").Text = "" And dr.Cells("KPMIK").Text <> 0 Then
                    MessageBox.Show(dr.Cells("PICKNO").Text & " nolu çekmeye ait " & dr.Cells("ITNBR").Text & " malzemesinin kapak kodu tanımsız." & vbNewLine & _
                                    "Kapak Miktarını Giremezsiniz.")
                    'dr.Cells("KPMIK").Text = 0
                    Exit Sub
                End If

                sQuery = "UPDATE SHPPACK SET KSAY = " & dr.Cells("KSAY").Text & ", PSAY = " & dr.Cells("PSAY").Text & ",KPMIK = " & dr.Cells("KPMIK").Text & _
                         " WHERE PICKNO = " & dr.Cells("PICKNO").Text & " AND ITNBR = " & sTirnakEkle(dr.Cells("ITNBR").Text)
                db.RunSql(sQuery)
            Next

            sSQL = "SELECT  1 Sira,cust_box_code as AmbalajTanimi,SUM(KSAY) AmbalajMiktari FROM  SHPPACK s" & _
                   " LEFT JOIN TR_itempack_edi p on p.item = s.ITNBR and p.pack_type = AMBKOD" & _
                   " WHERE   PICKNO IN (" & picklist & ") AND ISNULL(cust_box_code, '') <> '' GROUP BY cust_box_code HAVING  SUM(KSAY) > 0" & _
                   " UNION" & _
                   " SELECT  2,cust_palette_code,SUM(PSAY) FROM  SHPPACK s " & _
                   " LEFT JOIN TR_itempack_edi p on p.item = s.ITNBR and p.pack_type = AMBKOD " & _
                   " WHERE   PICKNO IN (" & picklist & ") AND ISNULL(cust_palette_code, '') <> '' GROUP BY cust_palette_code HAVING  SUM(PSAY) > 0" & _
                   " UNION" & _
                   " SELECT  3,cust_cover_code,SUM(KPMIK) FROM SHPPACK s" & _
                   " LEFT JOIN TR_itempack_edi p on p.item = s.ITNBR and p.pack_type = AMBKOD" & _
                   " WHERE   PICKNO IN (" & picklist & ") AND ISNULL(cust_cover_code, '') <> '' GROUP BY cust_cover_code HAVING  SUM(KPMIK) > 0"
            dt = db.RunSql(sSQL)

            Dim message As String
            For Each row As DataRow In dt.Rows
                message = message & " Ambalaj Tanımı : " & row("AmbalajTanimi") & " Ambalaj Miktarı : " & row("AmbalajMiktari") & vbNewLine
            Next
            message = message & "Devam etmek istiyor musunuz?"

            If MessageBox.Show(message, "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
            flag = True
        Finally
            Cursor = Cursors.Default
        End Try

        If Not flag Then
            MessageBox.Show("Palet Detayları Başarıyla Güncellendi.")
            Me.Close()
        End If

    End Sub

    Private Sub btnIptal_Click(sender As System.Object, e As System.EventArgs) Handles btnIptal.Click

        iptal = True
        Me.Close()

    End Sub

End Class