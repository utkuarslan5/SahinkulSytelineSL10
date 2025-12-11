Public Class frmYetkilendirme

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dt As DataTable
    Dim Str As String

    #End Region 'Fields

    #Region "Methods"

    Public Sub YetkiEkle()
        If txtkullanici.Text = "" Then
            MessageBox.Show("Lütfen Kullanıcı Seçiniz")
            Exit Sub
        End If
        Dim row As Janus.Windows.GridEX.GridEXRow
        Dim yetki As String
        Dim yetkiID As String
        Dim dtallyetkiler As New DataTable

        row = grdAllYetkiler.SelectedItems(0).GetRow
        yetki = row.Cells("Yetki").Text
        yetkiID = row.Cells("YetkiID").Text

        dt = grdKullaniciYetkileri.DataSource

        dtallyetkiler = grdAllYetkiler.DataSource

        For Each rowf As DataRow In dtallyetkiler.Rows
            If rowf("YetkiID") = yetkiID Then

                dtallyetkiler.Rows.Remove(rowf)
                Exit For
            End If
        Next
        grdAllYetkiler.DataSource = dtallyetkiler
        dt.Rows.Add(New Object() {yetki, yetkiID})
        grdKullaniciYetkileri.DataSource = dt
    End Sub

    Public Sub YetkiKaldır()
        'tıklanan satırdaki verileri almak için bir row tanımlanıyor
        Dim row As Janus.Windows.GridEX.GridEXRow
        'bu row daki değerleri değişkenlere aktarmak için değişkenler tanımlanıyor
        Dim yetki As String
        Dim yetkiId As String
        'kullanıcı yetkileri bir dt ye çıkılıp seçili satır bu dtden kaldırılıp tekrra gride aktarılacak.
        Dim dtkullanici As New DataTable

        row = grdKullaniciYetkileri.SelectedItems(0).GetRow
        yetki = row.Cells("Yetki").Text
        yetkiId = row.Cells("YetkiId").Text

        dt = grdAllYetkiler.DataSource
        dtkullanici = grdKullaniciYetkileri.DataSource

        For Each rowf As DataRow In dtkullanici.Rows
            If rowf("YetkiID") = yetkiId Then
                dtkullanici.Rows.Remove(rowf)
                Exit For
            End If
        Next

        grdKullaniciYetkileri.DataSource = dtkullanici
        dt.Rows.Add(New Object() {yetki, yetkiId})
        grdAllYetkiler.DataSource = dt
    End Sub

    Private Sub btnAddYetki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddYetki.Click
        YetkiEkle()
    End Sub

    Private Sub btnKaydet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKaydet.Click
        Try
            Dim yetkiId As String
            Dim yetki As String

            If grdKullaniciYetkileri.RowCount = 0 Then
                If (MessageBox.Show("Kullanıcıya ait bütün yetkileri kaldırmak istediğinizden emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo)) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.WaitCursor
            Str = "delete  from EDIYETKI where kullaniciID='" & lblId.Text & "'"
            db.RunSql(Str, True)

            For i As Integer = 0 To grdKullaniciYetkileri.RowCount - 1

                yetki = grdKullaniciYetkileri.GetRow(i).Cells("Yetki").Value
                yetkiId = grdKullaniciYetkileri.GetRow(i).Cells("YetkiID").Value

                Str = " insert into EDIYETKI (kullaniciID,yetkiID) " & _
                      " values ('" & Trim(lblId.Text) & "','" & yetkiId & "')"
                db.RunSql(Str, True)

            Next

            MessageBox.Show("Kayıt işlemi tamamlandı")

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    Private Sub btnRemoveYetki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveYetki.Click
        YetkiKaldır()
    End Sub

    Private Sub frmYetkilendirme_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            btnAddYetki.Enabled = False
            btnRemoveYetki.Enabled = False
            btnKaydet.Enabled = False

            Str = "select Kullanici,KullaniciID from EDIUSER order by KullaniciID"
            dt = db.RunSql(Str)
            GridEX1.DataSource = dt

            Str = " select * from EDIPROGRAM "
            Dim dtyetkiler As New DataTable

            dtyetkiler = db.RunSql(Str)
            grdAllYetkiler.DataSource = dtyetkiler

            If dtyetkiler.Rows.Count = 0 Then
                MessageBox.Show("Hiç Bir Yetki Bulunamadı...")
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub grdAllYetkiler_RowDoubleClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdAllYetkiler.RowDoubleClick
        YetkiEkle()
    End Sub

    Private Sub grdKullaniciYetkileri_RowDoubleClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdKullaniciYetkileri.RowDoubleClick
        YetkiKaldır()
    End Sub

    Private Sub GridEX1_RowDoubleClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles GridEX1.RowDoubleClick
        Try
            txtkullanici.Text = Trim(e.Row.Cells(0).Text)
            lblId.Text = Trim(e.Row.Cells(1).Text)

            lblyetki.Text = "Diğer Yetkiler"
            btnAddYetki.Enabled = True
            btnRemoveYetki.Enabled = True
            btnKaydet.Enabled = True

            Str = ""
            'sahip olmadığı yetkiler dolduruluyor...
            Str = " select Yetki,YetkiID from EDIPROGRAM " & _
                  " where YetkiID not in " & _
                  " (select YetkiID from EDIYETKI where KullaniciID='" & lblId.Text & "')"

            Dim dtyetkiler As New DataTable
            dtyetkiler = db.RunSql(Str)

            If Not dtyetkiler Is Nothing Then
                grdAllYetkiler.DataSource = Nothing
                grdAllYetkiler.DataSource = dtyetkiler
            End If

            'sahip olduğu yetkiler dolduruluyor
            Str = " select Yetki,YetkiID from EDIPROGRAM " & _
                  " where YetkiID  in " & _
                  " (select YetkiID from EDIYETKI where KullaniciID='" & lblId.Text & "')"

            Dim dt_kullanici_yetkileri As New DataTable
            dt_kullanici_yetkileri = db.RunSql(Str)

            If Not dt_kullanici_yetkileri Is Nothing Then
                grdKullaniciYetkileri.DataSource = Nothing
                grdKullaniciYetkileri.DataSource = dt_kullanici_yetkileri
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    #End Region 'Methods

End Class