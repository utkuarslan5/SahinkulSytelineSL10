<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSevkiyatOnay
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
        Me.components = New System.ComponentModel.Container()
        Dim GridEXLayout1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSevkiyatOnay))
        Me.btnGoster = New Janus.Windows.EditControls.UIButton()
        Me.btnAktar = New Janus.Windows.EditControls.UIButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtSevkTarihi = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.dtpSevkTarihi = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.chkKit = New System.Windows.Forms.CheckBox()
        Me.btnSevkiyat = New Janus.Windows.EditControls.UIButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPickNo = New System.Windows.Forms.TextBox()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.cmsSagTus = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SatýrSilToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsSagTus.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnGoster
        '
        Me.btnGoster.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.btnGoster.Image = Global.SyteLineEdi.My.Resources.Resources.Search
        Me.btnGoster.ImageSize = New System.Drawing.Size(46, 46)
        Me.btnGoster.Location = New System.Drawing.Point(251, 19)
        Me.btnGoster.Name = "btnGoster"
        Me.btnGoster.Size = New System.Drawing.Size(114, 54)
        Me.btnGoster.TabIndex = 0
        Me.btnGoster.Text = "Göster"
        Me.btnGoster.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnAktar
        '
        Me.btnAktar.Enabled = False
        Me.btnAktar.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.btnAktar.Image = Global.SyteLineEdi.My.Resources.Resources.Save
        Me.btnAktar.ImageSize = New System.Drawing.Size(46, 46)
        Me.btnAktar.Location = New System.Drawing.Point(394, 19)
        Me.btnAktar.Name = "btnAktar"
        Me.btnAktar.Size = New System.Drawing.Size(114, 54)
        Me.btnAktar.TabIndex = 1
        Me.btnAktar.Text = "Kaydet"
        Me.btnAktar.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'GroupBox1
        '
        Me.GroupBox1.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtSevkTarihi)
        Me.GroupBox1.Controls.Add(Me.dtpSevkTarihi)
        Me.GroupBox1.Controls.Add(Me.chkKit)
        Me.GroupBox1.Controls.Add(Me.btnSevkiyat)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtPickNo)
        Me.GroupBox1.Controls.Add(Me.btnAktar)
        Me.GroupBox1.Controls.Add(Me.btnGoster)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1005, 121)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label8.Location = New System.Drawing.Point(17, 60)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(97, 13)
        Me.Label8.TabIndex = 48
        Me.Label8.Text = "Sevkiyat Tarihi....:"
        '
        'txtSevkTarihi
        '
        Me.txtSevkTarihi.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtSevkTarihi.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtSevkTarihi.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtSevkTarihi.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtSevkTarihi.Location = New System.Drawing.Point(137, 56)
        Me.txtSevkTarihi.Name = "txtSevkTarihi"
        Me.txtSevkTarihi.ReadOnly = True
        Me.txtSevkTarihi.Size = New System.Drawing.Size(88, 21)
        Me.txtSevkTarihi.TabIndex = 47
        Me.txtSevkTarihi.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'dtpSevkTarihi
        '
        Me.dtpSevkTarihi.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.dtpSevkTarihi.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtpSevkTarihi.DropDownCalendar.Name = ""
        Me.dtpSevkTarihi.DropDownCalendar.Size = New System.Drawing.Size(164, 167)
        Me.dtpSevkTarihi.DropDownCalendar.TabIndex = 0
        Me.dtpSevkTarihi.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard
        Me.dtpSevkTarihi.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.dtpSevkTarihi.Location = New System.Drawing.Point(137, 56)
        Me.dtpSevkTarihi.Name = "dtpSevkTarihi"
        Me.dtpSevkTarihi.ShowNullButton = True
        Me.dtpSevkTarihi.Size = New System.Drawing.Size(101, 21)
        Me.dtpSevkTarihi.TabIndex = 46
        Me.dtpSevkTarihi.TodayButtonText = "Bugün"
        Me.dtpSevkTarihi.Value = New Date(1, 2, 1, 0, 0, 0, 0)
        '
        'chkKit
        '
        Me.chkKit.AutoSize = True
        Me.chkKit.BackColor = System.Drawing.Color.Transparent
        Me.chkKit.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.chkKit.Location = New System.Drawing.Point(136, 82)
        Me.chkKit.Name = "chkKit"
        Me.chkKit.Size = New System.Drawing.Size(84, 17)
        Me.chkKit.TabIndex = 5
        Me.chkKit.Text = "Kit Sevkiyatý"
        Me.chkKit.UseVisualStyleBackColor = False
        Me.chkKit.Visible = False
        '
        'btnSevkiyat
        '
        Me.btnSevkiyat.Enabled = False
        Me.btnSevkiyat.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.btnSevkiyat.Image = Global.SyteLineEdi.My.Resources.Resources.transmit_64x64
        Me.btnSevkiyat.ImageSize = New System.Drawing.Size(46, 46)
        Me.btnSevkiyat.Location = New System.Drawing.Point(526, 19)
        Me.btnSevkiyat.Name = "btnSevkiyat"
        Me.btnSevkiyat.Size = New System.Drawing.Size(114, 54)
        Me.btnSevkiyat.TabIndex = 4
        Me.btnSevkiyat.Text = "Sevkiyat Oluþtur"
        Me.btnSevkiyat.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label1.Location = New System.Drawing.Point(17, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Çekme Listesi No ....:"
        '
        'txtPickNo
        '
        Me.txtPickNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtPickNo.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtPickNo.Location = New System.Drawing.Point(137, 30)
        Me.txtPickNo.Name = "txtPickNo"
        Me.txtPickNo.Size = New System.Drawing.Size(83, 21)
        Me.txtPickNo.TabIndex = 2
        '
        'grd
        '
        Me.grd.AlternatingColors = True
        Me.grd.ContextMenuStrip = Me.cmsSagTus
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.grd.DesignTimeLayout = GridEXLayout1
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grd.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(0, 121)
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(1005, 354)
        Me.grd.TabIndex = 4
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'cmsSagTus
        '
        Me.cmsSagTus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SatýrSilToolStripMenuItem})
        Me.cmsSagTus.Name = "cmsSagTus"
        Me.cmsSagTus.Size = New System.Drawing.Size(113, 26)
        '
        'SatýrSilToolStripMenuItem
        '
        Me.SatýrSilToolStripMenuItem.Name = "SatýrSilToolStripMenuItem"
        Me.SatýrSilToolStripMenuItem.Size = New System.Drawing.Size(112, 22)
        Me.SatýrSilToolStripMenuItem.Text = "Satýr Sil"
        '
        'frmSevkiyatOnay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.ClientSize = New System.Drawing.Size(1005, 475)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSevkiyatOnay"
        Me.Text = "Sevkiyat Onay"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsSagTus.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnGoster As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnAktar As Janus.Windows.EditControls.UIButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPickNo As System.Windows.Forms.TextBox
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents cmsSagTus As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SatýrSilToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnSevkiyat As Janus.Windows.EditControls.UIButton
    Friend WithEvents chkKit As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtSevkTarihi As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents dtpSevkTarihi As Janus.Windows.CalendarCombo.CalendarCombo

End Class
