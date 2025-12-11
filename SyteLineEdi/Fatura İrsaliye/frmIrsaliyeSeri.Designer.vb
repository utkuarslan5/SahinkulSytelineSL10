<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIrsaliyeSeri
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIrsaliyeSeri))
        Dim GridEXLayout2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.txtTeslimAlan = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtIrsaliyeSeri = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNavlunFatura = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtPlakaNo = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnOlustur = New Janus.Windows.EditControls.UIButton()
        Me.btnSorgula = New Janus.Windows.EditControls.UIButton()
        Me.UıGroupBox1 = New Janus.Windows.EditControls.UIGroupBox()
        Me.txtNavlunNo = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtSeferNo = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbNakliyeci = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbTransport = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.txtBeyanNo = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dtmBeyanTarihi = New Janus.Windows.CalendarCombo.CalendarCombo()
        CType(Me.UıGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UıGroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtTeslimAlan
        '
        Me.txtTeslimAlan.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtTeslimAlan.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtTeslimAlan.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtTeslimAlan.Location = New System.Drawing.Point(123, 62)
        Me.txtTeslimAlan.MaxLength = 20
        Me.txtTeslimAlan.Name = "txtTeslimAlan"
        Me.txtTeslimAlan.Size = New System.Drawing.Size(191, 20)
        Me.txtTeslimAlan.TabIndex = 1
        Me.txtTeslimAlan.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(17, 66)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 13)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "Teslim Alan....:"
        '
        'txtIrsaliyeSeri
        '
        Me.txtIrsaliyeSeri.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtIrsaliyeSeri.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtIrsaliyeSeri.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtIrsaliyeSeri.Location = New System.Drawing.Point(123, 36)
        Me.txtIrsaliyeSeri.MaxLength = 30
        Me.txtIrsaliyeSeri.Name = "txtIrsaliyeSeri"
        Me.txtIrsaliyeSeri.Size = New System.Drawing.Size(191, 20)
        Me.txtIrsaliyeSeri.TabIndex = 0
        Me.txtIrsaliyeSeri.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "İrsaliye No....:"
        '
        'txtNavlunFatura
        '
        Me.txtNavlunFatura.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtNavlunFatura.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtNavlunFatura.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtNavlunFatura.Location = New System.Drawing.Point(123, 88)
        Me.txtNavlunFatura.MaxLength = 20
        Me.txtNavlunFatura.Name = "txtNavlunFatura"
        Me.txtNavlunFatura.Size = New System.Drawing.Size(191, 20)
        Me.txtNavlunFatura.TabIndex = 2
        Me.txtNavlunFatura.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtPlakaNo
        '
        Me.txtPlakaNo.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtPlakaNo.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtPlakaNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtPlakaNo.Location = New System.Drawing.Point(123, 114)
        Me.txtPlakaNo.MaxLength = 30
        Me.txtPlakaNo.Name = "txtPlakaNo"
        Me.txtPlakaNo.Size = New System.Drawing.Size(191, 20)
        Me.txtPlakaNo.TabIndex = 3
        Me.txtPlakaNo.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Plaka No....:"
        '
        'btnOlustur
        '
        Me.btnOlustur.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnOlustur.Image = Global.SyteLineEdi.My.Resources.Resources.Error1
        Me.btnOlustur.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnOlustur.Location = New System.Drawing.Point(181, 342)
        Me.btnOlustur.Name = "btnOlustur"
        Me.btnOlustur.Size = New System.Drawing.Size(82, 32)
        Me.btnOlustur.TabIndex = 7
        Me.btnOlustur.Text = "Vazgeç"
        Me.btnOlustur.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnSorgula
        '
        Me.btnSorgula.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnSorgula.Image = Global.SyteLineEdi.My.Resources.Resources._Select
        Me.btnSorgula.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnSorgula.Location = New System.Drawing.Point(63, 342)
        Me.btnSorgula.Name = "btnSorgula"
        Me.btnSorgula.Size = New System.Drawing.Size(83, 32)
        Me.btnSorgula.TabIndex = 6
        Me.btnSorgula.Text = "Tamam"
        Me.btnSorgula.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'UıGroupBox1
        '
        Me.UıGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.UıGroupBox1.Controls.Add(Me.dtmBeyanTarihi)
        Me.UıGroupBox1.Controls.Add(Me.Label10)
        Me.UıGroupBox1.Controls.Add(Me.txtBeyanNo)
        Me.UıGroupBox1.Controls.Add(Me.Label9)
        Me.UıGroupBox1.Controls.Add(Me.txtNavlunNo)
        Me.UıGroupBox1.Controls.Add(Me.Label2)
        Me.UıGroupBox1.Controls.Add(Me.Label8)
        Me.UıGroupBox1.Controls.Add(Me.Label7)
        Me.UıGroupBox1.Controls.Add(Me.txtSeferNo)
        Me.UıGroupBox1.Controls.Add(Me.Label5)
        Me.UıGroupBox1.Controls.Add(Me.cmbNakliyeci)
        Me.UıGroupBox1.Controls.Add(Me.Label4)
        Me.UıGroupBox1.Controls.Add(Me.cmbTransport)
        Me.UıGroupBox1.Controls.Add(Me.txtIrsaliyeSeri)
        Me.UıGroupBox1.Controls.Add(Me.btnOlustur)
        Me.UıGroupBox1.Controls.Add(Me.Label1)
        Me.UıGroupBox1.Controls.Add(Me.btnSorgula)
        Me.UıGroupBox1.Controls.Add(Me.txtNavlunFatura)
        Me.UıGroupBox1.Controls.Add(Me.txtTeslimAlan)
        Me.UıGroupBox1.Controls.Add(Me.Label6)
        Me.UıGroupBox1.Controls.Add(Me.Label3)
        Me.UıGroupBox1.Controls.Add(Me.txtPlakaNo)
        Me.UıGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UıGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.UıGroupBox1.Name = "UıGroupBox1"
        Me.UıGroupBox1.Size = New System.Drawing.Size(348, 405)
        Me.UıGroupBox1.TabIndex = 29
        Me.UıGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'txtNavlunNo
        '
        Me.txtNavlunNo.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtNavlunNo.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtNavlunNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtNavlunNo.Location = New System.Drawing.Point(123, 219)
        Me.txtNavlunNo.MaxLength = 20
        Me.txtNavlunNo.Name = "txtNavlunNo"
        Me.txtNavlunNo.Size = New System.Drawing.Size(191, 20)
        Me.txtNavlunNo.TabIndex = 38
        Me.txtNavlunNo.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 225)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Navlun No....:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(20, 92)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 13)
        Me.Label8.TabIndex = 36
        Me.Label8.Text = "Teslim Eden....:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 196)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 13)
        Me.Label7.TabIndex = 34
        Me.Label7.Text = "Sefer No....:"
        '
        'txtSeferNo
        '
        Me.txtSeferNo.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtSeferNo.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtSeferNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtSeferNo.Location = New System.Drawing.Point(123, 192)
        Me.txtSeferNo.MaxLength = 30
        Me.txtSeferNo.Name = "txtSeferNo"
        Me.txtSeferNo.Size = New System.Drawing.Size(191, 20)
        Me.txtSeferNo.TabIndex = 33
        Me.txtSeferNo.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 170)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Nakliyeci....:"
        '
        'cmbNakliyeci
        '
        Me.cmbNakliyeci.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.cmbNakliyeci.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.cmbNakliyeci.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.cmbNakliyeci.DesignTimeLayout = GridEXLayout1
        Me.cmbNakliyeci.DisplayMember = "ship_code"
        Me.cmbNakliyeci.Location = New System.Drawing.Point(123, 166)
        Me.cmbNakliyeci.Name = "cmbNakliyeci"
        Me.cmbNakliyeci.Size = New System.Drawing.Size(191, 20)
        Me.cmbNakliyeci.TabIndex = 5
        Me.cmbNakliyeci.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.cmbNakliyeci.ValueMember = "ship_code"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 144)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 13)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Nakliye Şekli....:"
        '
        'cmbTransport
        '
        Me.cmbTransport.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.cmbTransport.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.cmbTransport.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        GridEXLayout2.LayoutString = resources.GetString("GridEXLayout2.LayoutString")
        Me.cmbTransport.DesignTimeLayout = GridEXLayout2
        Me.cmbTransport.DisplayMember = "delterm"
        Me.cmbTransport.Location = New System.Drawing.Point(123, 140)
        Me.cmbTransport.Name = "cmbTransport"
        Me.cmbTransport.Size = New System.Drawing.Size(191, 20)
        Me.cmbTransport.TabIndex = 4
        Me.cmbTransport.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.cmbTransport.ValueMember = "delterm"
        '
        'txtBeyanNo
        '
        Me.txtBeyanNo.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtBeyanNo.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtBeyanNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtBeyanNo.Location = New System.Drawing.Point(123, 245)
        Me.txtBeyanNo.MaxLength = 20
        Me.txtBeyanNo.Name = "txtBeyanNo"
        Me.txtBeyanNo.Size = New System.Drawing.Size(191, 20)
        Me.txtBeyanNo.TabIndex = 40
        Me.txtBeyanNo.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(17, 251)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 13)
        Me.Label9.TabIndex = 39
        Me.Label9.Text = "Beyan No....:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(17, 277)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(81, 13)
        Me.Label10.TabIndex = 41
        Me.Label10.Text = "Beyan Tarihi....:"
        '
        'dtmBeyanTarihi
        '
        '
        '
        '
        Me.dtmBeyanTarihi.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtmBeyanTarihi.DropDownCalendar.Name = ""
        Me.dtmBeyanTarihi.DropDownCalendar.Size = New System.Drawing.Size(164, 167)
        Me.dtmBeyanTarihi.DropDownCalendar.TabIndex = 0
        Me.dtmBeyanTarihi.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard
        Me.dtmBeyanTarihi.Location = New System.Drawing.Point(123, 271)
        Me.dtmBeyanTarihi.Name = "dtmBeyanTarihi"
        Me.dtmBeyanTarihi.Size = New System.Drawing.Size(103, 20)
        Me.dtmBeyanTarihi.TabIndex = 42
        Me.dtmBeyanTarihi.Value = New Date(1, 2, 1, 0, 0, 0, 0)
        '
        'frmIrsaliyeSeri
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(348, 405)
        Me.Controls.Add(Me.UıGroupBox1)
        Me.Name = "frmIrsaliyeSeri"
        Me.Text = "İrsaliye Seri No Girişi"
        CType(Me.UıGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UıGroupBox1.ResumeLayout(False)
        Me.UıGroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtTeslimAlan As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtIrsaliyeSeri As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNavlunFatura As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtPlakaNo As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnOlustur As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSorgula As Janus.Windows.EditControls.UIButton
    Friend WithEvents UıGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbTransport As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbNakliyeci As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSeferNo As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtNavlunNo As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtBeyanNo As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtmBeyanTarihi As Janus.Windows.CalendarCombo.CalendarCombo
End Class
