<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTakvimGirisi
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
        Dim GridEXLayout4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTakvimGirisi))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnSil = New Janus.Windows.EditControls.UIButton
        Me.btnVazgec = New Janus.Windows.EditControls.UIButton
        Me.btnKaydet = New Janus.Windows.EditControls.UIButton
        Me.btnYeniKayit = New Janus.Windows.EditControls.UIButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkPazar = New Janus.Windows.EditControls.UICheckBox
        Me.chkCumartesi = New Janus.Windows.EditControls.UICheckBox
        Me.chkCuma = New Janus.Windows.EditControls.UICheckBox
        Me.chkPersembe = New Janus.Windows.EditControls.UICheckBox
        Me.chkCarsamba = New Janus.Windows.EditControls.UICheckBox
        Me.chkSali = New Janus.Windows.EditControls.UICheckBox
        Me.chkPazartesi = New Janus.Windows.EditControls.UICheckBox
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.txtYil = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.txtTanim = New Janus.Windows.GridEX.EditControls.EditBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GridEX1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(364, 147)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'GridEX1
        '
        GridEXLayout4.LayoutString = resources.GetString("GridEXLayout4.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEXLayout4
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.Location = New System.Drawing.Point(3, 16)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(358, 128)
        Me.GridEX1.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.btnSil)
        Me.GroupBox2.Controls.Add(Me.btnVazgec)
        Me.GroupBox2.Controls.Add(Me.btnKaydet)
        Me.GroupBox2.Controls.Add(Me.btnYeniKayit)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.chkPazar)
        Me.GroupBox2.Controls.Add(Me.chkCumartesi)
        Me.GroupBox2.Controls.Add(Me.chkCuma)
        Me.GroupBox2.Controls.Add(Me.chkPersembe)
        Me.GroupBox2.Controls.Add(Me.chkCarsamba)
        Me.GroupBox2.Controls.Add(Me.chkSali)
        Me.GroupBox2.Controls.Add(Me.chkPazartesi)
        Me.GroupBox2.Controls.Add(Me.LinkLabel1)
        Me.GroupBox2.Controls.Add(Me.txtYil)
        Me.GroupBox2.Controls.Add(Me.txtTanim)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 153)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(364, 310)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(228, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "2009 Formatında"
        '
        'btnSil
        '
        Me.btnSil.Image = Global.SyteLineEdi.My.Resources.Resources.Error1
        Me.btnSil.Location = New System.Drawing.Point(257, 274)
        Me.btnSil.Name = "btnSil"
        Me.btnSil.Size = New System.Drawing.Size(75, 30)
        Me.btnSil.TabIndex = 15
        Me.btnSil.Text = "Sil"
        '
        'btnVazgec
        '
        Me.btnVazgec.Image = Global.SyteLineEdi.My.Resources.Resources.dialog_cancel
        Me.btnVazgec.Location = New System.Drawing.Point(176, 274)
        Me.btnVazgec.Name = "btnVazgec"
        Me.btnVazgec.Size = New System.Drawing.Size(75, 30)
        Me.btnVazgec.TabIndex = 14
        Me.btnVazgec.Text = "Vazgeç"
        '
        'btnKaydet
        '
        Me.btnKaydet.Image = Global.SyteLineEdi.My.Resources.Resources.kaydet
        Me.btnKaydet.Location = New System.Drawing.Point(93, 274)
        Me.btnKaydet.Name = "btnKaydet"
        Me.btnKaydet.Size = New System.Drawing.Size(75, 30)
        Me.btnKaydet.TabIndex = 13
        Me.btnKaydet.Text = "Kaydet"
        '
        'btnYeniKayit
        '
        Me.btnYeniKayit.Image = Global.SyteLineEdi.My.Resources.Resources.Copy__2__of_address_book
        Me.btnYeniKayit.Location = New System.Drawing.Point(12, 274)
        Me.btnYeniKayit.Name = "btnYeniKayit"
        Me.btnYeniKayit.Size = New System.Drawing.Size(75, 30)
        Me.btnYeniKayit.TabIndex = 12
        Me.btnYeniKayit.Text = "Yeni Kayıt"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(122, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Takvim Başlangıç Yılı....:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Takvim Tanımı....:"
        '
        'chkPazar
        '
        Me.chkPazar.Location = New System.Drawing.Point(147, 240)
        Me.chkPazar.Name = "chkPazar"
        Me.chkPazar.Size = New System.Drawing.Size(104, 22)
        Me.chkPazar.TabIndex = 9
        Me.chkPazar.Text = "Pazar"
        '
        'chkCumartesi
        '
        Me.chkCumartesi.Location = New System.Drawing.Point(147, 218)
        Me.chkCumartesi.Name = "chkCumartesi"
        Me.chkCumartesi.Size = New System.Drawing.Size(104, 22)
        Me.chkCumartesi.TabIndex = 8
        Me.chkCumartesi.Text = "Cumartesi"
        '
        'chkCuma
        '
        Me.chkCuma.Location = New System.Drawing.Point(147, 196)
        Me.chkCuma.Name = "chkCuma"
        Me.chkCuma.Size = New System.Drawing.Size(104, 22)
        Me.chkCuma.TabIndex = 7
        Me.chkCuma.Text = "Cuma"
        '
        'chkPersembe
        '
        Me.chkPersembe.Location = New System.Drawing.Point(147, 174)
        Me.chkPersembe.Name = "chkPersembe"
        Me.chkPersembe.Size = New System.Drawing.Size(104, 22)
        Me.chkPersembe.TabIndex = 6
        Me.chkPersembe.Text = "Perşembe"
        '
        'chkCarsamba
        '
        Me.chkCarsamba.Location = New System.Drawing.Point(147, 151)
        Me.chkCarsamba.Name = "chkCarsamba"
        Me.chkCarsamba.Size = New System.Drawing.Size(104, 23)
        Me.chkCarsamba.TabIndex = 5
        Me.chkCarsamba.Text = "Çarşamba"
        '
        'chkSali
        '
        Me.chkSali.Location = New System.Drawing.Point(147, 128)
        Me.chkSali.Name = "chkSali"
        Me.chkSali.Size = New System.Drawing.Size(104, 23)
        Me.chkSali.TabIndex = 4
        Me.chkSali.Text = "Salı"
        '
        'chkPazartesi
        '
        Me.chkPazartesi.Location = New System.Drawing.Point(147, 105)
        Me.chkPazartesi.Name = "chkPazartesi"
        Me.chkPazartesi.Size = New System.Drawing.Size(104, 23)
        Me.chkPazartesi.TabIndex = 3
        Me.chkPazartesi.Text = "Pazartesi"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(144, 89)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(68, 13)
        Me.LinkLabel1.TabIndex = 2
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Sevk Günleri"
        '
        'txtYil
        '
        Me.txtYil.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtYil.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtYil.FormatString = "###0"
        Me.txtYil.Location = New System.Drawing.Point(147, 52)
        Me.txtYil.Name = "txtYil"
        Me.txtYil.Size = New System.Drawing.Size(75, 20)
        Me.txtYil.TabIndex = 1
        Me.txtYil.Text = "0"
        Me.txtYil.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtTanim
        '
        Me.txtTanim.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtTanim.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtTanim.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtTanim.Location = New System.Drawing.Point(147, 15)
        Me.txtTanim.Name = "txtTanim"
        Me.txtTanim.Size = New System.Drawing.Size(134, 20)
        Me.txtTanim.TabIndex = 0
        Me.txtTanim.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'TakvimGirisi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(364, 463)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "TakvimGirisi"
        Me.Text = "TakvimGirisi"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtTanim As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents txtYil As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents chkPazar As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents chkCumartesi As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents chkCuma As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents chkPersembe As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents chkCarsamba As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents chkSali As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents chkPazartesi As Janus.Windows.EditControls.UICheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSil As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnVazgec As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnKaydet As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnYeniKayit As Janus.Windows.EditControls.UIButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
