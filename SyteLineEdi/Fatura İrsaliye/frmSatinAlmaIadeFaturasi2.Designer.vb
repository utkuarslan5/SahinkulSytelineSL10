<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSatinAlmaIadeFaturasi2
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
        Dim GridEXLayout1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSatinAlmaIadeFaturasi2))
        Dim GridEXLayout2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXLayout3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim UıStatusBarPanel1 As Janus.Windows.UI.StatusBar.UIStatusBarPanel = New Janus.Windows.UI.StatusBar.UIStatusBarPanel
        Dim UıStatusBarPanel2 As Janus.Windows.UI.StatusBar.UIStatusBarPanel = New Janus.Windows.UI.StatusBar.UIStatusBarPanel
        Me.grdMain = New Janus.Windows.GridEX.GridEX
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmbVoucher = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.cmbVendNum = New Janus.Windows.GridEX.EditControls.EditBox
        Me.txtVendName = New Janus.Windows.GridEX.EditControls.EditBox
        Me.btnTemizle = New Janus.Windows.EditControls.UIButton
        Me.btnYazdir = New Janus.Windows.EditControls.UIButton
        Me.btnAdd = New Janus.Windows.EditControls.UIButton
        Me.txtVrgTutar2 = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.txtStnTutar2 = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.txtVrgTutar1 = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.txtStnTutar1 = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.txtFaturaNo = New Janus.Windows.GridEX.EditControls.EditBox
        Me.cmdDocNum = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.dtpFaturaTarihi = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.txtAciklama = New Janus.Windows.GridEX.EditControls.EditBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.stBar = New Janus.Windows.UI.StatusBar.UIStatusBar
        Me.txtVergiKodu = New Janus.Windows.GridEX.EditControls.EditBox
        Me.Label4 = New System.Windows.Forms.Label
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdMain
        '
        Me.grdMain.AlternatingColors = True
        Me.grdMain.BuiltInTextsData = "<LocalizableData ID=""LocalizableStrings"" Collection=""true""><GroupByBoxInfo>Grupla" & _
            "mak istediğiniz kolonu buraya taşıyın</GroupByBoxInfo></LocalizableData>"
        Me.grdMain.ColumnAutoResize = True
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.grdMain.DesignTimeLayout = GridEXLayout1
        Me.grdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdMain.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grdMain.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdMain.GroupByBoxVisible = False
        Me.grdMain.Location = New System.Drawing.Point(0, 258)
        Me.grdMain.Name = "grdMain"
        Me.grdMain.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.grdMain.Size = New System.Drawing.Size(938, 194)
        Me.grdMain.TabIndex = 1
        Me.grdMain.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'GroupBox1
        '
        Me.GroupBox1.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtVergiKodu)
        Me.GroupBox1.Controls.Add(Me.cmbVoucher)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.cmbVendNum)
        Me.GroupBox1.Controls.Add(Me.txtVendName)
        Me.GroupBox1.Controls.Add(Me.btnTemizle)
        Me.GroupBox1.Controls.Add(Me.btnYazdir)
        Me.GroupBox1.Controls.Add(Me.btnAdd)
        Me.GroupBox1.Controls.Add(Me.txtVrgTutar2)
        Me.GroupBox1.Controls.Add(Me.txtStnTutar2)
        Me.GroupBox1.Controls.Add(Me.txtVrgTutar1)
        Me.GroupBox1.Controls.Add(Me.txtStnTutar1)
        Me.GroupBox1.Controls.Add(Me.txtFaturaNo)
        Me.GroupBox1.Controls.Add(Me.cmdDocNum)
        Me.GroupBox1.Controls.Add(Me.dtpFaturaTarihi)
        Me.GroupBox1.Controls.Add(Me.txtAciklama)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(938, 258)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'cmbVoucher
        '
        Me.cmbVoucher.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.cmbVoucher.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        GridEXLayout2.LayoutString = resources.GetString("GridEXLayout2.LayoutString")
        Me.cmbVoucher.DesignTimeLayout = GridEXLayout2
        Me.cmbVoucher.DisplayMember = "voucher"
        Me.cmbVoucher.Location = New System.Drawing.Point(104, 80)
        Me.cmbVoucher.Name = "cmbVoucher"
        Me.cmbVoucher.Size = New System.Drawing.Size(75, 20)
        Me.cmbVoucher.TabIndex = 32
        Me.cmbVoucher.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.cmbVoucher.ValueMember = "voucher"
        Me.cmbVoucher.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(11, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "Fiş No :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Location = New System.Drawing.Point(273, 112)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(57, 13)
        Me.Label13.TabIndex = 30
        Me.Label13.Text = "Vrg Tutar :"
        '
        'cmbVendNum
        '
        Me.cmbVendNum.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.cmbVendNum.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.cmbVendNum.BackColor = System.Drawing.Color.Yellow
        Me.cmbVendNum.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.cmbVendNum.Location = New System.Drawing.Point(104, 24)
        Me.cmbVendNum.Name = "cmbVendNum"
        Me.cmbVendNum.Size = New System.Drawing.Size(137, 20)
        Me.cmbVendNum.TabIndex = 28
        Me.cmbVendNum.Tag = "1"
        Me.cmbVendNum.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.cmbVendNum.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'txtVendName
        '
        Me.txtVendName.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtVendName.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtVendName.Enabled = False
        Me.txtVendName.Location = New System.Drawing.Point(277, 24)
        Me.txtVendName.Name = "txtVendName"
        Me.txtVendName.Size = New System.Drawing.Size(222, 20)
        Me.txtVendName.TabIndex = 8
        Me.txtVendName.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtVendName.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'btnTemizle
        '
        Me.btnTemizle.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnTemizle.Image = Global.SyteLineEdi.My.Resources.Resources.trash_design
        Me.btnTemizle.ImageSize = New System.Drawing.Size(60, 60)
        Me.btnTemizle.Location = New System.Drawing.Point(373, 171)
        Me.btnTemizle.Name = "btnTemizle"
        Me.btnTemizle.Size = New System.Drawing.Size(126, 66)
        Me.btnTemizle.TabIndex = 17
        Me.btnTemizle.Text = "Temizle"
        Me.btnTemizle.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnYazdir
        '
        Me.btnYazdir.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnYazdir.Image = Global.SyteLineEdi.My.Resources.Resources.NetByte_Design_Studio___0917
        Me.btnYazdir.ImageSize = New System.Drawing.Size(60, 60)
        Me.btnYazdir.Location = New System.Drawing.Point(239, 171)
        Me.btnYazdir.Name = "btnYazdir"
        Me.btnYazdir.Size = New System.Drawing.Size(126, 66)
        Me.btnYazdir.TabIndex = 16
        Me.btnYazdir.Text = "Yazdır"
        Me.btnYazdir.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnAdd.Image = Global.SyteLineEdi.My.Resources.Resources.Copy__2__of_add
        Me.btnAdd.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnAdd.Location = New System.Drawing.Point(104, 171)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(126, 66)
        Me.btnAdd.TabIndex = 15
        Me.btnAdd.Text = "Ekle"
        Me.btnAdd.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'txtVrgTutar2
        '
        Me.txtVrgTutar2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtVrgTutar2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtVrgTutar2.BackColor = System.Drawing.Color.White
        Me.txtVrgTutar2.Enabled = False
        Me.txtVrgTutar2.FormatString = "###,###.#0"
        Me.txtVrgTutar2.Location = New System.Drawing.Point(444, 108)
        Me.txtVrgTutar2.Name = "txtVrgTutar2"
        Me.txtVrgTutar2.Size = New System.Drawing.Size(55, 20)
        Me.txtVrgTutar2.TabIndex = 14
        Me.txtVrgTutar2.Text = ".00"
        Me.txtVrgTutar2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtVrgTutar2.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'txtStnTutar2
        '
        Me.txtStnTutar2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtStnTutar2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtStnTutar2.BackColor = System.Drawing.Color.White
        Me.txtStnTutar2.Enabled = False
        Me.txtStnTutar2.FormatString = "###,###.#0"
        Me.txtStnTutar2.Location = New System.Drawing.Point(444, 80)
        Me.txtStnTutar2.Name = "txtStnTutar2"
        Me.txtStnTutar2.Size = New System.Drawing.Size(55, 20)
        Me.txtStnTutar2.TabIndex = 12
        Me.txtStnTutar2.Text = ".00"
        Me.txtStnTutar2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtStnTutar2.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'txtVrgTutar1
        '
        Me.txtVrgTutar1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtVrgTutar1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtVrgTutar1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtVrgTutar1.FormatString = "###,###.#0"
        Me.txtVrgTutar1.Location = New System.Drawing.Point(383, 108)
        Me.txtVrgTutar1.Name = "txtVrgTutar1"
        Me.txtVrgTutar1.Size = New System.Drawing.Size(55, 20)
        Me.txtVrgTutar1.TabIndex = 13
        Me.txtVrgTutar1.Text = ".00"
        Me.txtVrgTutar1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtVrgTutar1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'txtStnTutar1
        '
        Me.txtStnTutar1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtStnTutar1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtStnTutar1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtStnTutar1.FormatString = "###,###.#0"
        Me.txtStnTutar1.Location = New System.Drawing.Point(383, 80)
        Me.txtStnTutar1.Name = "txtStnTutar1"
        Me.txtStnTutar1.Size = New System.Drawing.Size(55, 20)
        Me.txtStnTutar1.TabIndex = 11
        Me.txtStnTutar1.Text = ".00"
        Me.txtStnTutar1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtStnTutar1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'txtFaturaNo
        '
        Me.txtFaturaNo.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtFaturaNo.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtFaturaNo.BackColor = System.Drawing.Color.Yellow
        Me.txtFaturaNo.Location = New System.Drawing.Point(104, 108)
        Me.txtFaturaNo.Name = "txtFaturaNo"
        Me.txtFaturaNo.Size = New System.Drawing.Size(75, 20)
        Me.txtFaturaNo.TabIndex = 5
        Me.txtFaturaNo.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtFaturaNo.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'cmdDocNum
        '
        Me.cmdDocNum.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.cmdDocNum.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        GridEXLayout3.LayoutString = resources.GetString("GridEXLayout3.LayoutString")
        Me.cmdDocNum.DesignTimeLayout = GridEXLayout3
        Me.cmdDocNum.DisplayMember = "document_num"
        Me.cmdDocNum.Location = New System.Drawing.Point(104, 52)
        Me.cmdDocNum.Name = "cmdDocNum"
        Me.cmdDocNum.Size = New System.Drawing.Size(75, 20)
        Me.cmdDocNum.TabIndex = 4
        Me.cmdDocNum.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.cmdDocNum.ValueMember = "document_num"
        Me.cmdDocNum.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'dtpFaturaTarihi
        '
        '
        '
        '
        Me.dtpFaturaTarihi.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtpFaturaTarihi.DropDownCalendar.Name = ""
        Me.dtpFaturaTarihi.DropDownCalendar.Size = New System.Drawing.Size(170, 173)
        Me.dtpFaturaTarihi.DropDownCalendar.TabIndex = 0
        Me.dtpFaturaTarihi.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        Me.dtpFaturaTarihi.EditStyle = Janus.Windows.CalendarCombo.EditStyle.Free
        Me.dtpFaturaTarihi.Location = New System.Drawing.Point(104, 136)
        Me.dtpFaturaTarihi.Name = "dtpFaturaTarihi"
        Me.dtpFaturaTarihi.NullButtonText = "Yok"
        Me.dtpFaturaTarihi.Size = New System.Drawing.Size(137, 20)
        Me.dtpFaturaTarihi.TabIndex = 6
        Me.dtpFaturaTarihi.TodayButtonText = "Bugün"
        Me.dtpFaturaTarihi.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        '
        'txtAciklama
        '
        Me.txtAciklama.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtAciklama.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtAciklama.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtAciklama.Location = New System.Drawing.Point(383, 52)
        Me.txtAciklama.Name = "txtAciklama"
        Me.txtAciklama.Size = New System.Drawing.Size(116, 20)
        Me.txtAciklama.TabIndex = 10
        Me.txtAciklama.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtAciklama.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(11, 112)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Fatura Numarası :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(11, 28)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Satıcı :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(11, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Döküman No :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(11, 140)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Fatura Tarihi :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(273, 84)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(62, 13)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Mal Bedeli :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(273, 56)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 13)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Açıklama :"
        '
        'stBar
        '
        Me.stBar.Location = New System.Drawing.Point(0, 429)
        Me.stBar.Name = "stBar"
        UıStatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        UıStatusBarPanel1.BorderColor = System.Drawing.Color.Empty
        UıStatusBarPanel1.Key = "Pn1"
        UıStatusBarPanel1.ProgressBarValue = 0
        UıStatusBarPanel1.Width = 815
        UıStatusBarPanel2.BorderColor = System.Drawing.Color.Empty
        UıStatusBarPanel2.Key = ""
        UıStatusBarPanel2.ProgressBarValue = 0
        Me.stBar.Panels.AddRange(New Janus.Windows.UI.StatusBar.UIStatusBarPanel() {UıStatusBarPanel1, UıStatusBarPanel2})
        Me.stBar.PanelsBorderColor = System.Drawing.SystemColors.ControlDark
        Me.stBar.Size = New System.Drawing.Size(938, 23)
        Me.stBar.TabIndex = 2
        '
        'txtVergiKodu
        '
        Me.txtVergiKodu.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtVergiKodu.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtVergiKodu.BackColor = System.Drawing.Color.Yellow
        Me.txtVergiKodu.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtVergiKodu.Location = New System.Drawing.Point(383, 136)
        Me.txtVergiKodu.Name = "txtVergiKodu"
        Me.txtVergiKodu.Size = New System.Drawing.Size(116, 20)
        Me.txtVergiKodu.TabIndex = 33
        Me.txtVergiKodu.Tag = "1"
        Me.txtVergiKodu.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtVergiKodu.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(278, 140)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "Vrg Kodu :"
        '
        'frmSatinAlmaIadeFaturasi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(938, 452)
        Me.Controls.Add(Me.stBar)
        Me.Controls.Add(Me.grdMain)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSatinAlmaIadeFaturasi"
        Me.Text = "Satınalma İade Faturası"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdMain As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpFaturaTarihi As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents cmdDocNum As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents txtStnTutar1 As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents txtVrgTutar1 As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents txtStnTutar2 As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents txtVrgTutar2 As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtFaturaNo As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtVendName As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtAciklama As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents btnYazdir As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnAdd As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnTemizle As Janus.Windows.EditControls.UIButton
    Friend WithEvents stBar As Janus.Windows.UI.StatusBar.UIStatusBar
    Friend WithEvents cmbVendNum As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbVoucher As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtVergiKodu As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
