Imports System.Collections.ObjectModel
Imports System.Data.SqlClient

Public Class frmKullaniciGirisi

    #Region "Fields"

    '
    Dim dbMaster As New Core.Data("")
    Dim dt As DataTable
    Dim sCon As String
    Dim Str As String

    #End Region 'Fields

    #Region "Methods"

    Public Sub Database()
        Try

          sDataSource = My.Settings.ServerTurkiye

          sInitialCatalog = My.Settings.Database
          sUId = My.Settings.UserName
          sPassword = My.Settings.Password

          sCon = "data source=" & sDataSource & ";" & _
              "initial catalog=" & sInitialCatalog & ";" & _
              "uid=" & sUId & ";" & _
              "pwd=" & sPassword & ";" & _
              "connection timeout=0" & ";" & _
              "Application Name=SytelineEdi" & My.Computer.Name

          dbMaster = New Core.Data(sCon)

          Dim ssorgu As String

            ssorgu = " select name as Ortam " & _
                        " from sysdatabases " & _
                        " where name in ( 'SL10P' ,'SL10T') " & _
                        " order by name"

          dt = dbMaster.RunSql(ssorgu)

          cmbOrtam.DataSource = Nothing

          cmbOrtam.DataSource = dt

        Catch ex As Exception

          cmbOrtam.DataSource = Nothing

        End Try
    End Sub

    '
    ''' <summary>
    ''' LOGIN GÝRÝÞ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnGiris_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGiris.Click
        'Dim dizi As New ArrayList

        'dizi.Add("TrnNo" & "|" & "16")
        'dizi.Add("Sicil_No" & "|" & "1")
        'dizi.Add("IsEmri" & "|" & "M000000003")
        'dizi.Add("Op_No" & "|" & "10")
        'dizi.Add("Start_Time" & "|" & "17:20:51")
        'dizi.Add("Finish_Time" & "|" & "17:40:51")
        'dizi.Add("Baslangic_Tarihi" & "|" & Now.ToString("yyyy-MM-dd HH:mm:ss"))
        'dizi.Add("Bitis_Tarihi" & "|" & Now.ToString("yyyy-MM-dd HH:mm:ss"))

        'My.Settings.ConnectionString = "Data Source=dataserver;Initial Catalog=CMak_App;User ID=sa;pwd=;Connect Timeout=0"

        ''Insert(dizi, "TR_Puantaj")

        'Dim FieldAndValue As Collection(Of FieldAndValue) = New Collection(Of FieldAndValue)

        'FieldAndValue.Clear()

        'FieldAndValue.Add(New FieldAndValue("TrnNo", "16"))
        'FieldAndValue.Add(New FieldAndValue("Sicil_No", "1"))
        'FieldAndValue.Add(New FieldAndValue("IsEmri", "M000000003"))
        'FieldAndValue.Add(New FieldAndValue("Op_No", 10))
        'FieldAndValue.Add(New FieldAndValue("Start_Time", "17:20:51"))
        'FieldAndValue.Add(New FieldAndValue("Finish_Time", "17:40:51"))
        'FieldAndValue.Add(New FieldAndValue("Baslangic_Tarihi", Now.ToString("yyyy-MM-dd HH:mm:ss")))
        'FieldAndValue.Add(New FieldAndValue("Bitis_Tarihi", Now.ToString("yyyy-MM-dd HH:mm:ss")))

        'Insert(FieldAndValue, "TR_Puantaj")

        Login()
    End Sub

    '
    ''' <summary>
    ''' LOGIN ENTER
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnGiris_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnGiris.KeyDown
        If e.KeyCode = Keys.Enter Then
          Login()
        End If
    End Sub

    '
    ''' <summary>
    ''' CLEAR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub clear()
        txtKullaniciAdi.Text = ""
        txtSifre.Text = ""
        txtKullaniciAdi.Focus()
    End Sub

    '
    Private Sub frmKullaniciGirisi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

          Database()

        Catch ex As Exception

          MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    '
    ''' <summary>
    ''' LOGIN
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Login()
        Try

          Dim ds As New DataSet

          '--------------------------------------
          'KONTROLLER
          '--------------------------------------

          If txtKullaniciAdi.Text = "" Then
        Throw New Exception("Kullanýcý Adý Giriniz !")
          End If

          If cmbOrtam.Text = "" Then
        Throw New Exception("Lütfen Ortam Seçiniz !")
          End If

            'sInitialCatalog = cmbOrtam.Text & "_App"

            sCon = "data source=" & sDataSource & ";" & _
                "initial catalog=" & cmbOrtam.Text & ";" & _
                "uid=" & sUId & ";" & _
                "pwd=" & sPassword & ";" & _
                "connection timeout=0" & ";" & _
                "Application Name=" & My.Computer.Name

          dbMaster = New Core.Data(sCon)

          My.Settings.ConnectionString = sCon

          Str = ""
          Str = " select * " & _
              " from EDIUSER " & _
              " where Kullanici='" & txtKullaniciAdi.Text & "'" & _
                  " and Sifre='" & txtSifre.Text & "'"

          Dim dt As New DataTable

          dt = dbMaster.RunSql(Str)

          If dt.Rows.Count = 0 Then

        Throw New Exception("Hatalý Giriþ !!!")

          Else

        KullaniciId = dt.Rows(0)("KullaniciID").ToString.Trim

        KullaniciAdi = dt.Rows(0)("Kullanici").ToString.Trim

        VarsayilanAmbar = dt.Rows(0)("Ambar").ToString.Trim

          End If

          Me.Hide()

            frmAnaMenu.Text = frmAnaMenu.Text & "-" & cmbOrtam.Text

          frmAnaMenu.Show()
          'frmUretimBildirimi.Show()

          ds.Dispose()

        Catch ex As Exception
          toolMsj.ForeColor = Color.Red
          toolMsj.Text = ex.Message.ToString
          clear()
        End Try
    End Sub

    '
    ''' <summary>
    ''' SÝFRE ENTER
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtSifre_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSifre.KeyDown
        If e.KeyCode = Keys.Enter Then
            Login()
        End If
    End Sub

    #End Region 'Methods

    #Region "Other"

    '
    '

    #End Region 'Other

End Class