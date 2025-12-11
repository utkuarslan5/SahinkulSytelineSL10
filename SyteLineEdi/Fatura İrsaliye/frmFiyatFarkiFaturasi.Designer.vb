<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFiyatFarkiFaturasi
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFiyatFarkiFaturasi))
        Me.dtmSevkTarihi2 = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.txtMusteriAdi1 = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnUnCheckedAll = New Janus.Windows.EditControls.UIButton()
        Me.btnCheckedAll = New Janus.Windows.EditControls.UIButton()
        Me.btnExceleAktar = New Janus.Windows.EditControls.UIButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtmFiyatListesiTarihi1 = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.chkTarihAraligi = New System.Windows.Forms.CheckBox()
        Me.txtMusteri1 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnYazdır = New Janus.Windows.EditControls.UIButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtmSevkTarihi1 = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.btnSorgula = New Janus.Windows.EditControls.UIButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTutarToplam = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtKdvTutari = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtGenelToplam = New System.Windows.Forms.TextBox()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.dtmSevkTarihi2.Location = New System.Drawing.Point(128, 44)
        Me.dtmSevkTarihi2.Name = "dtmSevkTarihi2"
        Me.dtmSevkTarihi2.Size = New System.Drawing.Size(103, 20)
        Me.dtmSevkTarihi2.TabIndex = 10
        '
        'txtMusteriAdi1
        '
        Me.txtMusteriAdi1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMusteriAdi1.Enabled = False
        Me.txtMusteriAdi1.Location = New System.Drawing.Point(390, 44)
        Me.txtMusteriAdi1.Name = "txtMusteriAdi1"
        Me.txtMusteriAdi1.Size = New System.Drawing.Size(209, 20)
        Me.txtMusteriAdi1.TabIndex = 30
        Me.txtMusteriAdi1.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.GroupBox1.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.GroupBox1.Controls.Add(Me.btnUnCheckedAll)
        Me.GroupBox1.Controls.Add(Me.btnCheckedAll)
        Me.GroupBox1.Controls.Add(Me.btnExceleAktar)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.dtmFiyatListesiTarihi1)
        Me.GroupBox1.Controls.Add(Me.chkTarihAraligi)
        Me.GroupBox1.Controls.Add(Me.dtmSevkTarihi2)
        Me.GroupBox1.Controls.Add(Me.txtMusteriAdi1)
        Me.GroupBox1.Controls.Add(Me.txtMusteri1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnYazdır)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.dtmSevkTarihi1)
        Me.GroupBox1.Controls.Add(Me.btnSorgula)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(996, 202)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        '
        'btnUnCheckedAll
        '
        Me.btnUnCheckedAll.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnUnCheckedAll.Image = Global.SyteLineEdi.My.Resources.Resources.Close
        Me.btnUnCheckedAll.Location = New System.Drawing.Point(123, 167)
        Me.btnUnCheckedAll.Name = "btnUnCheckedAll"
        Me.btnUnCheckedAll.Size = New System.Drawing.Size(114, 24)
        Me.btnUnCheckedAll.TabIndex = 64
        Me.btnUnCheckedAll.Text = "Seçileni İptal Et"
        Me.btnUnCheckedAll.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnCheckedAll
        '
        Me.btnCheckedAll.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnCheckedAll.Image = Global.SyteLineEdi.My.Resources.Resources.Checkmark
        Me.btnCheckedAll.Location = New System.Drawing.Point(22, 167)
        Me.btnCheckedAll.Name = "btnCheckedAll"
        Me.btnCheckedAll.Size = New System.Drawing.Size(95, 24)
        Me.btnCheckedAll.TabIndex = 63
        Me.btnCheckedAll.Text = "Tümünü Seç"
        Me.btnCheckedAll.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnExceleAktar
        '
        Me.btnExceleAktar.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnExceleAktar.Image = Global.SyteLineEdi.My.Resources.Resources._202px_Excel2007
        Me.btnExceleAktar.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnExceleAktar.Location = New System.Drawing.Point(286, 117)
        Me.btnExceleAktar.Name = "btnExceleAktar"
        Me.btnExceleAktar.Size = New System.Drawing.Size(126, 40)
        Me.btnExceleAktar.TabIndex = 62
        Me.btnExceleAktar.Text = "Excele Aktar"
        Me.btnExceleAktar.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(19, 86)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(105, 13)
        Me.Label6.TabIndex = 61
        Me.Label6.Text = "Fiyat Listesi Tarihi....:"
        '
        'dtmFiyatListesiTarihi1
        '
        '
        '
        '
        Me.dtmFiyatListesiTarihi1.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtmFiyatListesiTarihi1.DropDownCalendar.Name = ""
        Me.dtmFiyatListesiTarihi1.DropDownCalendar.Size = New System.Drawing.Size(164, 167)
        Me.dtmFiyatListesiTarihi1.DropDownCalendar.TabIndex = 0
        Me.dtmFiyatListesiTarihi1.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard
        Me.dtmFiyatListesiTarihi1.Location = New System.Drawing.Point(128, 83)
        Me.dtmFiyatListesiTarihi1.Name = "dtmFiyatListesiTarihi1"
        Me.dtmFiyatListesiTarihi1.Size = New System.Drawing.Size(103, 20)
        Me.dtmFiyatListesiTarihi1.TabIndex = 20
        '
        'chkTarihAraligi
        '
        Me.chkTarihAraligi.AutoSize = True
        Me.chkTarihAraligi.BackColor = System.Drawing.Color.Transparent
        Me.chkTarihAraligi.Location = New System.Drawing.Point(237, 22)
        Me.chkTarihAraligi.Name = "chkTarihAraligi"
        Me.chkTarihAraligi.Size = New System.Drawing.Size(81, 17)
        Me.chkTarihAraligi.TabIndex = 15
        Me.chkTarihAraligi.Text = "Tarih Aralığı"
        Me.chkTarihAraligi.UseVisualStyleBackColor = False
        '
        'txtMusteri1
        '
        Me.txtMusteri1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtMusteri1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtMusteri1.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtMusteri1.Location = New System.Drawing.Point(390, 18)
        Me.txtMusteri1.Name = "txtMusteri1"
        Me.txtMusteri1.Size = New System.Drawing.Size(103, 20)
        Me.txtMusteri1.TabIndex = 25
        Me.txtMusteri1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(319, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "Müşteri.....:"
        '
        'btnYazdır
        '
        Me.btnYazdır.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnYazdır.Image = Global.SyteLineEdi.My.Resources.Resources.NetByte_Design_Studio___0917
        Me.btnYazdır.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnYazdır.Location = New System.Drawing.Point(154, 117)
        Me.btnYazdır.Name = "btnYazdır"
        Me.btnYazdır.Size = New System.Drawing.Size(126, 40)
        Me.btnYazdır.TabIndex = 60
        Me.btnYazdır.Text = "Fatura Yazdırma"
        Me.btnYazdır.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(19, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Oluşturma Tarihi....:"
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
        Me.dtmSevkTarihi1.Location = New System.Drawing.Point(128, 18)
        Me.dtmSevkTarihi1.Name = "dtmSevkTarihi1"
        Me.dtmSevkTarihi1.Size = New System.Drawing.Size(103, 20)
        Me.dtmSevkTarihi1.TabIndex = 5
        '
        'btnSorgula
        '
        Me.btnSorgula.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnSorgula.Image = Global.SyteLineEdi.My.Resources.Resources.Search
        Me.btnSorgula.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnSorgula.Location = New System.Drawing.Point(22, 117)
        Me.btnSorgula.Name = "btnSorgula"
        Me.btnSorgula.Size = New System.Drawing.Size(126, 40)
        Me.btnSorgula.TabIndex = 55
        Me.btnSorgula.Text = "Sorgula"
        Me.btnSorgula.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(783, 542)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 102
        Me.Label1.Text = "Tutar Toplam............:"
        '
        'txtTutarToplam
        '
        Me.txtTutarToplam.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTutarToplam.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTutarToplam.Enabled = False
        Me.txtTutarToplam.Location = New System.Drawing.Point(902, 539)
        Me.txtTutarToplam.Name = "txtTutarToplam"
        Me.txtTutarToplam.Size = New System.Drawing.Size(94, 20)
        Me.txtTutarToplam.TabIndex = 75
        Me.txtTutarToplam.TabStop = False
        Me.txtTutarToplam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(783, 567)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 13)
        Me.Label3.TabIndex = 104
        Me.Label3.Text = "Kdv (%20) Tutarı........:"
        '
        'txtKdvTutari
        '
        Me.txtKdvTutari.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKdvTutari.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtKdvTutari.Enabled = False
        Me.txtKdvTutari.Location = New System.Drawing.Point(902, 564)
        Me.txtKdvTutari.Name = "txtKdvTutari"
        Me.txtKdvTutari.Size = New System.Drawing.Size(94, 20)
        Me.txtKdvTutari.TabIndex = 80
        Me.txtKdvTutari.TabStop = False
        Me.txtKdvTutari.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(783, 593)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 13)
        Me.Label4.TabIndex = 106
        Me.Label4.Text = "Genel Toplam............:"
        '
        'txtGenelToplam
        '
        Me.txtGenelToplam.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGenelToplam.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtGenelToplam.Enabled = False
        Me.txtGenelToplam.Location = New System.Drawing.Point(902, 590)
        Me.txtGenelToplam.Name = "txtGenelToplam"
        Me.txtGenelToplam.Size = New System.Drawing.Size(94, 20)
        Me.txtGenelToplam.TabIndex = 85
        Me.txtGenelToplam.TabStop = False
        Me.txtGenelToplam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GridEX1
        '
        Me.GridEX1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEXLayout1
        Me.GridEX1.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.Location = New System.Drawing.Point(0, 197)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(996, 336)
        Me.GridEX1.TabIndex = 70
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'frmFiyatFarkiFaturasi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(996, 621)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtGenelToplam)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtKdvTutari)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtTutarToplam)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmFiyatFarkiFaturasi"
        Me.Text = "Fiyat Farkı Faturası"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtmSevkTarihi2 As Janus.Windows.CalendarCombo.CalendarCombo
  Friend WithEvents txtMusteriAdi1 As System.Windows.Forms.TextBox
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents txtMusteri1 As Janus.Windows.GridEX.EditControls.EditBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents btnYazdır As Janus.Windows.EditControls.UIButton
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents dtmSevkTarihi1 As Janus.Windows.CalendarCombo.CalendarCombo
  Friend WithEvents btnSorgula As Janus.Windows.EditControls.UIButton
  Friend WithEvents chkTarihAraligi As System.Windows.Forms.CheckBox
  Friend WithEvents dtmFiyatListesiTarihi1 As Janus.Windows.CalendarCombo.CalendarCombo
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txtTutarToplam As System.Windows.Forms.TextBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents txtKdvTutari As System.Windows.Forms.TextBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents txtGenelToplam As System.Windows.Forms.TextBox
  Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
  Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnExceleAktar As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnUnCheckedAll As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnCheckedAll As Janus.Windows.EditControls.UIButton
End Class
