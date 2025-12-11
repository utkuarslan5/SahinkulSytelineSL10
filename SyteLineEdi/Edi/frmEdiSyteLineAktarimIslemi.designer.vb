<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class frmEdiSyteLineAktarimIslemi
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Friend  WithEvents Bar As System.Windows.Forms.ToolStripProgressBar
    Friend  WithEvents btnEdiMstAktar As Janus.Windows.EditControls.UIButton
    Friend  WithEvents btnExcel As Janus.Windows.EditControls.UIButton
    Friend  WithEvents btnExit As Janus.Windows.EditControls.UIButton
    Friend  WithEvents btnListele As Janus.Windows.EditControls.UIButton
    Friend  WithEvents btnMesajSil As Janus.Windows.EditControls.UIButton
    Friend  WithEvents btnNetTarihGuncelle As Janus.Windows.EditControls.UIButton
    Friend  WithEvents btnSyteLinDatarlariAktar As Janus.Windows.EditControls.UIButton
    Friend  WithEvents chkMesajDurumu1 As System.Windows.Forms.CheckBox
    Friend  WithEvents chkMesajDurumu2 As System.Windows.Forms.CheckBox
    Friend  WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend  WithEvents dtCalTar As System.Windows.Forms.DateTimePicker
    Friend  WithEvents dtHaftaTar As System.Windows.Forms.DateTimePicker
    Friend  WithEvents dtNetTar As System.Windows.Forms.DateTimePicker
    Friend  WithEvents Durum As System.Windows.Forms.ToolStripStatusLabel
    Friend  WithEvents grdDesAdv As Janus.Windows.GridEX.GridEX
    Friend  WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend  WithEvents grpUstBilgi As Janus.Windows.EditControls.UIGroupBox
    Friend  WithEvents SatýrýSilToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend  WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    #End Region 'Fields

    #Region "Methods"

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEdiSyteLineAktarimIslemi))
        Dim GridEXLayout1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXLayout2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXColumn1 As Janus.Windows.GridEX.GridEXColumn = New Janus.Windows.GridEX.GridEXColumn()
        Dim GridEXFormatStyle1 As Janus.Windows.GridEX.GridEXFormatStyle = New Janus.Windows.GridEX.GridEXFormatStyle()
        Dim GridEXFormatStyle2 As Janus.Windows.GridEX.GridEXFormatStyle = New Janus.Windows.GridEX.GridEXFormatStyle()
        Dim GridEXFormatStyle3 As Janus.Windows.GridEX.GridEXFormatStyle = New Janus.Windows.GridEX.GridEXFormatStyle()
        Me.grpUstBilgi = New Janus.Windows.EditControls.UIGroupBox()
        Me.btnExcel = New Janus.Windows.EditControls.UIButton()
        Me.btnMesajSil = New Janus.Windows.EditControls.UIButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkMesajDurumu2 = New System.Windows.Forms.CheckBox()
        Me.chkMesajDurumu1 = New System.Windows.Forms.CheckBox()
        Me.btnSyteLinDatarlariAktar = New Janus.Windows.EditControls.UIButton()
        Me.dtCalTar = New System.Windows.Forms.DateTimePicker()
        Me.dtHaftaTar = New System.Windows.Forms.DateTimePicker()
        Me.dtNetTar = New System.Windows.Forms.DateTimePicker()
        Me.btnNetTarihGuncelle = New Janus.Windows.EditControls.UIButton()
        Me.btnEdiMstAktar = New Janus.Windows.EditControls.UIButton()
        Me.btnExit = New Janus.Windows.EditControls.UIButton()
        Me.btnListele = New Janus.Windows.EditControls.UIButton()
        Me.grdDesAdv = New Janus.Windows.GridEX.GridEX()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SatýrýSilToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Bar = New System.Windows.Forms.ToolStripProgressBar()
        Me.Durum = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.grpUstBilgi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpUstBilgi.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdDesAdv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpUstBilgi
        '
        Me.grpUstBilgi.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.grpUstBilgi.Controls.Add(Me.btnExcel)
        Me.grpUstBilgi.Controls.Add(Me.btnMesajSil)
        Me.grpUstBilgi.Controls.Add(Me.GroupBox1)
        Me.grpUstBilgi.Controls.Add(Me.btnSyteLinDatarlariAktar)
        Me.grpUstBilgi.Controls.Add(Me.dtCalTar)
        Me.grpUstBilgi.Controls.Add(Me.dtHaftaTar)
        Me.grpUstBilgi.Controls.Add(Me.dtNetTar)
        Me.grpUstBilgi.Controls.Add(Me.btnNetTarihGuncelle)
        Me.grpUstBilgi.Controls.Add(Me.btnEdiMstAktar)
        Me.grpUstBilgi.Controls.Add(Me.btnExit)
        Me.grpUstBilgi.Controls.Add(Me.btnListele)
        Me.grpUstBilgi.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpUstBilgi.Location = New System.Drawing.Point(0, 0)
        Me.grpUstBilgi.Name = "grpUstBilgi"
        Me.grpUstBilgi.Size = New System.Drawing.Size(1132, 162)
        Me.grpUstBilgi.TabIndex = 0
        '
        'btnExcel
        '
        Me.btnExcel.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnExcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnExcel.Image = Global.SyteLineEdi.My.Resources.Resources._202px_Excel2007
        Me.btnExcel.ImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Location = New System.Drawing.Point(158, 95)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(129, 47)
        Me.btnExcel.TabIndex = 13
        Me.btnExcel.Text = "Excel e Aktar"
        Me.btnExcel.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnMesajSil
        '
        Me.btnMesajSil.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnMesajSil.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnMesajSil.Image = CType(resources.GetObject("btnMesajSil.Image"), System.Drawing.Image)
        Me.btnMesajSil.ImageSize = New System.Drawing.Size(48, 48)
        Me.btnMesajSil.Location = New System.Drawing.Point(10, 95)
        Me.btnMesajSil.Name = "btnMesajSil"
        Me.btnMesajSil.Size = New System.Drawing.Size(129, 47)
        Me.btnMesajSil.TabIndex = 12
        Me.btnMesajSil.Text = "Kayýtlarý Sil"
        Me.btnMesajSil.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.chkMesajDurumu2)
        Me.GroupBox1.Controls.Add(Me.chkMesajDurumu1)
        Me.GroupBox1.Location = New System.Drawing.Point(457, 82)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 74)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "SyteLine Aktarýmý"
        '
        'chkMesajDurumu2
        '
        Me.chkMesajDurumu2.AutoSize = True
        Me.chkMesajDurumu2.Checked = True
        Me.chkMesajDurumu2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMesajDurumu2.Location = New System.Drawing.Point(6, 43)
        Me.chkMesajDurumu2.Name = "chkMesajDurumu2"
        Me.chkMesajDurumu2.Size = New System.Drawing.Size(187, 17)
        Me.chkMesajDurumu2.TabIndex = 1
        Me.chkMesajDurumu2.Text = "Kümülatif Uyarý veren mesajlarý iþle"
        Me.chkMesajDurumu2.UseVisualStyleBackColor = True
        '
        'chkMesajDurumu1
        '
        Me.chkMesajDurumu1.AutoSize = True
        Me.chkMesajDurumu1.Checked = True
        Me.chkMesajDurumu1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMesajDurumu1.Location = New System.Drawing.Point(6, 20)
        Me.chkMesajDurumu1.Name = "chkMesajDurumu1"
        Me.chkMesajDurumu1.Size = New System.Drawing.Size(141, 17)
        Me.chkMesajDurumu1.TabIndex = 0
        Me.chkMesajDurumu1.Text = "Uyarý veren mesajlarý iþle"
        Me.chkMesajDurumu1.UseVisualStyleBackColor = True
        '
        'btnSyteLinDatarlariAktar
        '
        Me.btnSyteLinDatarlariAktar.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnSyteLinDatarlariAktar.Enabled = False
        Me.btnSyteLinDatarlariAktar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnSyteLinDatarlariAktar.Image = Global.SyteLineEdi.My.Resources.Resources.db_add
        Me.btnSyteLinDatarlariAktar.ImageSize = New System.Drawing.Size(48, 48)
        Me.btnSyteLinDatarlariAktar.Location = New System.Drawing.Point(306, 12)
        Me.btnSyteLinDatarlariAktar.Name = "btnSyteLinDatarlariAktar"
        Me.btnSyteLinDatarlariAktar.Size = New System.Drawing.Size(129, 47)
        Me.btnSyteLinDatarlariAktar.TabIndex = 10
        Me.btnSyteLinDatarlariAktar.Text = "SyteLine Verileri Aktar"
        Me.btnSyteLinDatarlariAktar.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'dtCalTar
        '
        Me.dtCalTar.Location = New System.Drawing.Point(862, 13)
        Me.dtCalTar.Name = "dtCalTar"
        Me.dtCalTar.Size = New System.Drawing.Size(129, 20)
        Me.dtCalTar.TabIndex = 9
        Me.dtCalTar.Visible = False
        '
        'dtHaftaTar
        '
        Me.dtHaftaTar.Location = New System.Drawing.Point(862, 39)
        Me.dtHaftaTar.Name = "dtHaftaTar"
        Me.dtHaftaTar.Size = New System.Drawing.Size(129, 20)
        Me.dtHaftaTar.TabIndex = 8
        Me.dtHaftaTar.Visible = False
        '
        'dtNetTar
        '
        Me.dtNetTar.Location = New System.Drawing.Point(862, 65)
        Me.dtNetTar.Name = "dtNetTar"
        Me.dtNetTar.Size = New System.Drawing.Size(129, 20)
        Me.dtNetTar.TabIndex = 7
        Me.dtNetTar.Visible = False
        '
        'btnNetTarihGuncelle
        '
        Me.btnNetTarihGuncelle.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnNetTarihGuncelle.Enabled = False
        Me.btnNetTarihGuncelle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnNetTarihGuncelle.Image = Global.SyteLineEdi.My.Resources.Resources.NetByte_Design_Studio___0671
        Me.btnNetTarihGuncelle.ImageSize = New System.Drawing.Size(48, 48)
        Me.btnNetTarihGuncelle.Location = New System.Drawing.Point(158, 12)
        Me.btnNetTarihGuncelle.Name = "btnNetTarihGuncelle"
        Me.btnNetTarihGuncelle.Size = New System.Drawing.Size(129, 47)
        Me.btnNetTarihGuncelle.TabIndex = 6
        Me.btnNetTarihGuncelle.Text = "Net Tarihleri Güncelle"
        Me.btnNetTarihGuncelle.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnEdiMstAktar
        '
        Me.btnEdiMstAktar.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnEdiMstAktar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnEdiMstAktar.Image = Global.SyteLineEdi.My.Resources.Resources.mailview
        Me.btnEdiMstAktar.ImageSize = New System.Drawing.Size(48, 48)
        Me.btnEdiMstAktar.Location = New System.Drawing.Point(10, 12)
        Me.btnEdiMstAktar.Name = "btnEdiMstAktar"
        Me.btnEdiMstAktar.Size = New System.Drawing.Size(129, 47)
        Me.btnEdiMstAktar.TabIndex = 4
        Me.btnEdiMstAktar.Text = "EDI Dosyasýndan Verileri Aktar"
        Me.btnEdiMstAktar.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnExit
        '
        Me.btnExit.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Location = New System.Drawing.Point(457, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(129, 47)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Çýkýþ"
        Me.btnExit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnListele
        '
        Me.btnListele.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnListele.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnListele.Image = CType(resources.GetObject("btnListele.Image"), System.Drawing.Image)
        Me.btnListele.ImageSize = New System.Drawing.Size(48, 48)
        Me.btnListele.Location = New System.Drawing.Point(306, 95)
        Me.btnListele.Name = "btnListele"
        Me.btnListele.Size = New System.Drawing.Size(129, 47)
        Me.btnListele.TabIndex = 0
        Me.btnListele.Text = "Listele"
        Me.btnListele.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'grdDesAdv
        '
        Me.grdDesAdv.AlternatingColors = True
        Me.grdDesAdv.ColumnSetHeaders = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdDesAdv.ContextMenuStrip = Me.ContextMenuStrip1
        Me.grdDesAdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDesAdv.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grdDesAdv.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdDesAdv.FrozenColumns = 3
        GridEXLayout1.Key = "desadvlistele"
        GridEXLayout1.LayoutReferences.AddRange(New Janus.Windows.Common.Layouts.JanusLayoutReference() {New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column4.ValueList.Item0.Image", CType(resources.GetObject("GridEXLayout1.LayoutReferences"), Object)), New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column4.ValueList.Item1.Image", CType(resources.GetObject("GridEXLayout1.LayoutReferences1"), Object)), New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column4.ValueList.Item2.Image", CType(resources.GetObject("GridEXLayout1.LayoutReferences2"), Object)), New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column4.ValueList.Item3.Image", CType(resources.GetObject("GridEXLayout1.LayoutReferences3"), Object))})
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        GridEXLayout2.IsCurrentLayout = True
        GridEXLayout2.Key = "CopyOfdesadvlistele"
        GridEXLayout2.LayoutReferences.AddRange(New Janus.Windows.Common.Layouts.JanusLayoutReference() {New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column4.ValueList.Item0.Image", CType(resources.GetObject("GridEXLayout2.LayoutReferences"), Object)), New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column4.ValueList.Item1.Image", CType(resources.GetObject("GridEXLayout2.LayoutReferences1"), Object)), New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column4.ValueList.Item2.Image", CType(resources.GetObject("GridEXLayout2.LayoutReferences2"), Object)), New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column4.ValueList.Item3.Image", CType(resources.GetObject("GridEXLayout2.LayoutReferences3"), Object))})
        GridEXLayout2.LayoutString = resources.GetString("GridEXLayout2.LayoutString")
        Me.grdDesAdv.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEXLayout1, GridEXLayout2})
        Me.grdDesAdv.Location = New System.Drawing.Point(0, 162)
        Me.grdDesAdv.Name = "grdDesAdv"
        Me.grdDesAdv.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        GridEXColumn1.BoundImageMaskColor = System.Drawing.Color.Empty
        GridEXColumn1.Caption = "Malzeme No"
        GridEXColumn1.CellStyle = GridEXFormatStyle1
        GridEXColumn1.DataMember = "Itnbr"
        GridEXColumn1.DefaultGroupPrefix = "Malzeme No:"
        GridEXColumn1.EditValueList = Nothing
        GridEXColumn1.EmptyStringValue = CType(resources.GetObject("GridEXColumn1.EmptyStringValue"), Object)
        GridEXColumn1.GroupRowStyle = GridEXFormatStyle2
        GridEXColumn1.HeaderStyle = GridEXFormatStyle3
        GridEXColumn1.Key = "Itnbr"
        GridEXColumn1.Width = 58
        Me.grdDesAdv.SearchColumn = GridEXColumn1
        Me.grdDesAdv.Size = New System.Drawing.Size(1132, 549)
        Me.grdDesAdv.TabIndex = 1
        Me.grdDesAdv.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SatýrýSilToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(116, 26)
        '
        'SatýrýSilToolStripMenuItem
        '
        Me.SatýrýSilToolStripMenuItem.Name = "SatýrýSilToolStripMenuItem"
        Me.SatýrýSilToolStripMenuItem.Size = New System.Drawing.Size(115, 22)
        Me.SatýrýSilToolStripMenuItem.Text = "Satýrý Sil"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Bar, Me.Durum})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 711)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1132, 22)
        Me.StatusStrip1.TabIndex = 15
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Bar
        '
        Me.Bar.Name = "Bar"
        Me.Bar.Size = New System.Drawing.Size(600, 16)
        '
        'Durum
        '
        Me.Durum.Name = "Durum"
        Me.Durum.Size = New System.Drawing.Size(0, 17)
        '
        'frmEdiSyteLineAktarimIslemi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1132, 733)
        Me.Controls.Add(Me.grdDesAdv)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.grpUstBilgi)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmEdiSyteLineAktarimIslemi"
        Me.Text = "Edi Syteline Aktarým Ýþlemi"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grpUstBilgi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpUstBilgi.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdDesAdv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    #End Region 'Methods

End Class