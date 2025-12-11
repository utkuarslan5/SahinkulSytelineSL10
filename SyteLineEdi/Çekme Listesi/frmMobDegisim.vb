Public Class frmMobDegisim
    Inherits System.Windows.Forms.Form
    '
#Region " IForm Interface "
    Implements AsVol.IForm
    '
    Public Property ConnectionString() As String Implements AsVol.IForm.ConnectionString
        Get
            Return sConnStr
        End Get
        Set(ByVal Value As String)
            sConnStr = Value
        End Set
    End Property

    Public Property ReportPaths() As String Implements AsVol.IForm.ReportPaths
        Get
            Return sReportPath
        End Get
        Set(ByVal Value As String)
            sReportPath = Value
        End Set
    End Property
    '
    Public Shadows Sub Show(ByVal Dialog As Boolean) Implements AsVol.IForm.Show
        If Not Dialog Then
            MyBase.Show()
        Else
            MyBase.ShowDialog()
        End If
    End Sub
    '
#End Region
    '
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        '
        loadConfig()
        '

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents txtBarkodNo As System.Windows.Forms.TextBox
    Friend WithEvents lblBarkodNo As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtYer2 As System.Windows.Forms.TextBox
    Friend WithEvents lblYer2 As System.Windows.Forms.Label
    Friend WithEvents txtYer1 As System.Windows.Forms.TextBox
    Friend WithEvents lblYer1 As System.Windows.Forms.Label
    Friend WithEvents txtLot1 As System.Windows.Forms.TextBox
    Friend WithEvents lblLot1 As System.Windows.Forms.Label
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents txtItem As System.Windows.Forms.TextBox
    Friend WithEvents lblItem As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMobDegisim))
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.txtBarkodNo = New System.Windows.Forms.TextBox
        Me.lblBarkodNo = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnSend = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtYer2 = New System.Windows.Forms.TextBox
        Me.lblYer2 = New System.Windows.Forms.Label
        Me.txtYer1 = New System.Windows.Forms.TextBox
        Me.lblYer1 = New System.Windows.Forms.Label
        Me.txtLot1 = New System.Windows.Forms.TextBox
        Me.lblLot1 = New System.Windows.Forms.Label
        Me.txtQty = New System.Windows.Forms.TextBox
        Me.lblQty = New System.Windows.Forms.Label
        Me.txtItem = New System.Windows.Forms.TextBox
        Me.lblItem = New System.Windows.Forms.Label
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.txtBarkodNo)
        Me.Panel3.Controls.Add(Me.lblBarkodNo)
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(240, 30)
        Me.Panel3.TabIndex = 3
        '
        'txtBarkodNo
        '
        Me.txtBarkodNo.Location = New System.Drawing.Point(80, 8)
        Me.txtBarkodNo.Name = "txtBarkodNo"
        Me.txtBarkodNo.Size = New System.Drawing.Size(152, 20)
        Me.txtBarkodNo.TabIndex = 0
        Me.txtBarkodNo.Text = ""
        '
        'lblBarkodNo
        '
        Me.lblBarkodNo.Location = New System.Drawing.Point(8, 8)
        Me.lblBarkodNo.Name = "lblBarkodNo"
        Me.lblBarkodNo.Size = New System.Drawing.Size(72, 20)
        Me.lblBarkodNo.TabIndex = 1
        Me.lblBarkodNo.Text = "Etiket No"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnClear)
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnSend)
        Me.Panel2.Location = New System.Drawing.Point(0, 136)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(240, 30)
        Me.Panel2.TabIndex = 4
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(64, 5)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(56, 20)
        Me.btnClear.TabIndex = 0
        Me.btnClear.Text = "Temizle"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(120, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(56, 20)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Kapat"
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(176, 5)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(56, 20)
        Me.btnSend.TabIndex = 2
        Me.btnSend.Text = "Kaydet"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtYer2)
        Me.Panel1.Controls.Add(Me.lblYer2)
        Me.Panel1.Controls.Add(Me.txtYer1)
        Me.Panel1.Controls.Add(Me.lblYer1)
        Me.Panel1.Controls.Add(Me.txtLot1)
        Me.Panel1.Controls.Add(Me.lblLot1)
        Me.Panel1.Controls.Add(Me.txtQty)
        Me.Panel1.Controls.Add(Me.lblQty)
        Me.Panel1.Controls.Add(Me.txtItem)
        Me.Panel1.Controls.Add(Me.lblItem)
        Me.Panel1.Location = New System.Drawing.Point(0, 28)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(240, 109)
        Me.Panel1.TabIndex = 5
        '
        'txtYer2
        '
        Me.txtYer2.Location = New System.Drawing.Point(80, 83)
        Me.txtYer2.Name = "txtYer2"
        Me.txtYer2.Size = New System.Drawing.Size(152, 20)
        Me.txtYer2.TabIndex = 0
        Me.txtYer2.Text = "URT"
        '
        'lblYer2
        '
        Me.lblYer2.Location = New System.Drawing.Point(9, 83)
        Me.lblYer2.Name = "lblYer2"
        Me.lblYer2.Size = New System.Drawing.Size(72, 20)
        Me.lblYer2.TabIndex = 1
        Me.lblYer2.Text = "Hedef Yer"
        '
        'txtYer1
        '
        Me.txtYer1.Location = New System.Drawing.Point(80, 43)
        Me.txtYer1.Name = "txtYer1"
        Me.txtYer1.Size = New System.Drawing.Size(152, 20)
        Me.txtYer1.TabIndex = 2
        Me.txtYer1.Text = ""
        '
        'lblYer1
        '
        Me.lblYer1.Location = New System.Drawing.Point(8, 43)
        Me.lblYer1.Name = "lblYer1"
        Me.lblYer1.Size = New System.Drawing.Size(72, 20)
        Me.lblYer1.TabIndex = 3
        Me.lblYer1.Text = "Kaynak Yer"
        '
        'txtLot1
        '
        Me.txtLot1.Location = New System.Drawing.Point(80, 63)
        Me.txtLot1.Name = "txtLot1"
        Me.txtLot1.Size = New System.Drawing.Size(152, 20)
        Me.txtLot1.TabIndex = 4
        Me.txtLot1.Text = ""
        '
        'lblLot1
        '
        Me.lblLot1.Location = New System.Drawing.Point(8, 63)
        Me.lblLot1.Name = "lblLot1"
        Me.lblLot1.Size = New System.Drawing.Size(72, 20)
        Me.lblLot1.TabIndex = 5
        Me.lblLot1.Text = "Kaynak Lot"
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(80, 23)
        Me.txtQty.MaxLength = 6
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(152, 20)
        Me.txtQty.TabIndex = 6
        Me.txtQty.Text = ""
        '
        'lblQty
        '
        Me.lblQty.Location = New System.Drawing.Point(8, 23)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(72, 20)
        Me.lblQty.TabIndex = 7
        Me.lblQty.Text = "Miktar"
        '
        'txtItem
        '
        Me.txtItem.Location = New System.Drawing.Point(80, 3)
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(152, 20)
        Me.txtItem.TabIndex = 8
        Me.txtItem.Text = ""
        '
        'lblItem
        '
        Me.lblItem.Location = New System.Drawing.Point(8, 3)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(72, 20)
        Me.lblItem.TabIndex = 9
        Me.lblItem.Text = "Malzeme"
        '
        'frmMobDegisim
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(240, 173)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMobDegisim"
        Me.Text = "Malzeme Transferi"
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    '
    Private db As Degisim.srvDegisim
    Private Surum As String = "1.0.0.1"
    '
