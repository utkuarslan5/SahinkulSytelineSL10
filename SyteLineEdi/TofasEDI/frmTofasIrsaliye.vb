Imports System
Imports System.IO
Imports System.Text
Imports System.Xml

Public Class frmTofasIrsaliye

    #Region "Fields"

    Dim data As String = ""
    Dim dataWeb As String = ""

    ''Dim dbAs400 As New Core.DataForDB2(My.Settings.cnn)
    Dim dbCore As New Core.Data(My.Settings.ConnectionString)
    Dim Dosyano As String
    Dim Dsy As System.IO.StreamWriter
    Dim dsyok As System.IO.StreamWriter
    Dim dt As DataTable
    Dim klasöryolu As String = ""
    Dim ssorgu As String = My.Settings.ConnectionString

    #End Region 'Fields

    #Region "Methods"

    Public Sub sayacart()
        My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\Sayac.txt")

        Dim Dosya As System.IO.StreamWriter

        Dosya = My.Computer.FileSystem.OpenTextFileWriter(Application.StartupPath & "\Sayac.txt", True)

        Dosya.Write(Dosyano + 1)

        Dosya.Close()
    End Sub

    Private Sub btnAktar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAktar.Click
        Try
            Dim irsaliyetar As String
            Dim alicixml As String = ""
            Dim imalatkoduxml As String = ""
            Dim siparisnoxml As String = ""

            If grd.DataSource Is Nothing Then
                MessageBox.Show("Aktarýlacak Veri Bulunamadý")
                Exit Sub
            End If

            dt = grd.DataSource

            Dim readerklasor As XmlReader = XmlReader.Create(Application.StartupPath & "\parametre.xml")

            While Not readerklasor.EOF

                If readerklasor.IsStartElement And readerklasor.LocalName = "imalatkodu" Then
                    imalatkoduxml = readerklasor.ReadElementContentAsString
                End If

                If readerklasor.IsStartElement And readerklasor.LocalName = "alici" Then
                    alicixml = readerklasor.ReadElementContentAsString
                End If

                'If readerklasor.IsStartElement And readerklasor.LocalName = "siparisno" Then
                '    siparisnoxml = readerklasor.ReadElementContentAsString
                'End If

                If readerklasor.IsStartElement And readerklasor.LocalName = "dosyayolu" Then
                    klasöryolu = readerklasor.ReadElementContentAsString
                End If
                readerklasor.Read()
            End While

            readerklasor.Close()

            'text dosyanýn çýkýlacaðý klasör yoksa oluþturuluyor..
            If Not IO.Directory.Exists(klasöryolu) Then
                IO.Directory.CreateDirectory(klasöryolu)
            End If

            '*.ok dosyasý oluþturuluyor..

            Dim utf8WithoutBom As New System.Text.UTF8Encoding(False)

            dsyok = My.Computer.FileSystem.OpenTextFileWriter(klasöryolu & "\" & _
                              "TFS" & imalatkoduxml & Now.Date.ToString("ddMMyyyy") & _
                              Now.Hour.ToString.PadLeft(2, "0") & Now.Minute.ToString.PadLeft(2, "0") & Now.Second.ToString.PadLeft(2, "0") & ".ok", True, utf8WithoutBom)

            dsyok.Close()

            'txt dosyaya oluþturuluyor....
            Dsy = My.Computer.FileSystem.OpenTextFileWriter(klasöryolu & "\" & _
                  "TFS" & imalatkoduxml & Now.Date.ToString("ddMMyyyy") & _
                  Now.Hour.ToString.PadLeft(2, "0") & Now.Minute.ToString.PadLeft(2, "0") & Now.Second.ToString.PadLeft(2, "0") & ".txt", True, utf8WithoutBom)

            Dim chkWebSrvFrmVal As Boolean = chkWebSrvFrm.Checked

            dataWeb = ""

            'dataWeb &= """seferNo"":" & """" & CLng(dt.Rows(0)("SeferNo").ToString).ToString & """" & ","

            '18.09.2025 istek üzerine kaldýrýldý
            If chkWebSrvFrmVal Then
                If dt.Rows.Count > 0 Then
                    'dataWeb = "firmaNo:" & dt.Rows(0)("ImalatciKodu").ToString
                    'Dsy.WriteLine(dataWeb)
                    'dataWeb = "irsaliyeData:{"
                    dataWeb = "{"
                    dataWeb &= """rampaNoktasi"":" & dt.Rows(0)("contact").ToString & "," & vbNewLine
                    dataWeb &= """dosyaTarihi"":" & """" & New Date(dt.Rows(0)("DosyaOlusTar").ToString.Substring(4, 4), dt.Rows(0)("DosyaOlusTar").ToString.Substring(2, 2), dt.Rows(0)("DosyaOlusTar").ToString.Substring(0, 2)).ToString("dd.MM.yyyy") & Space(1) & dt.Rows(0)("DosyaOlusSaat").ToString.Substring(0, 2) & ":" & dt.Rows(0)("DosyaOlusSaat").ToString.Substring(2, 2) & """" & "," & vbNewLine
                    dataWeb &= """dosyaNo"":" & """" & dt.Rows(0)("DosyaNo").ToString & """" & "," & vbNewLine
                    dataWeb &= """seferNo"":" & """" & dt.Rows(0)("SeferNo").ToString & """" & "," & vbNewLine
                    dataWeb &= """irsaliyeTarihi"":" & """" & New Date(dt.Rows(0)("IrsaliyeTarihi").ToString.Substring(4, 4), dt.Rows(0)("IrsaliyeTarihi").ToString.Substring(2, 2), dt.Rows(0)("IrsaliyeTarihi").ToString.Substring(0, 2)).ToString("dd.MM.yyyy") & Space(1) & dt.Rows(0)("DosyaOlusSaat").ToString.Substring(0, 2) & ":" & dt.Rows(0)("DosyaOlusSaat").ToString.Substring(2, 2) & """" & "," & vbNewLine
                    dataWeb &= """irsDetayList"":" & "["
                End If
            End If
            '18.09.2025 istek üzerine kaldýrýldý


            'griddeki veriler dt ye çýkýldý dt dekiler tek tek okunup txtye aktarýlýyor
            For j As Integer = 0 To dt.Rows.Count - 1

                irsaliyetar = ""
                irsaliyetar = dt.Rows(j)(5).ToString.Substring(0, 4) & dt.Rows(j)(5).ToString.Substring(4, 2) & dt.Rows(j)(5).ToString.Substring(6, 2)
                data = ""

                'dataWeb = ""
                'burda dt.Rows(j)("1.kolon").ToString & ";" & dt.Rows(j)("2.kolon").ToString

                'þeklinde satýr satýr verier data stringine çýkýlýyor

                'CLng(dt.Rows(j)("SeferNo").ToString) & ";" & _


                data = dt.Rows(j)("DosyaNo").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("ImalatciKodu").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("Alici").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("SiparisNo").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("IrsaliyeNo").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("IrsaliyeTarihi").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("JitNo").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("DosyaOlusTar").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("DosyaOlusSaat").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("SeferNo") & ";" & vbNewLine & _
                       dt.Rows(j)("Plaka").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("item").ToString.PadLeft(12, "0") & ";" & vbNewLine & _
                       dt.Rows(j)("Seri").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("OlcuBirimi").ToString & ";" & vbNewLine & _
                       CInt(dt.Rows(j)("Miktar")).ToString & ";" & vbNewLine & _
                       dt.Rows(j)("AmbalajKodu").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("AmbalajIciMik").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("AmbalajAdeti").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("OdmNo").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("UrtTrh").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("MuhNo").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("Not1").ToString & ";" & vbNewLine & _
                       dt.Rows(j)("Not2").ToString & ";" & vbNewLine

                If Not dataWeb.EndsWith("[") Then
                    dataWeb &= "," & vbNewLine
                End If

                dataWeb &= "{"
                dataWeb &= """muhendislikNo"":" & """" & dt.Rows(j)("MuhNo").ToString & """" & "," & vbNewLine
                dataWeb &= """parcaTuru"":" & """" & dt.Rows(j)("Seri").ToString & """" & "," & vbNewLine
                dataWeb &= """olcuBirimi"":" & """" & dt.Rows(j)("OlcuBirimi").ToString & """" & "," & vbNewLine
                dataWeb &= """not1"":" & """" & dt.Rows(j)("Not1").ToString & """" & "," & vbNewLine
                dataWeb &= """ambalajAdedi"":" & dt.Rows(j)("AmbalajAdeti").ToString & "," & vbNewLine
                dataWeb &= """not2"":" & """" & dt.Rows(j)("Not2").ToString & """" & "," & vbNewLine
                dataWeb &= """resimNo"":" & """" & dt.Rows(j)("item").ToString.PadLeft(12, "0") & """" & "," & vbNewLine
                dataWeb &= """miktar"":" & CInt(dt.Rows(j)("Miktar")).ToString & "," & vbNewLine
                dataWeb &= """parcaUretimTarihi"":" & """" & dt.Rows(j)("UrtTrh").ToString & """" & "," & vbNewLine
                dataWeb &= """ambalajIcAdedi"":" & dt.Rows(j)("AmbalajIciMik").ToString & "," & vbNewLine
                dataWeb &= """odmNo"":" & """" & dt.Rows(j)("OdmNo").ToString & """" & "," & vbNewLine
                dataWeb &= """ambalajKodu"":" & """" & dt.Rows(j)("AmbalajKodu").ToString & """"
                dataWeb &= "}"

                If Not chkWebSrvFrmVal Then
                    'data stringi txt ye kaydediliyor
                    Dsy.WriteLine(data)
                End If

            Next

            If chkWebSrvFrmVal Then

                dataWeb &= "]," & vbNewLine
                dataWeb &= """irsaliyeNo"":" & """" & dt.Rows(0)("IrsaliyeNo").ToString & """" & "," & vbNewLine
                dataWeb &= """firmaNo"":" & dt.Rows(0)("ImalatciKodu").ToString & "," & vbNewLine
                dataWeb &= """aracPlaka"":" & """" & dt.Rows(0)("Plaka").ToString & """" & "," & vbNewLine
                dataWeb &= """paketKodu"":" & """""" & "," & vbNewLine
                dataWeb &= """alici"":" & """" & dt.Rows(0)("Alici").ToString & """" & "," & vbNewLine
                dataWeb &= """siparisNo"":" & """" & dt.Rows(0)("SiparisNo").ToString & """" & "}"

                'data stringi txt ye kaydediliyor
                Dsy.WriteLine(dataWeb)
            End If

            If Not chkWebSrvFrmVal Then
                Dsy.WriteLine("#" & dt.Rows.Count)
            End If

            Dsy.Flush()
            sayacart()
            Dsy.Close()
            MessageBox.Show("Veriler Aktarýldý")

        Catch ex As Exception

            'hataya düþerse txt dosyayý oluþturursa silicek.
            'My.Computer.FileSystem.DeleteFile("c:\YPSTXT\deneme.txt")
            MessageBox.Show(ex.Message)

        Finally

        End Try
    End Sub

    Private Sub btnGoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGoster.Click
        Try
            Dim imalatkodu As String = ""
            Dim alici As String = ""
            Dim sipno As String = ""
            Dosyano = dosyaoku()

            Dim reader As XmlReader = XmlReader.Create(Application.StartupPath & "\parametre.xml")

            While Not reader.EOF
                If reader.IsStartElement And reader.LocalName = "imalatkodu" Then
                    imalatkodu = reader.ReadElementContentAsString
                End If
                If reader.IsStartElement And reader.LocalName = "alici" Then
                    alici = reader.ReadElementContentAsString
                End If
                'If reader.IsStartElement And reader.LocalName = "siparisno" Then
                '    sipno = reader.ReadElementContentAsString
                'End If
                If reader.IsStartElement And reader.LocalName = "dosyayolu" Then
                    klasöryolu = reader.ReadElementContentAsString
                End If
                reader.Read()
            End While
            reader.Close()

            Dim seri As String = ""
            Select Case ComboBox1.SelectedIndex
                Case 0
                    seri = "S"
                Case 1
                    seri = "N"
                Case Else
                    seri = "O"
            End Select

            '" cast(userf5 as numeric(17,0)) as SeferNo , " & _

            ssorgu = " select '" & Dosyano.PadLeft(7, "0") & "' as DosyaNo," & _
                     " document_num as IrsaliyeNo," & _
                     "'" & imalatkodu & "' as ImalatciKodu ," & _
                     "'" & alici & "' as Alici ," & _
                     " SHIPTO as SiparisNo ," & _
                     " REPLACE(convert(nvarchar(10), GETDATE(), 105),'-','') as IrsaliyeTarihi, JitNo, " & _
                     "'" & Now.Date.ToString("ddMMyyyy") & "' as DosyaOlusTar ," & _
                     " '" & Now.Hour.ToString.PadLeft(2, "0") & Now.Minute.ToString.PadLeft(2, "0") & Now.Second.ToString.PadLeft(2, "0") & "' as  DosyaOlusSaat ," & _
                     " userf5 as SeferNo , " & _
                     " PLAKA  as Plaka, " & _
                     " '" & seri & "' as Seri , u_m  as OlcuBirimi ," & _
                     " qty as  Miktar, KKOD  as AmbalajKodu ," & _
                     " KMIK as AmbalajIciMik ," & _
                     " KSAY as AmbalajAdeti, item, OdmNo, UrtTrh, MuhNo, Not1, Not2, contact " & _
                     " from tofastxt" & _
                     " where  document_num ='" & txtIrsNo.Text & "'"

            '  " case when KMIK = 0 then 0 else cast (qty/KMIK as numeric (18 , 0) )  end  as AmbalajAdeti, item" & _

            dt = dbCore.RunSql(ssorgu)
            'dt = dbAs400.RunSql(ssorgu)
            grd.DataSource = dt

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSabit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSabit.Click
        frmxmlkaydet.ShowDialog()
    End Sub

    Function dosyaoku() As String
        Dim Fs As FileStream
        Dim Okuma As StreamReader

        Dim dosyano As String

        Try

            Fs = New FileStream(Application.StartupPath & "\Sayac.txt", FileMode.Open)
            Okuma = New StreamReader(Fs, Encoding.GetEncoding("windows-1254"))

            While Okuma.Peek <> -1
                dosyano = Okuma.ReadLine ' bu satir ilk deðer baþlýklar
                Windows.Forms.Cursor.Current = Cursors.WaitCursor
            End While
            Okuma.Close()
            Fs.Close()

            dosyaoku = dosyano

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Okuma.Close()
            Fs.Close()
        End Try
    End Function

    Private Sub frmTofasIrsaliye_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Yenile()
    End Sub

    Private Sub txtIrsNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIrsNo.Leave
        Yenile()
    End Sub

    Private Sub Yenile()
        Try

            Dim documentnum As New AutoCompleteStringCollection

            ssorgu = " Select Distinct document_num" &
                        " From tofastxt"

            dt = dbCore.RunSql(ssorgu)

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                For Each row As DataRow In dt.Rows

                    documentnum.Add(row.Item("document_num"))

                Next

            End If

            ' Set ComboBox AutoComplete properties
            txtIrsNo.AutoCompleteMode = AutoCompleteMode.Suggest
            txtIrsNo.AutoCompleteSource = AutoCompleteSource.CustomSource
            txtIrsNo.AutoCompleteCustomSource = documentnum

        Catch ex As Exception

            MessageBox.Show(ex.ToString)

        End Try
    End Sub

    #End Region 'Methods

End Class