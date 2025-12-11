<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGumrukBildirimi
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
        Dim GridEXLayout2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGumrukBildirimi))
        Me.txtMusteriAdi2 = New System.Windows.Forms.TextBox
        Me.txtMusteriAdi1 = New System.Windows.Forms.TextBox
        Me.txtMusteri2 = New Janus.Windows.GridEX.EditControls.EditBox
        Me.txtMusteri1 = New Janus.Windows.GridEX.EditControls.EditBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnUnCheckedAll = New Janus.Windows.EditControls.UIButton
        Me.btnCheckedAll = New Janus.Windows.EditControls.UIButton
        Me.txtFaturaNo1 = New Janus.Windows.GridEX.EditControls.EditBox
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.btnYazdir = New Janus.Windows.EditControls.UIButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnAtamaIptal = New Janus.Windows.EditControls.UIButton
        Me.UıGroupBox2 = New Janus.Windows.EditControls.UIGroupBox
        Me.rdbBeyanNoAtanmis = New System.Windows.Forms.RadioButton
        Me.rdbBeyanNoAtanmamis = New System.Windows.Forms.RadioButton
        Me.txtBeyanNo2 = New Janus.Windows.GridEX.EditControls.EditBox
        Me.txtBeyanNo1 = New Janus.Windows.GridEX.EditControls.EditBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtFaturaNo2 = New Janus.Windows.GridEX.EditControls.EditBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnSorgula = New Janus.Windows.EditControls.UIButton
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.UıGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UıGroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtMusteriAdi2
        '
        Me.txtMusteriAdi2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMusteriAdi2.Enabled = False
        Me.txtMusteriAdi2.Location = New System.Drawing.Point(340, 45)
        Me.txtMusteriAdi2.Name = "txtMusteriAdi2"
        Me.txtMusteriAdi2.Size = New System.Drawing.Size(236, 20)
        Me.txtMusteriAdi2.TabIndex = 35
        '
        'txtMusteriAdi1
        '
        Me.txtMusteriAdi1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMusteriAdi1.Enabled = False
        Me.txtMusteriAdi1.Location = New System.Drawing.Point(125, 45)
        Me.txtMusteriAdi1.Name = "txtMusteriAdi1"
        Me.txtMusteriAdi1.Size = New System.Drawing.Size(209, 20)
        Me.txtMusteriAdi1.TabIndex = 34
        '
        'txtMusteri2
        '
        Me.txtMusteri2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtMusteri2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtMusteri2.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtMusteri2.Location = New System.Drawing.Point(340, 22)
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
        Me.txtMusteri1.Location = New System.Drawing.Point(125, 22)
        Me.txtMusteri1.Name = "txtMusteri1"
        Me.txtMusteri1.Size = New System.Drawing.Size(103, 20)
        Me.txtMusteri1.TabIndex = 31
        Me.txtMusteri1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(26, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "Müşteri.....:"
        '
        'btnUnCheckedAll
        '
        Me.btnUnCheckedAll.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnUnCheckedAll.Image = Global.SyteLineEdi.My.Resources.Resources.Close
        Me.btnUnCheckedAll.Location = New System.Drawing.Point(125, 134)
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
        Me.btnCheckedAll.Location = New System.Drawing.Point(24, 134)
        Me.btnCheckedAll.Name = "btnCheckedAll"
        Me.btnCheckedAll.Size = New System.Drawing.Size(95, 24)
        Me.btnCheckedAll.TabIndex = 15
        Me.btnCheckedAll.Text = "Tümünü Seç"
        Me.btnCheckedAll.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'txtFaturaNo1
        '
        Me.txtFaturaNo1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtFaturaNo1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtFaturaNo1.Location = New System.Drawing.Point(125, 71)
        Me.txtFaturaNo1.Name = "txtFaturaNo1"
        Me.txtFaturaNo1.Size = New System.Drawing.Size(103, 20)
        Me.txtFaturaNo1.TabIndex = 5
        Me.txtFaturaNo1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'GridEX1
        '
        GridEXLayout2.LayoutString = resources.GetString("GridEXLayout2.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEXLayout2
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.Location = New System.Drawing.Point(0, 164)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(1034, 457)
        Me.GridEX1.TabIndex = 5
        '
        'btnYazdir
        '
        Me.btnYazdir.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnYazdir.Image = Global.SyteLineEdi.My.Resources.Resources.NetByte_Design_Studio___0917
        Me.btnYazdir.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnYazdir.Location = New System.Drawing.Point(733, 13)
        Me.btnYazdir.Name = "btnYazdir"
        Me.btnYazdir.Size = New System.Drawing.Size(126, 40)
        Me.btnYazdir.TabIndex = 12
        Me.btnYazdir.Text = "Yazdır"
        Me.btnYazdir.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.GroupBox1.Controls.Add(Me.btnAtamaIptal)
        Me.GroupBox1.Controls.Add(Me.UıGroupBox2)
        Me.GroupBox1.Controls.Add(Me.txtBeyanNo2)
        Me.GroupBox1.Controls.Add(Me.txtBeyanNo1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtFaturaNo2)
        Me.GroupBox1.Controls.Add(Me.txtMusteriAdi2)
        Me.GroupBox1.Controls.Add(Me.txtMusteriAdi1)
        Me.GroupBox1.Controls.Add(Me.txtMusteri2)
        Me.GroupBox1.Controls.Add(Me.txtMusteri1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnUnCheckedAll)
        Me.GroupBox1.Controls.Add(Me.btnCheckedAll)
        Me.GroupBox1.Controls.Add(Me.txtFaturaNo1)
        Me.GroupBox1.Controls.Add(Me.btnYazdir)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.btnSorgula)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1034, 164)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'btnAtamaIptal
        '
        Me.btnAtamaIptal.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnAtamaIptal.Image = Global.SyteLineEdi.My.Resources.Resources.dialog_cancel
        Me.btnAtamaIptal.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnAtamaIptal.Location = New System.Drawing.Point(340, 123)
        Me.btnAtamaIptal.Name = "btnAtamaIptal"
        Me.btnAtamaIptal.Size = New System.Drawing.Size(103, 35)
        Me.btnAtamaIptal.TabIndex = 44
        Me.btnAtamaIptal.Text = "Atama İptal"
        Me.btnAtamaIptal.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'UıGroupBox2
        '
        Me.UıGroupBox2.Controls.Add(Me.rdbBeyanNoAtanmis)
        Me.UıGroupBox2.Controls.Add(Me.rdbBeyanNoAtanmamis)
        Me.UıGroupBox2.Location = New System.Drawing.Point(473, 71)
        Me.UıGroupBox2.Name = "UıGroupBox2"
        Me.UıGroupBox2.Size = New System.Drawing.Size(238, 67)
        Me.UıGroupBox2.TabIndex = 43
        Me.UıGroupBox2.Text = "Durum"
        '
        'rdbBeyanNoAtanmis
        '
        Me.rdbBeyanNoAtanmis.AutoSize = True
        Me.rdbBeyanNoAtanmis.Location = New System.Drawing.Point(18, 45)
        Me.rdbBeyanNoAtanmis.Name = "rdbBeyanNoAtanmis"
        Me.rdbBeyanNoAtanmis.Size = New System.Drawing.Size(182, 17)
        Me.rdbBeyanNoAtanmis.TabIndex = 2
        Me.rdbBeyanNoAtanmis.Text = "Beyanname No Atanmış Faturalar"
        Me.rdbBeyanNoAtanmis.UseVisualStyleBackColor = True
        '
        'rdbBeyanNoAtanmamis
        '
        Me.rdbBeyanNoAtanmamis.AutoSize = True
        Me.rdbBeyanNoAtanmamis.Checked = True
        Me.rdbBeyanNoAtanmamis.Location = New System.Drawing.Point(18, 22)
        Me.rdbBeyanNoAtanmamis.Name = "rdbBeyanNoAtanmamis"
        Me.rdbBeyanNoAtanmamis.Size = New System.Drawing.Size(196, 17)
        Me.rdbBeyanNoAtanmamis.TabIndex = 0
        Me.rdbBeyanNoAtanmamis.TabStop = True
        Me.rdbBeyanNoAtanmamis.Text = "Beyanname No Atanmamış Faturalar"
        Me.rdbBeyanNoAtanmamis.UseVisualStyleBackColor = True
        '
        'txtBeyanNo2
        '
        Me.txtBeyanNo2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtBeyanNo2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtBeyanNo2.Location = New System.Drawing.Point(340, 97)
        Me.txtBeyanNo2.Name = "txtBeyanNo2"
        Me.txtBeyanNo2.Size = New System.Drawing.Size(103, 20)
        Me.txtBeyanNo2.TabIndex = 42
        Me.txtBeyanNo2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtBeyanNo1
        '
        Me.txtBeyanNo1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtBeyanNo1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtBeyanNo1.Location = New System.Drawing.Point(125, 97)
        Me.txtBeyanNo1.Name = "txtBeyanNo1"
        Me.txtBeyanNo1.Size = New System.Drawing.Size(103, 20)
        Me.txtBeyanNo1.TabIndex = 40
        Me.txtBeyanNo1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 101)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 13)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "Gümr.Beyan.No ....:"
        '
        'txtFaturaNo2
        '
        Me.txtFaturaNo2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtFaturaNo2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtFaturaNo2.Location = New System.Drawing.Point(340, 71)
        Me.txtFaturaNo2.Name = "txtFaturaNo2"
        Me.txtFaturaNo2.Size = New System.Drawing.Size(103, 20)
        Me.txtFaturaNo2.TabIndex = 39
        Me.txtFaturaNo2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(26, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Fatura No....:"
        '
        'btnSorgula
        '
        Me.btnSorgula.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnSorgula.Image = Global.SyteLineEdi.My.Resources.Resources.Search
        Me.btnSorgula.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnSorgula.Location = New System.Drawing.Point(601, 13)
        Me.btnSorgula.Name = "btnSorgula"
        Me.btnSorgula.Size = New System.Drawing.Size(126, 40)
        Me.btnSorgula.TabIndex = 0
        Me.btnSorgula.Text = "Listele"
        Me.btnSorgula.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'frmGumrukBildirimi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1034, 621)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmGumrukBildirimi"
        Me.Text = "İhraç Evrakları Basımı"
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.UıGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UıGroupBox2.ResumeLayout(False)
        Me.UıGroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtMusteriAdi2 As System.Windows.Forms.TextBox
    Friend WithEvents txtMusteriAdi1 As System.Windows.Forms.TextBox
    Friend WithEvents txtMusteri2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtMusteri1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnUnCheckedAll As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnCheckedAll As Janus.Windows.EditControls.UIButton
    Friend WithEvents txtFaturaNo1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnYazdir As Janus.Windows.EditControls.UIButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSorgula As Janus.Windows.EditControls.UIButton
    Friend WithEvents txtFaturaNo2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtBeyanNo2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtBeyanNo1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UıGroupBox2 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents rdbBeyanNoAtanmis As System.Windows.Forms.RadioButton
    Friend WithEvents rdbBeyanNoAtanmamis As System.Windows.Forms.RadioButton
    Friend WithEvents btnAtamaIptal As Janus.Windows.EditControls.UIButton
End Class
