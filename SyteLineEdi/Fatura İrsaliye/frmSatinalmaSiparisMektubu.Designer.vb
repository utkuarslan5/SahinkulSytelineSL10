<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSatinalmaSiparisMektubu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSatinalmaSiparisMektubu))
        Me.dtmSiparis = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbPlanci2 = New System.Windows.Forms.ComboBox()
        Me.cbPlanci1 = New System.Windows.Forms.ComboBox()
        Me.UıGroupBox1 = New Janus.Windows.EditControls.UIGroupBox()
        Me.rbTumu2 = New System.Windows.Forms.RadioButton()
        Me.rbAcilmis = New System.Windows.Forms.RadioButton()
        Me.rbPlanli = New System.Windows.Forms.RadioButton()
        Me.UıGroupBox2 = New Janus.Windows.EditControls.UIGroupBox()
        Me.rbTumu1 = New System.Windows.Forms.RadioButton()
        Me.rbYenidenBasim = New System.Windows.Forms.RadioButton()
        Me.rbIlkBasim = New System.Windows.Forms.RadioButton()
        Me.txtTedarikciAdi2 = New System.Windows.Forms.TextBox()
        Me.txtTedarikciAdi1 = New System.Windows.Forms.TextBox()
        Me.txtMalzeme2 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtTedarikci2 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtMalzeme1 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtTedarikci1 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnYazdır = New Janus.Windows.EditControls.UIButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnSorgula = New Janus.Windows.EditControls.UIButton()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.cbSiparis1 = New System.Windows.Forms.ComboBox()
        Me.cbSiparis2 = New System.Windows.Forms.ComboBox()
        Me.cbAktif = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.UıGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UıGroupBox1.SuspendLayout()
        CType(Me.UıGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UıGroupBox2.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtmSiparis
        '
        '
        '
        '
        Me.dtmSiparis.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtmSiparis.DropDownCalendar.Name = ""
        Me.dtmSiparis.DropDownCalendar.Size = New System.Drawing.Size(164, 167)
        Me.dtmSiparis.DropDownCalendar.TabIndex = 0
        Me.dtmSiparis.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard
        Me.dtmSiparis.Location = New System.Drawing.Point(111, 153)
        Me.dtmSiparis.Name = "dtmSiparis"
        Me.dtmSiparis.Size = New System.Drawing.Size(103, 20)
        Me.dtmSiparis.TabIndex = 37
        Me.dtmSiparis.Value = New Date(2010, 12, 30, 0, 0, 0, 0)
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.GroupBox1.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.GroupBox1.Controls.Add(Me.cbAktif)
        Me.GroupBox1.Controls.Add(Me.cbSiparis2)
        Me.GroupBox1.Controls.Add(Me.cbPlanci2)
        Me.GroupBox1.Controls.Add(Me.cbSiparis1)
        Me.GroupBox1.Controls.Add(Me.cbPlanci1)
        Me.GroupBox1.Controls.Add(Me.dtmSiparis)
        Me.GroupBox1.Controls.Add(Me.UıGroupBox1)
        Me.GroupBox1.Controls.Add(Me.UıGroupBox2)
        Me.GroupBox1.Controls.Add(Me.txtTedarikciAdi2)
        Me.GroupBox1.Controls.Add(Me.txtTedarikciAdi1)
        Me.GroupBox1.Controls.Add(Me.txtMalzeme2)
        Me.GroupBox1.Controls.Add(Me.txtTedarikci2)
        Me.GroupBox1.Controls.Add(Me.txtMalzeme1)
        Me.GroupBox1.Controls.Add(Me.txtTedarikci1)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnYazdır)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.btnSorgula)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1025, 193)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'cbPlanci2
        '
        Me.cbPlanci2.FormattingEnabled = True
        Me.cbPlanci2.Location = New System.Drawing.Point(326, 66)
        Me.cbPlanci2.Name = "cbPlanci2"
        Me.cbPlanci2.Size = New System.Drawing.Size(103, 21)
        Me.cbPlanci2.TabIndex = 39
        '
        'cbPlanci1
        '
        Me.cbPlanci1.FormattingEnabled = True
        Me.cbPlanci1.Location = New System.Drawing.Point(111, 66)
        Me.cbPlanci1.Name = "cbPlanci1"
        Me.cbPlanci1.Size = New System.Drawing.Size(103, 21)
        Me.cbPlanci1.TabIndex = 39
        '
        'UıGroupBox1
        '
        Me.UıGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.UıGroupBox1.Controls.Add(Me.rbTumu2)
        Me.UıGroupBox1.Controls.Add(Me.rbAcilmis)
        Me.UıGroupBox1.Controls.Add(Me.rbPlanli)
        Me.UıGroupBox1.Location = New System.Drawing.Point(809, 19)
        Me.UıGroupBox1.Name = "UıGroupBox1"
        Me.UıGroupBox1.Size = New System.Drawing.Size(160, 95)
        Me.UıGroupBox1.TabIndex = 36
        Me.UıGroupBox1.Text = "Sipariş Durumu"
        '
        'rbTumu2
        '
        Me.rbTumu2.AutoSize = True
        Me.rbTumu2.Checked = True
        Me.rbTumu2.Location = New System.Drawing.Point(18, 68)
        Me.rbTumu2.Name = "rbTumu2"
        Me.rbTumu2.Size = New System.Drawing.Size(52, 17)
        Me.rbTumu2.TabIndex = 2
        Me.rbTumu2.TabStop = True
        Me.rbTumu2.Text = "Tümü"
        Me.rbTumu2.UseVisualStyleBackColor = True
        '
        'rbAcilmis
        '
        Me.rbAcilmis.AutoSize = True
        Me.rbAcilmis.Location = New System.Drawing.Point(18, 45)
        Me.rbAcilmis.Name = "rbAcilmis"
        Me.rbAcilmis.Size = New System.Drawing.Size(57, 17)
        Me.rbAcilmis.TabIndex = 2
        Me.rbAcilmis.Text = "Açılmış"
        Me.rbAcilmis.UseVisualStyleBackColor = True
        '
        'rbPlanli
        '
        Me.rbPlanli.AutoSize = True
        Me.rbPlanli.Location = New System.Drawing.Point(18, 22)
        Me.rbPlanli.Name = "rbPlanli"
        Me.rbPlanli.Size = New System.Drawing.Size(50, 17)
        Me.rbPlanli.TabIndex = 0
        Me.rbPlanli.Text = "Planlı"
        Me.rbPlanli.UseVisualStyleBackColor = True
        '
        'UıGroupBox2
        '
        Me.UıGroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.UıGroupBox2.Controls.Add(Me.rbTumu1)
        Me.UıGroupBox2.Controls.Add(Me.rbYenidenBasim)
        Me.UıGroupBox2.Controls.Add(Me.rbIlkBasim)
        Me.UıGroupBox2.Location = New System.Drawing.Point(628, 19)
        Me.UıGroupBox2.Name = "UıGroupBox2"
        Me.UıGroupBox2.Size = New System.Drawing.Size(160, 95)
        Me.UıGroupBox2.TabIndex = 36
        Me.UıGroupBox2.Text = "Yazdırma Durumu"
        '
        'rbTumu1
        '
        Me.rbTumu1.AutoSize = True
        Me.rbTumu1.Checked = True
        Me.rbTumu1.Location = New System.Drawing.Point(18, 68)
        Me.rbTumu1.Name = "rbTumu1"
        Me.rbTumu1.Size = New System.Drawing.Size(52, 17)
        Me.rbTumu1.TabIndex = 2
        Me.rbTumu1.TabStop = True
        Me.rbTumu1.Text = "Tümü"
        Me.rbTumu1.UseVisualStyleBackColor = True
        '
        'rbYenidenBasim
        '
        Me.rbYenidenBasim.AutoSize = True
        Me.rbYenidenBasim.Location = New System.Drawing.Point(18, 45)
        Me.rbYenidenBasim.Name = "rbYenidenBasim"
        Me.rbYenidenBasim.Size = New System.Drawing.Size(95, 17)
        Me.rbYenidenBasim.TabIndex = 2
        Me.rbYenidenBasim.Text = "Yeniden Basım"
        Me.rbYenidenBasim.UseVisualStyleBackColor = True
        '
        'rbIlkBasim
        '
        Me.rbIlkBasim.AutoSize = True
        Me.rbIlkBasim.Location = New System.Drawing.Point(18, 22)
        Me.rbIlkBasim.Name = "rbIlkBasim"
        Me.rbIlkBasim.Size = New System.Drawing.Size(67, 17)
        Me.rbIlkBasim.TabIndex = 0
        Me.rbIlkBasim.Text = "İlk Basım"
        Me.rbIlkBasim.UseVisualStyleBackColor = True
        '
        'txtTedarikciAdi2
        '
        Me.txtTedarikciAdi2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTedarikciAdi2.Enabled = False
        Me.txtTedarikciAdi2.Location = New System.Drawing.Point(326, 39)
        Me.txtTedarikciAdi2.Name = "txtTedarikciAdi2"
        Me.txtTedarikciAdi2.Size = New System.Drawing.Size(236, 20)
        Me.txtTedarikciAdi2.TabIndex = 35
        '
        'txtTedarikciAdi1
        '
        Me.txtTedarikciAdi1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTedarikciAdi1.Enabled = False
        Me.txtTedarikciAdi1.Location = New System.Drawing.Point(111, 39)
        Me.txtTedarikciAdi1.Name = "txtTedarikciAdi1"
        Me.txtTedarikciAdi1.Size = New System.Drawing.Size(209, 20)
        Me.txtTedarikciAdi1.TabIndex = 34
        '
        'txtMalzeme2
        '
        Me.txtMalzeme2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtMalzeme2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtMalzeme2.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtMalzeme2.Location = New System.Drawing.Point(326, 94)
        Me.txtMalzeme2.Name = "txtMalzeme2"
        Me.txtMalzeme2.Size = New System.Drawing.Size(146, 20)
        Me.txtMalzeme2.TabIndex = 32
        Me.txtMalzeme2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtTedarikci2
        '
        Me.txtTedarikci2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtTedarikci2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtTedarikci2.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtTedarikci2.Location = New System.Drawing.Point(326, 16)
        Me.txtTedarikci2.Name = "txtTedarikci2"
        Me.txtTedarikci2.Size = New System.Drawing.Size(103, 20)
        Me.txtTedarikci2.TabIndex = 32
        Me.txtTedarikci2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtMalzeme1
        '
        Me.txtMalzeme1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtMalzeme1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtMalzeme1.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtMalzeme1.Location = New System.Drawing.Point(111, 94)
        Me.txtMalzeme1.Name = "txtMalzeme1"
        Me.txtMalzeme1.Size = New System.Drawing.Size(146, 20)
        Me.txtMalzeme1.TabIndex = 31
        Me.txtMalzeme1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtTedarikci1
        '
        Me.txtTedarikci1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtTedarikci1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtTedarikci1.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtTedarikci1.Location = New System.Drawing.Point(111, 16)
        Me.txtTedarikci1.Name = "txtTedarikci1"
        Me.txtTedarikci1.Size = New System.Drawing.Size(103, 20)
        Me.txtTedarikci1.TabIndex = 31
        Me.txtTedarikci1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(12, 71)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(54, 13)
        Me.Label8.TabIndex = 33
        Me.Label8.Text = "Planci.....:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(12, 97)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 13)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Malzeme.....:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(12, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "Tedarikçi.....:"
        '
        'btnYazdır
        '
        Me.btnYazdır.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnYazdır.Image = Global.SyteLineEdi.My.Resources.Resources.NetByte_Design_Studio___0917
        Me.btnYazdır.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnYazdır.Location = New System.Drawing.Point(760, 130)
        Me.btnYazdır.Name = "btnYazdır"
        Me.btnYazdır.Size = New System.Drawing.Size(126, 40)
        Me.btnYazdır.TabIndex = 12
        Me.btnYazdır.Text = "Yazdırma"
        Me.btnYazdır.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(12, 127)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Sipariş....:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(12, 157)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Sip. Açma Tarihi..:"
        '
        'btnSorgula
        '
        Me.btnSorgula.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnSorgula.Image = Global.SyteLineEdi.My.Resources.Resources.Search
        Me.btnSorgula.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnSorgula.Location = New System.Drawing.Point(628, 130)
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
        Me.GridEX1.Size = New System.Drawing.Size(1025, 247)
        Me.GridEX1.TabIndex = 9
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'cbSiparis1
        '
        Me.cbSiparis1.FormattingEnabled = True
        Me.cbSiparis1.Location = New System.Drawing.Point(111, 119)
        Me.cbSiparis1.Name = "cbSiparis1"
        Me.cbSiparis1.Size = New System.Drawing.Size(146, 21)
        Me.cbSiparis1.TabIndex = 39
        '
        'cbSiparis2
        '
        Me.cbSiparis2.FormattingEnabled = True
        Me.cbSiparis2.Location = New System.Drawing.Point(326, 119)
        Me.cbSiparis2.Name = "cbSiparis2"
        Me.cbSiparis2.Size = New System.Drawing.Size(146, 21)
        Me.cbSiparis2.TabIndex = 39
        '
        'cbAktif
        '
        Me.cbAktif.AutoSize = True
        Me.cbAktif.BackColor = System.Drawing.Color.Transparent
        Me.cbAktif.Checked = True
        Me.cbAktif.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAktif.Location = New System.Drawing.Point(233, 156)
        Me.cbAktif.Name = "cbAktif"
        Me.cbAktif.Size = New System.Drawing.Size(47, 17)
        Me.cbAktif.TabIndex = 40
        Me.cbAktif.Text = "Aktif"
        Me.cbAktif.UseVisualStyleBackColor = False
        '
        'frmSatinalmaSiparisMektubu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1025, 440)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmSatinalmaSiparisMektubu"
        Me.Text = "Satınalma Sipariş Mektubu"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.UıGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UıGroupBox1.ResumeLayout(False)
        Me.UıGroupBox1.PerformLayout()
        CType(Me.UıGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UıGroupBox2.ResumeLayout(False)
        Me.UıGroupBox2.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtmSiparis As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents UıGroupBox2 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents rbYenidenBasim As System.Windows.Forms.RadioButton
    Friend WithEvents rbIlkBasim As System.Windows.Forms.RadioButton
    Friend WithEvents txtTedarikciAdi2 As System.Windows.Forms.TextBox
    Friend WithEvents txtTedarikciAdi1 As System.Windows.Forms.TextBox
    Friend WithEvents txtTedarikci2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtTedarikci1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnYazdır As Janus.Windows.EditControls.UIButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnSorgula As Janus.Windows.EditControls.UIButton
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtMalzeme2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtMalzeme1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cbPlanci2 As System.Windows.Forms.ComboBox
    Friend WithEvents cbPlanci1 As System.Windows.Forms.ComboBox
    Friend WithEvents UıGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents rbTumu2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbAcilmis As System.Windows.Forms.RadioButton
    Friend WithEvents rbPlanli As System.Windows.Forms.RadioButton
    Friend WithEvents rbTumu1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbSiparis2 As System.Windows.Forms.ComboBox
    Friend WithEvents cbSiparis1 As System.Windows.Forms.ComboBox
    Friend WithEvents cbAktif As System.Windows.Forms.CheckBox
End Class
