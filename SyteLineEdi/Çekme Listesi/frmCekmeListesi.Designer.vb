<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCekmeListesi
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim GridEXLayout2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCekmeListesi))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkDegisken = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.UýGroupBox1 = New Janus.Windows.EditControls.UIGroupBox()
        Me.rdbMalzeme = New System.Windows.Forms.RadioButton()
        Me.rdbKonsolide = New System.Windows.Forms.RadioButton()
        Me.txtMalzeme2 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtMalzeme1 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.UýGroupBox2 = New Janus.Windows.EditControls.UIGroupBox()
        Me.rdbTumu = New System.Windows.Forms.RadioButton()
        Me.rdbCekmeyeUygun = New System.Windows.Forms.RadioButton()
        Me.txtMusteriAdi2 = New System.Windows.Forms.TextBox()
        Me.txtMusteriAdi1 = New System.Windows.Forms.TextBox()
        Me.txtMusteri2 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtMusteri1 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPusno = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnUnCheckedAll = New Janus.Windows.EditControls.UIButton()
        Me.btnCheckedAll = New Janus.Windows.EditControls.UIButton()
        Me.txtKapi2 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtKapi1 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtTeslimAlan2 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtTeslimAlan1 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtAmbar1 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.btnOlustur = New Janus.Windows.EditControls.UIButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtmTeslimTarihi = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.btnSorgula = New Janus.Windows.EditControls.UIButton()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1.SuspendLayout()
        CType(Me.UýGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UýGroupBox1.SuspendLayout()
        CType(Me.UýGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UýGroupBox2.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.GroupBox1.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.GroupBox1.Controls.Add(Me.chkDegisken)
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Controls.Add(Me.UýGroupBox1)
        Me.GroupBox1.Controls.Add(Me.txtMalzeme2)
        Me.GroupBox1.Controls.Add(Me.txtMalzeme1)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.UýGroupBox2)
        Me.GroupBox1.Controls.Add(Me.txtMusteriAdi2)
        Me.GroupBox1.Controls.Add(Me.txtMusteriAdi1)
        Me.GroupBox1.Controls.Add(Me.txtMusteri2)
        Me.GroupBox1.Controls.Add(Me.txtMusteri1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtPusno)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.btnUnCheckedAll)
        Me.GroupBox1.Controls.Add(Me.btnCheckedAll)
        Me.GroupBox1.Controls.Add(Me.txtKapi2)
        Me.GroupBox1.Controls.Add(Me.txtKapi1)
        Me.GroupBox1.Controls.Add(Me.txtTeslimAlan2)
        Me.GroupBox1.Controls.Add(Me.txtTeslimAlan1)
        Me.GroupBox1.Controls.Add(Me.txtAmbar1)
        Me.GroupBox1.Controls.Add(Me.btnOlustur)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtmTeslimTarihi)
        Me.GroupBox1.Controls.Add(Me.btnSorgula)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(984, 193)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'chkDegisken
        '
        Me.chkDegisken.AutoSize = True
        Me.chkDegisken.BackColor = System.Drawing.Color.Transparent
        Me.chkDegisken.Location = New System.Drawing.Point(118, 121)
        Me.chkDegisken.Name = "chkDegisken"
        Me.chkDegisken.Size = New System.Drawing.Size(103, 17)
        Me.chkDegisken.TabIndex = 42
        Me.chkDegisken.Text = "Deðiþken Miktar"
        Me.chkDegisken.UseVisualStyleBackColor = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox1.Location = New System.Drawing.Point(228, 97)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox1.TabIndex = 41
        Me.CheckBox1.UseVisualStyleBackColor = False
        '
        'UýGroupBox1
        '
        Me.UýGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.UýGroupBox1.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.UýGroupBox1.Controls.Add(Me.rdbMalzeme)
        Me.UýGroupBox1.Controls.Add(Me.rdbKonsolide)
        Me.UýGroupBox1.Location = New System.Drawing.Point(659, 121)
        Me.UýGroupBox1.Name = "UýGroupBox1"
        Me.UýGroupBox1.Size = New System.Drawing.Size(134, 67)
        Me.UýGroupBox1.TabIndex = 40
        Me.UýGroupBox1.Text = "Konsolidasyon"
        '
        'rdbMalzeme
        '
        Me.rdbMalzeme.AutoSize = True
        Me.rdbMalzeme.BackColor = System.Drawing.Color.Transparent
        Me.rdbMalzeme.Location = New System.Drawing.Point(18, 45)
        Me.rdbMalzeme.Name = "rdbMalzeme"
        Me.rdbMalzeme.Size = New System.Drawing.Size(108, 17)
        Me.rdbMalzeme.TabIndex = 2
        Me.rdbMalzeme.Text = "Malzeme Bazýnda"
        Me.rdbMalzeme.UseVisualStyleBackColor = False
        '
        'rdbKonsolide
        '
        Me.rdbKonsolide.AutoSize = True
        Me.rdbKonsolide.BackColor = System.Drawing.Color.Transparent
        Me.rdbKonsolide.Checked = True
        Me.rdbKonsolide.Location = New System.Drawing.Point(18, 21)
        Me.rdbKonsolide.Name = "rdbKonsolide"
        Me.rdbKonsolide.Size = New System.Drawing.Size(71, 17)
        Me.rdbKonsolide.TabIndex = 0
        Me.rdbKonsolide.TabStop = True
        Me.rdbKonsolide.Text = "Konsolide"
        Me.rdbKonsolide.UseVisualStyleBackColor = False
        '
        'txtMalzeme2
        '
        Me.txtMalzeme2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtMalzeme2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtMalzeme2.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtMalzeme2.Location = New System.Drawing.Point(228, 71)
        Me.txtMalzeme2.Name = "txtMalzeme2"
        Me.txtMalzeme2.Size = New System.Drawing.Size(103, 20)
        Me.txtMalzeme2.TabIndex = 38
        Me.txtMalzeme2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtMalzeme1
        '
        Me.txtMalzeme1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtMalzeme1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtMalzeme1.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtMalzeme1.Location = New System.Drawing.Point(119, 71)
        Me.txtMalzeme1.Name = "txtMalzeme1"
        Me.txtMalzeme1.Size = New System.Drawing.Size(103, 20)
        Me.txtMalzeme1.TabIndex = 37
        Me.txtMalzeme1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(20, 71)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 13)
        Me.Label7.TabIndex = 39
        Me.Label7.Text = "Malzeme.....:"
        '
        'UýGroupBox2
        '
        Me.UýGroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.UýGroupBox2.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.UýGroupBox2.Controls.Add(Me.rdbTumu)
        Me.UýGroupBox2.Controls.Add(Me.rdbCekmeyeUygun)
        Me.UýGroupBox2.Location = New System.Drawing.Point(521, 121)
        Me.UýGroupBox2.Name = "UýGroupBox2"
        Me.UýGroupBox2.Size = New System.Drawing.Size(118, 67)
        Me.UýGroupBox2.TabIndex = 36
        Me.UýGroupBox2.Text = "Durum"
        '
        'rdbTumu
        '
        Me.rdbTumu.AutoSize = True
        Me.rdbTumu.BackColor = System.Drawing.Color.Transparent
        Me.rdbTumu.Location = New System.Drawing.Point(18, 45)
        Me.rdbTumu.Name = "rdbTumu"
        Me.rdbTumu.Size = New System.Drawing.Size(52, 17)
        Me.rdbTumu.TabIndex = 2
        Me.rdbTumu.Text = "Tümü"
        Me.rdbTumu.UseVisualStyleBackColor = False
        '
        'rdbCekmeyeUygun
        '
        Me.rdbCekmeyeUygun.AutoSize = True
        Me.rdbCekmeyeUygun.BackColor = System.Drawing.Color.Transparent
        Me.rdbCekmeyeUygun.Checked = True
        Me.rdbCekmeyeUygun.Location = New System.Drawing.Point(18, 22)
        Me.rdbCekmeyeUygun.Name = "rdbCekmeyeUygun"
        Me.rdbCekmeyeUygun.Size = New System.Drawing.Size(91, 17)
        Me.rdbCekmeyeUygun.TabIndex = 0
        Me.rdbCekmeyeUygun.TabStop = True
        Me.rdbCekmeyeUygun.Text = "Açýk Sipariþler"
        Me.rdbCekmeyeUygun.UseVisualStyleBackColor = False
        '
        'txtMusteriAdi2
        '
        Me.txtMusteriAdi2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMusteriAdi2.Enabled = False
        Me.txtMusteriAdi2.Location = New System.Drawing.Point(672, 42)
        Me.txtMusteriAdi2.Name = "txtMusteriAdi2"
        Me.txtMusteriAdi2.Size = New System.Drawing.Size(236, 20)
        Me.txtMusteriAdi2.TabIndex = 35
        '
        'txtMusteriAdi1
        '
        Me.txtMusteriAdi1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMusteriAdi1.Enabled = False
        Me.txtMusteriAdi1.Location = New System.Drawing.Point(457, 42)
        Me.txtMusteriAdi1.Name = "txtMusteriAdi1"
        Me.txtMusteriAdi1.Size = New System.Drawing.Size(209, 20)
        Me.txtMusteriAdi1.TabIndex = 34
        '
        'txtMusteri2
        '
        Me.txtMusteri2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtMusteri2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtMusteri2.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtMusteri2.Location = New System.Drawing.Point(672, 19)
        Me.txtMusteri2.Name = "txtMusteri2"
        Me.txtMusteri2.Size = New System.Drawing.Size(103, 20)
        Me.txtMusteri2.TabIndex = 32
        Me.txtMusteri2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtMusteri1
        '
        Me.txtMusteri1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtMusteri1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtMusteri1.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtMusteri1.Location = New System.Drawing.Point(457, 19)
        Me.txtMusteri1.Name = "txtMusteri1"
        Me.txtMusteri1.Size = New System.Drawing.Size(103, 20)
        Me.txtMusteri1.TabIndex = 31
        Me.txtMusteri1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(358, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "Müþteri.....:"
        '
        'txtPusno
        '
        Me.txtPusno.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtPusno.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtPusno.Location = New System.Drawing.Point(118, 45)
        Me.txtPusno.Name = "txtPusno"
        Me.txtPusno.Size = New System.Drawing.Size(103, 20)
        Me.txtPusno.TabIndex = 18
        Me.txtPusno.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(21, 45)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Pusno....:"
        '
        'btnUnCheckedAll
        '
        Me.btnUnCheckedAll.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnUnCheckedAll.Image = Global.SyteLineEdi.My.Resources.Resources.Close
        Me.btnUnCheckedAll.Location = New System.Drawing.Point(125, 163)
        Me.btnUnCheckedAll.Name = "btnUnCheckedAll"
        Me.btnUnCheckedAll.Size = New System.Drawing.Size(114, 24)
        Me.btnUnCheckedAll.TabIndex = 16
        Me.btnUnCheckedAll.Text = "Seçileni Ýptal Et"
        Me.btnUnCheckedAll.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnCheckedAll
        '
        Me.btnCheckedAll.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnCheckedAll.Image = Global.SyteLineEdi.My.Resources.Resources.Checkmark
        Me.btnCheckedAll.Location = New System.Drawing.Point(24, 163)
        Me.btnCheckedAll.Name = "btnCheckedAll"
        Me.btnCheckedAll.Size = New System.Drawing.Size(95, 24)
        Me.btnCheckedAll.TabIndex = 15
        Me.btnCheckedAll.Text = "Tümünü Seç"
        Me.btnCheckedAll.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'txtKapi2
        '
        Me.txtKapi2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtKapi2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtKapi2.Location = New System.Drawing.Point(597, 94)
        Me.txtKapi2.Name = "txtKapi2"
        Me.txtKapi2.Size = New System.Drawing.Size(103, 20)
        Me.txtKapi2.TabIndex = 8
        Me.txtKapi2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtKapi1
        '
        Me.txtKapi1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtKapi1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtKapi1.Location = New System.Drawing.Point(457, 94)
        Me.txtKapi1.Name = "txtKapi1"
        Me.txtKapi1.Size = New System.Drawing.Size(103, 20)
        Me.txtKapi1.TabIndex = 7
        Me.txtKapi1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtTeslimAlan2
        '
        Me.txtTeslimAlan2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtTeslimAlan2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtTeslimAlan2.Location = New System.Drawing.Point(597, 68)
        Me.txtTeslimAlan2.Name = "txtTeslimAlan2"
        Me.txtTeslimAlan2.Size = New System.Drawing.Size(103, 20)
        Me.txtTeslimAlan2.TabIndex = 6
        Me.txtTeslimAlan2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtTeslimAlan1
        '
        Me.txtTeslimAlan1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtTeslimAlan1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtTeslimAlan1.Location = New System.Drawing.Point(457, 68)
        Me.txtTeslimAlan1.Name = "txtTeslimAlan1"
        Me.txtTeslimAlan1.Size = New System.Drawing.Size(103, 20)
        Me.txtTeslimAlan1.TabIndex = 5
        Me.txtTeslimAlan1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtAmbar1
        '
        Me.txtAmbar1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtAmbar1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtAmbar1.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtAmbar1.Location = New System.Drawing.Point(118, 19)
        Me.txtAmbar1.Name = "txtAmbar1"
        Me.txtAmbar1.Size = New System.Drawing.Size(103, 20)
        Me.txtAmbar1.TabIndex = 1
        Me.txtAmbar1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'btnOlustur
        '
        Me.btnOlustur.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnOlustur.Image = Global.SyteLineEdi.My.Resources.Resources.Save
        Me.btnOlustur.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnOlustur.Location = New System.Drawing.Point(379, 148)
        Me.btnOlustur.Name = "btnOlustur"
        Me.btnOlustur.Size = New System.Drawing.Size(126, 40)
        Me.btnOlustur.TabIndex = 12
        Me.btnOlustur.Text = "Çekme Listesi Oluþtur"
        Me.btnOlustur.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(20, 98)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Teslim Tarihi....:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(358, 94)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Kapý....:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(358, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Teslim Alan....:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(21, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Ambar....:"
        '
        'dtmTeslimTarihi
        '
        '
        '
        '
        Me.dtmTeslimTarihi.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtmTeslimTarihi.DropDownCalendar.Name = ""
        Me.dtmTeslimTarihi.DropDownCalendar.Size = New System.Drawing.Size(164, 167)
        Me.dtmTeslimTarihi.DropDownCalendar.TabIndex = 0
        Me.dtmTeslimTarihi.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard
        Me.dtmTeslimTarihi.Enabled = False
        Me.dtmTeslimTarihi.Location = New System.Drawing.Point(118, 94)
        Me.dtmTeslimTarihi.Name = "dtmTeslimTarihi"
        Me.dtmTeslimTarihi.Size = New System.Drawing.Size(103, 20)
        Me.dtmTeslimTarihi.TabIndex = 9
        '
        'btnSorgula
        '
        Me.btnSorgula.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnSorgula.Image = Global.SyteLineEdi.My.Resources.Resources.Search
        Me.btnSorgula.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnSorgula.Location = New System.Drawing.Point(247, 148)
        Me.btnSorgula.Name = "btnSorgula"
        Me.btnSorgula.Size = New System.Drawing.Size(126, 40)
        Me.btnSorgula.TabIndex = 0
        Me.btnSorgula.Text = "Sorgula"
        Me.btnSorgula.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'GridEX1
        '
        GridEXLayout2.LayoutString = resources.GetString("GridEXLayout2.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEXLayout2
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.Location = New System.Drawing.Point(0, 193)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(984, 260)
        Me.GridEX1.TabIndex = 1
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'frmCekmeListesi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 453)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCekmeListesi"
        Me.Text = "Çekme Listesi"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.UýGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UýGroupBox1.ResumeLayout(False)
        Me.UýGroupBox1.PerformLayout()
        CType(Me.UýGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UýGroupBox2.ResumeLayout(False)
        Me.UýGroupBox2.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnSorgula As Janus.Windows.EditControls.UIButton
    Friend WithEvents dtmTeslimTarihi As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnOlustur As Janus.Windows.EditControls.UIButton
    Friend WithEvents txtKapi2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtKapi1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtTeslimAlan2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtTeslimAlan1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtAmbar1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents btnCheckedAll As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnUnCheckedAll As Janus.Windows.EditControls.UIButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPusno As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtMusteriAdi2 As System.Windows.Forms.TextBox
    Friend WithEvents txtMusteriAdi1 As System.Windows.Forms.TextBox
    Friend WithEvents txtMusteri2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtMusteri1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents UýGroupBox2 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents rdbTumu As System.Windows.Forms.RadioButton
    Friend WithEvents rdbCekmeyeUygun As System.Windows.Forms.RadioButton
    Friend WithEvents txtMalzeme2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtMalzeme1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents UýGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents rdbMalzeme As System.Windows.Forms.RadioButton
    Friend WithEvents rdbKonsolide As System.Windows.Forms.RadioButton
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkDegisken As System.Windows.Forms.CheckBox

End Class
