<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIrsaliye
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIrsaliye))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnExceleAktar = New Janus.Windows.EditControls.UIButton()
        Me.chkTarihAraligi = New System.Windows.Forms.CheckBox()
        Me.dtmSevkTarihi2 = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.UıGroupBox2 = New Janus.Windows.EditControls.UIGroupBox()
        Me.rdbYenidenBasim = New System.Windows.Forms.RadioButton()
        Me.rdbIlkBasim = New System.Windows.Forms.RadioButton()
        Me.txtMusteriAdi2 = New System.Windows.Forms.TextBox()
        Me.txtMusteriAdi1 = New System.Windows.Forms.TextBox()
        Me.txtMusteri2 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtMusteri1 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtKullanici = New Janus.Windows.GridEX.EditControls.EditBox()
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
        Me.dtmSevkTarihi1 = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.btnSorgula = New Janus.Windows.EditControls.UIButton()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.txtGefcoDocument = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.UıGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UıGroupBox2.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtGefcoDocument)
        Me.GroupBox1.Controls.Add(Me.btnExceleAktar)
        Me.GroupBox1.Controls.Add(Me.chkTarihAraligi)
        Me.GroupBox1.Controls.Add(Me.dtmSevkTarihi2)
        Me.GroupBox1.Controls.Add(Me.UıGroupBox2)
        Me.GroupBox1.Controls.Add(Me.txtMusteriAdi2)
        Me.GroupBox1.Controls.Add(Me.txtMusteriAdi1)
        Me.GroupBox1.Controls.Add(Me.txtMusteri2)
        Me.GroupBox1.Controls.Add(Me.txtMusteri1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtKullanici)
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
        Me.GroupBox1.Controls.Add(Me.dtmSevkTarihi1)
        Me.GroupBox1.Controls.Add(Me.btnSorgula)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1005, 193)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'btnExceleAktar
        '
        Me.btnExceleAktar.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnExceleAktar.Image = Global.SyteLineEdi.My.Resources.Resources._202px_Excel2007
        Me.btnExceleAktar.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnExceleAktar.Location = New System.Drawing.Point(563, 142)
        Me.btnExceleAktar.Name = "btnExceleAktar"
        Me.btnExceleAktar.Size = New System.Drawing.Size(126, 40)
        Me.btnExceleAktar.TabIndex = 41
        Me.btnExceleAktar.Text = "Excele Aktar"
        Me.btnExceleAktar.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'chkTarihAraligi
        '
        Me.chkTarihAraligi.AutoSize = True
        Me.chkTarihAraligi.BackColor = System.Drawing.Color.Transparent
        Me.chkTarihAraligi.Location = New System.Drawing.Point(227, 90)
        Me.chkTarihAraligi.Name = "chkTarihAraligi"
        Me.chkTarihAraligi.Size = New System.Drawing.Size(81, 17)
        Me.chkTarihAraligi.TabIndex = 40
        Me.chkTarihAraligi.Text = "Tarih Aralığı"
        Me.chkTarihAraligi.UseVisualStyleBackColor = False
        '
        'dtmSevkTarihi2
        '
        '
        '
        '
        Me.dtmSevkTarihi2.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtmSevkTarihi2.DropDownCalendar.Name = ""
        Me.dtmSevkTarihi2.DropDownCalendar.Size = New System.Drawing.Size(164, 167)
        Me.dtmSevkTarihi2.DropDownCalendar.TabIndex = 0
        Me.dtmSevkTarihi2.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard
        Me.dtmSevkTarihi2.Enabled = False
        Me.dtmSevkTarihi2.Location = New System.Drawing.Point(118, 120)
        Me.dtmSevkTarihi2.Name = "dtmSevkTarihi2"
        Me.dtmSevkTarihi2.Size = New System.Drawing.Size(103, 20)
        Me.dtmSevkTarihi2.TabIndex = 37
        Me.dtmSevkTarihi2.Value = New Date(1, 2, 1, 0, 0, 0, 0)
        '
        'UıGroupBox2
        '
        Me.UıGroupBox2.Controls.Add(Me.rdbYenidenBasim)
        Me.UıGroupBox2.Controls.Add(Me.rdbIlkBasim)
        Me.UıGroupBox2.Location = New System.Drawing.Point(706, 120)
        Me.UıGroupBox2.Name = "UıGroupBox2"
        Me.UıGroupBox2.Size = New System.Drawing.Size(160, 67)
        Me.UıGroupBox2.TabIndex = 36
        Me.UıGroupBox2.Text = "Durum"
        '
        'rdbYenidenBasim
        '
        Me.rdbYenidenBasim.AutoSize = True
        Me.rdbYenidenBasim.Location = New System.Drawing.Point(18, 45)
        Me.rdbYenidenBasim.Name = "rdbYenidenBasim"
        Me.rdbYenidenBasim.Size = New System.Drawing.Size(95, 17)
        Me.rdbYenidenBasim.TabIndex = 2
        Me.rdbYenidenBasim.Text = "Yeniden Basım"
        Me.rdbYenidenBasim.UseVisualStyleBackColor = True
        '
        'rdbIlkBasim
        '
        Me.rdbIlkBasim.AutoSize = True
        Me.rdbIlkBasim.Checked = True
        Me.rdbIlkBasim.Location = New System.Drawing.Point(18, 22)
        Me.rdbIlkBasim.Name = "rdbIlkBasim"
        Me.rdbIlkBasim.Size = New System.Drawing.Size(67, 17)
        Me.rdbIlkBasim.TabIndex = 0
        Me.rdbIlkBasim.TabStop = True
        Me.rdbIlkBasim.Text = "İlk Basım"
        Me.rdbIlkBasim.UseVisualStyleBackColor = True
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
        Me.Label2.Location = New System.Drawing.Point(358, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "Müşteri.....:"
        '
        'txtKullanici
        '
        Me.txtKullanici.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtKullanici.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtKullanici.Location = New System.Drawing.Point(118, 45)
        Me.txtKullanici.Name = "txtKullanici"
        Me.txtKullanici.Size = New System.Drawing.Size(103, 20)
        Me.txtKullanici.TabIndex = 18
        Me.txtKullanici.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(21, 45)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Kullanıcı....:"
        '
        'btnUnCheckedAll
        '
        Me.btnUnCheckedAll.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnUnCheckedAll.Image = Global.SyteLineEdi.My.Resources.Resources.Close
        Me.btnUnCheckedAll.Location = New System.Drawing.Point(125, 163)
        Me.btnUnCheckedAll.Name = "btnUnCheckedAll"
        Me.btnUnCheckedAll.Size = New System.Drawing.Size(114, 24)
        Me.btnUnCheckedAll.TabIndex = 16
        Me.btnUnCheckedAll.Text = "Seçileni İptal Et"
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
        Me.txtAmbar1.Size = New System.Drawing.Size(65, 20)
        Me.txtAmbar1.TabIndex = 1
        Me.txtAmbar1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'btnOlustur
        '
        Me.btnOlustur.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnOlustur.Image = Global.SyteLineEdi.My.Resources.Resources.Save
        Me.btnOlustur.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnOlustur.Location = New System.Drawing.Point(415, 142)
        Me.btnOlustur.Name = "btnOlustur"
        Me.btnOlustur.Size = New System.Drawing.Size(126, 40)
        Me.btnOlustur.TabIndex = 12
        Me.btnOlustur.Text = "İrsaliye Oluştur"
        Me.btnOlustur.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 87)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Sevk Tarihi....:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(358, 94)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Kapı....:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(358, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Teslim Alan....:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Ambar....:"
        '
        'dtmSevkTarihi1
        '
        '
        '
        '
        Me.dtmSevkTarihi1.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtmSevkTarihi1.DropDownCalendar.Name = ""
        Me.dtmSevkTarihi1.DropDownCalendar.Size = New System.Drawing.Size(164, 167)
        Me.dtmSevkTarihi1.DropDownCalendar.TabIndex = 0
        Me.dtmSevkTarihi1.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard
        Me.dtmSevkTarihi1.Location = New System.Drawing.Point(118, 87)
        Me.dtmSevkTarihi1.Name = "dtmSevkTarihi1"
        Me.dtmSevkTarihi1.Size = New System.Drawing.Size(103, 20)
        Me.dtmSevkTarihi1.TabIndex = 9
        Me.dtmSevkTarihi1.Value = New Date(1, 2, 1, 0, 0, 0, 0)
        '
        'btnSorgula
        '
        Me.btnSorgula.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnSorgula.Image = Global.SyteLineEdi.My.Resources.Resources.Search
        Me.btnSorgula.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnSorgula.Location = New System.Drawing.Point(264, 142)
        Me.btnSorgula.Name = "btnSorgula"
        Me.btnSorgula.Size = New System.Drawing.Size(126, 40)
        Me.btnSorgula.TabIndex = 0
        Me.btnSorgula.Text = "Sorgula"
        Me.btnSorgula.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'GridEX1
        '
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEXLayout1
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.Location = New System.Drawing.Point(0, 193)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(1005, 370)
        Me.GridEX1.TabIndex = 3
        '
        'txtGefcoDocument
        '
        Me.txtGefcoDocument.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtGefcoDocument.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtGefcoDocument.Location = New System.Drawing.Point(457, 120)
        Me.txtGefcoDocument.Name = "txtGefcoDocument"
        Me.txtGefcoDocument.Size = New System.Drawing.Size(103, 20)
        Me.txtGefcoDocument.TabIndex = 42
        Me.txtGefcoDocument.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(358, 126)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 13)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "Gefco Document"
        '
        'frmIrsaliye
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1005, 563)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmIrsaliye"
        Me.Text = "İrsaliye Basım"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.UıGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UıGroupBox2.ResumeLayout(False)
        Me.UıGroupBox2.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtmSevkTarihi2 As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents UıGroupBox2 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents rdbYenidenBasim As System.Windows.Forms.RadioButton
    Friend WithEvents rdbIlkBasim As System.Windows.Forms.RadioButton
    Friend WithEvents txtMusteriAdi2 As System.Windows.Forms.TextBox
    Friend WithEvents txtMusteriAdi1 As System.Windows.Forms.TextBox
    Friend WithEvents txtMusteri2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtMusteri1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtKullanici As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnUnCheckedAll As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnCheckedAll As Janus.Windows.EditControls.UIButton
    Friend WithEvents txtKapi2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtKapi1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtTeslimAlan2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtTeslimAlan1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtAmbar1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents btnOlustur As Janus.Windows.EditControls.UIButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtmSevkTarihi1 As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents btnSorgula As Janus.Windows.EditControls.UIButton
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents chkTarihAraligi As System.Windows.Forms.CheckBox
    Friend WithEvents btnExceleAktar As Janus.Windows.EditControls.UIButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtGefcoDocument As Janus.Windows.GridEX.EditControls.EditBox
End Class
