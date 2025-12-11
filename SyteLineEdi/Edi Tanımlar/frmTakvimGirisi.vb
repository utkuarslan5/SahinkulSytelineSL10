Public Class frmTakvimGirisi

    #Region "Fields"

    Dim CurrentSelection, NewSelection As String
    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dt As New DataTable
    Dim dtGrid As New DataTable
    Dim sSql As String

    #End Region 'Fields

    #Region "Methods"

    Public Sub InsertCalender(ByVal nGun As Integer, ByVal nYil As Double)
        Dim FirstDayOfYear, LastDay, ntmpDate As DateTime

        Dim nFirstDay As Integer

        Try

            FirstDayOfYear = TarihCevirSlashli("01/01/" & nYil)
            LastDay = TarihCevirSlashli("01/01/" & (nYil + 5))
            nFirstDay = FirstDayOfYear.DayOfWeek
            ntmpDate = DateAdd(DateInterval.Day, (((nGun + 7) - nFirstDay) Mod 7), FirstDayOfYear)

            '  if nFirstDay <= nGun then
            '    ntmpDate = ntmpDate - 7

            While ntmpDate < LastDay

                sSql = "INSERT INTO CALNDR" & _
                    "(caln, caldt) " & _
                    " values ( " & _
                        sTirnakEkle(txtTanim.Text) & "," & _
                        ntmpDate.ToString("1yyMMdd") & ")"

                db.RunSql(sSql)

                ntmpDate = DateAdd(DateInterval.Day, 7, ntmpDate)

            End While

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Public Sub RefreshRecords()
        Try

            sSql = "SELECT caln, substring(cast(min(caldt) as varchar(7)),6,2)+'/'+ " & _
                            "substring(cast(min(caldt) as varchar(7)),4,2)+'/20' + " & _
                            "substring(cast(min(caldt) as varchar(7)),2,2) starts, " & _
                            "substring(cast(max(caldt) as varchar(7)),6,2)+'/'+" & _
                            "substring(cast(max(caldt) as varchar(7)),4,2)+'/20'+" & _
                            "substring(cast(max(caldt) as varchar(7)),2,2) ends " & _
                    "FROM calndr " & _
                    "GROUP BY caln "

            dtGrid = db.RunSql(sSql)

            GridEX1.DataSource = dtGrid

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Public Sub Temizle()
        txtTanim.Text = ""
        txtYil.Value = 0
        chkPazartesi.Checked = False
        chkSali.Checked = False
        chkCarsamba.Checked = False
        chkPersembe.Checked = False
        chkCuma.Checked = False
        chkCumartesi.Checked = False
        chkPazar.Checked = False
    End Sub

    Private Sub btnKaydet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKaydet.Click
        Try

            If txtTanim.Text = "" Then

                MessageBox.Show("Takvim tanımını girmelisiniz!", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtTanim.Focus()
                Exit Sub

            End If

            If (txtYil.Text = "") Or (txtYil.Value = 0) Then

                MessageBox.Show("Takvim Başlamgıç yılını girmelisiniz!", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtYil.Focus()
                Exit Sub

            End If

            If Not ((chkPazartesi.Checked) Or (chkSali.Checked) Or (chkCarsamba.Checked) Or (chkPersembe.Checked) Or _
                   (chkCuma.Checked) Or (chkCumartesi.Checked) Or (chkPazar.Checked)) Then

                MessageBox.Show("Sevkiyat gününü seçmelisiniz!", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Exit Sub

            End If

            NewSelection = txtYil.Text
            If chkPazartesi.Checked Then
                NewSelection = NewSelection & "E"
            Else
                NewSelection = NewSelection & "H"
            End If

            If chkSali.Checked Then
                NewSelection = NewSelection & "E"
            Else
                NewSelection = NewSelection + "H"
            End If

            If chkCarsamba.Checked Then
                NewSelection = NewSelection & "E"
            Else
                NewSelection = NewSelection & "H"
            End If

            If chkPersembe.Checked Then
                NewSelection = NewSelection & "E"
            Else
                NewSelection = NewSelection & "H"
            End If
            If chkCuma.Checked Then
                NewSelection = NewSelection & "E"
            Else
                NewSelection = NewSelection & "H"
            End If
            If chkCumartesi.Checked Then
                NewSelection = NewSelection & "E"
            Else
                NewSelection = NewSelection & "H"
            End If
            If chkPazar.Checked Then
                NewSelection = NewSelection & "E"
            Else
                NewSelection = NewSelection & "H"
            End If
            If CurrentSelection = NewSelection Then
                MessageBox.Show("Bu takvimle ilgili değişiklik yapılmamıştır. Bilgilerinize.", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            sSql = "SELECT * FROM CALNDR " & _
                        "WHERE  caln = " & sTirnakEkle(txtTanim.Text)

            dt = db.RunSql(sSql)

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                sSql = "DELETE FROM CALNDR" & _
                       " WHERE  caln =" & sTirnakEkle(txtTanim.Text)
                db.RunSql(sSql)

            End If

            If chkPazartesi.Checked Then
                InsertCalender(1, txtYil.Value)
            End If
            If chkSali.Checked Then
                InsertCalender(2, txtYil.Value)
            End If
            If chkCarsamba.Checked Then
                InsertCalender(3, txtYil.Value)
            End If
            If chkPersembe.Checked Then
                InsertCalender(4, txtYil.Value)
            End If
            If chkCuma.Checked Then
                InsertCalender(5, txtYil.Value)
            End If
            If chkCumartesi.Checked Then
                InsertCalender(6, txtYil.Value)
            End If
            If chkPazar.Checked Then
                InsertCalender(7, txtYil.Value)
            End If

            RefreshRecords()

            Temizle()
            btnKaydet.Enabled = False
            btnSil.Enabled = False
            btnVazgec.Enabled = False
            btnYeniKayit.Enabled = True

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnYeniKayit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYeniKayit.Click
        btnKaydet.Enabled = True
        btnSil.Enabled = False
        btnVazgec.Enabled = True

        txtTanim.Enabled = True
        Temizle()
        CurrentSelection = "0000HHHHHHH"
        txtTanim.Focus()
    End Sub

    Private Sub GridEX1_RowDoubleClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles GridEX1.RowDoubleClick
        Dim sYil, sTarih As String
        Dim ADate As DateTime
        Dim CurrentSelection As String

        Try

            txtTanim.Enabled = False
            'pnlTakvim.Enabled = True

            btnKaydet.Enabled = True
            btnSil.Enabled = True
            btnVazgec.Enabled = True
            btnYeniKayit.Enabled = False

            txtTanim.Text = e.Row.Cells("CALN").Text
            sYil = e.Row.Cells("STARTS").Text
            sYil = Copy(sYil, 6, 4)
            txtYil.Text = sYil

            chkPazartesi.Checked = False
            chkSali.Checked = False
            chkCarsamba.Checked = False
            chkPersembe.Checked = False
            chkCuma.Checked = False
            chkCumartesi.Checked = False
            chkPazar.Checked = False

            sSql = "SELECT * FROM CALNDR" & _
               " WHERE  caln = " & sTirnakEkle(txtTanim.Text) & ""

            dt = db.RunSql(sSql)

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                For i As Integer = 0 To dt.Rows.Count - 1

                    sTarih = Copy(dt.Rows(i)("caldt").ToString, 5, 2) & "/" & _
                              Copy(dt.Rows(i)("caldt").ToString, 3, 2) & "/20" & _
                                Copy(dt.Rows(i)("caldt").ToString, 1, 2)

                    ADate = TarihCevirSlashli(sTarih)

                    If ADate.DayOfWeek = "1" Then
                        chkPazartesi.Checked = True
                    ElseIf ADate.DayOfWeek = "2" Then
                        chkSali.Checked = True
                    ElseIf ADate.DayOfWeek = "3" Then
                        chkCarsamba.Checked = True
                    ElseIf ADate.DayOfWeek = "4" Then
                        chkPersembe.Checked = True
                    ElseIf ADate.DayOfWeek = "5" Then
                        chkCuma.Checked = True
                    ElseIf ADate.DayOfWeek = "6" Then
                        chkCumartesi.Checked = True
                    ElseIf ADate.DayOfWeek = "7" Then
                        chkPazar.Checked = True
                    End If

                Next i
            End If

            CurrentSelection = txtYil.Text

            If chkPazartesi.Checked Then
                CurrentSelection = CurrentSelection & "E"
            Else
                CurrentSelection = CurrentSelection & "H"
            End If

            If chkSali.Checked Then
                CurrentSelection = CurrentSelection & "E"
            Else
                CurrentSelection = CurrentSelection & "H"
            End If

            If chkCarsamba.Checked Then
                CurrentSelection = CurrentSelection & "E"
            Else
                CurrentSelection = CurrentSelection & "H"
            End If

            If chkPersembe.Checked Then
                CurrentSelection = CurrentSelection & "E"
            Else
                CurrentSelection = CurrentSelection & "H"
            End If

            If chkCuma.Checked Then
                CurrentSelection = CurrentSelection & "E"
            Else
                CurrentSelection = CurrentSelection & "H"
            End If

            If chkCumartesi.Checked Then
                CurrentSelection = CurrentSelection & "E"
            Else
                CurrentSelection = CurrentSelection & "H"
            End If

            If chkPazar.Checked Then
                CurrentSelection = CurrentSelection & "E"
            Else
                CurrentSelection = CurrentSelection & "H"
            End If

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub TakvimGirisi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RefreshRecords()

        btnKaydet.Enabled = False
        btnSil.Enabled = False
        btnVazgec.Enabled = False
        btnYeniKayit.Enabled = True
    End Sub

    #End Region 'Methods

End Class