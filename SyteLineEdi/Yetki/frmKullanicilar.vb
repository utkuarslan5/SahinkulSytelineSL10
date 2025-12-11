Public Class frmKullanicilar

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dt As DataTable
    Dim Str As String
    Dim tmpKullaniciId As Integer

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnKaydet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKaydet.Click
        Str = " select KullaniciId,kullanici,sifre " & _
                    " from EDIUSER " & _
                    " where KullaniciId=" & tmpKullaniciId

        Dim dtkaydet_kontrol As New DataTable

        dtkaydet_kontrol = db.RunSql(Str)

        If Not dtkaydet_kontrol Is Nothing AndAlso dtkaydet_kontrol.Rows.Count <> 0 Then
            'MessageBox.Show("Bu İsimde Bir Kullanıcı Daha Önceden Girilmiş", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'txtkullanici.Focus()

            Str = " Update EDIUSER" & _
                        " Set Ambar=" & sTirnakEkle(txtAmbar.Text) & _
                        " ,Kullanici=" & sTirnakEkle(txtkullanici.Text) & _
                        " ,Sifre=" & sTirnakEkle(txtsifre.Text) & _
                    " Where KullaniciId=" & tmpKullaniciId

            db.RunSql(Str)

        Else

            'yeni kullanıcı insert oldu
            Str = " INSERT INTO EDIUSER (KULLANICI,SIFRE,AMBAR)" & _
                  " VALUES ('" & txtkullanici.Text & "','" & txtsifre.Text & "','" & txtAmbar.Text & "')"

            db.RunSql(Str, True)

        End If

        ''''''''ilk kullanıcı
        'If skullanici.ilkkullanici = True Then
        '    Str = " INSERT INTO Kullanici_Yetkileri (KullaniciID,YetkiID)" & _
        '                 " VALUES ('1','97')"

        'End If

        MessageBox.Show(" Kayıt İşlemi Tamamlandı", "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information)

        'ekran tekrar dolduruluyor
        frmKullanicilar_Load(sender, e)
    End Sub

    Private Sub btnYeni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYeni.Click
        txtAmbar.Text = ""
        txtkullanici.Text = ""
        txtsifre.Text = ""
        tmpKullaniciId = 0
        txtkullanici.Focus()
    End Sub

    Private Sub frmKullanicilar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            GridEX1.DataSource = Nothing

            Str = " select KullaniciId, Kullanici,Sifre,Ambar " & _
                            " from EDIUSER "

            dt = db.RunSql(Str)

            GridEX1.DataSource = dt

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub GridEX1_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.ColumnButtonClick
        Dim KullaniciId As Integer

        Dim grid As Janus.Windows.GridEX.GridEX = CType(sender, Janus.Windows.GridEX.GridEX)

        If e.Column.ButtonText = "Sil" Then

            KullaniciId = grid.SelectedItems.Item(0).GetRow.Cells("KullaniciId").Text

            If MessageBox.Show("Kullanıcıyı Silmek İstediğinizden Emin misiniz ? " & _
                               vbNewLine & vbNewLine & "Bu kullanıcıya ait bütün yetkiler kaldırılacak..", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Str = ""
                Str = " delete  from EDIUSER where KullaniciId=" & KullaniciId
                db.RunSql(Str, True)

                'daha sonra o kullanıcıya ait yetkiler silinicek
                Str = ""
                Str = " delete from EDIYETKI where KullaniciId=" & KullaniciId
                db.RunSql(Str, True)

                frmKullanicilar_Load(sender, e)

                Exit Sub

            End If

        ElseIf e.Column.ButtonText = "Değiştir" Then

            txtAmbar.Text = grid.SelectedItems.Item(0).GetRow.Cells("Ambar").Text

            txtkullanici.Text = grid.SelectedItems.Item(0).GetRow.Cells("Kullanici").Text

            txtsifre.Text = grid.SelectedItems.Item(0).GetRow.Cells("Sifre").Text

            tmpKullaniciId = grid.SelectedItems.Item(0).GetRow.Cells("KullaniciId").Text

        End If
    End Sub

    #End Region 'Methods

End Class