#Region " Methods "
    '
    Private Sub AddDcmove()
        '
        db = New Degisim.srvDegisim
        Dim deg As New Degisim.Degisim
        '
        'Add Params
        deg.dQty = Decimal.Parse(IIf((txtQty.Text.Length = 0), 0, txtQty.Text))
        deg.sEmp_num = sReportPath 'cmbEmpNum.SelectedValue
        deg.sLoc1 = txtYer1.Text
        deg.sLoc2 = txtYer2.Text
        deg.slot1 = txtLot1.Text
        deg.sItem = txtItem.Text
        deg.dEtiketNo = IIf(txtBarkodNo.Text.Trim.Length = 0, 0, txtBarkodNo.Text)
        '
        deg.sStat = "P"
        deg.sTermid = "1"
        deg.sTrans_type = "1" 'IIf((bild.dQty < 0), "2", "1") 'eðer - ise parça iadedir (07.07.2005)
        '
        If db.AddDcmove(deg) Then
            MsgBox("Kayýt Ýþlemi Baþarý Ýle Gerçekleþtirildi", MsgBoxStyle.Information, "Bilgi")
        Else
            MsgBox("Kayýt Ýþlemi Sýrasýnda Hata Oluþtu", MsgBoxStyle.Exclamation, "Hata")
        End If
    End Sub
    '
    Private Sub Clear()
        '
        txtBarkodNo.Text = ""
        txtItem.Text = ""
        txtQty.Text = ""
        '
        If Not txtItem.Enabled Then
            txtYer1.Text = ""
        End If
        '
        'txtYer2.Text = ""
        txtLot1.Text = ""
        txtItem.Focus()
        txtItem.Enabled = True
    End Sub
    '
    '
    'Kullanýcýya sorularak ekleniyor
    Private Function ItemLocValidate( _
        ByVal Whse As String, _
        ByVal Item As String, _
        ByVal Yer As String, _
        ByVal bEkle As Boolean) As Boolean
        '
        Dim db As New Bildirim_Po.srvBildirim_Po
        '
        'Add Params
        '
        Dim retvalue As Bildirim_Po.Return_Validate
        retvalue = db.ItemLocValidate(Whse, Item, Yer)
        If retvalue.ReturnValue Then
            '
            If retvalue.iReturn = -2 Then
                '
                If bEkle Then
                    'If MsgBox(retvalue.sInfoBar & vbNewLine & _
                    '        "Eklemek istiyor musunuz ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Yer Ekleme Onayý") = MsgBoxResult.Yes Then
                    '
                    If db.AddItemLoc(Whse, Item, Yer) Then
                        'Message("SaveCompleted")
                        Return True
                    Else
                        Message("SaveNotCompleted")
                        Return False
                    End If
                    '
                    'End If
                Else
                    MsgBox(retvalue.sInfoBar, MsgBoxStyle.Exclamation, "Ýliþki Bulunamadý")
                    Return False
                End If
                '
            ElseIf retvalue.iReturn = -1 Then
                MsgBox(retvalue.sInfoBar, MsgBoxStyle.Exclamation, "Yer Bulunamadý")
                Return False
                '
            ElseIf retvalue.iReturn = 0 Then
                Return True
            End If
            '
        Else
            MsgBox("Yer sorgulamasý sýrasýnda hata oluþtu", MsgBoxStyle.Exclamation, "Hata")

            Return False
        End If
        '
    End Function
    '
    '
    'Sorgusuz ekleniyor
    Private Function ItemLocValidate1( _
        ByVal Whse As String, _
        ByVal Item As String, _
        ByVal Yer As String) As Boolean
        '
        Dim db As New Bildirim_Po.srvBildirim_Po
        '
        'Add Params
        '
        Dim retvalue As Bildirim_Po.Return_Validate
        retvalue = db.ItemLocValidate(Whse, Item, Yer)
        If retvalue.ReturnValue Then
            '
            If retvalue.iReturn <> 0 Then
                '
                If retvalue.iReturn = -2 Then
                    '
                    If db.AddItemLoc(Whse, Item, Yer) Then
                        'Message("SaveCompleted")
                        Return True
                    Else
                        Message("SaveNotCompleted")
                        Return False
                    End If
                    '
                End If
            End If
            '
            Return True
        Else
            MsgBox("Yer sorgulamasý sýrasýnda hata oluþtu", MsgBoxStyle.Exclamation, "Hata")
            '
            Return False
        End If
        '
    End Function
    '
    Private Function ItemLotValidate(ByVal lot As String, ByVal Item As String) As Boolean
        If lot <> "" Then
            '
            Dim db As New Bildirim_Po.srvBildirim_Po
            '
            'Add Params
            Dim retvalue As Bildirim_Po.Return_Validate
            retvalue = db.ItemLotValidate(lot, Item)
            If retvalue.ReturnValue Then
                '
                If retvalue.iReturn <> 0 Then
                    MsgBox(retvalue.sInfoBar, MsgBoxStyle.Exclamation, "Lot Uyarýsý")
                    '
                    Return False
                    '
                Else
                    Return True
                End If
            End If
            '
            Return True
        Else
            MsgBox("Lot numarasý sorgulamasý sýrasýnda hata oluþtu", MsgBoxStyle.Exclamation, "Hata")
            Return False
        End If
        '
    End Function
    '
    Private Function LotLocQtyValidate( _
        ByVal Whse As String, _
        ByVal Item As String, _
        ByVal Yer As String, _
        ByVal Lot As String, _
        ByVal MasQty As String) As Boolean
        '
        Dim db As New Bildirim_Po.srvBildirim_Po
        '
        'Add Params
        '
        Dim retvalue As Bildirim_Po.Return_Validate
        retvalue = db.LotLocQtyValidate(Whse, Item, Yer, Lot)
        If retvalue.ReturnValue Then
            '
            If retvalue.iReturn = -1 Then
                '
                MsgBox(retvalue.sInfoBar & vbNewLine, MsgBoxStyle.Exclamation)
                Return False
            Else
                If Decimal.Parse(MasQty) > retvalue.iReturn Then
                    MsgBox("Belirtilen yer de bulunan malzeme miktarý : " & retvalue.iReturn.ToString & " " & vbNewLine & _
                            "Göndermek istediðiniz miktar : " & MasQty.ToString & " ", MsgBoxStyle.Exclamation, _
                            "Miktar Aþýmý")
                    '
                    Return False
                Else
                    Return True
                End If
                '
            End If
            '
        Else
            Return False
        End If
        '
    End Function
    '
    Private Function FifoValidate( _
        ByVal Whse As String, _
        ByVal Item As String, _
        ByVal Lot As String) As Boolean
        '
        Dim db As New Bildirim_Po.srvBildirim_Po
        '
        'Add Params
        '
        Dim retvalue As Bildirim_Po.Return_Validate
        retvalue = db.FifoValidate(Whse, Item, Lot)
        If retvalue.ReturnValue Then
            '
            If retvalue.iReturn = -1 Then
                '
                If MsgBox(" Daha önce giriþ yapmýþ veya daha küçük Lotlu malzeme bulundu " & _
                        vbNewLine & retvalue.sInfoBar & vbNewLine & _
                        "Devam etmek istiyor musunuz?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Uyarý") = MsgBoxResult.Yes Then
                    Return True
                Else
                    Return False
                End If
                '
            ElseIf retvalue.iReturn = 0 Then
                Return True
                '
            End If
            '
        Else
            Return False
        End If
        '
    End Function

