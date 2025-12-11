<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaletEtiketBasim
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
        Dim GridEXLayout1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPaletEtiketBasim))
        Me.rdbTumu = New System.Windows.Forms.RadioButton
        Me.rdbYazdirilmadi = New System.Windows.Forms.RadioButton
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtIrsaliye = New System.Windows.Forms.TextBox
        Me.txtCeklistNo = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.UýGroupBox1 = New Janus.Windows.EditControls.UIGroupBox
        Me.btnOlustur = New Janus.Windows.EditControls.UIButton
        Me.btnSorgula = New Janus.Windows.EditControls.UIButton
        Me.UýGroupBox2 = New Janus.Windows.EditControls.UIGroupBox
        Me.rdbYazdirildi = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UýGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UýGroupBox1.SuspendLayout()
        CType(Me.UýGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UýGroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'rdbTumu
        '
        Me.rdbTumu.AutoSize = True
        Me.rdbTumu.Location = New System.Drawing.Point(30, 62)
        Me.rdbTumu.Name = "rdbTumu"
        Me.rdbTumu.Size = New System.Drawing.Size(52, 17)
        Me.rdbTumu.TabIndex = 2
        Me.rdbTumu.TabStop = True
        Me.rdbTumu.Text = "Tümü"
        Me.rdbTumu.UseVisualStyleBackColor = True
        '
        'rdbYazdirilmadi
        '
        Me.rdbYazdirilmadi.AutoSize = True
        Me.rdbYazdirilmadi.Location = New System.Drawing.Point(30, 40)
        Me.rdbYazdirilmadi.Name = "rdbYazdirilmadi"
        Me.rdbYazdirilmadi.Size = New System.Drawing.Size(80, 17)
        Me.rdbYazdirilmadi.TabIndex = 1
        Me.rdbYazdirilmadi.TabStop = True
        Me.rdbYazdirilmadi.Text = "Yazdýrýlmadý"
        Me.rdbYazdirilmadi.UseVisualStyleBackColor = True
        '
        'GridEX1
        '
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEXLayout1
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.GridEX1.Location = New System.Drawing.Point(0, 100)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(987, 392)
        Me.GridEX1.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 13)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Ýrsaliye Numarasý...:"
        '
        'txtIrsaliye
        '
        Me.txtIrsaliye.Location = New System.Drawing.Point(149, 68)
        Me.txtIrsaliye.Name = "txtIrsaliye"
        Me.txtIrsaliye.Size = New System.Drawing.Size(100, 20)
        Me.txtIrsaliye.TabIndex = 15
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
        'UýGroupBox1
        '
        Me.UýGroupBox1.BackColor = System.Drawing.Color.DodgerBlue
        Me.UýGroupBox1.Controls.Add(Me.Label2)
        Me.UýGroupBox1.Controls.Add(Me.txtIrsaliye)
        Me.UýGroupBox1.Controls.Add(Me.btnOlustur)
        Me.UýGroupBox1.Controls.Add(Me.btnSorgula)
        Me.UýGroupBox1.Controls.Add(Me.UýGroupBox2)
        Me.UýGroupBox1.Controls.Add(Me.Label1)
        Me.UýGroupBox1.Controls.Add(Me.txtCeklistNo)
        Me.UýGroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UýGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.UýGroupBox1.Name = "UýGroupBox1"
        Me.UýGroupBox1.Size = New System.Drawing.Size(987, 100)
        Me.UýGroupBox1.TabIndex = 3
        '
        'btnOlustur
        '
        Me.btnOlustur.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnOlustur.Icon = CType(resources.GetObject("btnOlustur.Icon"), System.Drawing.Icon)
        Me.btnOlustur.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnOlustur.Location = New System.Drawing.Point(396, 48)
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
        Me.btnSorgula.Location = New System.Drawing.Point(264, 48)
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
        Me.UýGroupBox2.Location = New System.Drawing.Point(528, 15)
        Me.UýGroupBox2.Name = "UýGroupBox2"
        Me.UýGroupBox2.Size = New System.Drawing.Size(160, 82)
        Me.UýGroupBox2.TabIndex = 3
        Me.UýGroupBox2.Text = "Durum"
        Me.UýGroupBox2.Visible = False
        '
        'rdbYazdirildi
        '
        Me.rdbYazdirildi.AutoSize = True
        Me.rdbYazdirildi.Location = New System.Drawing.Point(30, 18)
        Me.rdbYazdirildi.Name = "rdbYazdirildi"
        Me.rdbYazdirildi.Size = New System.Drawing.Size(66, 17)
        Me.rdbYazdirildi.TabIndex = 0
        Me.rdbYazdirildi.TabStop = True
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
        'frmPaletEtiketBasim
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(987, 492)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.UýGroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPaletEtiketBasim"
        Me.Text = "Palet Etiket Basim"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UýGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UýGroupBox1.ResumeLayout(False)
        Me.UýGroupBox1.PerformLayout()
        CType(Me.UýGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UýGroupBox2.ResumeLayout(False)
        Me.UýGroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rdbTumu As System.Windows.Forms.RadioButton
    Friend WithEvents rdbYazdirilmadi As System.Windows.Forms.RadioButton
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtIrsaliye As System.Windows.Forms.TextBox
    Friend WithEvents btnSorgula As Janus.Windows.EditControls.UIButton
    Friend WithEvents txtCeklistNo As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents UýGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents btnOlustur As Janus.Windows.EditControls.UIButton
    Friend WithEvents UýGroupBox2 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents rdbYazdirildi As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
