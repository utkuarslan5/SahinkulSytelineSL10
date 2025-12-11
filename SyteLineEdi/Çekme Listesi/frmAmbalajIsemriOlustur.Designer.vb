<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAmbalajIsemriOlustur
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAmbalajIsemriOlustur))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.btnisemriolustur = New Janus.Windows.EditControls.UIButton
        Me.txtMusteriAdi2 = New System.Windows.Forms.TextBox
        Me.txtMusteriAdi1 = New System.Windows.Forms.TextBox
        Me.btnSorgula = New Janus.Windows.EditControls.UIButton
        Me.txtKullanici = New Janus.Windows.GridEX.EditControls.EditBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtCekmeListeNo = New Janus.Windows.GridEX.EditControls.EditBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnUnCheckedAll = New Janus.Windows.EditControls.UIButton
        Me.btnCheckedAll = New Janus.Windows.EditControls.UIButton
        Me.txtKapi2 = New Janus.Windows.GridEX.EditControls.EditBox
        Me.txtKapi1 = New Janus.Windows.GridEX.EditControls.EditBox
        Me.txtTeslimAlan2 = New Janus.Windows.GridEX.EditControls.EditBox
        Me.txtTeslimAlan1 = New Janus.Windows.GridEX.EditControls.EditBox
        Me.txtAmbar1 = New Janus.Windows.GridEX.EditControls.EditBox
        Me.txtMusteri2 = New Janus.Windows.GridEX.EditControls.EditBox
        Me.txtMusteri1 = New Janus.Windows.GridEX.EditControls.EditBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtmTeslimTarihi = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Controls.Add(Me.btnisemriolustur)
        Me.GroupBox1.Controls.Add(Me.txtMusteriAdi2)
        Me.GroupBox1.Controls.Add(Me.txtMusteriAdi1)
        Me.GroupBox1.Controls.Add(Me.btnSorgula)
        Me.GroupBox1.Controls.Add(Me.txtKullanici)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtCekmeListeNo)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.btnUnCheckedAll)
        Me.GroupBox1.Controls.Add(Me.btnCheckedAll)
        Me.GroupBox1.Controls.Add(Me.txtKapi2)
        Me.GroupBox1.Controls.Add(Me.txtKapi1)
        Me.GroupBox1.Controls.Add(Me.txtTeslimAlan2)
        Me.GroupBox1.Controls.Add(Me.txtTeslimAlan1)
        Me.GroupBox1.Controls.Add(Me.txtAmbar1)
        Me.GroupBox1.Controls.Add(Me.txtMusteri2)
        Me.GroupBox1.Controls.Add(Me.txtMusteri1)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtmTeslimTarihi)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(930, 169)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox1.Location = New System.Drawing.Point(227, 101)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox1.TabIndex = 32
        Me.CheckBox1.UseVisualStyleBackColor = False
        '
        'btnisemriolustur
        '
        Me.btnisemriolustur.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnisemriolustur.Image = Global.SyteLineEdi.My.Resources.Resources.NetByte_Design_Studio___0917
        Me.btnisemriolustur.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnisemriolustur.Location = New System.Drawing.Point(474, 127)
        Me.btnisemriolustur.Name = "btnisemriolustur"
        Me.btnisemriolustur.Size = New System.Drawing.Size(127, 36)
        Me.btnisemriolustur.TabIndex = 31
        Me.btnisemriolustur.Text = "Ýþ Emri Oluþtur"
        Me.btnisemriolustur.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'txtMusteriAdi2
        '
        Me.txtMusteriAdi2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMusteriAdi2.Enabled = False
        Me.txtMusteriAdi2.Location = New System.Drawing.Point(462, 42)
        Me.txtMusteriAdi2.Name = "txtMusteriAdi2"
        Me.txtMusteriAdi2.Size = New System.Drawing.Size(209, 20)
        Me.txtMusteriAdi2.TabIndex = 30
        '
        'txtMusteriAdi1
        '
        Me.txtMusteriAdi1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMusteriAdi1.Enabled = False
        Me.txtMusteriAdi1.Location = New System.Drawing.Point(462, 19)
        Me.txtMusteriAdi1.Name = "txtMusteriAdi1"
        Me.txtMusteriAdi1.Size = New System.Drawing.Size(209, 20)
        Me.txtMusteriAdi1.TabIndex = 29
        '
        'btnSorgula
        '
        Me.btnSorgula.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnSorgula.Image = Global.SyteLineEdi.My.Resources.Resources.Search
        Me.btnSorgula.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnSorgula.Location = New System.Drawing.Point(353, 127)
        Me.btnSorgula.Name = "btnSorgula"
        Me.btnSorgula.Size = New System.Drawing.Size(103, 36)
        Me.btnSorgula.TabIndex = 28
        Me.btnSorgula.Text = "Sorgula"
        Me.btnSorgula.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'txtKullanici
        '
        Me.txtKullanici.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtKullanici.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtKullanici.Location = New System.Drawing.Point(118, 45)
        Me.txtKullanici.Name = "txtKullanici"
        Me.txtKullanici.Size = New System.Drawing.Size(103, 20)
        Me.txtKullanici.TabIndex = 22
        Me.txtKullanici.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(15, 49)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(61, 13)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Kullanýcý....:"
        '
        'txtCekmeListeNo
        '
        Me.txtCekmeListeNo.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtCekmeListeNo.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtCekmeListeNo.Location = New System.Drawing.Point(118, 71)
        Me.txtCekmeListeNo.Name = "txtCekmeListeNo"
        Me.txtCekmeListeNo.Size = New System.Drawing.Size(103, 20)
        Me.txtCekmeListeNo.TabIndex = 19
        Me.txtCekmeListeNo.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(15, 78)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(97, 13)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Çekme Liste No....:"
        '
        'btnUnCheckedAll
        '
        Me.btnUnCheckedAll.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnUnCheckedAll.Image = Global.SyteLineEdi.My.Resources.Resources.Close
        Me.btnUnCheckedAll.Location = New System.Drawing.Point(125, 140)
        Me.btnUnCheckedAll.Name = "btnUnCheckedAll"
        Me.btnUnCheckedAll.Size = New System.Drawing.Size(114, 24)
        Me.btnUnCheckedAll.TabIndex = 16
        Me.btnUnCheckedAll.Text = "Seçileni Ýptal Et"
        Me.btnUnCheckedAll.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnCheckedAll
        '
        Me.btnCheckedAll.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnCheckedAll.Image = Global.SyteLineEdi.My.Resources.Resources.Checkmark
        Me.btnCheckedAll.Location = New System.Drawing.Point(24, 140)
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
        Me.txtKapi2.Location = New System.Drawing.Point(462, 94)
        Me.txtKapi2.Name = "txtKapi2"
        Me.txtKapi2.Size = New System.Drawing.Size(103, 20)
        Me.txtKapi2.TabIndex = 8
        Me.txtKapi2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtKapi1
        '
        Me.txtKapi1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtKapi1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtKapi1.Location = New System.Drawing.Point(353, 94)
        Me.txtKapi1.Name = "txtKapi1"
        Me.txtKapi1.Size = New System.Drawing.Size(103, 20)
        Me.txtKapi1.TabIndex = 7
        Me.txtKapi1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtTeslimAlan2
        '
        Me.txtTeslimAlan2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtTeslimAlan2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtTeslimAlan2.Location = New System.Drawing.Point(462, 68)
        Me.txtTeslimAlan2.Name = "txtTeslimAlan2"
        Me.txtTeslimAlan2.Size = New System.Drawing.Size(103, 20)
        Me.txtTeslimAlan2.TabIndex = 6
        Me.txtTeslimAlan2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtTeslimAlan1
        '
        Me.txtTeslimAlan1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtTeslimAlan1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtTeslimAlan1.Location = New System.Drawing.Point(353, 68)
        Me.txtTeslimAlan1.Name = "txtTeslimAlan1"
        Me.txtTeslimAlan1.Size = New System.Drawing.Size(103, 20)
        Me.txtTeslimAlan1.TabIndex = 5
        Me.txtTeslimAlan1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtAmbar1
        '
        Me.txtAmbar1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtAmbar1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtAmbar1.Location = New System.Drawing.Point(118, 19)
        Me.txtAmbar1.Name = "txtAmbar1"
        Me.txtAmbar1.Size = New System.Drawing.Size(103, 20)
        Me.txtAmbar1.TabIndex = 1
        Me.txtAmbar1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtMusteri2
        '
        Me.txtMusteri2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtMusteri2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtMusteri2.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtMusteri2.Location = New System.Drawing.Point(353, 42)
        Me.txtMusteri2.Name = "txtMusteri2"
        Me.txtMusteri2.Size = New System.Drawing.Size(103, 20)
        Me.txtMusteri2.TabIndex = 4
        Me.txtMusteri2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtMusteri1
        '
        Me.txtMusteri1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtMusteri1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtMusteri1.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtMusteri1.Location = New System.Drawing.Point(353, 19)
        Me.txtMusteri1.Name = "txtMusteri1"
        Me.txtMusteri1.Size = New System.Drawing.Size(103, 20)
        Me.txtMusteri1.TabIndex = 3
        Me.txtMusteri1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(15, 104)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "ÇekmeTarihi....:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(271, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Kapý....:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(271, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Teslim Alan....:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(271, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Müþteri...:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(15, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Ambar....:"
        '
        'dtmTeslimTarihi
        '
        '
        '
        '
        Me.dtmTeslimTarihi.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtmTeslimTarihi.DropDownCalendar.Name = ""
        Me.dtmTeslimTarihi.DropDownCalendar.Size = New System.Drawing.Size(164, 167)
        Me.dtmTeslimTarihi.DropDownCalendar.TabIndex = 0
        Me.dtmTeslimTarihi.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard
        Me.dtmTeslimTarihi.Enabled = False
        Me.dtmTeslimTarihi.Location = New System.Drawing.Point(118, 97)
        Me.dtmTeslimTarihi.Name = "dtmTeslimTarihi"
        Me.dtmTeslimTarihi.Size = New System.Drawing.Size(103, 20)
        Me.dtmTeslimTarihi.TabIndex = 9
        '
        'GridEX1
        '
        Me.GridEX1.AlternatingColors = True
        Me.GridEX1.BuiltInTextsData = "<LocalizableData ID=""LocalizableStrings"" Collection=""true""><GroupByBoxInfo>Grupla" & _
            "mak istediðiniz alaný sürükleyip býrakýnýz</GroupByBoxInfo></LocalizableData>"
        Me.GridEX1.ColumnAutoResize = True
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEXLayout1
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.Location = New System.Drawing.Point(0, 169)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(930, 567)
        Me.GridEX1.TabIndex = 9
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'frmAmbalajIsemriOlustur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DodgerBlue
        Me.ClientSize = New System.Drawing.Size(930, 736)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximumSize = New System.Drawing.Size(999, 1000)
        Me.MinimumSize = New System.Drawing.Size(100, 736)
        Me.Name = "frmAmbalajIsemriOlustur"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ambalaj Ýþ Emirleri"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUnCheckedAll As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnCheckedAll As Janus.Windows.EditControls.UIButton
    Friend WithEvents txtKapi2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtKapi1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtTeslimAlan2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtTeslimAlan1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtAmbar1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtMusteri2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtMusteri1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtmTeslimTarihi As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents txtCekmeListeNo As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtKullanici As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnSorgula As Janus.Windows.EditControls.UIButton
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtMusteriAdi2 As System.Windows.Forms.TextBox
    Friend WithEvents txtMusteriAdi1 As System.Windows.Forms.TextBox
    Friend WithEvents btnisemriolustur As Janus.Windows.EditControls.UIButton
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
End Class
