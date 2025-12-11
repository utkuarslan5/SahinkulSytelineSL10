<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class frmSatinAlmaGirisEtiketi
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Friend  WithEvents btnListele As Janus.Windows.EditControls.UIButton
    Friend  WithEvents btnTemizle As Janus.Windows.EditControls.UIButton
    Friend  WithEvents btnYazdir As Janus.Windows.EditControls.UIButton
    Friend  WithEvents cmbDurum As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend  WithEvents dtTarihBaslar As Janus.Windows.CalendarCombo.CalendarCombo
    Friend  WithEvents dtTarihBiter As Janus.Windows.CalendarCombo.CalendarCombo
    Friend  WithEvents EdtIrsaliyeNo As Janus.Windows.GridEX.EditControls.EditBox
    Friend  WithEvents EdtSaticiAdi As Janus.Windows.GridEX.EditControls.EditBox
    Friend  WithEvents EdtSaticiKodu As Janus.Windows.GridEX.EditControls.EditBox
    Friend  WithEvents GridEXBarkod As Janus.Windows.GridEX.GridEX
    Friend  WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend  WithEvents Label1 As System.Windows.Forms.Label
    Friend  WithEvents Label2 As System.Windows.Forms.Label
    Friend  WithEvents Label3 As System.Windows.Forms.Label
    Friend  WithEvents Label4 As System.Windows.Forms.Label
    Friend  WithEvents rbFason As System.Windows.Forms.RadioButton
    Friend  WithEvents rbSatinalma As System.Windows.Forms.RadioButton

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    #End Region 'Fields

    #Region "Methods"

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough> _
    Private Sub InitializeComponent()
        Dim GridEXLayout1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSatinAlmaGirisEtiketi))
        Dim GridEXLayout2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbDurum = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnTemizle = New Janus.Windows.EditControls.UIButton()
        Me.dtTarihBiter = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.EdtIrsaliyeNo = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnYazdir = New Janus.Windows.EditControls.UIButton()
        Me.EdtSaticiAdi = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.EdtSaticiKodu = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtTarihBaslar = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.btnListele = New Janus.Windows.EditControls.UIButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GridEXBarkod = New Janus.Windows.GridEX.GridEX()
        Me.rbSatinalma = New System.Windows.Forms.RadioButton()
        Me.rbFason = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridEXBarkod, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbFason)
        Me.GroupBox1.Controls.Add(Me.rbSatinalma)
        Me.GroupBox1.Controls.Add(Me.cmbDurum)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnTemizle)
        Me.GroupBox1.Controls.Add(Me.dtTarihBiter)
        Me.GroupBox1.Controls.Add(Me.EdtIrsaliyeNo)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btnYazdir)
        Me.GroupBox1.Controls.Add(Me.EdtSaticiAdi)
        Me.GroupBox1.Controls.Add(Me.EdtSaticiKodu)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.dtTarihBaslar)
        Me.GroupBox1.Controls.Add(Me.btnListele)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(855, 190)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'cmbDurum
        '
        Me.cmbDurum.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.cmbDurum.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.cmbDurum.ComboStyle = Janus.Windows.GridEX.ComboStyle.DropDownList
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.cmbDurum.DesignTimeLayout = GridEXLayout1
        Me.cmbDurum.DisplayMember = "Durum Adı"
        Me.cmbDurum.Location = New System.Drawing.Point(88, 163)
        Me.cmbDurum.Name = "cmbDurum"
        Me.cmbDurum.Size = New System.Drawing.Size(106, 20)
        Me.cmbDurum.TabIndex = 30
        Me.cmbDurum.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.cmbDurum.ValueMember = "Durum Kodu"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(23, 163)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 71
        Me.Label2.Text = "Durum      :"
        '
        'btnTemizle
        '
        Me.btnTemizle.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnTemizle.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnTemizle.Location = New System.Drawing.Point(494, 89)
        Me.btnTemizle.Name = "btnTemizle"
        Me.btnTemizle.Size = New System.Drawing.Size(93, 36)
        Me.btnTemizle.TabIndex = 40
        Me.btnTemizle.Text = "Temizle"
        Me.btnTemizle.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'dtTarihBiter
        '
        '
        '
        '
        Me.dtTarihBiter.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtTarihBiter.DropDownCalendar.Name = ""
        Me.dtTarihBiter.DropDownCalendar.Size = New System.Drawing.Size(164, 167)
        Me.dtTarihBiter.DropDownCalendar.TabIndex = 0
        Me.dtTarihBiter.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard
        Me.dtTarihBiter.Location = New System.Drawing.Point(214, 124)
        Me.dtTarihBiter.Name = "dtTarihBiter"
        Me.dtTarihBiter.Size = New System.Drawing.Size(105, 20)
        Me.dtTarihBiter.TabIndex = 25
        Me.dtTarihBiter.Value = New Date(2010, 11, 1, 0, 0, 0, 0)
        '
        'EdtIrsaliyeNo
        '
        Me.EdtIrsaliyeNo.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.EdtIrsaliyeNo.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.EdtIrsaliyeNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.EdtIrsaliyeNo.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.EdtIrsaliyeNo.Location = New System.Drawing.Point(89, 86)
        Me.EdtIrsaliyeNo.Name = "EdtIrsaliyeNo"
        Me.EdtIrsaliyeNo.Size = New System.Drawing.Size(80, 20)
        Me.EdtIrsaliyeNo.TabIndex = 15
        Me.EdtIrsaliyeNo.Tag = ""
        Me.EdtIrsaliyeNo.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(22, 90)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 13)
        Me.Label4.TabIndex = 70
        Me.Label4.Text = "İrsaliye No :"
        '
        'btnYazdir
        '
        Me.btnYazdir.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnYazdir.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnYazdir.Location = New System.Drawing.Point(494, 129)
        Me.btnYazdir.Name = "btnYazdir"
        Me.btnYazdir.Size = New System.Drawing.Size(93, 36)
        Me.btnYazdir.TabIndex = 45
        Me.btnYazdir.Text = "Yazdır"
        Me.btnYazdir.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'EdtSaticiAdi
        '
        Me.EdtSaticiAdi.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.EdtSaticiAdi.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.EdtSaticiAdi.Enabled = False
        Me.EdtSaticiAdi.Location = New System.Drawing.Point(175, 48)
        Me.EdtSaticiAdi.Name = "EdtSaticiAdi"
        Me.EdtSaticiAdi.ReadOnly = True
        Me.EdtSaticiAdi.Size = New System.Drawing.Size(144, 20)
        Me.EdtSaticiAdi.TabIndex = 10
        Me.EdtSaticiAdi.TabStop = False
        Me.EdtSaticiAdi.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'EdtSaticiKodu
        '
        Me.EdtSaticiKodu.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.EdtSaticiKodu.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.EdtSaticiKodu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.EdtSaticiKodu.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.EdtSaticiKodu.Location = New System.Drawing.Point(89, 48)
        Me.EdtSaticiKodu.Name = "EdtSaticiKodu"
        Me.EdtSaticiKodu.Size = New System.Drawing.Size(80, 20)
        Me.EdtSaticiKodu.TabIndex = 5
        Me.EdtSaticiKodu.Tag = ""
        Me.EdtSaticiKodu.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(22, 127)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 56
        Me.Label3.Text = "Tarih         :"
        '
        'dtTarihBaslar
        '
        '
        '
        '
        Me.dtTarihBaslar.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtTarihBaslar.DropDownCalendar.Name = ""
        Me.dtTarihBaslar.DropDownCalendar.Size = New System.Drawing.Size(164, 167)
        Me.dtTarihBaslar.DropDownCalendar.TabIndex = 0
        Me.dtTarihBaslar.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard
        Me.dtTarihBaslar.Location = New System.Drawing.Point(89, 124)
        Me.dtTarihBaslar.Name = "dtTarihBaslar"
        Me.dtTarihBaslar.Size = New System.Drawing.Size(105, 20)
        Me.dtTarihBaslar.TabIndex = 20
        Me.dtTarihBaslar.Value = New Date(2010, 11, 1, 0, 0, 0, 0)
        '
        'btnListele
        '
        Me.btnListele.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnListele.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnListele.Location = New System.Drawing.Point(494, 48)
        Me.btnListele.Name = "btnListele"
        Me.btnListele.Size = New System.Drawing.Size(93, 36)
        Me.btnListele.TabIndex = 35
        Me.btnListele.Text = "Listele"
        Me.btnListele.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(22, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 48
        Me.Label1.Text = "Satıcı        :"
        '
        'GridEXBarkod
        '
        Me.GridEXBarkod.AlternatingColors = True
        Me.GridEXBarkod.BuiltInTextsData = "<LocalizableData ID=""LocalizableStrings"" Collection=""true""><GroupByBoxInfo>Grupla" & _
            "mak istediğiniz alanı sürükleyip bırakınız</GroupByBoxInfo></LocalizableData>"
        Me.GridEXBarkod.ColumnAutoResize = True
        GridEXLayout2.LayoutString = resources.GetString("GridEXLayout2.LayoutString")
        Me.GridEXBarkod.DesignTimeLayout = GridEXLayout2
        Me.GridEXBarkod.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEXBarkod.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.GridEXBarkod.EmptyRows = True
        Me.GridEXBarkod.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEXBarkod.GroupByBoxVisible = False
        Me.GridEXBarkod.IncrementalSearchMode = Janus.Windows.GridEX.IncrementalSearchMode.FirstCharacter
        Me.GridEXBarkod.Location = New System.Drawing.Point(0, 190)
        Me.GridEXBarkod.Name = "GridEXBarkod"
        Me.GridEXBarkod.Size = New System.Drawing.Size(855, 306)
        Me.GridEXBarkod.TabIndex = 17
        Me.GridEXBarkod.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'rbSatinalma
        '
        Me.rbSatinalma.AutoSize = True
        Me.rbSatinalma.Checked = True
        Me.rbSatinalma.Location = New System.Drawing.Point(392, 50)
        Me.rbSatinalma.Name = "rbSatinalma"
        Me.rbSatinalma.Size = New System.Drawing.Size(71, 17)
        Me.rbSatinalma.TabIndex = 72
        Me.rbSatinalma.TabStop = True
        Me.rbSatinalma.Text = "Satınalma"
        Me.rbSatinalma.UseVisualStyleBackColor = True
        '
        'rbFason
        '
        Me.rbFason.AutoSize = True
        Me.rbFason.Location = New System.Drawing.Point(392, 74)
        Me.rbFason.Name = "rbFason"
        Me.rbFason.Size = New System.Drawing.Size(54, 17)
        Me.rbFason.TabIndex = 73
        Me.rbFason.Text = "Fason"
        Me.rbFason.UseVisualStyleBackColor = True
        '
        'frmSatinAlmaGirisEtiketi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(855, 496)
        Me.Controls.Add(Me.GridEXBarkod)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "frmSatinAlmaGirisEtiketi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SatınAlma Giriş Etiketi"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.GridEXBarkod, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    #End Region 'Methods

End Class