<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class frmNakilIrsaliyesi
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Friend  WithEvents btnEkle As System.Windows.Forms.Button
    Friend  WithEvents btnSecimiKaldir As System.Windows.Forms.Button
    Friend  WithEvents btnTumunuSec As System.Windows.Forms.Button
    Friend  WithEvents btnYazdir As System.Windows.Forms.Button
    Friend  WithEvents chkIrsaliye As System.Windows.Forms.CheckBox
    Friend  WithEvents cmbSatici As Janus.Windows.GridEX.EditControls.EditBox
    Friend  WithEvents dtmFaturaTarihi As System.Windows.Forms.DateTimePicker
    Friend  WithEvents grdIrsaliye As Janus.Windows.GridEX.GridEX
    Friend  WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend  WithEvents Label1 As System.Windows.Forms.Label
    Friend  WithEvents Label2 As System.Windows.Forms.Label
    Friend  WithEvents Label3 As System.Windows.Forms.Label
    Friend  WithEvents Label4 As System.Windows.Forms.Label
    Friend  WithEvents Label5 As System.Windows.Forms.Label
    Friend  WithEvents lblSatici As System.Windows.Forms.Label
    Friend  WithEvents txtAciklama As System.Windows.Forms.TextBox
    Friend  WithEvents txtAdres As System.Windows.Forms.TextBox
    Friend  WithEvents txtIrsaliyeNo As System.Windows.Forms.TextBox
    Friend  WithEvents txtSaticiAdi As System.Windows.Forms.TextBox
    Friend  WithEvents txtVergiDairesi As System.Windows.Forms.TextBox
    Friend  WithEvents txtVergiNo As System.Windows.Forms.TextBox

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
        Dim GridEXLayout1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNakilIrsaliyesi))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbSatici = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtAciklama = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkIrsaliye = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtVergiNo = New System.Windows.Forms.TextBox()
        Me.txtVergiDairesi = New System.Windows.Forms.TextBox()
        Me.txtAdres = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtmFaturaTarihi = New System.Windows.Forms.DateTimePicker()
        Me.btnYazdir = New System.Windows.Forms.Button()
        Me.btnSecimiKaldir = New System.Windows.Forms.Button()
        Me.btnTumunuSec = New System.Windows.Forms.Button()
        Me.btnEkle = New System.Windows.Forms.Button()
        Me.txtIrsaliyeNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSaticiAdi = New System.Windows.Forms.TextBox()
        Me.lblSatici = New System.Windows.Forms.Label()
        Me.grdIrsaliye = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdIrsaliye, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbSatici)
        Me.GroupBox1.Controls.Add(Me.txtAciklama)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.chkIrsaliye)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtVergiNo)
        Me.GroupBox1.Controls.Add(Me.txtVergiDairesi)
        Me.GroupBox1.Controls.Add(Me.txtAdres)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtmFaturaTarihi)
        Me.GroupBox1.Controls.Add(Me.btnYazdir)
        Me.GroupBox1.Controls.Add(Me.btnSecimiKaldir)
        Me.GroupBox1.Controls.Add(Me.btnTumunuSec)
        Me.GroupBox1.Controls.Add(Me.btnEkle)
        Me.GroupBox1.Controls.Add(Me.txtIrsaliyeNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtSaticiAdi)
        Me.GroupBox1.Controls.Add(Me.lblSatici)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(992, 181)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'cmbSatici
        '
        Me.cmbSatici.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.cmbSatici.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.cmbSatici.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.cmbSatici.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.cmbSatici.Location = New System.Drawing.Point(125, 19)
        Me.cmbSatici.Name = "cmbSatici"
        Me.cmbSatici.Size = New System.Drawing.Size(129, 20)
        Me.cmbSatici.TabIndex = 49
        Me.cmbSatici.Tag = ""
        Me.cmbSatici.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtAciklama
        '
        Me.txtAciklama.Location = New System.Drawing.Point(629, 19)
        Me.txtAciklama.MaxLength = 225
        Me.txtAciklama.Multiline = True
        Me.txtAciklama.Name = "txtAciklama"
        Me.txtAciklama.Size = New System.Drawing.Size(253, 94)
        Me.txtAciklama.TabIndex = 37
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(537, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "Aciklama....:"
        '
        'chkIrsaliye
        '
        Me.chkIrsaliye.AutoSize = True
        Me.chkIrsaliye.Location = New System.Drawing.Point(269, 73)
        Me.chkIrsaliye.Name = "chkIrsaliye"
        Me.chkIrsaliye.Size = New System.Drawing.Size(132, 17)
        Me.chkIrsaliye.TabIndex = 34
        Me.chkIrsaliye.Text = "Ýrsaliye Yeniden Yazdýr"
        Me.chkIrsaliye.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 74)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 13)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Vergi Numarasý....:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Vergi Dairesi....:"
        '
        'txtVergiNo
        '
        Me.txtVergiNo.Enabled = False
        Me.txtVergiNo.Location = New System.Drawing.Point(125, 71)
        Me.txtVergiNo.Name = "txtVergiNo"
        Me.txtVergiNo.Size = New System.Drawing.Size(129, 20)
        Me.txtVergiNo.TabIndex = 31
        '
        'txtVergiDairesi
        '
        Me.txtVergiDairesi.Enabled = False
        Me.txtVergiDairesi.Location = New System.Drawing.Point(125, 46)
        Me.txtVergiDairesi.Name = "txtVergiDairesi"
        Me.txtVergiDairesi.Size = New System.Drawing.Size(129, 20)
        Me.txtVergiDairesi.TabIndex = 30
        '
        'txtAdres
        '
        Me.txtAdres.Enabled = False
        Me.txtAdres.Location = New System.Drawing.Point(269, 45)
        Me.txtAdres.Name = "txtAdres"
        Me.txtAdres.Size = New System.Drawing.Size(253, 20)
        Me.txtAdres.TabIndex = 29
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(266, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Tarih....:"
        '
        'dtmFaturaTarihi
        '
        Me.dtmFaturaTarihi.Location = New System.Drawing.Point(330, 93)
        Me.dtmFaturaTarihi.Name = "dtmFaturaTarihi"
        Me.dtmFaturaTarihi.Size = New System.Drawing.Size(192, 20)
        Me.dtmFaturaTarihi.TabIndex = 27
        '
        'btnYazdir
        '
        Me.btnYazdir.BackColor = System.Drawing.Color.SkyBlue
        Me.btnYazdir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnYazdir.Location = New System.Drawing.Point(411, 121)
        Me.btnYazdir.Name = "btnYazdir"
        Me.btnYazdir.Size = New System.Drawing.Size(111, 36)
        Me.btnYazdir.TabIndex = 26
        Me.btnYazdir.Text = "Yazdýr"
        Me.btnYazdir.UseVisualStyleBackColor = False
        '
        'btnSecimiKaldir
        '
        Me.btnSecimiKaldir.BackColor = System.Drawing.Color.SkyBlue
        Me.btnSecimiKaldir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSecimiKaldir.Location = New System.Drawing.Point(277, 121)
        Me.btnSecimiKaldir.Name = "btnSecimiKaldir"
        Me.btnSecimiKaldir.Size = New System.Drawing.Size(120, 36)
        Me.btnSecimiKaldir.TabIndex = 25
        Me.btnSecimiKaldir.Text = "Seçimi Kaldýr"
        Me.btnSecimiKaldir.UseVisualStyleBackColor = False
        '
        'btnTumunuSec
        '
        Me.btnTumunuSec.BackColor = System.Drawing.Color.SkyBlue
        Me.btnTumunuSec.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTumunuSec.Location = New System.Drawing.Point(151, 121)
        Me.btnTumunuSec.Name = "btnTumunuSec"
        Me.btnTumunuSec.Size = New System.Drawing.Size(120, 36)
        Me.btnTumunuSec.TabIndex = 24
        Me.btnTumunuSec.Text = "Tümünü Seç"
        Me.btnTumunuSec.UseVisualStyleBackColor = False
        '
        'btnEkle
        '
        Me.btnEkle.BackColor = System.Drawing.Color.SkyBlue
        Me.btnEkle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEkle.Location = New System.Drawing.Point(25, 121)
        Me.btnEkle.Name = "btnEkle"
        Me.btnEkle.Size = New System.Drawing.Size(117, 36)
        Me.btnEkle.TabIndex = 23
        Me.btnEkle.Text = "Listele"
        Me.btnEkle.UseVisualStyleBackColor = False
        '
        'txtIrsaliyeNo
        '
        Me.txtIrsaliyeNo.BackColor = System.Drawing.Color.Yellow
        Me.txtIrsaliyeNo.Location = New System.Drawing.Point(125, 93)
        Me.txtIrsaliyeNo.Name = "txtIrsaliyeNo"
        Me.txtIrsaliyeNo.Size = New System.Drawing.Size(129, 20)
        Me.txtIrsaliyeNo.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Ýrsaliye No...:"
        '
        'txtSaticiAdi
        '
        Me.txtSaticiAdi.Enabled = False
        Me.txtSaticiAdi.Location = New System.Drawing.Point(269, 19)
        Me.txtSaticiAdi.Name = "txtSaticiAdi"
        Me.txtSaticiAdi.Size = New System.Drawing.Size(253, 20)
        Me.txtSaticiAdi.TabIndex = 20
        '
        'lblSatici
        '
        Me.lblSatici.AutoSize = True
        Me.lblSatici.Location = New System.Drawing.Point(22, 22)
        Me.lblSatici.Name = "lblSatici"
        Me.lblSatici.Size = New System.Drawing.Size(79, 13)
        Me.lblSatici.TabIndex = 19
        Me.lblSatici.Text = "Fason Firma....:"
        '
        'grdIrsaliye
        '
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.grdIrsaliye.DesignTimeLayout = GridEXLayout1
        Me.grdIrsaliye.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdIrsaliye.Location = New System.Drawing.Point(0, 181)
        Me.grdIrsaliye.Name = "grdIrsaliye"
        Me.grdIrsaliye.Size = New System.Drawing.Size(992, 265)
        Me.grdIrsaliye.TabIndex = 1
        '
        'frmNakilIrsaliyesi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(992, 446)
        Me.Controls.Add(Me.grdIrsaliye)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmNakilIrsaliyesi"
        Me.Text = "Fason Ýrsaliye Basýmý"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdIrsaliye, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    #End Region 'Methods

End Class