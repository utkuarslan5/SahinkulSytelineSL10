<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKutuEtiketBasim
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmKutuEtiketBasim))
        Dim GridEXLayout1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.UýGroupBox1 = New Janus.Windows.EditControls.UIGroupBox()
        Me.UýGroupBox5 = New Janus.Windows.EditControls.UIGroupBox()
        Me.chkYazdirma = New System.Windows.Forms.CheckBox()
        Me.grpEtiketTipi = New Janus.Windows.EditControls.UIGroupBox()
        Me.chkPaletEtiketi = New System.Windows.Forms.CheckBox()
        Me.chkKutuEtiketi = New System.Windows.Forms.CheckBox()
        Me.UýGroupBox4 = New Janus.Windows.EditControls.UIGroupBox()
        Me.rdbLaser = New System.Windows.Forms.RadioButton()
        Me.rdbTermal = New System.Windows.Forms.RadioButton()
        Me.UýGroupBox3 = New Janus.Windows.EditControls.UIGroupBox()
        Me.rdbSevkiyat = New System.Windows.Forms.RadioButton()
        Me.rdbCekmeListesi = New System.Windows.Forms.RadioButton()
        Me.btnOlustur = New Janus.Windows.EditControls.UIButton()
        Me.btnSorgula = New Janus.Windows.EditControls.UIButton()
        Me.UýGroupBox2 = New Janus.Windows.EditControls.UIGroupBox()
        Me.rdbTumu = New System.Windows.Forms.RadioButton()
        Me.rdbYazdirilmadi = New System.Windows.Forms.RadioButton()
        Me.rdbYazdirildi = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCeklistNo = New Janus.Windows.GridEX.EditControls.NumericEditBox()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        CType(Me.UýGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UýGroupBox1.SuspendLayout()
        CType(Me.UýGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UýGroupBox5.SuspendLayout()
        CType(Me.grpEtiketTipi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpEtiketTipi.SuspendLayout()
        CType(Me.UýGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UýGroupBox4.SuspendLayout()
        CType(Me.UýGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UýGroupBox3.SuspendLayout()
        CType(Me.UýGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UýGroupBox2.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UýGroupBox1
        '
        Me.UýGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.UýGroupBox1.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.UýGroupBox1.Controls.Add(Me.UýGroupBox5)
        Me.UýGroupBox1.Controls.Add(Me.grpEtiketTipi)
        Me.UýGroupBox1.Controls.Add(Me.UýGroupBox4)
        Me.UýGroupBox1.Controls.Add(Me.UýGroupBox3)
        Me.UýGroupBox1.Controls.Add(Me.btnOlustur)
        Me.UýGroupBox1.Controls.Add(Me.btnSorgula)
        Me.UýGroupBox1.Controls.Add(Me.UýGroupBox2)
        Me.UýGroupBox1.Controls.Add(Me.Label1)
        Me.UýGroupBox1.Controls.Add(Me.txtCeklistNo)
        Me.UýGroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UýGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.UýGroupBox1.Name = "UýGroupBox1"
        Me.UýGroupBox1.Size = New System.Drawing.Size(904, 100)
        Me.UýGroupBox1.TabIndex = 0
        '
        'UýGroupBox5
        '
        Me.UýGroupBox5.Controls.Add(Me.chkYazdirma)
        Me.UýGroupBox5.Location = New System.Drawing.Point(648, 18)
        Me.UýGroupBox5.Name = "UýGroupBox5"
        Me.UýGroupBox5.Size = New System.Drawing.Size(108, 80)
        Me.UýGroupBox5.TabIndex = 20
        Me.UýGroupBox5.Text = "Yazdýrma Seçimi"
        '
        'chkYazdirma
        '
        Me.chkYazdirma.AutoSize = True
        Me.chkYazdirma.Checked = True
        Me.chkYazdirma.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkYazdirma.Location = New System.Drawing.Point(3, 15)
        Me.chkYazdirma.Name = "chkYazdirma"
        Me.chkYazdirma.Size = New System.Drawing.Size(55, 17)
        Me.chkYazdirma.TabIndex = 0
        Me.chkYazdirma.Text = "Yazdýr"
        Me.chkYazdirma.UseVisualStyleBackColor = True
        '
        'grpEtiketTipi
        '
        Me.grpEtiketTipi.Controls.Add(Me.chkPaletEtiketi)
        Me.grpEtiketTipi.Controls.Add(Me.chkKutuEtiketi)
        Me.grpEtiketTipi.Location = New System.Drawing.Point(570, 18)
        Me.grpEtiketTipi.Name = "grpEtiketTipi"
        Me.grpEtiketTipi.Size = New System.Drawing.Size(72, 80)
        Me.grpEtiketTipi.TabIndex = 19
        Me.grpEtiketTipi.Text = "Etiket Tipi"
        '
        'chkPaletEtiketi
        '
        Me.chkPaletEtiketi.AutoSize = True
        Me.chkPaletEtiketi.Location = New System.Drawing.Point(3, 35)
        Me.chkPaletEtiketi.Name = "chkPaletEtiketi"
        Me.chkPaletEtiketi.Size = New System.Drawing.Size(50, 17)
        Me.chkPaletEtiketi.TabIndex = 1
        Me.chkPaletEtiketi.Text = "Palet"
        Me.chkPaletEtiketi.UseVisualStyleBackColor = True
        '
        'chkKutuEtiketi
        '
        Me.chkKutuEtiketi.AutoSize = True
        Me.chkKutuEtiketi.Checked = True
        Me.chkKutuEtiketi.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkKutuEtiketi.Location = New System.Drawing.Point(3, 15)
        Me.chkKutuEtiketi.Name = "chkKutuEtiketi"
        Me.chkKutuEtiketi.Size = New System.Drawing.Size(48, 17)
        Me.chkKutuEtiketi.TabIndex = 0
        Me.chkKutuEtiketi.Text = "Kutu"
        Me.chkKutuEtiketi.UseVisualStyleBackColor = True
        '
        'UýGroupBox4
        '
        Me.UýGroupBox4.Controls.Add(Me.rdbLaser)
        Me.UýGroupBox4.Controls.Add(Me.rdbTermal)
        Me.UýGroupBox4.Location = New System.Drawing.Point(492, 18)
        Me.UýGroupBox4.Name = "UýGroupBox4"
        Me.UýGroupBox4.Size = New System.Drawing.Size(72, 80)
        Me.UýGroupBox4.TabIndex = 18
        Me.UýGroupBox4.Text = "Yazýcý Tipi"
        '
        'rdbLaser
        '
        Me.rdbLaser.AutoSize = True
        Me.rdbLaser.Checked = True
        Me.rdbLaser.Location = New System.Drawing.Point(3, 29)
        Me.rdbLaser.Name = "rdbLaser"
        Me.rdbLaser.Size = New System.Drawing.Size(51, 17)
        Me.rdbLaser.TabIndex = 2
        Me.rdbLaser.TabStop = True
        Me.rdbLaser.Text = "Laser"
        Me.rdbLaser.UseVisualStyleBackColor = True
        '
        'rdbTermal
        '
        Me.rdbTermal.AutoSize = True
        Me.rdbTermal.Location = New System.Drawing.Point(3, 14)
        Me.rdbTermal.Name = "rdbTermal"
        Me.rdbTermal.Size = New System.Drawing.Size(57, 17)
        Me.rdbTermal.TabIndex = 1
        Me.rdbTermal.Text = "Termal"
        Me.rdbTermal.UseVisualStyleBackColor = True
        '
        'UýGroupBox3
        '
        Me.UýGroupBox3.Controls.Add(Me.rdbSevkiyat)
        Me.UýGroupBox3.Controls.Add(Me.rdbCekmeListesi)
        Me.UýGroupBox3.Location = New System.Drawing.Point(393, 18)
        Me.UýGroupBox3.Name = "UýGroupBox3"
        Me.UýGroupBox3.Size = New System.Drawing.Size(93, 80)
        Me.UýGroupBox3.TabIndex = 17
        Me.UýGroupBox3.Text = "Kaynak"
        '
        'rdbSevkiyat
        '
        Me.rdbSevkiyat.AutoSize = True
        Me.rdbSevkiyat.Location = New System.Drawing.Point(3, 29)
        Me.rdbSevkiyat.Name = "rdbSevkiyat"
        Me.rdbSevkiyat.Size = New System.Drawing.Size(66, 17)
        Me.rdbSevkiyat.TabIndex = 2
        Me.rdbSevkiyat.Text = "Sevkiyat"
        Me.rdbSevkiyat.UseVisualStyleBackColor = True
        '
        'rdbCekmeListesi
        '
        Me.rdbCekmeListesi.AutoSize = True
        Me.rdbCekmeListesi.Checked = True
        Me.rdbCekmeListesi.Location = New System.Drawing.Point(3, 14)
        Me.rdbCekmeListesi.Name = "rdbCekmeListesi"
        Me.rdbCekmeListesi.Size = New System.Drawing.Size(90, 17)
        Me.rdbCekmeListesi.TabIndex = 1
        Me.rdbCekmeListesi.TabStop = True
        Me.rdbCekmeListesi.Text = "Çekme Listesi"
        Me.rdbCekmeListesi.UseVisualStyleBackColor = True
        '
        'btnOlustur
        '
        Me.btnOlustur.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnOlustur.Enabled = False
        Me.btnOlustur.Icon = CType(resources.GetObject("btnOlustur.Icon"), System.Drawing.Icon)
        Me.btnOlustur.Image = Global.SyteLineEdi.My.Resources.Resources.SL_Barkod1
        Me.btnOlustur.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnOlustur.Location = New System.Drawing.Point(160, 57)
        Me.btnOlustur.Name = "btnOlustur"
        Me.btnOlustur.Size = New System.Drawing.Size(126, 40)
        Me.btnOlustur.TabIndex = 14
        Me.btnOlustur.Text = "Etiket Oluþtur"
        Me.btnOlustur.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnSorgula
        '
        Me.btnSorgula.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnSorgula.Image = Global.SyteLineEdi.My.Resources.Resources.Search
        Me.btnSorgula.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnSorgula.Location = New System.Drawing.Point(15, 57)
        Me.btnSorgula.Name = "btnSorgula"
        Me.btnSorgula.Size = New System.Drawing.Size(126, 40)
        Me.btnSorgula.TabIndex = 13
        Me.btnSorgula.Text = "Sorgula"
        Me.btnSorgula.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'UýGroupBox2
        '
        Me.UýGroupBox2.Controls.Add(Me.rdbTumu)
        Me.UýGroupBox2.Controls.Add(Me.rdbYazdirilmadi)
        Me.UýGroupBox2.Controls.Add(Me.rdbYazdirildi)
        Me.UýGroupBox2.Location = New System.Drawing.Point(292, 18)
        Me.UýGroupBox2.Name = "UýGroupBox2"
        Me.UýGroupBox2.Size = New System.Drawing.Size(95, 79)
        Me.UýGroupBox2.TabIndex = 3
        Me.UýGroupBox2.Text = "Durum"
        '
        'rdbTumu
        '
        Me.rdbTumu.AutoSize = True
        Me.rdbTumu.Location = New System.Drawing.Point(12, 59)
        Me.rdbTumu.Name = "rdbTumu"
        Me.rdbTumu.Size = New System.Drawing.Size(52, 17)
        Me.rdbTumu.TabIndex = 2
        Me.rdbTumu.Text = "Tümü"
        Me.rdbTumu.UseVisualStyleBackColor = True
        '
        'rdbYazdirilmadi
        '
        Me.rdbYazdirilmadi.AutoSize = True
        Me.rdbYazdirilmadi.Checked = True
        Me.rdbYazdirilmadi.Location = New System.Drawing.Point(12, 37)
        Me.rdbYazdirilmadi.Name = "rdbYazdirilmadi"
        Me.rdbYazdirilmadi.Size = New System.Drawing.Size(80, 17)
        Me.rdbYazdirilmadi.TabIndex = 1
        Me.rdbYazdirilmadi.TabStop = True
        Me.rdbYazdirilmadi.Text = "Yazdýrýlmadý"
        Me.rdbYazdirilmadi.UseVisualStyleBackColor = True
        '
        'rdbYazdirildi
        '
        Me.rdbYazdirildi.AutoSize = True
        Me.rdbYazdirildi.Location = New System.Drawing.Point(12, 15)
        Me.rdbYazdirildi.Name = "rdbYazdirildi"
        Me.rdbYazdirildi.Size = New System.Drawing.Size(66, 17)
        Me.rdbYazdirildi.TabIndex = 0
        Me.rdbYazdirildi.Text = "Yazdýrýldý"
        Me.rdbYazdirildi.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(131, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Cekme Listesi Numarasý...:"
        '
        'txtCeklistNo
        '
        Me.txtCeklistNo.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtCeklistNo.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtCeklistNo.FormatMask = Janus.Windows.GridEX.NumericEditFormatMask.General
        Me.txtCeklistNo.FormatString = "#0"
        Me.txtCeklistNo.Location = New System.Drawing.Point(149, 22)
        Me.txtCeklistNo.Name = "txtCeklistNo"
        Me.txtCeklistNo.Size = New System.Drawing.Size(100, 20)
        Me.txtCeklistNo.TabIndex = 0
        Me.txtCeklistNo.Text = "0"
        Me.txtCeklistNo.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtCeklistNo.ValueType = Janus.Windows.GridEX.NumericEditValueType.Int32
        Me.txtCeklistNo.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'GridEX1
        '
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEXLayout1
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.Location = New System.Drawing.Point(0, 100)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(904, 431)
        Me.GridEX1.TabIndex = 2
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'frmKutuEtiketBasim
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(904, 531)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.UýGroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmKutuEtiketBasim"
        Me.Text = "Kutu Etiket Basýmý"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.UýGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UýGroupBox1.ResumeLayout(False)
        Me.UýGroupBox1.PerformLayout()
        CType(Me.UýGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UýGroupBox5.ResumeLayout(False)
        Me.UýGroupBox5.PerformLayout()
        CType(Me.grpEtiketTipi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpEtiketTipi.ResumeLayout(False)
        Me.grpEtiketTipi.PerformLayout()
        CType(Me.UýGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UýGroupBox4.ResumeLayout(False)
        Me.UýGroupBox4.PerformLayout()
        CType(Me.UýGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UýGroupBox3.ResumeLayout(False)
        Me.UýGroupBox3.PerformLayout()
        CType(Me.UýGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UýGroupBox2.ResumeLayout(False)
        Me.UýGroupBox2.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UýGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents txtCeklistNo As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UýGroupBox2 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents rdbYazdirilmadi As System.Windows.Forms.RadioButton
    Friend WithEvents rdbYazdirildi As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTumu As System.Windows.Forms.RadioButton
    Friend WithEvents btnOlustur As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSorgula As Janus.Windows.EditControls.UIButton
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents UýGroupBox3 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents rdbSevkiyat As System.Windows.Forms.RadioButton
    Friend WithEvents rdbCekmeListesi As System.Windows.Forms.RadioButton
    Friend WithEvents UýGroupBox4 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents rdbLaser As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTermal As System.Windows.Forms.RadioButton
    Friend WithEvents grpEtiketTipi As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents chkPaletEtiketi As System.Windows.Forms.CheckBox
    Friend WithEvents chkKutuEtiketi As System.Windows.Forms.CheckBox
    Friend WithEvents UýGroupBox5 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents chkYazdirma As System.Windows.Forms.CheckBox
End Class
