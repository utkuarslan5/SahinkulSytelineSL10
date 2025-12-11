Public Class frmProgramEkle

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dt As DataTable
    Dim Str As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnKaydet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKaydet.Click
        Try

            If txtkullanici.Text = "" Then
                MessageBox.Show("Program Adını Boş Bırakmayınız")
            End If

            Str = ""
            Str = " select max(YetkiID) as YetkiID from EDIPROGRAM "
            Dim dtmaxid As New DataTable

            dtmaxid = db.RunSql(Str)

            Dim maxid As Integer

            If Not dtmaxid Is Nothing AndAlso dtmaxid.Rows.Count > 0 Then
                maxid = IIf(IsDBNull(dtmaxid.Rows(0)("YetkiID")), 0, dtmaxid.Rows(0)("YetkiID")) + 1
            Else
                Exit Sub
            End If

            Str = ""
            'yeni kullanıcı insert oldu
            Str = " INSERT INTO EDIPROGRAM (Yetki,YetkiID)" & _
                  " VALUES ('" & txtkullanici.Text & "'," & maxid & ")"
            db.RunSql(Str, True)

            If chkyetki.Checked = True Then

                Str = ""
                Str = " select KullaniciId from EDIUSER"
                Dim dtkullaniciid As New DataTable
                dtkullaniciid = db.RunSql(Str)

                Str = ""
                Str = "select YetkiId from EDIPROGRAM " & _
                      " where Yetki='" & txtkullanici.Text & "'"
                Dim dtyetkiid As New DataTable
                dtyetkiid = db.RunSql(Str)

                For i As Integer = 0 To dtkullaniciid.Rows.Count - 1
                    Str = ""
                    Str = " insert into EDIYETKI (YetkiID,KullaniciID) " & _
                          " values ('" & dtyetkiid.Rows(0)("YetkiId") & "','" & dtkullaniciid.Rows(i)("KullaniciId") & "')"
                    db.RunSql(Str, True)

                Next

            End If

            MessageBox.Show(" Kayıt İşlemi Tamamlandı", "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information)
            chkyetki.Checked = False
            txtkullanici.Text = ""

            'ekran tekrar dolduruluyor

            frmKullanicilar_Load(sender, e)

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub frmKullanicilar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            GridEX1.DataSource = Nothing

            Str = " select Yetki from EDIPROGRAM "

            dt = db.RunSql(Str)

            GridEX1.DataSource = dt

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub GridEX1_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.ColumnButtonClick
        Dim kullanici_adi As String = ""

        Try

            kullanici_adi = CType(sender, Janus.Windows.GridEX.GridEX).SelectedItems.Item(0).GetRow.Cells("Yetki").Text

            If MessageBox.Show("Programı  Silmek İstediğinizden Emin misiniz ? ", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                'daha sonra o kullanıcıya ait yetkiler silinicek
                Str = ""
                Str = " select YetkiID from EDIPROGRAM  where Yetki ='" & Trim(kullanici_adi) & "'"
                Dim dtyetkiid As New DataTable
                dtyetkiid = db.RunSql(Str)

                Str = ""
                Str = " delete  from EDIYETKI " & _
                      " where YetkiId='" & dtyetkiid.Rows(0)("YetkiId") & "'"

                db.RunSql(Str, True)
                Str = ""
                Str = " delete  from EDIPROGRAM where Yetki='" & Trim(kullanici_adi) & "'"
                db.RunSql(Str, True)

                frmKullanicilar_Load(sender, e)

            End If

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    #End Region 'Methods

End Class