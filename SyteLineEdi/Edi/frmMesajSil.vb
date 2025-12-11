Public Class frmMesajSil

    #Region "Methods"

    Private Sub btnIptal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIptal.Click
        Me.Close()
    End Sub

    Private Sub btnTamam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTamam.Click
        Dim sQuery, sQuery1 As String

        Dim db As New Core.Data(My.Settings.ConnectionString.ToString)

        Dim dsEdiMst As DataSet

        Try

            If chkTumu.Checked Then

                sQuery = " DELETE FROM EDIMST Where CRDUSR=" & sTirnakEkle(KullaniciAdi)

                db.RunSql(sQuery, True)

                sQuery = " DELETE FROM EDIWRK Where CRDUSR=" & sTirnakEkle(KullaniciAdi)

                db.RunSql(sQuery, True)

                sQuery = " DELETE FROM EDIPRC Where CRDUSR=" & sTirnakEkle(KullaniciAdi)

                db.RunSql(sQuery, True)

            ElseIf chkUyari1.Checked Or chkUyari2.Checked Or chkError.Checked Then

                sQuery = " SELECT DISTINCT SENDER,MSGNUM,PLANTID,GATEID,CUSITM,DELDTE " & _
                            " FROM EDIMST " & _
                                " WHERE SENDER<>''  And CRDUSR=" & sTirnakEkle(KullaniciAdi)

                sQuery1 = ""

                If chkUyari1.Checked Then

                    sQuery1 = " AND ( HATADRM='1'"

                End If

                If (chkUyari2.Checked) And (sQuery1 = "") Then

                    sQuery1 = sQuery1 & " AND ( HATADRM='2'"

                ElseIf chkUyari2.Checked Then

                    sQuery1 = sQuery1 & " OR  HATADRM='2'"

                End If

                If (chkError.Checked) And (sQuery1 = "") Then

                    sQuery1 = sQuery1 & " AND ( HATADRM='3' "

                ElseIf chkError.Checked Then

                    sQuery1 = sQuery1 & " OR  HATADRM='3' "

                End If

                sQuery = sQuery & sQuery1 & " ) "

                dsEdiMst = db.RunSql(sQuery, "EdiMst")

                If Not dsEdiMst Is Nothing Then

                    If dsEdiMst.Tables("EdiMst").Rows.Count > 0 Then

                        For i As Integer = 0 To dsEdiMst.Tables("EdiMst").Rows.Count - 1

                            With dsEdiMst.Tables("EdiMst").Rows(i)

                                sQuery = " DELETE " & _
                                             " FROM EDIWRK " & _
                                                 " WHERE CRDUSR=" & sTirnakEkle(KullaniciAdi) & _
                                                    " And SENDER='" & RTrim(RTrim(.Item("SENDER").ToString)) & "'" & _
                                                    " AND MSGNUM='" & RTrim(.Item("MSGNUM").ToString) & "' AND PLANTID='" & RTrim(.Item("PLANTID").ToString) & "'" & _
                                                    " AND GATEID='" & RTrim(.Item("GATEID").ToString) & "'" & _
                                                    " AND CUSITM='" & RTrim(.Item("CUSITM").ToString) & "' AND DELDTE='" & CStr(RTrim(.Item("DELDTE").ToString)) & "'"

                                db.RunSql(sQuery, True)

                            End With

                        Next i

                    End If

                End If

                If chkUyari1.Checked Then

                    sQuery = " DELETE " & _
                                "FROM EDIMST" & _
                                    " WHERE CRDUSR=" & sTirnakEkle(KullaniciAdi) & _
                                                    " And HATADRM='1'"

                    db.RunSql(sQuery, True)

                End If

                If chkUyari2.Checked Then

                    sQuery = " DELETE " & _
                                " FROM EDIMST " & _
                                    " WHERE CRDUSR=" & sTirnakEkle(KullaniciAdi) & _
                                                    " And HATADRM='2'"

                    db.RunSql(sQuery, True)

                End If

                If chkError.Checked Then

                    sQuery = " DELETE" & _
                                " FROM EDIMST" & _
                                    " WHERE CRDUSR=" & sTirnakEkle(KullaniciAdi) & _
                                                    " And HATADRM='3' "

                    db.RunSql(sQuery, True)

                End If

            End If

            Me.Close()

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    #End Region 'Methods

End Class