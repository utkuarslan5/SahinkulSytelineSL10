Public Class frmPaletEtiketBasim

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString.ToString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnOlustur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOlustur.Click
        Dim sQuery As String

        Dim dsITMPACK As DataSet

        Dim iKapakAdeti, iSeperatorAdeti As Integer

        Dim dPaletBirimAgirligi, _
            dSeparatorBirimAgirligi, dKapakBirimAgirligi, dNetAgirlik, dBrutAgirilik As Decimal

        Dim sEtkTip, sMusteriPaletKodu As String

        sEtkTip = ""

        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        If GridEX1.RowCount > 0 Then

            dbAccess.RunSql("Delete From ETIKETDTY", True)

            Dim row As Janus.Windows.GridEX.GridEXRow

            For i As Integer = 0 To GridEX1.RowCount - 1

                row = GridEX1.GetRow(i)

                sQuery = " Select I.ETKTIP, I.KKOD, I.MKKOD, I.PKOD, I.MPKOD, I.PKSIR, " & _
                                " I.PKMIK, I.REVNO, I.REVTAR , I.KPMIK , I.SPMIK , ITEMDIM_K.BRMAG AS KutuA, " & _
                                " ITEMDIM_P.BRMAG AS PaletA, ITEMDIM_KP.BRMAG AS KapakA, " & _
                                " ITEMDIM_SP.BRMAG AS SeperatorA, k.USER1 As HNDCODE, k.KANBAN " & _
                             " FROM   ITEMDIM ITEMDIM_SP RIGHT OUTER JOIN " & _
                                    " ITMPACK I ON ITEMDIM_SP.ITNBR = I.SPKOD LEFT OUTER JOIN " & _
                                    " ITEMDIM ITEMDIM_KP ON I.KPKKOD = ITEMDIM_KP.ITNBR LEFT OUTER JOIN " & _
                                    " ITEMDIM ITEMDIM_P ON I.PKOD = ITEMDIM_P.ITNBR LEFT OUTER JOIN " & _
                                    " ITEMDIM ITEMDIM_K ON I.KKOD = ITEMDIM_K.ITNBR RIGHT OUTER JOIN " & _
                                    " KONTRTPF k ON I.ITNBR = k.BZMITM AND I.AMBKOD = k.AMBKOD " & _
                             " where k.cust=" & row.Cells("CUST").Text & _
                                 " And SHIPTO='" & row.Cells("PLANTID").Text & "'" & _
                                 " And KAPI='" & row.Cells("GATEID").Text & "'" & _
                                 " And k.BZMITM='" & row.Cells("ITNBR").Text & "'"

                dsITMPACK = db.RunSql(sQuery, "ITMPACK")

                If Not dsITMPACK Is Nothing Then

                    If dsITMPACK.Tables("ITMPACK").Rows.Count > 0 Then

                        dPaletBirimAgirligi = dsITMPACK.Tables("ITMPACK").Rows(0).Item("PaletA").ToString

                        dKapakBirimAgirligi = dsITMPACK.Tables("ITMPACK").Rows(0).Item("KapakA").ToString

                        dSeparatorBirimAgirligi = dsITMPACK.Tables("ITMPACK").Rows(0).Item("SeperatorA").ToString

                        iKapakAdeti = dsITMPACK.Tables("ITMPACK").Rows(0).Item("KPMIK").ToString

                        iSeperatorAdeti = dsITMPACK.Tables("ITMPACK").Rows(0).Item("SPMIK").ToString

                        sMusteriPaletKodu = dsITMPACK.Tables("ITMPACK").Rows(0).Item("MPKOD").ToString

                    End If

                End If

                dNetAgirlik = row.Cells("NETWEIGHT").Text

                dBrutAgirilik = dNetAgirlik + (dPaletBirimAgirligi + _
                                (iKapakAdeti * dKapakBirimAgirligi) + _
                                (iSeperatorAdeti * dSeparatorBirimAgirligi))

                sQuery = "INSERT INTO ETIKETDTY" & _
                             " (PICKNO, PICKDT, CUST, ETKADR1, ETKADR2, ETKTIP, " & _
                             " KUTUETK, PLTETK, KPAPTIP, PPAPTIP, PLANTID, GATEID, " & _
                             " MURNKOD, MURNTNM, ITNBR, MIKTAR, KKOD, MKKOD, " & _
                             " KSAY, KMIK, PKOD, MPKOD, PSAY, PKSIR, " & _
                             " PKMIK, HKSAY, REVNO, REVTAR, DUNSNO, IBRMAGR, " & _
                             " KBRMAGR, PBRMAGR, KAGOB, PAGOB, IAGOB, SPAGOB, " & _
                             " KPAGOB, SPBRMAGR, KPBRMAGR, PUSNO, SEVKTAR, USERF2, " & _
                             " DNGRCODE, IRSNO, DOCK, HNDCODE, SPPLRADR, NETWEIGHT, " & _
                             " BRTWEIGHT, BOXES, QTYPPACK, ETKSERINO, PLTNO, PLTTYPE, L3P , ORDNO , ORDSEQ , HOUSE, KANBANNO ) " & _
                         " VALUES(" & row.Cells("PICKNO").Text & "," & row.Cells("PICKDT").Text & _
                                  ", " & row.Cells("CUST").Text & " , '" & row.Cells("ETKADR1").Text & "' , '" & row.Cells("ETKADR2").Text & "' , '" & row.Cells("ETKTIP").Text & _
                                  "' , 0 , 1 , '' , '',  '" & row.Cells("PLANTID").Text & "','" & row.Cells("GATEID").Text & "'" & _
                                  ",'" & row.Cells("MURNKOD").Text & "','" & row.Cells("MURNTNM").Text & "','" & row.Cells("ITNBR").Text & _
                                  "'," & row.Cells("KMIK").Text & "," & "'" & row.Cells("KKOD").Text & "'" & ",'" & row.Cells("MKKOD").Text & "'" & _
                                  "," & row.Cells("KSAY").Text & "," & row.Cells("KMIK").Text & "," & "'" & row.Cells("PKOD").Text & "'" & "," & "'" & _
                                  row.Cells("MPKOD").Text & "'" & ", " & 1 & " , " & 0 & " " & _
                                  ", " & row.Cells("KSAY").Text & " , 0 ," & "'" & row.Cells("REVNO").Text & "'," & "'" & row.Cells("REVTAR").Text & "'," & "'" & row.Cells("DUNSNO").Text & "'," & 0 & _
                                  ", " & 0 & " , " & dPaletBirimAgirligi & " , " & " 'KG','KG','KG','KG' " & _
                                  ", 'KG' , " & dSeparatorBirimAgirligi & " , " & dKapakBirimAgirligi & " , '" & row.Cells("Pusno").Text & "'," & row.Cells("SEVKTAR").Text & _
                                  ", '" & row.Cells("USERF2").Text & "' ,'','" & row.Cells("IRSNO").Text & "', '" & row.Cells("DOCK").Text & "' , " & " '" & row.Cells("HNDCODE").Text & "', '" & row.Cells("SPPLRADR").Text & "' " & _
                                  ", " & dNetAgirlik & " , " & dBrutAgirilik & " , " & row.Cells("KMIK").Text & " ," & row.Cells("KMIK").Text & ", '" & row.Cells("PLTNO").Text & "' , '" & row.Cells("PLTNO").Text & "' , '" & row.Cells("PLTTYPE").Text & "', '" & _
                                  row.Cells("L3P").Text & "','" & "" & "'," & 0 & ",'" & "" & "','" & row.Cells("KANBANNO").Text & "'" & _
                                  ") "

                dbAccess.RunSql(sQuery, True)

                sEtkTip = row.Cells("ETKTIP").Text

            Next i

            MsgBox(AppDomain.CurrentDomain.SetupInformation.ApplicationBase & sEtkTip & "Pallet.rpt")

            'Select Case sEtkTip.Substring(0, 3)

            '    Case "ETK"

            '        frmRapor.sRapor = AppDomain.CurrentDomain.SetupInformation.ApplicationBase & "\GALIA_PALET.rpt"

            'End Select

            'Select Case sEtkTip

            '    Case "FORD"

            '        frmRapor.sRapor = AppDomain.CurrentDomain.SetupInformation.ApplicationBase & "\PaletEtiketi.rpt"

            '    Case ""
            '        frmRapor.sRapor = ""

            'End Select

            '
            RaporCagir(sEtkTip & "Pallet.rpt")
            '

        End If

        System.Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click
        Dim sQuery As String

        Dim ds As DataSet

        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Try

            sQuery = " SELECT  PICKNO, PICKDT, CUST, ETKADR1, ETKADR2, " & _
                            " ETKTIP,	PLANTID, GATEID, MIN(MURNKOD) As MURNKOD, MIN(MURNTNM)As MURNTNM, MIN(ITNBR) As ITNBR , " & _
                            " MIN(KKOD) As KKOD, MIN(MKKOD) As MKKOD,  Sum(Boxes) KSAY , Sum(KMIK) KMIK , MIN(PKOD) As PKOD, " & _
                            " MIN(MPKOD) As MPKOD, MIN(PKMIK) As PKMIK, MIN(REVNO) As REVNO , " & _
                            " MIN(REVTAR) As REVTAR, MIN(DUNSNO) As DUNSNO, MIN(SEVKTAR) As SEVKTAR, MIN(USERF2) As USERF2, MIN(IRSNO) As IRSNO," & _
                            " MIN(DOCK) As DOCK, MIN(HNDCODE) As HNDCODE, MIN(SPPLRADR) As SPPLRADR, SUM(NETWEIGHT) As NETWEIGHT , " & _
                            " PLTNO  , PLTTYPE, MIN(L3P) As L3P, KANBANNO , MIN(Pusno) As Pusno" & _
                        " FROM  ETIKETDTY " & _
                        " Where PICKNO=" & txtCeklistNo.Text & _
                        " And PLTNO<>'0'" & _
                        " Group By PLTNO,PLTTYPE ,PICKNO, PICKDT, CUST, ETKADR1, ETKADR2, " & _
                                " ETKTIP, PLANTID, GATEID , KANBANNO "

            ds = db.RunSql(sQuery, "Cekme")

            If Not (ds Is Nothing) AndAlso _
                            ds.Tables.Count > 0 AndAlso _
                            ds.Tables("Cekme").Rows.Count > 0 Then

                GridEX1.DataSource = ds

                GridEX1.DataMember = "Cekme"

            Else

                MessageBox.Show(" Kayýt Bulunamadý! ", "Ekip Mapics", MessageBoxButtons.OK)

            End If

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    #End Region 'Methods

End Class