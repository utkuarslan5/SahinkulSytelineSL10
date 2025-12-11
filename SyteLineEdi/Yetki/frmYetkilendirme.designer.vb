<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class frmYetkilendirme
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Friend  WithEvents btnAddYetki As Janus.Windows.EditControls.UIButton
    Friend  WithEvents btnKaydet As Janus.Windows.EditControls.UIButton
    Friend  WithEvents btnRemoveYetki As Janus.Windows.EditControls.UIButton
    Friend  WithEvents grdAllYetkiler As Janus.Windows.GridEX.GridEX
    Friend  WithEvents grdKullaniciYetkileri As Janus.Windows.GridEX.GridEX
    Friend  WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend  WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend  WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend  WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend  WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend  WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend  WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend  WithEvents Label1 As System.Windows.Forms.Label
    Friend  WithEvents lblId As System.Windows.Forms.Label
    Friend  WithEvents lblyetki As System.Windows.Forms.Label
    Friend  WithEvents txtkullanici As System.Windows.Forms.TextBox

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
        Dim GridEXLayout4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmYetkilendirme))
        Dim GridEXLayout5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXLayout6 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.txtkullanici = New System.Windows.Forms.TextBox
        Me.lblyetki = New System.Windows.Forms.Label
        Me.btnAddYetki = New Janus.Windows.EditControls.UIButton
        Me.btnRemoveYetki = New Janus.Windows.EditControls.UIButton
        Me.btnKaydet = New Janus.Windows.EditControls.UIButton
        Me.grdAllYetkiler = New Janus.Windows.GridEX.GridEX
        Me.lblId = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grdKullaniciYetkileri = New Janus.Windows.GridEX.GridEX
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.grdAllYetkiler, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdKullaniciYetkileri, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtkullanici
        '
        Me.txtkullanici.BackColor = System.Drawing.Color.Yellow
        Me.txtkullanici.Enabled = False
        Me.txtkullanici.Location = New System.Drawing.Point(9, 59)
        Me.txtkullanici.Name = "txtkullanici"
        Me.txtkullanici.Size = New System.Drawing.Size(104, 20)
        Me.txtkullanici.TabIndex = 1
        '
        'lblyetki
        '
        Me.lblyetki.AutoSize = True
        Me.lblyetki.BackColor = System.Drawing.Color.Transparent
        Me.lblyetki.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblyetki.ForeColor = System.Drawing.Color.Red
        Me.lblyetki.Location = New System.Drawing.Point(231, 6)
        Me.lblyetki.Name = "lblyetki"
        Me.lblyetki.Size = New System.Drawing.Size(100, 16)
        Me.lblyetki.TabIndex = 2
        Me.lblyetki.Text = "Bütün Yetkiler"
        '
        'btnAddYetki
        '
        Me.btnAddYetki.Image = Global.SyteLineEdi.My.Resources.Resources._3617_5784
        Me.btnAddYetki.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnAddYetki.Location = New System.Drawing.Point(6, 27)
        Me.btnAddYetki.Name = "btnAddYetki"
        Me.btnAddYetki.Size = New System.Drawing.Size(88, 34)
        Me.btnAddYetki.TabIndex = 4
        Me.btnAddYetki.Text = ">>  Ekle"
        Me.btnAddYetki.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnRemoveYetki
        '
        Me.btnRemoveYetki.Image = Global.SyteLineEdi.My.Resources.Resources._3616_28881
        Me.btnRemoveYetki.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnRemoveYetki.Location = New System.Drawing.Point(6, 67)
        Me.btnRemoveYetki.Name = "btnRemoveYetki"
        Me.btnRemoveYetki.Size = New System.Drawing.Size(88, 34)
        Me.btnRemoveYetki.TabIndex = 4
        Me.btnRemoveYetki.Text = "<<  Çıkar"
        Me.btnRemoveYetki.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnKaydet
        '
        Me.btnKaydet.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnKaydet.Image = Global.SyteLineEdi.My.Resources.Resources.kaydet
        Me.btnKaydet.ImageSize = New System.Drawing.Size(24, 24)
        Me.btnKaydet.Location = New System.Drawing.Point(3, 139)
        Me.btnKaydet.Name = "btnKaydet"
        Me.btnKaydet.Size = New System.Drawing.Size(91, 32)
        Me.btnKaydet.TabIndex = 4
        Me.btnKaydet.Text = "Kaydet"
        Me.btnKaydet.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'grdAllYetkiler
        '
        Me.grdAllYetkiler.ColumnAutoResize = True
        GridEXLayout4.LayoutString = resources.GetString("GridEXLayout4.LayoutString")
        Me.grdAllYetkiler.DesignTimeLayout = GridEXLayout4
        Me.grdAllYetkiler.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdAllYetkiler.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grdAllYetkiler.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.grdAllYetkiler.GroupByBoxVisible = False
        Me.grdAllYetkiler.Location = New System.Drawing.Point(3, 16)
        Me.grdAllYetkiler.Name = "grdAllYetkiler"
        Me.grdAllYetkiler.Size = New System.Drawing.Size(305, 708)
        Me.grdAllYetkiler.TabIndex = 5
        Me.grdAllYetkiler.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'lblId
        '
        Me.lblId.AutoSize = True
        Me.lblId.BackColor = System.Drawing.Color.Transparent
        Me.lblId.Location = New System.Drawing.Point(11, 42)
        Me.lblId.Name = "lblId"
        Me.lblId.Size = New System.Drawing.Size(13, 13)
        Me.lblId.TabIndex = 6
        Me.lblId.Text = "--"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.GridEX1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(190, 746)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Kullanıcılar"
        '
        'GridEX1
        '
        Me.GridEX1.ColumnAutoResize = True
        GridEXLayout5.LayoutString = resources.GetString("GridEXLayout5.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEXLayout5
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.Location = New System.Drawing.Point(3, 16)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(184, 727)
        Me.GridEX1.TabIndex = 1
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox2.Controls.Add(Me.grdKullaniciYetkileri)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(739, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(200, 746)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Kullanıcıya Ait Yetkiler"
        '
        'grdKullaniciYetkileri
        '
        Me.grdKullaniciYetkileri.ColumnAutoResize = True
        GridEXLayout6.LayoutString = resources.GetString("GridEXLayout6.LayoutString")
        Me.grdKullaniciYetkileri.DesignTimeLayout = GridEXLayout6
        Me.grdKullaniciYetkileri.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdKullaniciYetkileri.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grdKullaniciYetkileri.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.grdKullaniciYetkileri.GroupByBoxVisible = False
        Me.grdKullaniciYetkileri.Location = New System.Drawing.Point(3, 18)
        Me.grdKullaniciYetkileri.Name = "grdKullaniciYetkileri"
        Me.grdKullaniciYetkileri.Size = New System.Drawing.Size(194, 725)
        Me.grdKullaniciYetkileri.TabIndex = 8
        Me.grdKullaniciYetkileri.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.GroupBox3.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.GroupBox3.Controls.Add(Me.lblyetki)
        Me.GroupBox3.Controls.Add(Me.GroupBox6)
        Me.GroupBox3.Controls.Add(Me.GroupBox5)
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(190, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(549, 746)
        Me.GroupBox3.TabIndex = 10
        Me.GroupBox3.TabStop = False
        '
        'GroupBox6
        '
        Me.GroupBox6.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.GroupBox6.Controls.Add(Me.grdAllYetkiler)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox6.Location = New System.Drawing.Point(135, 16)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(311, 727)
        Me.GroupBox6.TabIndex = 9
        Me.GroupBox6.TabStop = False
        '
        'GroupBox5
        '
        Me.GroupBox5.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.GroupBox5.Controls.Add(Me.btnRemoveYetki)
        Me.GroupBox5.Controls.Add(Me.btnKaydet)
        Me.GroupBox5.Controls.Add(Me.btnAddYetki)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupBox5.Location = New System.Drawing.Point(446, 16)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(100, 727)
        Me.GroupBox5.TabIndex = 8
        Me.GroupBox5.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.txtkullanici)
        Me.GroupBox4.Controls.Add(Me.lblId)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox4.Location = New System.Drawing.Point(3, 16)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(132, 727)
        Me.GroupBox4.TabIndex = 7
        Me.GroupBox4.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Sylfaen", 11.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 19)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Seçili Kullanıcı"
        '
        'frmYetkilendirme
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.ClientSize = New System.Drawing.Size(939, 746)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmYetkilendirme"
        Me.Text = "Yetkilendirme"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdAllYetkiler, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdKullaniciYetkileri, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    #End Region 'Methods

End Class