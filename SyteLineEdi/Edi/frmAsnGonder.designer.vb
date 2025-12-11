<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class frmAsnGonder
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Friend  WithEvents btnAsnGonder As Janus.Windows.EditControls.UIButton
    Friend  WithEvents btnCikis As Janus.Windows.EditControls.UIButton
    Friend  WithEvents btnListele As Janus.Windows.EditControls.UIButton
    Friend  WithEvents chkAsnTekrar As System.Windows.Forms.RadioButton
    Friend  WithEvents chkAsnYeni As System.Windows.Forms.RadioButton
    Friend  WithEvents chkTarihAraligi As System.Windows.Forms.CheckBox
    Friend  WithEvents dtTarih1 As Janus.Windows.CalendarCombo.CalendarCombo
    Friend  WithEvents dtTarih2 As Janus.Windows.CalendarCombo.CalendarCombo
    Friend  WithEvents grdDesAdv As Janus.Windows.GridEX.GridEX
    Friend  WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend  WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend  WithEvents Label1 As System.Windows.Forms.Label
    Friend  WithEvents Label2 As System.Windows.Forms.Label
    Friend  WithEvents Label3 As System.Windows.Forms.Label
    Friend  WithEvents Label4 As System.Windows.Forms.Label
    Friend  WithEvents Label5 As System.Windows.Forms.Label
    Friend  WithEvents mError As Janus.Windows.GridEX.EditControls.EditBox
    Friend  WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend  WithEvents txtCekmeListeNo1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend  WithEvents txtCekmeListeNo2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend  WithEvents txtFaturaNo1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend  WithEvents txtFaturaNo2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend  WithEvents txtMusteri1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend  WithEvents txtMusteri2 As Janus.Windows.GridEX.EditControls.EditBox
    Friend  WithEvents txtMusteriAdi1 As System.Windows.Forms.TextBox
    Friend  WithEvents txtMusteriAdi2 As System.Windows.Forms.TextBox
    Friend  WithEvents txtTeslimAlan1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend  WithEvents UıButton1 As Janus.Windows.EditControls.UIButton

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAsnGonder))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkTarihAraligi = New System.Windows.Forms.CheckBox()
        Me.UıButton1 = New Janus.Windows.EditControls.UIButton()
        Me.txtCekmeListeNo2 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtCekmeListeNo1 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtFaturaNo2 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtFaturaNo1 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtMusteriAdi2 = New System.Windows.Forms.TextBox()
        Me.txtMusteriAdi1 = New System.Windows.Forms.TextBox()
        Me.txtTeslimAlan1 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtMusteri2 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtMusteri1 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnCikis = New Janus.Windows.EditControls.UIButton()
        Me.btnListele = New Janus.Windows.EditControls.UIButton()
        Me.btnAsnGonder = New Janus.Windows.EditControls.UIButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkAsnTekrar = New System.Windows.Forms.RadioButton()
        Me.chkAsnYeni = New System.Windows.Forms.RadioButton()
        Me.dtTarih2 = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.dtTarih1 = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.grdDesAdv = New Janus.Windows.GridEX.GridEX()
        Me.mError = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdDesAdv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.GroupBox1.Controls.Add(Me.chkTarihAraligi)
        Me.GroupBox1.Controls.Add(Me.UıButton1)
        Me.GroupBox1.Controls.Add(Me.txtCekmeListeNo2)
        Me.GroupBox1.Controls.Add(Me.txtCekmeListeNo1)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtFaturaNo2)
        Me.GroupBox1.Controls.Add(Me.txtFaturaNo1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtMusteriAdi2)
        Me.GroupBox1.Controls.Add(Me.txtMusteriAdi1)
        Me.GroupBox1.Controls.Add(Me.txtTeslimAlan1)
        Me.GroupBox1.Controls.Add(Me.txtMusteri2)
        Me.GroupBox1.Controls.Add(Me.txtMusteri1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnCikis)
        Me.GroupBox1.Controls.Add(Me.btnListele)
        Me.GroupBox1.Controls.Add(Me.btnAsnGonder)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.dtTarih2)
        Me.GroupBox1.Controls.Add(Me.dtTarih1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1015, 182)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'chkTarihAraligi
        '
        Me.chkTarihAraligi.AutoSize = True
        Me.chkTarihAraligi.BackColor = System.Drawing.Color.Transparent
        Me.chkTarihAraligi.Location = New System.Drawing.Point(469, 150)
        Me.chkTarihAraligi.Name = "chkTarihAraligi"
        Me.chkTarihAraligi.Size = New System.Drawing.Size(81, 17)
        Me.chkTarihAraligi.TabIndex = 48
        Me.chkTarihAraligi.Text = "Tarih Aralığı"
        Me.chkTarihAraligi.UseVisualStyleBackColor = False
        '
        'UıButton1
        '
        Me.UıButton1.Image = Global.SyteLineEdi.My.Resources.Resources.edit_clear
        Me.UıButton1.Location = New System.Drawing.Point(628, 125)
        Me.UıButton1.Name = "UıButton1"
        Me.UıButton1.Size = New System.Drawing.Size(91, 29)
        Me.UıButton1.TabIndex = 47
        Me.UıButton1.Text = "ASN Sil"
        Me.UıButton1.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'txtCekmeListeNo2
        '
        Me.txtCekmeListeNo2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtCekmeListeNo2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtCekmeListeNo2.Location = New System.Drawing.Point(359, 122)
        Me.txtCekmeListeNo2.Name = "txtCekmeListeNo2"
        Me.txtCekmeListeNo2.Size = New System.Drawing.Size(103, 20)
        Me.txtCekmeListeNo2.TabIndex = 46
        Me.txtCekmeListeNo2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtCekmeListeNo1
        '
        Me.txtCekmeListeNo1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtCekmeListeNo1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtCekmeListeNo1.Location = New System.Drawing.Point(118, 122)
        Me.txtCekmeListeNo1.Name = "txtCekmeListeNo1"
        Me.txtCekmeListeNo1.Size = New System.Drawing.Size(103, 20)
        Me.txtCekmeListeNo1.TabIndex = 44
        Me.txtCekmeListeNo1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(19, 125)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(97, 13)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "Çekme Liste No....:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(19, 151)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "Sevk Tarihi....:"
        '
        'txtFaturaNo2
        '
        Me.txtFaturaNo2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtFaturaNo2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtFaturaNo2.Location = New System.Drawing.Point(359, 96)
        Me.txtFaturaNo2.Name = "txtFaturaNo2"
        Me.txtFaturaNo2.Size = New System.Drawing.Size(103, 20)
        Me.txtFaturaNo2.TabIndex = 42
        Me.txtFaturaNo2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtFaturaNo1
        '
        Me.txtFaturaNo1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtFaturaNo1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtFaturaNo1.Location = New System.Drawing.Point(118, 96)
        Me.txtFaturaNo1.Name = "txtFaturaNo1"
        Me.txtFaturaNo1.Size = New System.Drawing.Size(103, 20)
        Me.txtFaturaNo1.TabIndex = 40
        Me.txtFaturaNo1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(19, 99)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "Fatura No....:"
        '
        'txtMusteriAdi2
        '
        Me.txtMusteriAdi2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMusteriAdi2.Enabled = False
        Me.txtMusteriAdi2.Location = New System.Drawing.Point(359, 41)
        Me.txtMusteriAdi2.Name = "txtMusteriAdi2"
        Me.txtMusteriAdi2.Size = New System.Drawing.Size(236, 20)
        Me.txtMusteriAdi2.TabIndex = 37
        '
        'txtMusteriAdi1
        '
        Me.txtMusteriAdi1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMusteriAdi1.Enabled = False
        Me.txtMusteriAdi1.Location = New System.Drawing.Point(118, 41)
        Me.txtMusteriAdi1.Name = "txtMusteriAdi1"
        Me.txtMusteriAdi1.Size = New System.Drawing.Size(236, 20)
        Me.txtMusteriAdi1.TabIndex = 36
        '
        'txtTeslimAlan1
        '
        Me.txtTeslimAlan1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtTeslimAlan1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtTeslimAlan1.Location = New System.Drawing.Point(118, 70)
        Me.txtTeslimAlan1.Name = "txtTeslimAlan1"
        Me.txtTeslimAlan1.Size = New System.Drawing.Size(103, 20)
        Me.txtTeslimAlan1.TabIndex = 33
        Me.txtTeslimAlan1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtMusteri2
        '
        Me.txtMusteri2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtMusteri2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtMusteri2.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtMusteri2.Location = New System.Drawing.Point(359, 18)
        Me.txtMusteri2.Name = "txtMusteri2"
        Me.txtMusteri2.Size = New System.Drawing.Size(103, 20)
        Me.txtMusteri2.TabIndex = 32
        Me.txtMusteri2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtMusteri1
        '
        Me.txtMusteri1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtMusteri1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtMusteri1.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtMusteri1.Location = New System.Drawing.Point(118, 18)
        Me.txtMusteri1.Name = "txtMusteri1"
        Me.txtMusteri1.Size = New System.Drawing.Size(103, 20)
        Me.txtMusteri1.TabIndex = 31
        Me.txtMusteri1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(19, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 35
        Me.Label3.Text = "Teslim Alan....:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(19, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "Müşteri.....:"
        '
        'btnCikis
        '
        Me.btnCikis.Image = Global.SyteLineEdi.My.Resources.Resources._exit
        Me.btnCikis.Location = New System.Drawing.Point(738, 125)
        Me.btnCikis.Name = "btnCikis"
        Me.btnCikis.Size = New System.Drawing.Size(91, 29)
        Me.btnCikis.TabIndex = 9
        Me.btnCikis.Text = "Çıkış"
        Me.btnCikis.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnListele
        '
        Me.btnListele.Image = Global.SyteLineEdi.My.Resources.Resources.NetByte_Design_Studio___0001
        Me.btnListele.Location = New System.Drawing.Point(628, 87)
        Me.btnListele.Name = "btnListele"
        Me.btnListele.Size = New System.Drawing.Size(91, 29)
        Me.btnListele.TabIndex = 8
        Me.btnListele.Text = "Listele"
        Me.btnListele.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnAsnGonder
        '
        Me.btnAsnGonder.Image = Global.SyteLineEdi.My.Resources.Resources.email
        Me.btnAsnGonder.Location = New System.Drawing.Point(738, 87)
        Me.btnAsnGonder.Name = "btnAsnGonder"
        Me.btnAsnGonder.Size = New System.Drawing.Size(91, 29)
        Me.btnAsnGonder.TabIndex = 7
        Me.btnAsnGonder.Text = "ASN Gönder"
        Me.btnAsnGonder.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.chkAsnTekrar)
        Me.GroupBox2.Controls.Add(Me.chkAsnYeni)
        Me.GroupBox2.Location = New System.Drawing.Point(623, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(124, 55)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        '
        'chkAsnTekrar
        '
        Me.chkAsnTekrar.AutoSize = True
        Me.chkAsnTekrar.Location = New System.Drawing.Point(20, 33)
        Me.chkAsnTekrar.Name = "chkAsnTekrar"
        Me.chkAsnTekrar.Size = New System.Drawing.Size(81, 17)
        Me.chkAsnTekrar.TabIndex = 5
        Me.chkAsnTekrar.Text = "Tekrar ASN"
        Me.chkAsnTekrar.UseVisualStyleBackColor = True
        '
        'chkAsnYeni
        '
        Me.chkAsnYeni.AutoSize = True
        Me.chkAsnYeni.Checked = True
        Me.chkAsnYeni.Location = New System.Drawing.Point(20, 9)
        Me.chkAsnYeni.Name = "chkAsnYeni"
        Me.chkAsnYeni.Size = New System.Drawing.Size(71, 17)
        Me.chkAsnYeni.TabIndex = 4
        Me.chkAsnYeni.TabStop = True
        Me.chkAsnYeni.Text = "Yeni ASN"
        Me.chkAsnYeni.UseVisualStyleBackColor = True
        '
        'dtTarih2
        '
        '
        '
        '
        Me.dtTarih2.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtTarih2.DropDownCalendar.Name = ""
        Me.dtTarih2.DropDownCalendar.Size = New System.Drawing.Size(164, 167)
        Me.dtTarih2.DropDownCalendar.TabIndex = 0
        Me.dtTarih2.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard
        Me.dtTarih2.Enabled = False
        Me.dtTarih2.Location = New System.Drawing.Point(359, 148)
        Me.dtTarih2.Name = "dtTarih2"
        Me.dtTarih2.Size = New System.Drawing.Size(104, 20)
        Me.dtTarih2.TabIndex = 1
        '
        'dtTarih1
        '
        '
        '
        '
        Me.dtTarih1.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtTarih1.DropDownCalendar.Name = ""
        Me.dtTarih1.DropDownCalendar.Size = New System.Drawing.Size(164, 167)
        Me.dtTarih1.DropDownCalendar.TabIndex = 0
        Me.dtTarih1.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard
        Me.dtTarih1.Location = New System.Drawing.Point(118, 148)
        Me.dtTarih1.Name = "dtTarih1"
        Me.dtTarih1.Size = New System.Drawing.Size(104, 20)
        Me.dtTarih1.TabIndex = 0
        '
        'grdDesAdv
        '
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.grdDesAdv.DesignTimeLayout = GridEXLayout1
        Me.grdDesAdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDesAdv.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grdDesAdv.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.grdDesAdv.Location = New System.Drawing.Point(0, 182)
        Me.grdDesAdv.Name = "grdDesAdv"
        Me.grdDesAdv.Size = New System.Drawing.Size(1015, 156)
        Me.grdDesAdv.TabIndex = 1
        '
        'mError
        '
        Me.mError.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.mError.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.mError.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.mError.Location = New System.Drawing.Point(0, 338)
        Me.mError.Multiline = True
        Me.mError.Name = "mError"
        Me.mError.Size = New System.Drawing.Size(1015, 40)
        Me.mError.TabIndex = 2
        Me.mError.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 378)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1015, 22)
        Me.StatusStrip1.TabIndex = 12
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'frmAsnGonder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1015, 400)
        Me.Controls.Add(Me.grdDesAdv)
        Me.Controls.Add(Me.mError)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmAsnGonder"
        Me.Text = "Asn Gönder"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grdDesAdv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    #End Region 'Methods

End Class