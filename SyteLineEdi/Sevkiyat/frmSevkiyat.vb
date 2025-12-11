Public Structure EtiketBilgisi

    #Region "Fields"

    Public EtkSeriNo As String 'EtiketNo
    Public House As String
    Public Itnbr As String
    Public L3P As String '16.01.07 tarihinde eklendi
    Public PickNo As Integer
    Public Pkmik As Integer
    Public Pkod As String

    #End Region 'Fields

End Structure

Public Class frmSevkiyat
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Public ReturnValue As New ReturnValue
    Public sAmflib As String = ""
    Public sConnStr As String '= "data source=VOLKAN;initial catalog=Demo_App;Uid=sa;pwd="
    Public sEkipEdi As String = ""
    Public Sorgu As String

    Friend  WithEvents btnCancel As System.Windows.Forms.Button
    Friend  WithEvents btnCreatePalet As System.Windows.Forms.Button
    Friend  WithEvents btnDelete As System.Windows.Forms.Button
    Friend  WithEvents btnSend As System.Windows.Forms.Button
    Friend  WithEvents dtgrdMain As System.Windows.Forms.DataGrid
    Friend  WithEvents EtiketNo As System.Windows.Forms.DataGridTextBoxColumn
    Friend  WithEvents lblEtiketNo As System.Windows.Forms.Label
    Friend  WithEvents lblListeNo As System.Windows.Forms.Label
    Friend  WithEvents lblMiktar As System.Windows.Forms.Label
    Friend  WithEvents lblPaletNo As System.Windows.Forms.Label
    Friend  WithEvents listNo As System.Windows.Forms.DataGridTextBoxColumn
    Friend  WithEvents miktar As System.Windows.Forms.DataGridTextBoxColumn
    Friend  WithEvents paletNo As System.Windows.Forms.DataGridTextBoxColumn
    Friend  WithEvents Panel1 As System.Windows.Forms.Panel

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend  WithEvents Panel3 As System.Windows.Forms.Panel
    Friend  WithEvents tbSevk As System.Windows.Forms.DataGridTableStyle
    Friend  WithEvents txtEtiketNo As System.Windows.Forms.TextBox
    Friend  WithEvents txtListeNo As System.Windows.Forms.TextBox
    Friend  WithEvents txtMiktar As System.Windows.Forms.TextBox
    Friend  WithEvents txtPaletNo As System.Windows.Forms.TextBox

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbcore As New Core.Data(My.Settings.ConnectionString)
    Private ds As DataSet
    Dim dt As New DataTable
    Private etkBilg As EtiketBilgisi
    Private palet As Boolean
    Private Qty As Decimal
    Private QtyMax As Integer = 0
    Dim RetVal As New ReturnValue
    Private sL3p As String = ""
    Private Surum As String = "1.0.0.1"

    #End Region 'Fields

    #Region "Constructors"

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub

    #End Region 'Constructors

    #Region "Methods"

    '
    'Belirtilen EtiketNo belirtilen listeye ait Olup olmadýðý kontrol edilir
    Public Function CheckEtiketNo( _
        ByVal PickNo As Integer, _
        ByVal EtiketNo As String, _
        ByRef EtiketBilgisi As EtiketBilgisi) As ReturnValue
        '

        '
        Try

            '
            dt = dbcore.RunSql( _
                   "Select PICKNO, ETKSERINO, HOUSE, ITNBR, KMIK, isnull(Pkod,'') As Pkod , PKMIK,ORDNO, ORDSEQ, rtrim(l3p)+rtrim(hndcode) L3P  " & _
                       "From ETIKETDTY " & _
                       "Where Durum <> 'X' And PICKNO = " & PickNo.ToString & _
                            " And EtkSeriNo = '" & EtiketNo & "' ")

            If Not (dt Is Nothing) Then
                With dt
                    If .Rows.Count > 0 Then
                        '
                        EtiketBilgisi.Itnbr = IIf(IsDBNull(.Rows(0).Item("ITNBR")), 0, .Rows(0).Item("ITNBR"))
                        EtiketBilgisi.EtkSeriNo = IIf(IsDBNull(.Rows(0).Item("ETKSERINO")), 0, .Rows(0).Item("ETKSERINO"))
                        EtiketBilgisi.PickNo = IIf(IsDBNull(.Rows(0).Item("PICKNO")), 0, .Rows(0).Item("PICKNO"))
                        EtiketBilgisi.House = IIf(IsDBNull(.Rows(0).Item("HOUSE")), 0, .Rows(0).Item("HOUSE"))
                        EtiketBilgisi.L3P = IIf(IsDBNull(.Rows(0).Item("L3P")), 0, .Rows(0).Item("L3P"))
                        EtiketBilgisi.Pkmik = IIf(IsDBNull(.Rows(0).Item("L3P")), 0, .Rows(0).Item("PKMIK"))
                        EtiketBilgisi.Pkod = IIf(IsDBNull(.Rows(0).Item("L3P")), 0, .Rows(0).Item("Pkod"))

                        '
                        Me.ReturnValue.ReturnValue = True
                    Else
                        Me.ReturnValue.Add( _
                            PickNo.ToString & " Numaralý Çekme Listesi ve " & _
                            EtiketNo & " Numaralý Etiket eþleþmiyor !")
                        'sMsg = sPickNo & " Numaralý Çekme Listesi sistemde tanýmlý deðil!"
                        Me.ReturnValue.ReturnValue = False
                    End If
                End With
            Else
                Me.ReturnValue.ReturnValue = False
            End If
            '
            Return Me.ReturnValue
            '
        Catch ex As Exception
            '
            Me.ReturnValue.Add(ex.Message)
            Me.ReturnValue.ReturnValue = False
            '
            Return Me.ReturnValue
        Finally

        End Try
        ''
    End Function

    'Belirtilen EtiketNo belirtilen listeye ait Olup olmadýðý kontrol edilir
    Public Function CheckEtiketNoxxx( _
        ByVal PickNo As Integer, _
        ByVal EtiketNo As String) As ReturnValue
        '

        '
        Try

            '
            dt = dbcore.RunSql( _
                   "Select PICKNO, ETKSERINO, HOUSE, ITNBR,KMiK ,ORDNO, ORDSEQ, trim(l3p) || trim(hndcode) L3P  " & _
                       "From ETIKETDTY " & _
                       "Where PICKNO = " & PickNo.ToString & _
                            " And EtkSeriNo = '" & EtiketNo & "' ")

            If Not (dt Is Nothing) Then
                With dt
                    If .Rows.Count > 0 Then
                        '
                        Me.ReturnValue.ReturnValue = True
                    Else
                        Me.ReturnValue.Add( _
                            PickNo.ToString & " Numaralý Çekme Listesi ve " & _
                            EtiketNo & " Numaralý Etiket eþleþmiyor !")
                        'sMsg = sPickNo & " Numaralý Çekme Listesi sistemde tanýmlý deðil!"
                        Me.ReturnValue.ReturnValue = False
                    End If
                End With
            Else
                Me.ReturnValue.ReturnValue = False
            End If
            '
            Return Me.ReturnValue
            '
        Catch ex As Exception
            '
            Me.ReturnValue.Add(ex.Message)
            Me.ReturnValue.ReturnValue = False
            '
            Return Me.ReturnValue
        Finally

        End Try
        ''
    End Function

    '
    'Belirtilen Çekme listesinin Olup olmadýðý kontrol edilir
    Public Function CheckPickNo( _
        ByVal PickNo As Integer) As ReturnValue
        '

        Dim Qty As Decimal

        '
        Try

            '
            dt = dbcore.RunSql( _
                   "Select PICKNO " & _
                       "From ETIKETDTY " & _
                       "Where PICKNO = " & PickNo.ToString & " ")

            If Not (dt Is Nothing) Then
                With dt
                    If .Rows.Count > 0 Then
                        '
                        Qty = .Rows.Count
                        ReturnValue.iInfo = Qty
                        Me.ReturnValue.ReturnValue = True
                    Else
                        Me.ReturnValue.Add(PickNo.ToString & " Numaralý Çekme Listesi sistemde tanýmlý deðil!")
                        'sMsg = sPickNo & " Numaralý Çekme Listesi sistemde tanýmlý deðil!"
                        Me.ReturnValue.ReturnValue = False
                    End If
                End With
            Else
                Me.ReturnValue.ReturnValue = False
            End If
            '
            Return Me.ReturnValue
            '
        Catch ex As Exception
            '
            Me.ReturnValue.Add(ex.Message)
            Me.ReturnValue.ReturnValue = False
            '
            Return Me.ReturnValue
        Finally

        End Try
        ''
    End Function

    '
    'ETÝKET NO ONAY -- Etikete ait ETÝKETNOONAY deðerini getirir.
    '
    'Belirtilen Malzeme Ambarda ne kadar olduðu kontrol edilir
    '
    'Sistemden yeni bir etiketno üretilir ve getirilir
    Public Function GetEtiketNo() As String
        '

        Dim sEtiketNo As String
        Dim iEtiketNo As Integer
        '
        Try

            '
            dbcore.RunSql( _
                    "Update TR_COUNTER " & _
                        "Set ETIKETNO = ETIKETNO + 1 ")
            If dbcore.Result.ReturnValue Then
                '
                dt = dbcore.RunSql( _
                       "Select ETIKETNO  " & _
                           "From TR_COUNTER ")
                '
                If Not (dt Is Nothing) Then
                    With dt
                        If .Rows.Count > 0 Then
                            iEtiketNo = IIf(IsDBNull(.Rows(0).Item("ETIKETNO")), 0, .Rows(0).Item("ETIKETNO"))
                            Me.ReturnValue.ReturnValue = True
                        Else
                            Me.ReturnValue.ReturnValue = False
                        End If
                        '
                    End With
                    '
                    sEtiketNo = "1" & Now.ToString("yy") & "".PadLeft(6 - iEtiketNo.ToString.Length, "0") & iEtiketNo.ToString
                    Return sEtiketNo
                Else
                    Return ""
                    '
                End If
            Else
                Me.ReturnValue.ReturnValue = False
                Return ""
            End If
            '
        Catch ex As Exception
            '
            Me.ReturnValue.Add(ex.Message)
            Me.ReturnValue.ReturnValue = False
            '
            Return ""
        Finally

        End Try
        ''
    End Function

    'Etiket Numarasýna Ait Malmeze Getirilir
    Public Function GetMalzemeNo(ByVal strEtiketNo As String) As String
        Try
            If strEtiketNo.Length = 0 Then
                Throw New Exception("Lütfen etiket numarasý giriniz")
            End If

            Dim strAltMalmeze As String  'KULLANILAN DEÐER

            Dim Sorgu As String

            '
            Dim textA As String = sAmflib
            Sorgu = "Select (Select B.UVMCIM FROM ITEMASA As B  Where B.ITNBR=A.ITNBR) as UVMCIM" & _
                           " From ETIKETDTY As A Where ETKSERINO='" & strEtiketNo & "'"

            dt = dbcore.RunSql(Sorgu)
            '
            If Not (dt Is Nothing) Then
                With dt
                    If .Rows.Count > 0 Then
                        ' strMalzemeNo = IIf(IsDBNull(.Rows(0).Item("ITNBR")), 0, .Rows(0).Item("ITNBR"))
                        strAltMalmeze = IIf(IsDBNull(.Rows(0).Item("UVMCIM")), 0, .Rows(0).Item("UVMCIM"))
                        Me.ReturnValue.ReturnValue = True
                    Else
                        Me.ReturnValue.ReturnValue = False
                        strAltMalmeze = ""
                    End If
                    '
                End With
                '
                Return strAltMalmeze
            Else
                Return ""
            End If
        Catch ex As Exception
            '
            Me.ReturnValue.Add(ex.Message)
            Me.ReturnValue.ReturnValue = False
            '
            Return ""
        Finally

        End Try
        ''
    End Function

    '
    'verilen bilgileri RF_TRDATA Tablosuna ekler
    Public Function InsertTRData( _
        ByVal PickNo As String, _
        ByVal House As String, _
        ByVal Item As String, _
        ByVal Miktar As Decimal, _
        ByVal OrdNo As String, _
        ByVal OrdSeq As String, _
        ByVal Location As String) As ReturnValue
        '

        Dim iTrnNo As Integer
        '
        Try

            '-- Sonradan her satýrý bekleme yapýlmadan direk eklenmesi için yapýldý
            dbcore.RunSql("Select Max(TRNNO) From RFTRDATA")
            Me.ReturnValue = dbcore.Result
            '
            iTrnNo = CInt("0" & Me.ReturnValue.Others("i"))
            iTrnNo += 1
            '
            dbcore.RunSql( _
                    "Insert Into RFTRDATA ( " & _
                                "WSID, REFNO, " & _
                                "IPLOC, TRFMT, ITNBR, " & _
                                "TRQTY, " & _
                                "TDATE, TTIME, " & _
                                "TRNNO, ACRECP, " & _
                                "ORDNO, LLOCN , SEQNM)" & _
                        " Values ( " & _
                                "'EKIP', '" & _
                                PickNo & "', '" & _
                                House & "', 'SA', '" & _
                                Item & "', " & _
                                Miktar & ", " & _
                                "1" & Now.ToString("yyMMdd") & ", " & _
                                Now.ToString("HHmmss") & ", " & _
                                iTrnNo & ", '', '" & _
                                OrdNo & "', '" & _
                                Location & "', " & _
                                OrdSeq & ") ")

            Me.ReturnValue = dbcore.Result

            If Not Me.ReturnValue.ReturnValue Then
                Throw New Exception("Hata oluþtu")
            End If
            '
            Me.ReturnValue.ReturnValue = True
            Return Me.ReturnValue
        Catch ex As Exception
            '
            Me.ReturnValue.Add(ex.Message)
            Me.ReturnValue.ReturnValue = False
            '
            Return Me.ReturnValue
        Finally

        End Try
        ''
    End Function

    Public Function Message(ByVal Type As String) As MsgBoxResult
        Dim sMessage As String = ""
        Dim sTitle As String = ""
        Dim msgStyl As MsgBoxStyle

        Select Case Type
            Case "SaveQuestion"
                sMessage = "Deðiþiklikleri Kaydetmek Ýstediðinizden Emin misiniz?"
                sTitle = "Kayýt Onayý"
                msgStyl = MsgBoxStyle.YesNoCancel + MsgBoxStyle.Question
                '
            Case "SendCompleted"
                sMessage = "Gönderme Ýþlemi Baþarý Ýle Gerçekleþtirildi"
                sTitle = "Bilgi"
                msgStyl = MsgBoxStyle.Information
                '
            Case "SendNotCompleted"
                sMessage = "Gönderme Ýþlemi Sýrasýnda Hata Oluþtu"
                sTitle = "Bilgi"
                msgStyl = MsgBoxStyle.Exclamation

            Case "PaletMalzemeYok"
                sMessage = "Palete ait malzeme bulunamadý! " & vbNewLine _
                            & "Paleti iptal etmek istiyor musunuz ?"
                sTitle = "Uyarý"
                msgStyl = MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation
                '
            Case "AynýEtiket"
                sMessage = "Ayný etiket tekrar okutulamaz"
                sTitle = "Uyarý"
                msgStyl = MsgBoxStyle.Exclamation
                '
            Case "MalzemeYok"
                sMessage = "Listede sevk edilecek malzeme bulunamadý."
                sTitle = "Uyarý"
                msgStyl = MsgBoxStyle.Exclamation
                '
            Case "SaveCompleted"
                sMessage = "Kaydetme Ýþlemi Baþarý Ýle Gerçekleþtirildi"
                sTitle = "Bilgi"
                msgStyl = MsgBoxStyle.Information
                '
            Case "SaveNotCompleted"
                sMessage = "Kaydetme Ýþlemi Sýrasýnda Hata Oluþtu"
                sTitle = "Bilgi"
                msgStyl = MsgBoxStyle.Exclamation
                '
            Case "DeleteQuestion"
                sMessage = "Seçili Kaydý Silmek Ýstediðinizden Emin misiniz?"
                sTitle = "Silme Onayý"
                msgStyl = MsgBoxStyle.YesNoCancel + MsgBoxStyle.Question

            Case "NotFound"
                sMessage = "Kayýt bulunamadý"
                sTitle = "Bilgi"
                msgStyl = MsgBoxStyle.Exclamation
                '

            Case "IadeMiktarAsimi"
                sMessage = "Ýade edilmek istenen malzeme miktarý alýnandan daha fazla olamaz"
                sTitle = "Uyarý"
                msgStyl = MsgBoxStyle.Exclamation
                '
        End Select

        If sMessage.Trim = "" Then
            sMessage = "Mesaj Bulunamadý"
        End If
        '
        Return MsgBox(sMessage, msgStyl, sTitle)
        ''
    End Function

    Public Sub PaletleriInsert(ByVal PaletNo As String)
        Dim dtPalet As New DataTable

        Dim nPickNo As Integer
        Dim sMalzeme As String = ""
        Dim sAmbalajKodu As String = ""
        Dim sMusteriNo As String = ""
        Dim sPlant As String = ""
        Dim sKapi As String = ""

        Dim dKutuBirimAgirligi, dPaletBirimAgirligi, _
            dSeparatorBirimAgirligi, dKapakBirimAgirligi, dNetAgirlik, dBrutAgirilik, _
            KPMik, SPMik As Decimal

        Dim sPaletTipi As String = ""

        Dim PSay As Double = 0

        Dim HSay As Double = 0

        Dim SSay As Double = 0

        Dim KSay As Double = 0

        Dim PKmik As Double = 0

        Dim PaletKSay As Double = 0

        Try

            Sorgu = " Select Max(Plttype) As PaletTipi, Max(Itnbr) As Malzeme , " & _
                        " Max(PICKNO) As PickNo , Max(Cust) As MusteriNo, " & _
                        " Max(GateId) As Kapi , Max(PLANTID) As Plant" & _
                        " From Etiketdty" & _
                        " Where Pltno=" & PaletNo

            dt = db.RunSql(Sorgu)

            GetRowInfo(sPaletTipi, dt, 0, "PaletTipi")
            GetRowInfo(sMalzeme, dt, 0, "Malzeme")
            GetRowInfo(nPickNo, dt, 0, "PickNo")
            GetRowInfo(sMusteriNo, dt, 0, "MusteriNo")
            GetRowInfo(sKapi, dt, 0, "Kapi")
            GetRowInfo(sPlant, dt, 0, "Plant")

            If sPaletTipi = "MONO" Then
                'Mono Palet Kaydý Ýnsert Ediliyor
                Sorgu = "Insert Into ETIKETDTY" & _
                " SELECT MAx(PICKNO) ,MAx(PICKDT), MAx(CUST), MAx(ETKADR1)" & _
                  ",MAx(ETKADR2), MAx(ETKTIP)      ,0      ,1" & _
                  ",MAx(KPAPTIP), MAx(PPAPTIP), MAx(PLANTID), MAx(GATEID)" & _
                  ",MAx(MURNKOD), MAx(MURNTNM), MAx(ITNBR)      ,Sum(MIKTAR)" & _
                  ",MAx(KKOD), MAx(MKKOD)      ,Sum(KSAY) As KSAY, Sum(KMIK)" & _
                  ",MAx(PKOD), MAx(MPKOD), MAx(PSAY), MAx(PKSIR)" & _
                  ",MAx(PKMIK), MAx(HKSAY), MAx(REVNO), MAx(REVTAR)" & _
                  ",MAx(DUNSNO), MAx(IBRMAGR), MAx(KBRMAGR), MAx(PBRMAGR)" & _
                  ",MAx(KAGOB), MAx(PAGOB), MAx(IAGOB), MAx(SPAGOB)" & _
                  ",MAx(KPAGOB), MAx(SPBRMAGR), MAx(KPBRMAGR), MAx(PUSNO)" & _
                  ",MAx(SEVKTAR), MAx(USERF2), MAx(DNGRCODE), MAx(IRSNO)" & _
                  ",MAx(DOCK), MAx(HNDCODE), MAx(SPPLRADR)      ,Sum(NETWEIGHT)" & _
                  ",Sum(BRTWEIGHT)      ,Sum(BOXES), Sum(QTYPPACK)      , Max(Pltno)" & _
                  ",0      ,'MONO', MAx(L3P), MAx(ORDNO)" & _
                  ",MAx(ORDSEQ), MAx(HOUSE), MAx(KANBANNO), MAx(ONAYKODU)" & _
                  ",MAx(DURUM)" & _
                " FROM ETIKETDTY" & _
                " Where Pltno='" & PaletNo & "'"

                db.RunSql(Sorgu, True)

            ElseIf sPaletTipi = "MIXED" Then
                'Mixed Palet Kaydý Ýnsert Ediliyor
                Sorgu = "Insert Into ETIKETDTY" & _
                " SELECT MAx(PICKNO) ,MAx(PICKDT), MAx(CUST), MAx(ETKADR1)" & _
                  ",MAx(ETKADR2), MAx(ETKTIP)      ,0      ,1" & _
                  ",MAx(KPAPTIP), MAx(PPAPTIP), MAx(PLANTID), MAx(GATEID)" & _
                  ",'MIXED'      ,'MIXED'      ,'MIXED'      ,Sum(MIKTAR)" & _
                  ",MAx(KKOD), MAx(MKKOD)      ,Sum(KSAY) As KSAY, Sum(KMIK)" & _
                  ",MAx(PKOD), MAx(MPKOD), MAx(PSAY), MAx(PKSIR)" & _
                  ",MAx(PKMIK), MAx(HKSAY), MAx(REVNO), MAx(REVTAR)" & _
                  ",MAx(DUNSNO), MAx(IBRMAGR), MAx(KBRMAGR), MAx(PBRMAGR)" & _
                  ",MAx(KAGOB), MAx(PAGOB), MAx(IAGOB), MAx(SPAGOB)" & _
                  ",MAx(KPAGOB), MAx(SPBRMAGR), MAx(KPBRMAGR), MAx(PUSNO)" & _
                  ",MAx(SEVKTAR), MAx(USERF2), MAx(DNGRCODE), MAx(IRSNO)" & _
                  ",MAx(DOCK), MAx(HNDCODE), MAx(SPPLRADR)      ,Sum(NETWEIGHT)" & _
                  ",Sum(BRTWEIGHT)      ,Sum(BOXES), Sum(QTYPPACK)      , Max(Pltno)" & _
                  ",0      ,'MIXED', MAx(L3P), MAx(ORDNO)" & _
                  ",MAx(ORDSEQ), MAx(HOUSE), MAx(KANBANNO), MAx(ONAYKODU)" & _
                  ",MAx(DURUM)" & _
                " FROM ETIKETDTY" & _
                " Where Pltno='" & PaletNo & "'"

                db.RunSql(Sorgu, True)

            End If

            'Insert edilen Palet için Brüt Aðýrlýk hesaplanýyor.
            sAmbalajKodu = sLookup("Ambkod", "Trceklist", "CEKLIST=" & nPickNo & " And ADAITX=" & sTirnakEkle(sMalzeme))

            Sorgu = " Select I.KPMIK, I.SPMIK " & _
                         " FROM   ITMPACK I " & _
                         " where I.Itnbr=" & sTirnakEkle(sMalzeme) & _
                            " And AMBKOD=" & sTirnakEkle(sAmbalajKodu)

            dt = db.RunSql(Sorgu)

            If Not dt Is Nothing Then

                If dt.Rows.Count > 0 Then

                    KPMik = dt.Rows(0).Item("KPMIK").ToString

                    SPMik = dt.Rows(0).Item("SPMIK").ToString

                End If

            End If

            Sorgu = " Select NETWEIGHT, KSAY, KBRMAGR, PBRMAGR, KPBRMAGR, SPBRMAGR" & _
                        " FROM ETIKETDTY" & _
                        " Where ETKSERINO='" & PaletNo & "'"

            dt = db.RunSql(Sorgu)

            If Not dt Is Nothing Then

                If dt.Rows.Count > 0 Then

                    dNetAgirlik = dt.Rows(0).Item("NETWEIGHT").ToString

                    KSay = dt.Rows(0).Item("KSAY").ToString

                    dKutuBirimAgirligi = dt.Rows(0).Item("KBRMAGR").ToString

                    dPaletBirimAgirligi = dt.Rows(0).Item("PBRMAGR").ToString

                    dSeparatorBirimAgirligi = dt.Rows(0).Item("SPBRMAGR").ToString

                    dKapakBirimAgirligi = dt.Rows(0).Item("KPBRMAGR").ToString

                End If

            End If

            dBrutAgirilik = dNetAgirlik + _
                            (KSay * dKutuBirimAgirligi) + _
                            (KPMik * dKapakBirimAgirligi) + _
                            (SPMik * dSeparatorBirimAgirligi) + _
                            (dPaletBirimAgirligi)

            Sorgu = " Update ETIKETDTY" & _
                        " Set BRTWEIGHT=" & dBrutAgirilik & _
                         " Where ETKSERINO=" & sTirnakEkle(PaletNo)

            db.RunSql(Sorgu, True)

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    'Sistemden yeni bir etiketno üretilir ve getirilir
    Public Function UpdateOnayKodu(ByVal strEtiketNo As String) As Boolean
        '
        '
        Try

            '
            dbcore.RunSql( _
                                " Update ETIKETDTY " & _
                                " Set ONAYKODU =1 " & _
                                " Where ETKSERINO='" & strEtiketNo & "'")

            Me.ReturnValue.ReturnValue = dbcore.Result.ReturnValue
            '

            Return Me.ReturnValue.ReturnValue
            '
        Catch ex As Exception
            '
            Me.ReturnValue.Add(ex.Message)
            Me.ReturnValue.ReturnValue = False
            '
            Return ""
        Finally

        End Try
        ''
    End Function

    '
    '
    'Sevkiyat tamamdýðýnda EtiketDty Tablosundaki PltNo alaný güncellenecek
    Public Function UpdatePaletNo(ByVal EtiketNo As String, _
        ByVal PaletNo As String, _
        ByVal PaletTip As String) As ReturnValue
        '

        '
        Try

            '
            dbcore.RunSql( _
                    "Update ETIKETDTY " & _
                        "Set PLTNO = '" & PaletNo & "', " & _
                        "    PLTTYPE = '" & PaletTip & "' " & _
                        "Where ETKSERINO = '" & EtiketNo & "' ", True)

            If dbcore.Result.ReturnValue Then
                '
                Return dbcore.Result

            End If
            '
        Catch ex As Exception
            '
            Me.ReturnValue.Add(ex.Message)
            Me.ReturnValue.ReturnValue = False
            '
            Return dbcore.Result
        Finally

        End Try
        ''
    End Function

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        MyBase.Dispose(disposing)
    End Sub

    '
    '
    Private Sub Add()
        Cursor.Current = Cursors.WaitCursor
        '
        Try
            If ds.Tables("Sevk").Select("etiketNo = '" & txtEtiketNo.Text & "'").Length = 0 Then
                Dim dr As DataRow
                'Dim db As New Sevkiyat
                '
                dr = ds.Tables("Sevk").NewRow
                '
                dr("listeNo") = txtListeNo.Text
                dr("paletNo") = txtPaletNo.Text
                dr("etiketNo") = txtEtiketNo.Text
                dr("Item") = etkBilg.Itnbr
                dr("House") = etkBilg.House
                dr("newCreated") = 0
                dr("PLTTYPE") = "MONO"
                '
                ds.Tables("Sevk").Rows.Add(dr)
                bindData()
                etkBilg = Nothing
                '
                txtEtiketNo.Text = ""
                txtEtiketNo.Focus()
                ds.Tables("Sevk").DefaultView.Sort = "etiketNo desc"
            Else
                Message("AynýEtiket")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
        '
        Cursor.Current = Cursors.Default
    End Sub

    '
    Private Sub bindData()
        '
        With dtgrdMain
            .DataSource = ds.Tables(0)
            '
        End With
        '
        txtMiktar.Text = (ds.Tables("Sevk").Rows.Count)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnCreatePalet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreatePalet.Click
        '
        Cursor.Current = Cursors.WaitCursor
        If Not palet Then
            '
            palet = 1
            txtPaletNo.Text = 1
            btnCreatePalet.Text = "Palet Kapat"
            sL3p = ""
            '
        ElseIf palet Then
            '
            Qty = ds.Tables("Sevk").Select("paletNo = '1'").Length()
            '
            If Not (Qty = 0) Then

                Dim sPaletNo As String
                '
                txtPaletNo.Text = SeriNoAl("ETK")
                sPaletNo = txtPaletNo.Text
                '
                'Palet tipi ayarlanýyor
                Dim sItem As String = ""
                Dim iItemQty As Int16 = 0
                For Each dr As DataRow In ds.Tables("Sevk").Select("paletNo = '1'")
                    '
                    If sItem <> dr("Item") Then
                        iItemQty += 1
                        sItem = dr("Item")
                    End If
                Next
                '
                Dim sPltType As String = ""
                sPltType = IIf(iItemQty <= 1, "MONO", "MIXED")
                For Each dr As DataRow In ds.Tables("Sevk").Select("paletNo = '1'")
                    '
                    dr("paletNo") = txtPaletNo.Text
                    dr("newCreated") = 1
                    dr("PLTTYPE") = sPltType
                Next
                '
                bindData()
                '
                palet = False
                txtPaletNo.Text = ""
                btnCreatePalet.Text = "Palet Aç"
            Else
                If Message("PaletMalzemeYok") = MsgBoxResult.Yes Then
                    palet = False
                    txtPaletNo.Text = ""
                    btnCreatePalet.Text = "Palet Aç"
                End If
            End If
        End If
        '
        txtEtiketNo.Text = ""
        txtEtiketNo.Focus()
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        '
        If Message("DeleteQuestion") = MsgBoxResult.Yes Then
            Cursor.Current = Cursors.WaitCursor
            '
            If ds.Tables("Sevk").Rows.Count > 0 Then
                '
                'Dim lPaletNo As Long
                'lPaletNo = Long.Parse("0" & ds.Tables("Sevk").Rows(dtgrdMain.CurrentRowIndex)("PaletNo"))
                ''
                'If lPaletNo <> 0 And lPaletNo <> 1 Then
                '    '
                '    If Not UpdatePalet(False, lPaletNo) Then
                '        '
                '        Message("SaveNotCompleted")
                '        '
                '    End If
                'End If
                '
                'ds.Tables("Sevk").Rows.RemoveAt(dtgrdMain.CurrentRowIndex)

                For Each row As DataRow In ds.Tables("Sevk").Rows
                    If row("etiketNo").ToString = dtgrdMain.Item(dtgrdMain.CurrentRowIndex, 2).ToString Then
                        row.Delete()
                        Exit For
                    End If
                Next
                '
                bindData()
                '
            End If
            '
            Cursor.Current = Cursors.Default
        End If
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        '
        Me.Enabled = False
        Cursor.Current = Cursors.WaitCursor
        SendData()
        Cursor.Current = Cursors.Default
        Me.Enabled = True
        ''
    End Sub

    '
    Private Sub createTable()
        '
        ds = Nothing
        Dim dt As New DataTable("Sevk")
        '
        dt.Columns.Add("listeNo")
        dt.Columns.Add("paletNo")
        dt.Columns.Add("etiketNo")
        dt.Columns.Add("newCreated")
        dt.Columns.Add("loc")
        dt.Columns.Add("Item")
        dt.Columns.Add("House")
        dt.Columns.Add("PLTTYPE")
        '
        ds = New DataSet
        ds.Tables.Add(dt)
        ''
    End Sub

    '
    Private Sub DeletePalet(ByVal ListNo As Integer)
        Try
            '
            '
            Sorgu = " Update ETIKETDTY" & _
                        " Set PLTNO=0" & _
                        " Where Pickno=" & ListNo

            db.RunSql(Sorgu, True)

            Sorgu = " Delete From ETIKETDTY" & _
                        " Where PickNo=" & ListNo & _
                        " And PLTETK=1"

            db.RunSql(Sorgu, True)

            '
        Catch ex As Exception
            '
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Hata")
            '

        End Try
    End Sub

    '
    Private Function Formclosing() As Boolean
        If Not ds Is Nothing Then
            '
            If palet Or _
                ds.Tables("Sevk").Rows.Count >= 1 Then
                '
                Dim a As MsgBoxResult
                a = Message("SaveQuestion")
                If a = MsgBoxResult.Yes Then
                    '
                    If palet Then
                        '
                        Qty = ds.Tables("Sevk").Select("paletNo = '1'").Length()
                        '
                        If Not (Qty = 0) Then

                            Dim sPaletNo As String
                            '
                            txtPaletNo.Text = SeriNoAl("ETK")
                            sPaletNo = txtPaletNo.Text
                            '
                            For Each dr As DataRow In ds.Tables("Sevk").Select("paletNo = '1'")
                                '
                                dr("paletNo") = txtPaletNo.Text
                                dr("newCreated") = 1
                                '
                            Next
                            '
                            bindData()
                            '
                            palet = False
                            txtPaletNo.Text = ""
                            btnCreatePalet.Text = "Palet Aç"
                        Else
                            If Message("PaletMalzemeYok") = MsgBoxResult.Yes Then
                                palet = False
                                txtPaletNo.Text = ""
                                btnCreatePalet.Text = "Palet Aç"
                            End If
                        End If
                    End If
                    '
                    SendData()
                    '
                ElseIf a = MsgBoxResult.No Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return True
            End If
        Else
            Return True
        End If
    End Function

    '
    Private Sub frmSevkiyat_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Formclosing() Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub frmSevkiyat_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor.Current = Cursors.WaitCursor

        '
        Me.Text = Me.Text & " "
        createTable()
        Cursor.Current = Cursors.Default
    End Sub

    '
    Private Function GetBarkod() As Boolean
        Try
            If txtEtiketNo.Text.Trim <> "" Then

                etkBilg = New EtiketBilgisi

                If txtListeNo.Text = "" Then

                    MsgBox("Çekme No Tanýmsýz", MsgBoxStyle.Exclamation, "Uyarý")

                    txtEtiketNo.Text = ""

                    Return False

                End If

                '
                RetVal = CheckEtiketNo(txtListeNo.Text, txtEtiketNo.Text, etkBilg)
                '
                If etkBilg.Pkmik = 0 Or etkBilg.Pkod = "" Then

                    MsgBox("Bu ürün paletlenmiyor..", MsgBoxStyle.Exclamation, "Uyarý")

                    txtEtiketNo.Text = ""

                    Return False

                End If

                If etkBilg.Pkmik = ds.Tables("Sevk").Select("paletNo = '1'").Length() Then

                    MsgBox("Bu Palet Dolmuþ Durumda Lütfen Palet i Kapatýp Yeni Palet Açýnýz..", MsgBoxStyle.Exclamation, "Uyarý")

                    txtEtiketNo.Text = ""

                    Return False

                End If
                '
                If RetVal.ReturnValue = False Then

                    MsgBox("Etiket Tanýmsýz", MsgBoxStyle.Exclamation, "Uyarý")

                    txtEtiketNo.Text = ""

                    Return False

                End If
                '
                '
                'L3P Karþýlaþtýrmasý
                If sL3p = "" Then sL3p = etkBilg.L3P

                If sL3p.Trim <> etkBilg.L3P.Trim Then
                    MsgBox("Kutu bu palete eklenemez ! ", MsgBoxStyle.Exclamation, "Uyarý")
                    txtEtiketNo.Text = ""
                    Return False
                End If
                '
                Return True
            End If
            '
        Catch ex As Exception
            'Clear
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            '
            Return False
        End Try
        ''
    End Function

    <System.Diagnostics.DebuggerStepThrough> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSevkiyat))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtEtiketNo = New System.Windows.Forms.TextBox
        Me.lblEtiketNo = New System.Windows.Forms.Label
        Me.txtPaletNo = New System.Windows.Forms.TextBox
        Me.lblPaletNo = New System.Windows.Forms.Label
        Me.txtListeNo = New System.Windows.Forms.TextBox
        Me.lblListeNo = New System.Windows.Forms.Label
        Me.btnCreatePalet = New System.Windows.Forms.Button
        Me.btnSend = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.btnDelete = New System.Windows.Forms.Button
        Me.txtMiktar = New System.Windows.Forms.TextBox
        Me.lblMiktar = New System.Windows.Forms.Label
        Me.dtgrdMain = New System.Windows.Forms.DataGrid
        Me.tbSevk = New System.Windows.Forms.DataGridTableStyle
        Me.listNo = New System.Windows.Forms.DataGridTextBoxColumn
        Me.paletNo = New System.Windows.Forms.DataGridTextBoxColumn
        Me.EtiketNo = New System.Windows.Forms.DataGridTextBoxColumn
        Me.miktar = New System.Windows.Forms.DataGridTextBoxColumn
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.dtgrdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtEtiketNo)
        Me.Panel1.Controls.Add(Me.lblEtiketNo)
        Me.Panel1.Controls.Add(Me.txtPaletNo)
        Me.Panel1.Controls.Add(Me.lblPaletNo)
        Me.Panel1.Controls.Add(Me.txtListeNo)
        Me.Panel1.Controls.Add(Me.lblListeNo)
        Me.Panel1.Controls.Add(Me.btnCreatePalet)
        Me.Panel1.Controls.Add(Me.btnSend)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(240, 69)
        Me.Panel1.TabIndex = 1
        '
        'txtEtiketNo
        '
        Me.txtEtiketNo.Location = New System.Drawing.Point(64, 48)
        Me.txtEtiketNo.MaxLength = 15
        Me.txtEtiketNo.Name = "txtEtiketNo"
        Me.txtEtiketNo.Size = New System.Drawing.Size(89, 20)
        Me.txtEtiketNo.TabIndex = 0
        '
        'lblEtiketNo
        '
        Me.lblEtiketNo.Font = New System.Drawing.Font("Tahoma", 7.5!)
        Me.lblEtiketNo.Location = New System.Drawing.Point(5, 52)
        Me.lblEtiketNo.Name = "lblEtiketNo"
        Me.lblEtiketNo.Size = New System.Drawing.Size(53, 15)
        Me.lblEtiketNo.TabIndex = 1
        Me.lblEtiketNo.Text = "Etiket No :"
        '
        'txtPaletNo
        '
        Me.txtPaletNo.Location = New System.Drawing.Point(64, 28)
        Me.txtPaletNo.Name = "txtPaletNo"
        Me.txtPaletNo.Size = New System.Drawing.Size(89, 20)
        Me.txtPaletNo.TabIndex = 2
        '
        'lblPaletNo
        '
        Me.lblPaletNo.Location = New System.Drawing.Point(5, 32)
        Me.lblPaletNo.Name = "lblPaletNo"
        Me.lblPaletNo.Size = New System.Drawing.Size(52, 20)
        Me.lblPaletNo.TabIndex = 3
        Me.lblPaletNo.Text = "Palet No"
        '
        'txtListeNo
        '
        Me.txtListeNo.Location = New System.Drawing.Point(64, 8)
        Me.txtListeNo.Name = "txtListeNo"
        Me.txtListeNo.Size = New System.Drawing.Size(89, 20)
        Me.txtListeNo.TabIndex = 4
        '
        'lblListeNo
        '
        Me.lblListeNo.Location = New System.Drawing.Point(5, 12)
        Me.lblListeNo.Name = "lblListeNo"
        Me.lblListeNo.Size = New System.Drawing.Size(52, 20)
        Me.lblListeNo.TabIndex = 5
        Me.lblListeNo.Text = "Liste No"
        '
        'btnCreatePalet
        '
        Me.btnCreatePalet.Location = New System.Drawing.Point(154, 8)
        Me.btnCreatePalet.Name = "btnCreatePalet"
        Me.btnCreatePalet.Size = New System.Drawing.Size(84, 30)
        Me.btnCreatePalet.TabIndex = 6
        Me.btnCreatePalet.Text = "Palet Aç"
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(154, 37)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(84, 32)
        Me.btnSend.TabIndex = 7
        Me.btnSend.Text = "Gönder"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(168, 176)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 20)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Kapat"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnDelete)
        Me.Panel3.Controls.Add(Me.txtMiktar)
        Me.Panel3.Controls.Add(Me.lblMiktar)
        Me.Panel3.Controls.Add(Me.dtgrdMain)
        Me.Panel3.Controls.Add(Me.btnCancel)
        Me.Panel3.Location = New System.Drawing.Point(0, 70)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(240, 198)
        Me.Panel3.TabIndex = 0
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(128, 176)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(43, 20)
        Me.btnDelete.TabIndex = 0
        Me.btnDelete.Text = "Sil"
        '
        'txtMiktar
        '
        Me.txtMiktar.Location = New System.Drawing.Point(80, 176)
        Me.txtMiktar.Name = "txtMiktar"
        Me.txtMiktar.ReadOnly = True
        Me.txtMiktar.Size = New System.Drawing.Size(48, 20)
        Me.txtMiktar.TabIndex = 1
        '
        'lblMiktar
        '
        Me.lblMiktar.Location = New System.Drawing.Point(6, 176)
        Me.lblMiktar.Name = "lblMiktar"
        Me.lblMiktar.Size = New System.Drawing.Size(74, 13)
        Me.lblMiktar.TabIndex = 2
        Me.lblMiktar.Text = "Kalem Sayýsý"
        '
        'dtgrdMain
        '
        Me.dtgrdMain.DataMember = ""
        Me.dtgrdMain.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dtgrdMain.Location = New System.Drawing.Point(0, 0)
        Me.dtgrdMain.Name = "dtgrdMain"
        Me.dtgrdMain.Size = New System.Drawing.Size(240, 176)
        Me.dtgrdMain.TabIndex = 3
        Me.dtgrdMain.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.tbSevk})
        '
        'tbSevk
        '
        Me.tbSevk.DataGrid = Me.dtgrdMain
        Me.tbSevk.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.listNo, Me.paletNo, Me.EtiketNo, Me.miktar})
        Me.tbSevk.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.tbSevk.MappingName = "Sevk"
        '
        'listNo
        '
        Me.listNo.Format = ""
        Me.listNo.FormatInfo = Nothing
        Me.listNo.HeaderText = "List No"
        Me.listNo.MappingName = "listeNo"
        Me.listNo.NullText = ""
        Me.listNo.Width = 30
        '
        'paletNo
        '
        Me.paletNo.Format = ""
        Me.paletNo.FormatInfo = Nothing
        Me.paletNo.HeaderText = "Palet No"
        Me.paletNo.MappingName = "paletNo"
        Me.paletNo.NullText = ""
        '
        'EtiketNo
        '
        Me.EtiketNo.Format = ""
        Me.EtiketNo.FormatInfo = Nothing
        Me.EtiketNo.HeaderText = "Etiket No"
        Me.EtiketNo.MappingName = "etiketNo"
        Me.EtiketNo.NullText = ""
        '
        'miktar
        '
        Me.miktar.Format = ""
        Me.miktar.FormatInfo = Nothing
        Me.miktar.HeaderText = "Mik."
        Me.miktar.MappingName = "miktar"
        Me.miktar.NullText = ""
        Me.miktar.Width = 30
        '
        'frmSevkiyat
        '
        Me.ClientSize = New System.Drawing.Size(240, 271)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(248, 305)
        Me.Name = "frmSevkiyat"
        Me.Text = "Sevkiyat"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.dtgrdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    Private Function PaletKontrol(ByVal PickNo As String) As Boolean
        Try

            Sorgu = "Select 1" & _
                        " From ETIKETDTY" & _
                        " Where PICKNO =" & PickNo & _
                        " And PltNo<>0"

            dt = dbcore.RunSql(Sorgu)

            If Not dt Is Nothing AndAlso _
                dt.Rows.Count > 0 Then

                MessageBox.Show(PickNo & " nolu Picklist daha önce paletlenmiþ...", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                PaletKontrol = False

            Else

                PaletKontrol = True

            End If

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Function

    '
    Private Sub SendData(Optional ByVal bKapat As Boolean = False)
        Dim QtyToplam As Integer

        Try
            If Not palet Then
                If Not (ds Is Nothing) And _
                (ds.Tables(0).Rows.Count >= 1) Then
                    '
                    For Each row As DataRow In SelectDistinct(ds.Tables(0), "listeNo").Rows

                        RetVal = CheckPickNo(txtListeNo.Text)

                        If RetVal.ReturnValue = False Then

                            MsgBox(RetVal.GetMessages, MsgBoxStyle.Exclamation, "Uyarý")

                            QtyMax = 0

                            Exit Sub

                        End If

                        QtyMax = RetVal.iInfo

                        QtyToplam = QtyToplam + QtyMax

                    Next

                    If QtyToplam <> Integer.Parse(txtMiktar.Text) Then

                        If MsgBox(" Gönderilmesi gereken Kutu Adedi : " & QtyToplam.ToString & vbNewLine & _
                                " Gönderilmek istenen Kutu Adedi : " & txtMiktar.Text & vbNewLine & _
                                " Eksik Kutular Palete Eklenecek mi?", MsgBoxStyle.YesNo, "Ekip Mapics") = MsgBoxResult.Yes Then

                            Exit Sub

                        End If

                    End If

                    DeletePalet(txtListeNo.Text)

                    '
                    Dim ds1 As New DataSet
                    ds1 = ds.Clone
                    ds1.Tables("Sevk").Clear()
                    '
                    For Each dr As DataRow In ds.Tables("Sevk").Rows

                        Dim dr1 As DataRow
                        dr1 = ds1.Tables("Sevk").NewRow
                        '
                        dr1("listeNo") = dr("listeNo")
                        dr1("paletNo") = dr("paletNo")
                        dr1("etiketNo") = dr("etiketNo")
                        dr1("Item") = dr("Item")
                        dr1("House") = dr("House")
                        '

                        ds1.Tables("Sevk").Rows.Add(dr1)
                        '

                        If dr("newCreated") = 0 Then
                            '
                            If Long.Parse("0" & dr("paletNo")) <> 0 And Long.Parse("0" & dr("paletNo")) <> 1 Then
                                '
                                UpdatePalet(dr("etiketNo"), dr("paletNo"), dr("PLTTYPE"))

                            End If
                            '
                        Else
                            '
                            UpdatePalet(dr("etiketNo"), dr("paletNo"), dr("PLTTYPE"))

                        End If
                    Next

                    '
                    'Sevkiyat tamamlanýyor
                    dt = SelectDistinct(ds.Tables("Sevk"), "paletNo")

                    For Each row As DataRow In dt.Rows

                        PaletleriInsert(row("paletNo").ToString)

                    Next

                    Sorgu = "Update ETIKETDTY" & _
                            " Set PLTNO=-1 " & _
                            " Where Pickno=" & txtListeNo.Text & _
                            " And PLTNO=0"

                    db.RunSql(Sorgu, True)

                    txtEtiketNo.Text = ""
                    txtListeNo.Text = ""
                    txtPaletNo.Text = ""
                    Qty = 0
                    palet = False
                    dtgrdMain.DataSource = Nothing
                    '
                    createTable()
                    '
                    Message("SendCompleted")
                    '
                    If bKapat Then
                        Me.Close()
                    End If
                Else
                    If Not bKapat Then
                        Message("MalzemeYok")
                    Else
                        Me.Close()
                    End If
                End If
            Else
                MsgBox("Paleti kapatmadan gönderme yapamazsýnýz!" + MsgBoxStyle.Exclamation, "Uyarý")
            End If

        Catch ex As Exception
            '
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Hata")
            '
        End Try
    End Sub

    '
    Private Sub txtEtiketNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEtiketNo.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = Cursors.WaitCursor
            If txtEtiketNo.Text.Length > 0 Then
                txtEtiketNo.Text = IIf(txtEtiketNo.Text.ToUpper.StartsWith("S"), txtEtiketNo.Text.Remove(0, 1), txtEtiketNo.Text)
                Dim cnt As Integer = 0
                '
                If GetBarkod() Then
                    '
                    cnt = ds.Tables("Sevk").Select("etiketNo = '" & txtEtiketNo.Text & "'").Length()
                    '
                    If cnt = 0 Then
                        'Ekleniyor

                        Add()
                        '
                    Else

                        Message("AynýEtiket")

                        txtEtiketNo.Text = ""

                        txtEtiketNo.Focus()

                    End If

                End If

            Else
                txtEtiketNo.Text = ""
                txtEtiketNo.Focus()
            End If
            '
            Cursor.Current = Cursors.Default
        End If
    End Sub

    '
    Private Sub txtListeNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtListeNo.KeyDown
        If e.KeyCode = Keys.Enter Then
            Cursor.Current = Cursors.WaitCursor
            '
            If txtListeNo.Text.Length > 0 Then
                txtListeNo.Text = IIf((txtListeNo.Text.ToUpper.Substring(0, 1).ToUpper = "L"), _
                                    txtListeNo.Text.Remove(0, 1), txtListeNo.Text)
                txtListeNo.Text = Int(txtListeNo.Text)

                'Sorgu = "select ETKPAL" & _
                '            " from plantprm p " & _
                '            " inner Join " & _
                '                " (" & _
                '                        " Select Distinct C6CANB, C6B9CD " & _
                '                                " FRom trceklist " & _
                '                                " Where Ceklist = " & txtListeNo.Text & _
                '                  " Group By C6CANB, C6B9CD" & _
                '                " ) T" & _
                '                 " On T.C6CANB = p.CANB " & _
                '                 " and T.C6B9CD = p.B9CD "

                'dt = db.RunSql(Sorgu)

                'If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                '    If dt.Rows(0).Item(0).ToString = "1" Then

                '        MessageBox.Show(txtListeNo.Text & " Nolu Çekmenin Paletleme Paremetreleri Otomatik Olarak Ayarlandý.., Buradan Paletleme Yapamazsýnýz...")

                '        txtListeNo.Text = ""

                '        Exit Sub

                '    End If

                'End If

                '
                Try

                    RetVal = CheckPickNo(txtListeNo.Text)
                    '

                    'If PaletKontrol(txtListeNo.Text) = False Then
                    '    txtListeNo.Text = ""
                    '    txtListeNo.Focus()
                    '    Exit Sub

                    'End If

                    If RetVal.ReturnValue = False Then
                        MsgBox(RetVal.GetMessages, MsgBoxStyle.Exclamation, "Uyarý")
                        txtListeNo.Text = ""
                        QtyMax = 0
                        Exit Sub
                    End If
                    QtyMax = RetVal.iInfo

                    'Barkodlu Sevkiyat baþýnda çekmenin paletlerini sýfýrlayýp tüm kutularý harici kutuya çeviriyoruz.

                    Sorgu = " Delete From Etiketdty" & _
                                " Where Pickno=" & txtListeNo.Text & _
                                " And PltEtk=1"

                    db.RunSql(Sorgu)

                    Sorgu = " Update Etiketdty" & _
                                " Set Pltno=-1" & _
                                " Where Pickno=" & txtListeNo.Text

                    db.RunSql(Sorgu)
                    '
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Hata")
                    txtListeNo.Text = ""
                    Exit Sub
                End Try
                '
                txtEtiketNo.Text = ""
                txtEtiketNo.Focus()
            End If
            '
            Cursor.Current = Cursors.Default
        End If
    End Sub

    Private Function UpdatePalet(ByVal etiketNo As String, _
        ByVal paletNo As String, _
        ByVal paletTip As String) As Boolean
        Try
            '
            '
            Return UpdatePaletNo(etiketNo, paletNo, paletTip).ReturnValue
            '
        Catch ex As Exception
            '
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Hata")
            '
            Return False
        End Try
        ''
    End Function

    #End Region 'Methods

    #Region "Other"

    '
    '
    '
    '
    '
    '
    '
    '
    ' Me.ReturnValue = dbCore.Execute( _
    '                        "Insert Into RFTRDATA ( " & _
    '                                    "WSID, REFNO, " & _
    '                                    "TRFMT, ITNBR, " & _
    '                                    "TRQTY, " & _
    '                                    "TDATE, TTIME, " & _
    '                                    "TRNNO, ACRECP, " & _
    '                                    "ORDNO, SEQNM )" & _
    '                            "Values( " & _
    '                                    "'Ekip', '" & _
    '                                    pickno & "', 'SA', '" & _
    '                                    dr("Itnbr") & "', " & _
    '                                    dr("Miktar") & ", " & _
    '                                    "1" & Now.ToString("yyMMdd") & ", " & _
    '                                    Now.ToString("HHmmss") & ", " & _
    '                                    iTrnNo & ", '', '" & _
    '                                    OrdNo & "', '" & _
    '                                    OrdSeq & "', " ) ")
    '
    'Function SeriNoAl(ByVal Type As String, Optional ByVal Artim As Integer = 1) As Double
    '    Dim Sorgu As String
    '    Dim db As New Core.Data(My.Settings.ConnectionString.ToString)
    '    Dim dtSyc As DataTable
    '    Sorgu = " Select Value " & _
    '                        " From TR_Counter" & _
    '                        " Where CountField='" & Type & "'"
    '    dtSyc = db.RunSql(Sorgu)
    '    If Not (dtSyc Is Nothing) AndAlso _
    '                    dtSyc.Rows.Count > 0 Then
    '        SeriNoAl = dtSyc.Rows(0).Item("Value").ToString() + 1
    '        Sorgu = " Update TR_Counter" & _
    '                    " Set Value=" & (dtSyc.Rows(0).Item("Value").ToString() + Artim) & _
    '                    " Where CountField='" & Type & "'"
    '        db.RunSql(Sorgu, True)
    '    Else
    '        Sorgu = " INSERT INTO TR_Counter (CountField,Value)" & _
    '                    " VALUES( '" & Type & "', 1 )"
    '        db.RunSql(Sorgu, True)
    '        SeriNoAl = 1
    '    End If
    'End Function

    #End Region 'Other

End Class