Public Class frmKonsinyeBilgileri

    Public dtKonsinye As DataTable
    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dt As New DataTable
    Dim dtTmp As New DataTable
    Dim sQuery As String = ""
    Dim HataVar As Boolean = False
    Dim MaxTransNum As Integer


    Private Sub frmKonsinyeBilgileri_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        btnIsle.Enabled = True
        GridEX1.DataSource = dtKonsinye
    End Sub

    Private Sub btnIsle_Click(sender As System.Object, e As System.EventArgs) Handles btnIsle.Click
        Try
            Dim param As New ArrayList
            Dim paramout As New ArrayList

            For Each row As Janus.Windows.GridEX.GridEXRow In GridEX1.GetRows

                If row.Cells("KonsinyeDurum").Text = "1" Then
                    Try
                        Dim trans_type As String = 3
                        Dim Gun As String = Date.Now.ToString("yyyy-MM-dd HH:mm:ss.fff").ToString
                        '& " 00:00:00:000"

                        sQuery = " select isnull(max(trans_num),0)+1 from dcitem "
                        dtTmp = db.RunSql(sQuery)
                        If Not dtTmp Is Nothing AndAlso dtTmp.Rows.Count > 0 Then
                            MaxTransNum = CInt(dtTmp.Rows(0)(0).ToString)
                        End If

                        param.Clear()
                        param.Add(New SqlClient.SqlParameter("@trans_num", MaxTransNum))
                        param.Add(New SqlClient.SqlParameter("@trans_date", Gun))
                        param.Add(New SqlClient.SqlParameter("@trans_type", trans_type))
                        param.Add(New SqlClient.SqlParameter("@item", row.Cells("ITNBR").Value))
                        param.Add(New SqlClient.SqlParameter("@loc", "GEFCO"))
                        param.Add(New SqlClient.SqlParameter("@lot", row.Cells("SHPNO").Value))
                        param.Add(New SqlClient.SqlParameter("@count_qty", row.Cells("SHIPMIK").Value))
                        param.Add(New SqlClient.SqlParameter("@whse", "GEFC"))
                        param.Add(New SqlClient.SqlParameter("@siteref", "Default"))
                        param.Add(New SqlClient.SqlParameter("@emp_num", KullaniciId))
                        param.Add(New SqlClient.SqlParameter("@document_num", row.Cells("SHPNO").Value))

                        'Dim p10 As New SqlClient.SqlParameter("@rowsAffected", SqlDbType.Int)
                        'p10.Direction = ParameterDirection.Output
                        'param.Add(p10)

                        Dim p5 As New SqlClient.SqlParameter("@pResult", SqlDbType.NVarChar, 500)
                        p5.Direction = ParameterDirection.Output
                        param.Add(p5)

                        row.BeginEdit()

                        If db.RunSp("[TRM_Dcitem_Insert_Post_DocNum]", param, 1, False) = True Then

                            HataVar = True

                        Else

                            If Not IsDBNull(p5.Value) Then
                                HataVar = True

                                row.Cells("KonsinyeDurum").Value = "1"
                                row.Cells("KonsinyeDurum").Text = "1"

                                row.Cells("Sonuc").Value = p5.Value
                                row.Cells("Sonuc").Text = p5.Value


                                sQuery = "UPDATE SHPPACK SET KonsinyeDurum = 1 where SHPNO = " & row.Cells("SHPNO").Text & " AND ITNBR = " & sTirnakEkle(row.Cells("ITNBR").Text)
                                db.RunSql(sQuery)


                            Else
                                HataVar = False

                                row.Cells("KonsinyeDurum").Value = "2"
                                row.Cells("KonsinyeDurum").Text = "2"

                                row.Cells("Sonuc").Value = "İşlem Tamamlandı."
                                row.Cells("Sonuc").Text = "İşlem Tamamlandı."

                                sQuery = "UPDATE SHPPACK SET KonsinyeDurum = 2 where SHPNO = " & row.Cells("SHPNO").Text & " AND ITNBR = " & sTirnakEkle(row.Cells("ITNBR").Text)
                                db.RunSql(sQuery)

                            End If

                            sQuery = "DELETE FROM dcitem where trans_num = " & MaxTransNum
                            db.RunSql(sQuery)

                        End If


                        row.EndEdit()

                    Catch ex As Exception

                        row.BeginEdit()

                        If ex.Message.ToString.Contains("PRIMARY KEY") Then

                            row.Cells("Kayit").Value = "Atılmak istenen kayıt sistemde zaten mevcut"

                        Else

                            row.Cells("KonsinyeDurum").Value = "1"
                            row.Cells("KonsinyeDurum").Text = "1"

                            row.Cells("Sonuc").Value = ex.Message.ToString
                            row.Cells("Sonuc").Text = ex.Message.ToString

                            sQuery = "UPDATE SHPPACK SET KonsinyeDurum = 1 where SHPNO = " & row.Cells("SHPNO").Text & " AND ITNBR = " & sTirnakEkle(row.Cells("ITNBR").Text)
                            db.RunSql(sQuery)

                            sQuery = "DELETE FROM dcitem where trans_num = " & MaxTransNum
                            db.RunSql(sQuery)
                            'row.Cells("Kayit").Value = ex.Message.ToString

                        End If

                        row.EndEdit()
                    End Try

                End If


            Next

            btnIsle.Enabled = False

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnKapat_Click(sender As System.Object, e As System.EventArgs) Handles btnKapat.Click
        Me.Close()
    End Sub
End Class