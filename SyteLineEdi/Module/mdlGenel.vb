Module mdlGenel

    #Region "Fields"

    Public sConn As String
    Public sSevkiyat As New Sevkiyat

#End Region 'Fields

#Region "Methods"

    'Public sInitialCatalog As String
    'Public sDataSource As String
    'Public sUId As String
    'Public sPassword As String
    Public Sub FindFormCagir(ByVal Sorgu As String, ByVal NumaraEtiketi As String, ByVal TanimEtiketi As String,
        ByRef Numara As String, ByRef Tanim As String,
        Optional ByRef Alan3 As String = "", Optional ByVal Alan3Etiketi As String = "",
        Optional ByRef Alan4 As String = "", Optional ByVal Alan4Etiketi As String = "",
        Optional ByRef Alan5 As String = "", Optional ByVal Alan5Etiketi As String = "",
        Optional ByRef Alan6 As String = "", Optional ByVal Alan6Etiketi As String = "",
        Optional ByRef Alan7 As String = "", Optional ByVal Alan7Etiketi As String = "",
        Optional ByRef Alan8 As String = "", Optional ByVal Alan8Etiketi As String = "",
        Optional ByVal ConnectionString As String = "",
                             Optional ByRef PurplastKodu As String = "", Optional ByVal PurplastKoduEtiketi As String = "")
        Try

            If ConnectionString = "" Then

                ConnectionString = My.Settings.ConnectionString.ToString

            End If

            Dim frmFindForm As New General.FindForm
            'frmFindForm.DefaultSite = DefaultSite
            frmFindForm.ConnectionString = ConnectionString
            frmFindForm.FindQuerySelect = Sorgu

            'frmFindForm.NumFindId = True
            frmFindForm.labelID = TanimEtiketi
            frmFindForm.labelDescription = NumaraEtiketi
            'frmFindForm.labelCriteria = PurplastKoduEtiketi
            frmFindForm.FindSelectedItem_1 = Alan3Etiketi
            frmFindForm.FindSelectedItem_2 = Alan4Etiketi
            frmFindForm.FindSelectedItem_3 = Alan5Etiketi
            frmFindForm.FindSelectedItem_4 = Alan6Etiketi
            frmFindForm.FindSelectedItem_5 = Alan7Etiketi
            frmFindForm.FindSelectedItem_6 = Alan8Etiketi

            frmFindForm.ShowDialog()
            If (frmFindForm.Onay = True) Then
                Numara = frmFindForm.FindSelectedID
                Tanim = frmFindForm.FindSelectedDescription
                'PurplastKodu = frmFindForm.FindSelectedCriteria
                Alan3 = frmFindForm.FindSelectedItem_1
                Alan4 = frmFindForm.FindSelectedItem_2
                Alan5 = frmFindForm.FindSelectedItem_3
                Alan6 = frmFindForm.FindSelectedItem_4
                Alan7 = frmFindForm.FindSelectedItem_5
                Alan8 = frmFindForm.FindSelectedItem_6
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub GridSec(ByRef Gridex As Janus.Windows.GridEX.GridEX, ByVal KolonAdi As String, ByVal sender As Object, ByVal e As System.EventArgs)
        Dim grid As Janus.Windows.GridEX.GridEX = CType(sender, Janus.Windows.GridEX.GridEX)

        Dim Inx As Integer = grid.Row

        If Not IsNothing(grid.CurrentColumn) Then

          If grid.CurrentColumn.ColumnType = Janus.Windows.GridEX.ColumnType.CheckBox Then

        For i As Integer = 0 To Gridex.RowCount - 1

          Dim row As Janus.Windows.GridEX.GridEXRow

          row = Gridex.GetRow(i)

          If row.Cells(KolonAdi).Text <> "0" Then

            If row.RowIndex <> grid.GetRow(Inx).RowIndex Then

              If row.Cells(KolonAdi).Text = Gridex.GetRow(Inx).Cells(KolonAdi).Text Then

                If Not grid.GetRow(Inx).IsChecked Then

                  row.IsChecked = True

                Else

                  row.IsChecked = False

                End If

              End If

            End If

          End If

        Next

          End If

        End If
    End Sub

    Public Sub GridSec2(ByRef Gridex As Janus.Windows.GridEX.GridEX, ByVal KolonAdi As String, ByVal KolonAdi2 As String, ByVal sender As Object, ByVal e As System.EventArgs)
        Dim grid As Janus.Windows.GridEX.GridEX = CType(sender, Janus.Windows.GridEX.GridEX)

        Dim Inx As Integer = grid.Row

        If Not IsNothing(grid.CurrentColumn) Then

            If grid.CurrentColumn.ColumnType = Janus.Windows.GridEX.ColumnType.CheckBox Then

                For i As Integer = 0 To Gridex.RowCount - 1

                    Dim row As Janus.Windows.GridEX.GridEXRow

                    row = Gridex.GetRow(i)

                    If row.Cells(KolonAdi).Text <> "0" And row.Cells(KolonAdi2).Text <> "" Then

                        If row.RowIndex <> grid.GetRow(Inx).RowIndex Then

                            If row.Cells(KolonAdi).Text = Gridex.GetRow(Inx).Cells(KolonAdi).Text And row.Cells(KolonAdi2).Text = Gridex.GetRow(Inx).Cells(KolonAdi2).Text Then

                                If Not grid.GetRow(Inx).IsChecked Then

                                    row.IsChecked = True

                                Else

                                    row.IsChecked = False

                                End If

                            End If

                        End If

                    End If

                Next

            End If

        End If
    End Sub

    Public Function SayiYazOndalikli(ByVal Sayi As String, _
        ByVal ParaBirimi As String, _
        ByVal Kurus As String) As String
        Try

          Dim result As String
          Dim B As String() = {"Bir", "Ýki", "Üç", "Dört", "Beþ", "Altý", _
           "Yedi", "Sekiz", "Dokuz"}
          Dim O As String() = {"", "On", "Yirmi", "Otuz", "Kýrk", "Elli", _
           "Altmýþ", "Yetmiþ", "Seksen", "Doksan"}
          Dim i As Integer
          Dim h As Integer
          Dim hane As Integer
          Dim onlar As Integer
          Dim yuzler As Integer
          Dim text As String
          Dim yuzde As String
          Dim DotPos As Integer
          Dim OndalikYok As Boolean
          Sayi = FormatNumber(Sayi, 2)

          DotPos = Sayi.IndexOf(".")

          Try
        If DotPos <> 0 Then
          If Copy(Sayi, DotPos + 1 - 1, 10).Length <> 1 Then
            Sayi = Convert.ToDecimal(Sayi).ToString("#0.#0")
          End If
        End If
          Catch
        result = "Tanýmsýz"
        Return result
          End Try

          DotPos = Sayi.IndexOf(".")

          If Copy(Sayi, DotPos + 1, 2) = "00" Then
        OndalikYok = True
        Sayi = Sayi.Remove(DotPos, 3)
          End If

          If DotPos <> 0 Then
        If Copy(Sayi, DotPos + 1, 10).Length = 2 Then
          If Copy(Sayi, DotPos + 2, 1) = "0" Then
            yuzde = O(CInt(Copy(Sayi, DotPos + 1, 1)))
          Else
            yuzde = O(CInt(Copy(Sayi, DotPos + 1, 1)))
            yuzde = yuzde + B(CInt(Copy(Sayi, DotPos + 2, 1)) - 1)
          End If
        ElseIf Copy(Sayi, DotPos + 1, 10).Length = 1 Then
          yuzde = B(CInt(Copy(Sayi, DotPos + 1, 1)) - 1)
        End If
        If OndalikYok = False Then
          Sayi = Sayi.Remove(DotPos, 3)
        End If
          End If
          text = ""
          For i = 0 To Sayi.Length - 1
        h = CInt(Copy(Sayi, i, 1))
        hane = Sayi.Length - i
        Select Case hane
          Case 1
            If h > 0 Then
              text = text + B(h - 1)
            ElseIf Sayi.Length = 1 Then
              text = text & "Sýfýr"
            End If
            Exit Select
          Case 2
            If h > 0 Then
              text = text + O(h)
            End If
            Exit Select
          Case 3
            If h > 0 Then
              If h = 1 Then
                text = text & "Yüz"
              Else
                text = text + B(h - 1) & "Yüz"
              End If
            End If
            Exit Select
          Case 4
            If h > 0 Then
              If (h = 1) AndAlso (Sayi.Length = 4) Then
                text = text & "Bin"
              Else
                text = text + B(h - 1) & "Bin"
              End If
            ElseIf (onlar <> 0) OrElse (yuzler <> 0) Then
              text = text & "Bin"
            End If
            Exit Select
          Case 5
            If h > 0 Then
              text = text + O(h)
              onlar = h
            Else
              onlar = 0
            End If
            Exit Select
          Case 6
            If h > 0 Then
              If h = 1 Then
                text = text & "Yüz"
                yuzler = h
              Else
                text = text + B(h - 1) & "Yüz"
                yuzler = h
              End If
            Else
              yuzler = 0
            End If
            Exit Select
          Case 7
            If h > 0 Then
              text = text + B(h - 1) & "Milyon"
            ElseIf (onlar <> 0) OrElse (yuzler <> 0) Then
              text = text & "Milyon"
            End If
            Exit Select
          Case 8
            If h > 0 Then
              text = text + O(h)
              onlar = h
            Else
              onlar = 0
            End If
            Exit Select
          Case 9
            If h > 0 Then
              If h = 1 Then
                text = text & "Yüz"
                yuzler = h
              Else
                text = text + B(h - 1) & "Yüz"
                yuzler = h
              End If
            Else
              yuzler = 0
            End If
            Exit Select
          Case 10
            If h > 0 Then
              text = text + B(h - 1) & "Milyar"
            ElseIf (onlar <> 0) OrElse (yuzler <> 0) Then
              text = text & "Milyar"
            End If
            Exit Select
          Case 11
            If h > 0 Then
              text = text + O(h)
              onlar = h
            Else
              onlar = 0
            End If
            Exit Select
          Case 12
            If h > 0 Then
              If h = 1 Then
                text = text & "Yüz"
                yuzler = h
              Else
                text = text + B(h - 1) & "Yüz"
                yuzler = h
              End If
            Else
              yuzler = 0
            End If
            Exit Select
          Case 13
            If h > 0 Then
              text = text + B(h - 1) & "Trilyon"
            ElseIf (onlar <> 0) OrElse (yuzler <> 0) Then
              text = text & "Trilyon"
            End If
            Exit Select
          Case 14
            If h > 0 Then
              text = text + O(h)
              onlar = h
            Else
              onlar = 0
            End If
            Exit Select
          Case 15
            If h > 0 Then
              If h = 1 Then
                text = text & "Yüz"
                yuzler = h
              Else
                text = text + B(h - 1) & "Yüz"
                yuzler = h
              End If
            Else
              yuzler = 0
            End If
            Exit Select
        End Select
          Next
          ' for do

          If yuzde <> "" Then
        text = text & " " & ParaBirimi & ". " + yuzde & " " & Kurus & "."
          Else
        text = text & " " & ParaBirimi & ". "
          End If

          result = text

          Return result

        Catch ex As Exception
          Throw ex
        End Try
    End Function

    Public Function YetkiKontrol(ByVal FormId As String, ByVal KullaniciId As Integer) As Boolean
        Dim sQuery As String

        Dim db As New Core.Data(My.Settings.ConnectionString.ToString)

        Dim dtYetki As DataTable

        Try

          sQuery = " Select *" & _
                  " From EDIYETKI" & _
                  " Where KullaniciID=" & KullaniciId & _
                  " And YetkiId=" & FormId

          dtYetki = db.RunSql(sQuery)

          If Not dtYetki Is Nothing AndAlso dtYetki.Rows.Count > 0 Then

        YetkiKontrol = True

          Else

        YetkiKontrol = False

          End If

          Return YetkiKontrol

        Catch ex As Exception

          MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

          Throw ex

        End Try
    End Function

    Function SeriNoAl(ByVal Type As String, Optional ByVal Artim As Integer = 1) As Double
        Dim db As New Core.Data(My.Settings.ConnectionString.ToString)

        'Tr_GetSeqNo()

        Dim param As New ArrayList

        Dim p As New SqlClient.SqlParameter("@CountField", Type)
        param.Add(p)

        Dim p1 As New SqlClient.SqlParameter("@Inc", Artim)
        param.Add(p1)

        Dim p2 As New SqlClient.SqlParameter("@Value", 0)
        p2.Direction = ParameterDirection.Output
        param.Add(p2)

        db.RunSp("Tr_GetSeqNo", param)

        Return p2.Value

        'sQuery = " Select Value " & _
        '                    " From TR_Counter" & _
        '                    " Where CountField='" & Type & "'"

        'dtSyc = db.RunSql(sQuery)

        'If Not (dtSyc Is Nothing) AndAlso _
        '                dtSyc.Rows.Count > 0 Then

        '    SeriNoAl = dtSyc.Rows(0).Item("Value").ToString() + 1

        '    sQuery = " Update TR_Counter" & _
        '                " Set Value=" & (dtSyc.Rows(0).Item("Value").ToString() + Artim) & _
        '                " Where CountField='" & Type & "'"

        '    db.RunSql(sQuery, True)

        'Else

        '    sQuery = " INSERT INTO TR_Counter (CountField,Value)" & _
        '                " VALUES( '" & Type & "', 1 )"

        '    db.RunSql(sQuery, True)

        '    SeriNoAl = 1

        'End If
    End Function

    #End Region 'Methods

    #Region "Nested Types"

    Public Structure Sevkiyat

        #Region "Fields"

        Dim BoxQty As Double
        Dim co_line As Integer
        Dim co_num As String
        Dim Description As String
        Dim Iptal As Boolean
        Dim Item As String
        Dim PackingCode As String
        Dim ShipQty As Double

        #End Region 'Fields

    End Structure

    #End Region 'Nested Types

End Module