#End Region
    '
#Region " Events "
    '
    Private Sub txtLot1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLot1.LostFocus
        If Not (txtLot1.Text.Trim.Length = 0) Then
            If Not Me.ItemLotValidate(txtLot1.Text, txtItem.Text) Then
                'txtLot1.Focus()
            End If
        End If
    End Sub
    '
    Private Sub txtYer1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtYer1.LostFocus
        If Not (txtYer1.Text.Trim.Length = 0) And _
           Not (txtItem.Text.Trim.Length = 0) Then
            If Not Me.ItemLocValidate("MAIN", txtItem.Text, txtYer1.Text, False) Then
                'txtYer1.Focus()
            End If
        End If
    End Sub
    '
    Private Sub txtLot1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLot1.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtYer2.Focus()
        End If
    End Sub
    '
    Private Sub txtYer2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtYer2.LostFocus
        If Not (txtYer2.Text.Trim.Length = 0) And _
           Not (txtItem.Text.Trim.Length = 0) Then
            If Me.ItemLocValidate("MAIN", txtItem.Text, txtYer2.Text, True) Then
                Me.ItemLocValidate1("MAIN", txtItem.Text, txtYer2.Text)
                '
                LotLocValidate("MAIN", txtItem.Text, txtYer2.Text, txtLot1.Text)
            End If
        End If
    End Sub
    '
    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        '
        If txtYer1.Text.Trim <> "" And _
            txtQty.Text.Length <> 0 And _
            txtYer2.Text.Length <> 0 And _
            txtItem.Text.Length <> 0 Then
            ''
            'If txtYer2.Text.Trim = "URT" Then
            '    If Not Me.FifoValidate("MAIN", txtItem.Text, txtLot1.Text) Then
            '        '
            '        Exit Sub
            '        '
            '    End If
            'End If
            '
            'Yer1, yer2 ye eþit olmamalý
            'MsgBox(txtYer1.Text.CompareTo(txtYer2.Text.Trim))
            If txtYer1.Text.CompareTo(txtYer2.Text.Trim) = 0 Then
                MsgBox("Kaynak yer ve Hedef yer farklý olmalý ... ", MsgBoxStyle.Exclamation, "Yer Hatasý")
                Exit Sub
            End If
            '
            If Me.ItemLocValidate("MAIN", txtItem.Text, txtYer2.Text, True) And _
                Me.ItemLotValidate(txtLot1.Text, txtItem.Text) And _
                Me.ItemLocValidate1("MAIN", txtItem.Text, txtYer2.Text) And _
                Me.ItemLocValidate("MAIN", txtItem.Text, txtYer1.Text, False) And _
                Me.LotLocQtyValidate("MAIN", txtItem.Text, txtYer1.Text, txtLot1.Text, txtQty.Text) Then
                '
                AddDcmove()
                '
                'Clear
                Clear()
                '
            End If
            '
        Else
            MsgBox("Lütfen bilgileri eksiksiz giriniz.", MsgBoxStyle.Exclamation, "Uyarý")
        End If
    End Sub
    ' 
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    '
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Clear()
        Clear()
    End Sub
    '
    Private Sub txtItem_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtItem.KeyDown
        If e.KeyCode = Keys.Enter Then
            '
            txtQty.Focus()
        End If
    End Sub
    '
    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        ''
        'If txtQty.Text.ToUpper.StartsWith("Q") Then
        '    txtQty.Text = txtQty.Text.Remove(0, 1)
        'End If
        '
        txtQty.Text = txtQty.Text.Replace(".", "")
        txtQty.SelectionStart = txtQty.Text.Length
        '
    End Sub

    Private Sub txtQty_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQty.KeyDown

        If e.KeyCode = Keys.Enter Then
            '
            If txtYer1.Text.Length > 0 Then
                txtLot1.Focus()
            Else
                txtYer1.Focus()
            End If
            '
        End If
    End Sub
    '
    Private Sub txtItem_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtItem.TextChanged
        ''
        'If txtItem.Text.ToUpper.StartsWith("P") Then
        '    txtItem.Text = txtItem.Text.Remove(0, 1)
        'End If
    End Sub

    Private Sub txtLot1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLot1.TextChanged
        ''
        'If txtLot1.Text.ToUpper.StartsWith("S") Or txtLot1.Text.ToUpper.StartsWith("H") Then
        '    txtLot1.Text = txtLot1.Text.Remove(0, 1)
        'End If
    End Sub
    '
    Private Sub txtYer1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtYer1.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtLot1.Focus()
        End If
    End Sub
    '
    Private Sub txtYer2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtYer2.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnSend.Focus()
        End If
    End Sub

    '
    Private Sub frmDegisim_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '
        txtItem.Focus()
        '
    End Sub
    '
    Private Sub txtBarkodNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBarkodNo.KeyDown
        If e.KeyCode = Keys.Enter Then
            '
            'txtBarkodNo.Text = IIf((txtBarkodNo.Text.ToUpper.Substring(0, 1).ToUpper = "S"), _
            '                       txtBarkodNo.Text.Remove(0, 1), txtBarkodNo.Text)
            ''
            'txtBarkodNo.Text = txtBarkodNo.Text.Replace(".", "")
            'txtBarkodNo.Text = Integer.Parse(txtBarkodNo.Text)
            '
            If Not GirisDuzenle(txtBarkodNo.Text, basGeneral.GirisTip.EtiketNo) Then Exit Sub
            '
            Try
                GetBarkod(txtBarkodNo.Text, _
                            txtItem.Text, _
                            txtLot1.Text, _
                            txtQty.Text, _
                            txtYer1.Text, _
                            1, _
                            "Etiket ambara gelmedi veya sevk edildi")
                '
                txtYer2.Focus()
                '
                txtItem.Enabled = False
                '
            Catch ex As Exception
                MsgBox(ex.ToString)

            End Try
            '
        End If
    End Sub
    '
#End Region
    '

    Private Sub txtBarkodNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBarkodNo.TextChanged

    End Sub
End Class
