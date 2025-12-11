<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIsEmriEtiketi
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIsEmriEtiketi))
        Dim GridEXLayout2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbDurum = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnTemizle = New Janus.Windows.EditControls.UIButton()
        Me.btnYazdir = New Janus.Windows.EditControls.UIButton()
        Me.EdtIsemriTanimi = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.EdtIsemriKodu = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.btnListele = New Janus.Windows.EditControls.UIButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GridEXBarkod = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridEXBarkod, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbDurum)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnTemizle)
        Me.GroupBox1.Controls.Add(Me.btnYazdir)
        Me.GroupBox1.Controls.Add(Me.EdtIsemriTanimi)
        Me.GroupBox1.Controls.Add(Me.EdtIsemriKodu)
        Me.GroupBox1.Controls.Add(Me.btnListele)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(855, 155)
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
        Me.cmbDurum.Location = New System.Drawing.Point(88, 67)
        Me.cmbDurum.Name = "cmbDurum"
        Me.cmbDurum.Size = New System.Drawing.Size(106, 20)
        Me.cmbDurum.TabIndex = 30
        Me.cmbDurum.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.cmbDurum.ValueMember = "Durum Kodu"
        Me.cmbDurum.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(23, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 71
        Me.Label2.Text = "Durum      :"
        Me.Label2.Visible = False
        '
        'btnTemizle
        '
        Me.btnTemizle.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnTemizle.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnTemizle.Location = New System.Drawing.Point(356, 70)
        Me.btnTemizle.Name = "btnTemizle"
        Me.btnTemizle.Size = New System.Drawing.Size(93, 36)
        Me.btnTemizle.TabIndex = 40
        Me.btnTemizle.Text = "Temizle"
        Me.btnTemizle.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnYazdir
        '
        Me.btnYazdir.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnYazdir.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnYazdir.Location = New System.Drawing.Point(356, 110)
        Me.btnYazdir.Name = "btnYazdir"
        Me.btnYazdir.Size = New System.Drawing.Size(93, 36)
        Me.btnYazdir.TabIndex = 45
        Me.btnYazdir.Text = "Yazdır"
        Me.btnYazdir.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'EdtIsemriTanimi
        '
        Me.EdtIsemriTanimi.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.EdtIsemriTanimi.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.EdtIsemriTanimi.Enabled = False
        Me.EdtIsemriTanimi.Location = New System.Drawing.Point(175, 29)
        Me.EdtIsemriTanimi.Name = "EdtIsemriTanimi"
        Me.EdtIsemriTanimi.ReadOnly = True
        Me.EdtIsemriTanimi.Size = New System.Drawing.Size(144, 20)
        Me.EdtIsemriTanimi.TabIndex = 10
        Me.EdtIsemriTanimi.TabStop = False
        Me.EdtIsemriTanimi.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'EdtIsemriKodu
        '
        Me.EdtIsemriKodu.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.EdtIsemriKodu.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.EdtIsemriKodu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.EdtIsemriKodu.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.EdtIsemriKodu.Location = New System.Drawing.Point(89, 29)
        Me.EdtIsemriKodu.Name = "EdtIsemriKodu"
        Me.EdtIsemriKodu.Size = New System.Drawing.Size(80, 20)
        Me.EdtIsemriKodu.TabIndex = 5
        Me.EdtIsemriKodu.Tag = ""
        Me.EdtIsemriKodu.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'btnListele
        '
        Me.btnListele.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnListele.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnListele.Location = New System.Drawing.Point(356, 29)
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
        Me.Label1.Location = New System.Drawing.Point(22, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 48
        Me.Label1.Text = "İşemri       :"
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
        Me.GridEXBarkod.Location = New System.Drawing.Point(0, 155)
        Me.GridEXBarkod.Name = "GridEXBarkod"
        Me.GridEXBarkod.Size = New System.Drawing.Size(855, 341)
        Me.GridEXBarkod.TabIndex = 17
        Me.GridEXBarkod.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'frmIsEmriEtiketi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(855, 496)
        Me.Controls.Add(Me.GridEXBarkod)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "frmIsEmriEtiketi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "İşemri Etiketi"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.GridEXBarkod, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnYazdir As Janus.Windows.EditControls.UIButton
    Friend WithEvents EdtIsemriKodu As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents btnListele As Janus.Windows.EditControls.UIButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GridEXBarkod As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnTemizle As Janus.Windows.EditControls.UIButton
    Friend WithEvents EdtIsemriTanimi As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbDurum As Janus.Windows.GridEX.EditControls.MultiColumnCombo
End Class
