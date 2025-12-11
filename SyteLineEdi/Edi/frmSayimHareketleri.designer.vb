<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class frmSayimHareketleri
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Friend  WithEvents btnHurdaOlustur As Janus.Windows.EditControls.UIButton
    Friend  WithEvents btnSorgula As Janus.Windows.EditControls.UIButton
    Friend  WithEvents chkCesCik As System.Windows.Forms.CheckBox
    Friend  WithEvents chkCesGir As System.Windows.Forms.CheckBox
    Friend  WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend  WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend  WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend  WithEvents Label1 As System.Windows.Forms.Label
    Friend  WithEvents Label5 As System.Windows.Forms.Label
    Friend  WithEvents txtPath As Janus.Windows.GridEX.EditControls.EditBox

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSayimHareketleri))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkCesCik = New System.Windows.Forms.CheckBox()
        Me.chkCesGir = New System.Windows.Forms.CheckBox()
        Me.btnHurdaOlustur = New Janus.Windows.EditControls.UIButton()
        Me.btnSorgula = New Janus.Windows.EditControls.UIButton()
        Me.txtPath = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.Controls.Add(Me.chkCesCik)
        Me.GroupBox2.Controls.Add(Me.chkCesGir)
        Me.GroupBox2.Controls.Add(Me.btnHurdaOlustur)
        Me.GroupBox2.Controls.Add(Me.btnSorgula)
        Me.GroupBox2.Controls.Add(Me.txtPath)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(938, 124)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'chkCesCik
        '
        Me.chkCesCik.AutoSize = True
        Me.chkCesCik.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.chkCesCik.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCesCik.Location = New System.Drawing.Point(477, 91)
        Me.chkCesCik.Name = "chkCesCik"
        Me.chkCesCik.Size = New System.Drawing.Size(78, 17)
        Me.chkCesCik.TabIndex = 6
        Me.chkCesCik.Text = "Çeşitli Çıkış"
        Me.chkCesCik.UseVisualStyleBackColor = True
        '
        'chkCesGir
        '
        Me.chkCesGir.AutoSize = True
        Me.chkCesGir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.chkCesGir.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCesGir.Location = New System.Drawing.Point(477, 72)
        Me.chkCesGir.Name = "chkCesGir"
        Me.chkCesGir.Size = New System.Drawing.Size(76, 17)
        Me.chkCesGir.TabIndex = 5
        Me.chkCesGir.Text = "Çeşitli Giriş"
        Me.chkCesGir.UseVisualStyleBackColor = True
        '
        'btnHurdaOlustur
        '
        Me.btnHurdaOlustur.ImageSize = New System.Drawing.Size(48, 48)
        Me.btnHurdaOlustur.Location = New System.Drawing.Point(574, 72)
        Me.btnHurdaOlustur.Name = "btnHurdaOlustur"
        Me.btnHurdaOlustur.Size = New System.Drawing.Size(75, 36)
        Me.btnHurdaOlustur.TabIndex = 2
        Me.btnHurdaOlustur.Text = "Oluştur"
        Me.btnHurdaOlustur.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnSorgula
        '
        Me.btnSorgula.ImageSize = New System.Drawing.Size(48, 48)
        Me.btnSorgula.Location = New System.Drawing.Point(378, 72)
        Me.btnSorgula.Name = "btnSorgula"
        Me.btnSorgula.Size = New System.Drawing.Size(75, 36)
        Me.btnSorgula.TabIndex = 1
        Me.btnSorgula.Text = "Sorgula"
        Me.btnSorgula.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'txtPath
        '
        Me.txtPath.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtPath.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPath.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtPath.Location = New System.Drawing.Point(178, 45)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(471, 20)
        Me.txtPath.TabIndex = 0
        Me.txtPath.Tag = "1"
        Me.txtPath.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label5.Location = New System.Drawing.Point(156, -78)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Beden Seti :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(41, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Sayım Dosyası :"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox1.Controls.Add(Me.grd)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 124)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(938, 277)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'grd
        '
        Me.grd.AlternatingColors = True
        Me.grd.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grd.BuiltInTextsData = "<LocalizableData ID=""LocalizableStrings"" Collection=""true""><RecordNavigator>Kayıt" & _
    " Sayısı :|</RecordNavigator></LocalizableData>"
        Me.grd.ColumnAutoResize = True
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.grd.DesignTimeLayout = GridEXLayout1
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grd.EmptyRows = True
        Me.grd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(3, 17)
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(932, 257)
        Me.grd.TabIndex = 1
        Me.grd.TabStop = False
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'frmSayimHareketleri
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(938, 401)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.KeyPreview = True
        Me.Name = "frmSayimHareketleri"
        Me.Text = "Sayım Hareketleri"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    #End Region 'Methods

End Class