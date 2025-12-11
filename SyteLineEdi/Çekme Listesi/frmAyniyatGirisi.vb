Public Class frmAyniyatGirisi

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Dim dt As New DataTable
    Dim sQuery As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnCikis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCikis.Click
        Me.Close()
    End Sub

    Private Sub btnTamam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTamam.Click
        Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

        Dim row As Janus.Windows.GridEX.GridEXRow

        Dim Ambar, Malzeme, Loc, Lot, OlcuBirimi As String

        Dim Miktar, EldekiMiktar, nTransNum As Double

        checkedRows = GridEX1.GetCheckedRows()

        If checkedRows.Length > 0 Then

            For Each row In checkedRows

                If CDbl(row.Cells("MIKTAR").Text) > CDbl(row.Cells("ELDEKIMIKTAR").Text) Then

                    sQuery = " Insert Into TR_AyniyatLog" & _
                                "(SevkNo, Ambar , Malzeme , Tanim ," & _
                                " OlcuBirimi , Miktar	, EldekiMiktar , EksikMiktar ," & _
                                " Tarih, Kullanici)" & _
                            " Values (" & _
                            sIrsaliyeSeri.SevkNo & "," & _
                            sTirnakEkle(row.Cells("Ambar").Text) & "," & _
                            sTirnakEkle(row.Cells("Malzeme").Text) & "," & _
                            sTirnakEkle(row.Cells("TANIM").Text) & "," & _
                            sTirnakEkle(row.Cells("OLCUBIRIMI").Text) & "," & _
                            CDbl(row.Cells("MIKTAR").Text) & "," & _
                            CDbl(row.Cells("ELDEKIMIKTAR").Text) & "," & _
                            CDbl(row.Cells("MIKTAR").Text) - CDbl(row.Cells("ELDEKIMIKTAR").Text) & "," & _
                            sTarih(Now.Date) & "," & _
                            sTirnakEkle(KullaniciAdi) & _
                            ")"

                    db.RunSql(sQuery, True)

                    Miktar = CDbl(row.Cells("ELDEKIMIKTAR").Text)

                Else

                    Miktar = CDbl(row.Cells("MIKTAR").Text)

                End If

                If Miktar > 0 Then

                    Ambar = row.Cells("Ambar").Text

                    Malzeme = row.Cells("MALZEME").Text

                    OlcuBirimi = row.Cells("OLCUBIRIMI").Text

                    sQuery = "select loc, lot, qty_on_hand   " & _
                            " from lot_loc_net " & _
                            " Where whse=" & sTirnakEkle(Ambar) & _
                            " And Item=" & sTirnakEkle(Malzeme) & _
                            " And qty_on_hand > 0 " & _
                            " Order By CreateDate Asc, lot Asc  "

                    dt = db.RunSql(sQuery)

                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                        For Each rowdt As DataRow In dt.Rows

                            Loc = rowdt.Item("loc").ToString

                            Lot = rowdt.Item("lot").ToString

                            EldekiMiktar = rowdt.Item("qty_on_hand").ToString

                            If Miktar >= EldekiMiktar Then

                                nTransNum = SeriNoAl("AYN")

                                sQuery = " INSERT INTO dcitem " & _
                                        " (trans_num ,stat ,trans_date ,emp_num" & _
                                        " ,trans_type ,item ,whse ,loc ,lot" & _
                                        " ,override ,count_qty ,reason_code" & _
                                        " ,u_m ,document_num)" & _
                                " VALUES " & _
                                       "( " & nTransNum & _
                                       " ,'P'" & _
                                       " ,getdate()" & _
                                       " ,'EDI'" & _
                                       " ,'2'" & _
                                       " , " & sTirnakEkle(Malzeme) & _
                                       " , " & sTirnakEkle(Ambar) & _
                                       " , " & sTirnakEkle(Loc) & _
                                       " , " & sTirnakEkle(Lot) & _
                                       " ,1" & _
                                       " ," & EldekiMiktar & _
                                       " ,'AYR' " & _
                                       " ," & sTirnakEkle(OlcuBirimi) & _
                                       " ," & sTirnakEkle(sIrsaliyeSeri.SevkNo) & ")"

                                db.RunSql(sQuery)

                                sQuery = " BEGIN TRAN " & vbNewLine & _
                                " declare @p2 tinyint " & vbNewLine & _
                                " set @p2=0 " & vbNewLine & _
                                " declare @p3 nvarchar(2800) " & vbNewLine & _
                                " set @p3=NULL " & vbNewLine & _
                                " declare @presult nvarchar(2800) " & vbNewLine & _
                                " set  @presult = @p3 " & vbNewLine & _
                                " exec DcmatlPSp @PTransNum=" & nTransNum & ",@PCanOverride=@p2 output,@Infobar=@p3 output " & vbNewLine & _
                                " select @p2, @p3 " & vbNewLine & _
                                " COMMIT TRAN "
                                db.RunSql(sQuery)

                                Miktar = Miktar - EldekiMiktar

                                If Miktar = 0 Then

                                    Exit For

                                End If

                            Else

                                nTransNum = SeriNoAl("AYN")

                                sQuery = " INSERT INTO dcitem " & _
                                        " (trans_num ,stat ,trans_date ,emp_num" & _
                                        " ,trans_type ,item ,whse ,loc ,lot" & _
                                        " ,override ,count_qty ,reason_code" & _
                                        " ,u_m ,document_num)" & _
                                " VALUES " & _
                                       "( " & nTransNum & _
                                       " ,'P'" & _
                                       " ,getdate()" & _
                                       " ,'EDI'" & _
                                       " ,'2'" & _
                                       " , " & sTirnakEkle(Malzeme) & _
                                       " , " & sTirnakEkle(Ambar) & _
                                       " , " & sTirnakEkle(Loc) & _
                                       " , " & sTirnakEkle(Lot) & _
                                       " ,1" & _
                                       " , " & Miktar & _
                                       " ,'AYR' " & _
                                       " , " & sTirnakEkle(OlcuBirimi) & _
                                       " , " & sTirnakEkle(sIrsaliyeSeri.SevkNo) & ")"

                                db.RunSql(sQuery)

                                sQuery = " BEGIN TRAN " & vbNewLine & _
                                " declare @p2 tinyint " & vbNewLine & _
                                " set @p2=0 " & vbNewLine & _
                                " declare @p3 nvarchar(2800) " & vbNewLine & _
                                " set @p3=NULL " & vbNewLine & _
                                " declare @presult nvarchar(2800) " & vbNewLine & _
                                " set  @presult = @p3 " & vbNewLine & _
                                " exec DcmatlPSp @PTransNum=" & nTransNum & ",@PCanOverride=@p2 output,@Infobar=@p3 output " & vbNewLine & _
                                " select @p2, @p3 " & vbNewLine & _
                                " COMMIT TRAN "
                                db.RunSql(sQuery)

                                Miktar = 0

                                Exit For

                            End If

                        Next

                    End If

                End If

            Next

        End If

      

        Me.Close()

    End Sub

    Private Sub frmAyniyatGirisi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GridEX1.DataSource = dtAyniyat

        GridEX1.CheckAllRecords()
    End Sub

    #End Region 'Methods

End Class