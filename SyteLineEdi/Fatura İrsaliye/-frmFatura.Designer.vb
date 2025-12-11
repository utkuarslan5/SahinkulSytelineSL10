<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFatura
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFatura))
        Me.dtmSevkTarihi2 = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtFaturaTarihi = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtSevkNo2 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtSevkNo1 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkTarihAraligi = New System.Windows.Forms.CheckBox()
        Me.chkGuncelFiyat = New Janus.Windows.EditControls.UICheckBox()
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
        Me.dtpFaturaTarihi = New Janus.Windows.CalendarCombo.CalendarCombo()
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
        Me.dtmSevkTarihi2.Location = New System.Drawing.Point(118, 120)
        Me.dtmSevkTarihi2.Name = "dtmSevkTarihi2"
        Me.dtmSevkTarihi2.Size = New System.Drawing.Size(103, 20)
        Me.dtmSevkTarihi2.TabIndex = 37
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.GroupBox1.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtFaturaTarihi)
        Me.GroupBox1.Controls.Add(Me.txtSevkNo2)
        Me.GroupBox1.Controls.Add(Me.txtSevkNo1)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.chkTarihAraligi)
        Me.GroupBox1.Controls.Add(Me.chkGuncelFiyat)
        Me.GroupBox1.Controls.Add(Me.dtmSevkTarihi2)
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
        Me.GroupBox1.Controls.Add(Me.dtpFaturaTarihi)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(951, 193)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(28, 72)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 13)
        Me.Label8.TabIndex = 45
        Me.Label8.Text = "Fatura Tarihi....:"
        '
        'txtFaturaTarihi
        '
        Me.txtFaturaTarihi.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtFaturaTarihi.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtFaturaTarihi.BackColor = System.Drawing.Color.Yellow
        Me.txtFaturaTarihi.Location = New System.Drawing.Point(118, 68)
        Me.txtFaturaTarihi.Name = "txtFaturaTarihi"
        Me.txtFaturaTarihi.ReadOnly = True
        Me.txtFaturaTarihi.Size = New System.Drawing.Size(88, 20)
        Me.txtFaturaTarihi.TabIndex = 44
        Me.txtFaturaTarihi.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtSevkNo2
        '
        Me.txtSevkNo2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtSevkNo2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtSevkNo2.Location = New System.Drawing.Point(597, 120)
        Me.txtSevkNo2.Name = "txtSevkNo2"
        Me.txtSevkNo2.Size = New System.Drawing.Size(103, 20)
        Me.txtSevkNo2.TabIndex = 41
        Me.txtSevkNo2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtSevkNo1
        '
        Me.txtSevkNo1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtSevkNo1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtSevkNo1.Location = New System.Drawing.Point(457, 120)
        Me.txtSevkNo1.Name = "txtSevkNo1"
        Me.txtSevkNo1.Size = New System.Drawing.Size(103, 20)
        Me.txtSevkNo1.TabIndex = 40
        Me.txtSevkNo1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(358, 124)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 13)
        Me.Label7.TabIndex = 42
        Me.Label7.Text = "Sevk No....:"
        '
        'chkTarihAraligi
        '
        Me.chkTarihAraligi.AutoSize = True
        Me.chkTarihAraligi.BackColor = System.Drawing.Color.Transparent
        Me.chkTarihAraligi.Location = New System.Drawing.Point(223, 97)
        Me.chkTarihAraligi.Name = "chkTarihAraligi"
        Me.chkTarihAraligi.Size = New System.Drawing.Size(81, 17)
        Me.chkTarihAraligi.TabIndex = 39
        Me.chkTarihAraligi.Text = "Tarih Aralığı"
        Me.chkTarihAraligi.UseVisualStyleBackColor = False
        '
        'chkGuncelFiyat
        '
        Me.chkGuncelFiyat.BackColor = System.Drawing.Color.Transparent
        Me.chkGuncelFiyat.Checked = True
        Me.chkGuncelFiyat.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkGuncelFiyat.Location = New System.Drawing.Point(223, 120)
        Me.chkGuncelFiyat.Name = "chkGuncelFiyat"
        Me.chkGuncelFiyat.Size = New System.Drawing.Size(109, 20)
        Me.chkGuncelFiyat.TabIndex = 38
        Me.chkGuncelFiyat.Text = "Fiyatları Güncelle"
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
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(28, 45)
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
        Me.txtTeslimAlan2.Location = New System.Drawing.Point(597, 64)
        Me.txtTeslimAlan2.Name = "txtTeslimAlan2"
        Me.txtTeslimAlan2.Size = New System.Drawing.Size(103, 20)
        Me.txtTeslimAlan2.TabIndex = 6
        Me.txtTeslimAlan2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtTeslimAlan1
        '
        Me.txtTeslimAlan1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtTeslimAlan1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtTeslimAlan1.Location = New System.Drawing.Point(457, 64)
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
        Me.btnOlustur.Location = New System.Drawing.Point(530, 147)
        Me.btnOlustur.Name = "btnOlustur"
        Me.btnOlustur.Size = New System.Drawing.Size(126, 40)
        Me.btnOlustur.TabIndex = 12
        Me.btnOlustur.Text = "Fatura Oluştur"
        Me.btnOlustur.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(28, 110)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Sevk Tarihi....:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(358, 94)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Kapı....:"
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
        Me.Label1.Location = New System.Drawing.Point(28, 19)
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
        Me.dtmSevkTarihi1.Location = New System.Drawing.Point(118, 94)
        Me.dtmSevkTarihi1.Name = "dtmSevkTarihi1"
        Me.dtmSevkTarihi1.Size = New System.Drawing.Size(103, 20)
        Me.dtmSevkTarihi1.TabIndex = 9
        '
        'btnSorgula
        '
        Me.btnSorgula.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnSorgula.Image = Global.SyteLineEdi.My.Resources.Resources.Search
        Me.btnSorgula.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnSorgula.Location = New System.Drawing.Point(398, 147)
        Me.btnSorgula.Name = "btnSorgula"
        Me.btnSorgula.Size = New System.Drawing.Size(126, 40)
        Me.btnSorgula.TabIndex = 0
        Me.btnSorgula.Text = "Sorgula"
        Me.btnSorgula.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'dtpFaturaTarihi
        '
        Me.dtpFaturaTarihi.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.dtpFaturaTarihi.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtpFaturaTarihi.DropDownCalendar.Name = ""
        Me.dtpFaturaTarihi.DropDownCalendar.Size = New System.Drawing.Size(164, 167)
        Me.dtpFaturaTarihi.DropDownCalendar.TabIndex = 0
        Me.dtpFaturaTarihi.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard
        Me.dtpFaturaTarihi.Location = New System.Drawing.Point(118, 68)
        Me.dtpFaturaTarihi.Name = "dtpFaturaTarihi"
        Me.dtpFaturaTarihi.ShowNullButton = True
        Me.dtpFaturaTarihi.Size = New System.Drawing.Size(101, 20)
        Me.dtpFaturaTarihi.TabIndex = 43
        Me.dtpFaturaTarihi.TodayButtonText = "Bugün"
        Me.dtpFaturaTarihi.Value = New Date(1, 2, 1, 0, 0, 0, 0)
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
        Me.GridEX1.Size = New System.Drawing.Size(951, 432)
        Me.GridEX1.TabIndex = 5
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'frmFatura
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(951, 625)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmFatura"
        Me.Text = "Fatura Oluşturma"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtmSevkTarihi2 As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
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
    Friend WithEvents chkGuncelFiyat As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents chkTarihAraligi As System.Windows.Forms.CheckBox
    Friend WithEvents txtSevkNo2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtSevkNo1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpFaturaTarihi As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents txtFaturaTarihi As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
End Class
