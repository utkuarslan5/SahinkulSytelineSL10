Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml

Public Class frmSayimHareketleri

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dt As DataTable
    Dim Str As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnHurdaOlustur_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles btnHurdaOlustur.Click
        Try


            If txtPath.Text = String.Empty Then
                MsgBox("Lütfen görüntülemek istediğiniz dosyayı seçiniz !", MsgBoxStyle.Exclamation, "Hata")
                Exit Sub
            End If

            If grd.RowCount = 0 Then
                MsgBox("Oluşturulacak veri bulunamadı !", MsgBoxStyle.Exclamation, "Hata")
                Exit Sub
            End If

            If Not chkCesGir.Checked AndAlso _
           Not chkCesCik.Checked Then
                MsgBox("Lütfen " & chkCesGir.Text & " veya" & vbNewLine & chkCesCik.Text & vbNewLine & "seçeneklerinden birini seçiniz !", MsgBoxStyle.Exclamation, "Hata")
                Exit Sub
                'Else
                '    If chkCesGir.Checked Then
                '        IsemriOlustur()
                '    ElseIf chkCesCik.Checked Then
                '        AltIsemriIleOlustur()
                '    End If
            End If

            Me.Cursor = Cursors.WaitCursor

            btnHurdaOlustur.Enabled = False

            Dim param As New ArrayList
            Dim paramout As New ArrayList

            Dim HataVar As Boolean = False

            For Each row As Janus.Windows.GridEX.GridEXRow In grd.RootTable.GridEX.GetRows

                Try

                    If row.Cells("Malzeme").Value.ToString.Length > 30 Then
                        Throw New Exception("Malzeme karakter uzunluğu fazla !")
                    ElseIf row.Cells("Lokasyon").Value.ToString.Length > 15 Then
                        Throw New Exception("Lokasyon karakter uzunluğu fazla !")
                    ElseIf row.Cells("Lot").Value.ToString.Length > 15 Then
                        Throw New Exception("Lot karakter uzunluğu fazla !")
                    End If


                    If chkCesCik.Checked AndAlso row.Cells("NedenKodu").Value <> "" Then
                        Str = "select * from reason_mst where  reason_class='MISC ISSUE' and reason_code= '" & row.Cells("NedenKodu").Value & "'"
                        dt = db.RunSql(Str)

                        If dt.Rows.Count = 0 Then
                            MessageBox.Show(row.Cells("NedenKodu").Value & " nolu neden kodu tanımsız")
                            Exit Sub
                        End If

                        Str = "select * from lot_loc where lot = '" & row.Cells("Lot").Value & _
                            "' and item = '" & row.Cells("Malzeme").Value & "' and loc='" & row.Cells("Lokasyon").Value & "'"
                        dt = db.RunSql(Str)

                        If dt.Rows.Count = 0 Then
                            MessageBox.Show("Malzeme yer stok bilgisi geçersiz.")
                            Exit Sub
                        End If
                    End If
                   

                    Dim trans_type As String = IIf(chkCesGir.Checked = True, 3, 2)
                    Dim Gun As String = Date.Now.ToString("yyyy-MM-dd").ToString & " 00:00:00:000"

                    Dim sStr As String
                    Dim MaxTransNum As Integer
                    Dim dtTmp As New DataTable
                    sStr = " select isnull(max(trans_num),0)+1 from dcitem "
                    dtTmp = db.RunSql(sStr)
                    If Not dtTmp Is Nothing AndAlso dtTmp.Rows.Count > 0 Then
                        MaxTransNum = CInt(dtTmp.Rows(0)(0).ToString)
                    End If

                    param.Clear()
                    param.Add(New SqlClient.SqlParameter("@trans_num", MaxTransNum))
                    param.Add(New SqlClient.SqlParameter("@trans_date", Gun))
                    param.Add(New SqlClient.SqlParameter("@trans_type", trans_type))
                    param.Add(New SqlClient.SqlParameter("@item", row.Cells("Malzeme").Value))
                    param.Add(New SqlClient.SqlParameter("@loc", row.Cells("Lokasyon").Value))
                    param.Add(New SqlClient.SqlParameter("@lot", row.Cells("Lot").Value))
                    param.Add(New SqlClient.SqlParameter("@count_qty ", row.Cells("Miktar").Value))
                    param.Add(New SqlClient.SqlParameter("@whse", row.Cells("Ambar").Value))
                    param.Add(New SqlClient.SqlParameter("@docnum", row.Cells("Dokuman").Value))
                    param.Add(New SqlClient.SqlParameter("@NedenKodu", row.Cells("NedenKodu").Value))

                    'Dim p5 As New SqlClient.SqlParameter("@po_num", SqlDbType.NVarChar, 10)
                    'p5.Direction = ParameterDirection.Output
                    'param.Add(p5)

                    row.BeginEdit()

                    If db.RunSp("[TUR_Dcitem_Insert_Kanban]", param, 1, False) = True Then

                        HataVar = True

                        'If db.Result.Items(0).ToString.Contains("PRIMARY KEY") Then

                        '    row.Cells("Kayit").Value = "Atılmak istenen kayıt sistemde zaten mevcut"

                        'Else

                        '    row.Cells("Kayit").Value = db.Result.Items(0).ToString()

                        'End If

                    Else

                        'row.Cells("SiparisNo").Value = p5.Value

                        row.Cells("Kayit").Value = "İşlem Tamamlandı"

                        'row.Cells("Durum").Value = "Sipariş Güncelleme"

                    End If

                    row.EndEdit()

                Catch ex As Exception

                    row.BeginEdit()

                    If ex.Message.ToString.Contains("PRIMARY KEY") Then

                        row.Cells("Kayit").Value = "Atılmak istenen kayıt sistemde zaten mevcut"

                    Else

                        row.Cells("Kayit").Value = ex.Message.ToString

                    End If

                    row.EndEdit()

                End Try

            Next row

            btnHurdaOlustur.Enabled = True

            If Not HataVar Then

                'MessageBox.Show("İşlem Tamamlandı", "Ekip Mapics", MessageBoxButtons.OK)

            Else

                MessageBox.Show("Bazı Verisel Hatalar Oluştu !" & vbNewLine & "Seçtiğiniz excel dökümanındaki ilgili satırları" & vbNewLine & "lütfen kontrol edin !", "Ekip Mapics", MessageBoxButtons.OK)

            End If

        Catch ex As Exception
            btnHurdaOlustur.Enabled = True
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")

        Finally

            Me.Cursor = Cursors.Arrow

            Duzenle(grd)

        End Try
    End Sub

    Private Sub btnSorgula_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles btnSorgula.Click
        Try

          If txtPath.Text = String.Empty Then
        MsgBox("Lütfen görüntülemek istediğiniz dosyayı seçiniz !", MsgBoxStyle.Exclamation, "Hata")
        Exit Sub
          End If

          Me.Cursor = Cursors.WaitCursor

          chkCesGir.Checked = False
          chkCesCik.Checked = False

          grd.DataSource = DataTableHelper.ExcelImportGeneral(txtPath.Text, txtPath.Text, chkCesGir.Checked)

          Duzenle(grd)

        Catch ex As Exception

          MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")

        Finally

          Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    Private Sub chkCesCik_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles chkCesCik.CheckedChanged
        If chkCesCik.Checked Then
          chkCesGir.Checked = False
          'ChkIsemri.Enabled = False
        Else
          'chkCesCik.Checked = False
          'chkCesCik.Enabled = False
          'chkCesGir.Enabled = True
        End If
    End Sub

    Private Sub chkCesGir_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles chkCesGir.CheckedChanged
        If chkCesGir.Checked Then
          chkCesCik.Checked = False
          'chkCesCik.Enabled = False
        Else
          'chkCesGir.Checked = False
          'chkCesGir.Enabled = False
          'chkCesCik.Enabled = True
        End If
    End Sub

    Private Sub SetEnabled(ByVal Bool As Boolean)
        If Bool Then
          chkCesGir.Enabled = True
          chkCesCik.Enabled = True
          chkCesGir.Checked = False
          chkCesCik.Checked = False
        Else
          chkCesGir.Enabled = False
          chkCesCik.Enabled = False
        End If
    End Sub

    Private Sub txtPath_ButtonClick(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles txtPath.ButtonClick
        Dim openDir As OpenFileDialog = New OpenFileDialog
        '
        openDir.Title = " Görüntülemek İstediğiniz Dosyayı Seçiniz! "
        '
        'DialogResult = openDir.ShowDialog(Me)
        '
        openDir.InitialDirectory = txtPath.Text

        openDir.Filter = "Excel Dosyası (*.xls)|*.xls"

        If openDir.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
          '
          If IO.File.Exists(openDir.FileName) = True Then
        '
        txtPath.Text = System.IO.Path.GetFullPath(openDir.FileName)
        '
          Else
        '
        MessageBox.Show("Böyle bir dosya bulunamadı !", " Error", _
        MessageBoxButtons.OK, MessageBoxIcon.Error)
        '
          End If
          '
        End If
        ''
    End Sub

    #End Region 'Methods

End Class