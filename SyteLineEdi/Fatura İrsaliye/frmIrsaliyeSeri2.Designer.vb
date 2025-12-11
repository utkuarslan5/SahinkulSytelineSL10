<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIrsaliyeSeri2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim GridEXLayout1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIrsaliyeSeri2))
        Dim GridEXLayout2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.txtSeferNo = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtIrsaliyeSeri = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNavlunFatura = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtPlakaNo = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnOlustur = New Janus.Windows.EditControls.UIButton()
        Me.btnKaydet = New Janus.Windows.EditControls.UIButton()
        Me.UıGroupBox1 = New Janus.Windows.EditControls.UIGroupBox()
        Me.dtmFiiliSevk = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.dtmFiiliSevkSaati = New System.Windows.Forms.DateTimePicker()
        Me.dtmFiiliSevkTarihi = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbNakliyeSekli = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtPaletAdet = New Janus.Windows.GridEX.EditControls.NumericEditBox()
        Me.txtKutuAdet = New Janus.Windows.GridEX.EditControls.NumericEditBox()
        Me.txtAciklama = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtTeslimAlan = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtTeslimEden = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbNakliyeci = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbTransport = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        CType(Me.UıGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UıGroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtSeferNo
        '
        Me.txtSeferNo.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtSeferNo.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtSeferNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtSeferNo.Location = New System.Drawing.Point(125, 43)
        Me.txtSeferNo.Name = "txtSeferNo"
        Me.txtSeferNo.Size = New System.Drawing.Size(170, 20)
        Me.txtSeferNo.TabIndex = 22
        Me.txtSeferNo.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(19, 47)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 13)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "Sefer No....:"
        '
        'txtIrsaliyeSeri
        '
        Me.txtIrsaliyeSeri.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtIrsaliyeSeri.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtIrsaliyeSeri.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtIrsaliyeSeri.Location = New System.Drawing.Point(125, 17)
        Me.txtIrsaliyeSeri.Name = "txtIrsaliyeSeri"
        Me.txtIrsaliyeSeri.Size = New System.Drawing.Size(170, 20)
        Me.txtIrsaliyeSeri.TabIndex = 19
        Me.txtIrsaliyeSeri.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "İrsaliye No....:"
        '
        'txtNavlunFatura
        '
        Me.txtNavlunFatura.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtNavlunFatura.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtNavlunFatura.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtNavlunFatura.Location = New System.Drawing.Point(125, 69)
        Me.txtNavlunFatura.Name = "txtNavlunFatura"
        Me.txtNavlunFatura.Size = New System.Drawing.Size(170, 20)
        Me.txtNavlunFatura.TabIndex = 23
        Me.txtNavlunFatura.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtPlakaNo
        '
        Me.txtPlakaNo.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtPlakaNo.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtPlakaNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtPlakaNo.Location = New System.Drawing.Point(125, 95)
        Me.txtPlakaNo.Name = "txtPlakaNo"
        Me.txtPlakaNo.Size = New System.Drawing.Size(170, 20)
        Me.txtPlakaNo.TabIndex = 24
        Me.txtPlakaNo.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 13)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Navlun Fatura No....:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 99)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Plaka No....:"
        '
        'btnOlustur
        '
        Me.btnOlustur.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnOlustur.Image = Global.SyteLineEdi.My.Resources.Resources.Error1
        Me.btnOlustur.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnOlustur.Location = New System.Drawing.Point(213, 438)
        Me.btnOlustur.Name = "btnOlustur"
        Me.btnOlustur.Size = New System.Drawing.Size(82, 32)
        Me.btnOlustur.TabIndex = 28
        Me.btnOlustur.Text = "Vazgeç"
        Me.btnOlustur.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnKaydet
        '
        Me.btnKaydet.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnKaydet.Image = Global.SyteLineEdi.My.Resources.Resources._Select
        Me.btnKaydet.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnKaydet.Location = New System.Drawing.Point(98, 438)
        Me.btnKaydet.Name = "btnKaydet"
        Me.btnKaydet.Size = New System.Drawing.Size(83, 32)
        Me.btnKaydet.TabIndex = 27
        Me.btnKaydet.Text = "Tamam"
        Me.btnKaydet.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'UıGroupBox1
        '
        Me.UıGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.UıGroupBox1.Controls.Add(Me.dtmFiiliSevk)
        Me.UıGroupBox1.Controls.Add(Me.dtmFiiliSevkSaati)
        Me.UıGroupBox1.Controls.Add(Me.dtmFiiliSevkTarihi)
        Me.UıGroupBox1.Controls.Add(Me.Label12)
        Me.UıGroupBox1.Controls.Add(Me.Label13)
        Me.UıGroupBox1.Controls.Add(Me.cmbNakliyeSekli)
        Me.UıGroupBox1.Controls.Add(Me.Label11)
        Me.UıGroupBox1.Controls.Add(Me.Label10)
        Me.UıGroupBox1.Controls.Add(Me.txtPaletAdet)
        Me.UıGroupBox1.Controls.Add(Me.txtKutuAdet)
        Me.UıGroupBox1.Controls.Add(Me.txtAciklama)
        Me.UıGroupBox1.Controls.Add(Me.Label9)
        Me.UıGroupBox1.Controls.Add(Me.txtTeslimAlan)
        Me.UıGroupBox1.Controls.Add(Me.txtTeslimEden)
        Me.UıGroupBox1.Controls.Add(Me.Label8)
        Me.UıGroupBox1.Controls.Add(Me.Label7)
        Me.UıGroupBox1.Controls.Add(Me.Label5)
        Me.UıGroupBox1.Controls.Add(Me.cmbNakliyeci)
        Me.UıGroupBox1.Controls.Add(Me.Label4)
        Me.UıGroupBox1.Controls.Add(Me.cmbTransport)
        Me.UıGroupBox1.Controls.Add(Me.txtIrsaliyeSeri)
        Me.UıGroupBox1.Controls.Add(Me.btnOlustur)
        Me.UıGroupBox1.Controls.Add(Me.Label1)
        Me.UıGroupBox1.Controls.Add(Me.btnKaydet)
        Me.UıGroupBox1.Controls.Add(Me.txtNavlunFatura)
        Me.UıGroupBox1.Controls.Add(Me.txtSeferNo)
        Me.UıGroupBox1.Controls.Add(Me.Label6)
        Me.UıGroupBox1.Controls.Add(Me.Label3)
        Me.UıGroupBox1.Controls.Add(Me.txtPlakaNo)
        Me.UıGroupBox1.Controls.Add(Me.Label2)
        Me.UıGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UıGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.UıGroupBox1.Name = "UıGroupBox1"
        Me.UıGroupBox1.Size = New System.Drawing.Size(374, 482)
        Me.UıGroupBox1.TabIndex = 29
        Me.UıGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'dtmFiiliSevk
        '
        '
        '
        '
        Me.dtmFiiliSevk.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtmFiiliSevk.DropDownCalendar.Name = ""
        Me.dtmFiiliSevk.DropDownCalendar.Size = New System.Drawing.Size(164, 167)
        Me.dtmFiiliSevk.DropDownCalendar.TabIndex = 0
        Me.dtmFiiliSevk.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard
        Me.dtmFiiliSevk.Location = New System.Drawing.Point(125, 121)
        Me.dtmFiiliSevk.Name = "dtmFiiliSevk"
        Me.dtmFiiliSevk.Size = New System.Drawing.Size(103, 20)
        Me.dtmFiiliSevk.TabIndex = 64
        Me.dtmFiiliSevk.Value = New Date(1, 2, 1, 0, 0, 0, 0)
        '
        'dtmFiiliSevkSaati
        '
        Me.dtmFiiliSevkSaati.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtmFiiliSevkSaati.Location = New System.Drawing.Point(125, 147)
        Me.dtmFiiliSevkSaati.Name = "dtmFiiliSevkSaati"
        Me.dtmFiiliSevkSaati.Size = New System.Drawing.Size(170, 20)
        Me.dtmFiiliSevkSaati.TabIndex = 63
        Me.dtmFiiliSevkSaati.Value = New Date(2013, 12, 6, 11, 2, 0, 0)
        '
        'dtmFiiliSevkTarihi
        '
        Me.dtmFiiliSevkTarihi.Location = New System.Drawing.Point(125, 121)
        Me.dtmFiiliSevkTarihi.Name = "dtmFiiliSevkTarihi"
        Me.dtmFiiliSevkTarihi.Size = New System.Drawing.Size(170, 20)
        Me.dtmFiiliSevkTarihi.TabIndex = 62
        Me.dtmFiiliSevkTarihi.Value = New Date(2013, 12, 17, 0, 0, 0, 0)
        Me.dtmFiiliSevkTarihi.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(19, 151)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(94, 13)
        Me.Label12.TabIndex = 61
        Me.Label12.Text = "Fiili Sevk Saati ....:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(19, 125)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(96, 13)
        Me.Label13.TabIndex = 60
        Me.Label13.Text = "Fiili Sevk Tarihi ....:"
        '
        'cmbNakliyeSekli
        '
        Me.cmbNakliyeSekli.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cmbNakliyeSekli.FormattingEnabled = True
        Me.cmbNakliyeSekli.Items.AddRange(New Object() {"Normal Sevkiyat", "Ekstra Sevkiyat", "Bedelsiz Sevkiyat", "Üretim Bakiyesi"})
        Me.cmbNakliyeSekli.Location = New System.Drawing.Point(186, 198)
        Me.cmbNakliyeSekli.Name = "cmbNakliyeSekli"
        Me.cmbNakliyeSekli.Size = New System.Drawing.Size(170, 21)
        Me.cmbNakliyeSekli.TabIndex = 57
        Me.cmbNakliyeSekli.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(19, 204)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(76, 13)
        Me.Label11.TabIndex = 56
        Me.Label11.Text = "Palet Adedi....:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(19, 178)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(74, 13)
        Me.Label10.TabIndex = 55
        Me.Label10.Text = "Kutu Adedi....:"
        '
        'txtPaletAdet
        '
        Me.txtPaletAdet.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtPaletAdet.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtPaletAdet.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtPaletAdet.FormatString = "###,###.#0"
        Me.txtPaletAdet.Location = New System.Drawing.Point(125, 199)
        Me.txtPaletAdet.Name = "txtPaletAdet"
        Me.txtPaletAdet.Size = New System.Drawing.Size(55, 20)
        Me.txtPaletAdet.TabIndex = 54
        Me.txtPaletAdet.Text = ".00"
        Me.txtPaletAdet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtPaletAdet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'txtKutuAdet
        '
        Me.txtKutuAdet.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtKutuAdet.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtKutuAdet.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtKutuAdet.FormatString = "###,###.#0"
        Me.txtKutuAdet.Location = New System.Drawing.Point(125, 173)
        Me.txtKutuAdet.Name = "txtKutuAdet"
        Me.txtKutuAdet.Size = New System.Drawing.Size(55, 20)
        Me.txtKutuAdet.TabIndex = 53
        Me.txtKutuAdet.Text = ".00"
        Me.txtKutuAdet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtKutuAdet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'txtAciklama
        '
        Me.txtAciklama.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtAciklama.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtAciklama.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtAciklama.Location = New System.Drawing.Point(125, 331)
        Me.txtAciklama.Multiline = True
        Me.txtAciklama.Name = "txtAciklama"
        Me.txtAciklama.Size = New System.Drawing.Size(219, 91)
        Me.txtAciklama.TabIndex = 38
        Me.txtAciklama.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(19, 336)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 13)
        Me.Label9.TabIndex = 37
        Me.Label9.Text = "Açıklama....:"
        '
        'txtTeslimAlan
        '
        Me.txtTeslimAlan.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtTeslimAlan.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtTeslimAlan.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtTeslimAlan.Location = New System.Drawing.Point(125, 305)
        Me.txtTeslimAlan.Name = "txtTeslimAlan"
        Me.txtTeslimAlan.Size = New System.Drawing.Size(219, 20)
        Me.txtTeslimAlan.TabIndex = 36
        Me.txtTeslimAlan.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtTeslimEden
        '
        Me.txtTeslimEden.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtTeslimEden.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtTeslimEden.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtTeslimEden.Location = New System.Drawing.Point(125, 278)
        Me.txtTeslimEden.Name = "txtTeslimEden"
        Me.txtTeslimEden.Size = New System.Drawing.Size(219, 20)
        Me.txtTeslimEden.TabIndex = 35
        Me.txtTeslimEden.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(19, 310)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 13)
        Me.Label8.TabIndex = 34
        Me.Label8.Text = "Teslim Alan....:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(19, 282)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 13)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Teslim Eden....:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 255)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Nakliyeci....:"
        '
        'cmbNakliyeci
        '
        Me.cmbNakliyeci.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.cmbNakliyeci.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.cmbNakliyeci.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.cmbNakliyeci.DesignTimeLayout = GridEXLayout1
        Me.cmbNakliyeci.DisplayMember = "description"
        Me.cmbNakliyeci.Location = New System.Drawing.Point(125, 251)
        Me.cmbNakliyeci.Name = "cmbNakliyeci"
        Me.cmbNakliyeci.Size = New System.Drawing.Size(170, 20)
        Me.cmbNakliyeci.TabIndex = 31
        Me.cmbNakliyeci.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.cmbNakliyeci.ValueMember = "ship_code"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 229)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 13)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Nakliye Şekli....:"
        '
        'cmbTransport
        '
        Me.cmbTransport.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.cmbTransport.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.cmbTransport.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        GridEXLayout2.LayoutString = resources.GetString("GridEXLayout2.LayoutString")
        Me.cmbTransport.DesignTimeLayout = GridEXLayout2
        Me.cmbTransport.DisplayMember = "description"
        Me.cmbTransport.Location = New System.Drawing.Point(125, 225)
        Me.cmbTransport.Name = "cmbTransport"
        Me.cmbTransport.Size = New System.Drawing.Size(170, 20)
        Me.cmbTransport.TabIndex = 29
        Me.cmbTransport.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.cmbTransport.ValueMember = "delterm"
        '
        'frmIrsaliyeSeri
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(374, 482)
        Me.Controls.Add(Me.UıGroupBox1)
        Me.Name = "frmIrsaliyeSeri"
        Me.Text = "İrsaliye Seri No Girişi"
        CType(Me.UıGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UıGroupBox1.ResumeLayout(False)
        Me.UıGroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtSeferNo As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtIrsaliyeSeri As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNavlunFatura As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtPlakaNo As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnOlustur As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnKaydet As Janus.Windows.EditControls.UIButton
    Friend WithEvents UıGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbTransport As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbNakliyeci As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents txtTeslimAlan As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtTeslimEden As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAciklama As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtPaletAdet As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents txtKutuAdet As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents cmbNakliyeSekli As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents dtmFiiliSevkSaati As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtmFiiliSevkTarihi As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtmFiiliSevk As Janus.Windows.CalendarCombo.CalendarCombo
End Class
