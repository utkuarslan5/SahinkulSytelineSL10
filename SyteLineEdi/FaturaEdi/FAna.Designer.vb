<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FAna
#Region "Windows Form Designer generated code "
    <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Public WithEvents Command1 As System.Windows.Forms.Button
    Public WithEvents Txt_DosyaYol As System.Windows.Forms.TextBox
    Public WithEvents Chk_Hepsi As System.Windows.Forms.CheckBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents Txt_Taraf As System.Windows.Forms.TextBox
    Public WithEvents cmdTanim As System.Windows.Forms.Button
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents FaturaTarihi As System.Windows.Forms.ColumnHeader
    Public WithEvents FaturaSeriNo As System.Windows.Forms.ColumnHeader
    Public WithEvents FaturaTutar As System.Windows.Forms.ColumnHeader
    Public WithEvents FaturaKdv As System.Windows.Forms.ColumnHeader
    Public WithEvents IrsaliyeNo As System.Windows.Forms.ColumnHeader
    Public WithEvents ResimNo As System.Windows.Forms.ColumnHeader
    Public WithEvents ResimAdi As System.Windows.Forms.ColumnHeader
    Public WithEvents ResimBirim As System.Windows.Forms.ColumnHeader
    Public WithEvents ResimMiktari As System.Windows.Forms.ColumnHeader
    Public WithEvents ResimTutari As System.Windows.Forms.ColumnHeader
    Public WithEvents ResimKdv As System.Windows.Forms.ColumnHeader
    Public WithEvents FaturaSistemNo As System.Windows.Forms.ColumnHeader
    Public WithEvents ListView1 As System.Windows.Forms.ListView
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.FaturaTarihi = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FaturaSeriNo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FaturaTutar = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FaturaKdv = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.IrsaliyeNo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FaturaSistemNo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ResimNo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ResimAdi = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ResimBirim = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ResimMiktari = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ResimTutari = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ResimKdv = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Command1 = New System.Windows.Forms.Button()
        Me.Txt_DosyaYol = New System.Windows.Forms.TextBox()
        Me.Chk_Hepsi = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.dtmBitis = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.dtmBaslangic = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.btnOlustur = New Janus.Windows.EditControls.UIButton()
        Me.btnSorgula = New Janus.Windows.EditControls.UIButton()
        Me.chkAralik = New System.Windows.Forms.CheckBox()
        Me.Txt_Taraf = New System.Windows.Forms.TextBox()
        Me.cmdTanim = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(597, 18)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.RichTextBox1.Size = New System.Drawing.Size(203, 121)
        Me.RichTextBox1.TabIndex = 20
        Me.RichTextBox1.Text = ""
        Me.RichTextBox1.Visible = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.ListView1)
        Me.Frame2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 155)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(960, 430)
        Me.Frame2.TabIndex = 15
        Me.Frame2.TabStop = False
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.BackColor = System.Drawing.SystemColors.Window
        Me.ListView1.CheckBoxes = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FaturaTarihi, Me.FaturaSeriNo, Me.FaturaTutar, Me.FaturaKdv, Me.IrsaliyeNo, Me.FaturaSistemNo, Me.ResimNo, Me.ResimAdi, Me.ResimBirim, Me.ResimMiktari, Me.ResimTutari, Me.ResimKdv})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(3, 16)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(954, 411)
        Me.ListView1.TabIndex = 5
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'FaturaTarihi
        '
        Me.FaturaTarihi.Text = "Fatura Tarihi"
        Me.FaturaTarihi.Width = 118
        '
        'FaturaSeriNo
        '
        Me.FaturaSeriNo.Text = "Fatura No"
        Me.FaturaSeriNo.Width = 118
        '
        'FaturaTutar
        '
        Me.FaturaTutar.Text = "Fatura Tutarý"
        Me.FaturaTutar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.FaturaTutar.Width = 236
        '
        'FaturaKdv
        '
        Me.FaturaKdv.Text = "Fatura KDV Tutarý"
        Me.FaturaKdv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.FaturaKdv.Width = 236
        '
        'IrsaliyeNo
        '
        Me.IrsaliyeNo.Text = "Ýrsaliye No"
        Me.IrsaliyeNo.Width = 170
        '
        'FaturaSistemNo
        '
        Me.FaturaSistemNo.DisplayIndex = 11
        Me.FaturaSistemNo.Text = "FATNO"
        Me.FaturaSistemNo.Width = 0
        '
        'ResimNo
        '
        Me.ResimNo.DisplayIndex = 5
        Me.ResimNo.Text = "Resim No"
        Me.ResimNo.Width = 170
        '
        'ResimAdi
        '
        Me.ResimAdi.DisplayIndex = 6
        Me.ResimAdi.Text = "Resim Adý"
        Me.ResimAdi.Width = 170
        '
        'ResimBirim
        '
        Me.ResimBirim.DisplayIndex = 7
        Me.ResimBirim.Text = "Resim Birim Fiyatý"
        Me.ResimBirim.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ResimBirim.Width = 236
        '
        'ResimMiktari
        '
        Me.ResimMiktari.DisplayIndex = 8
        Me.ResimMiktari.Text = "Resim Miktarý"
        Me.ResimMiktari.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ResimMiktari.Width = 170
        '
        'ResimTutari
        '
        Me.ResimTutari.DisplayIndex = 9
        Me.ResimTutari.Text = "Resim Tutarý"
        Me.ResimTutari.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ResimTutari.Width = 236
        '
        'ResimKdv
        '
        Me.ResimKdv.DisplayIndex = 10
        Me.ResimKdv.Text = "Resim KDV Tutarý"
        Me.ResimKdv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ResimKdv.Width = 236
        '
        'Command1
        '
        Me.Command1.BackColor = System.Drawing.SystemColors.Control
        Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command1.Location = New System.Drawing.Point(564, 130)
        Me.Command1.Name = "Command1"
        Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command1.Size = New System.Drawing.Size(21, 20)
        Me.Command1.TabIndex = 19
        Me.Command1.Text = "?"
        Me.Command1.UseVisualStyleBackColor = False
        '
        'Txt_DosyaYol
        '
        Me.Txt_DosyaYol.AcceptsReturn = True
        Me.Txt_DosyaYol.BackColor = System.Drawing.SystemColors.Window
        Me.Txt_DosyaYol.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Txt_DosyaYol.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Txt_DosyaYol.Location = New System.Drawing.Point(199, 130)
        Me.Txt_DosyaYol.MaxLength = 0
        Me.Txt_DosyaYol.Name = "Txt_DosyaYol"
        Me.Txt_DosyaYol.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_DosyaYol.Size = New System.Drawing.Size(365, 20)
        Me.Txt_DosyaYol.TabIndex = 18
        '
        'Chk_Hepsi
        '
        Me.Chk_Hepsi.BackColor = System.Drawing.SystemColors.Control
        Me.Chk_Hepsi.Cursor = System.Windows.Forms.Cursors.Default
        Me.Chk_Hepsi.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Chk_Hepsi.Location = New System.Drawing.Point(12, 130)
        Me.Chk_Hepsi.Name = "Chk_Hepsi"
        Me.Chk_Hepsi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Chk_Hepsi.Size = New System.Drawing.Size(71, 19)
        Me.Chk_Hepsi.TabIndex = 16
        Me.Chk_Hepsi.Text = "Hepsi"
        Me.Chk_Hepsi.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(92, 130)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(101, 19)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Text Dosya Yolu..:"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.dtmBitis)
        Me.Frame1.Controls.Add(Me.dtmBaslangic)
        Me.Frame1.Controls.Add(Me.btnOlustur)
        Me.Frame1.Controls.Add(Me.btnSorgula)
        Me.Frame1.Controls.Add(Me.RichTextBox1)
        Me.Frame1.Controls.Add(Me.Chk_Hepsi)
        Me.Frame1.Controls.Add(Me.Command1)
        Me.Frame1.Controls.Add(Me.Label5)
        Me.Frame1.Controls.Add(Me.Txt_DosyaYol)
        Me.Frame1.Controls.Add(Me.chkAralik)
        Me.Frame1.Controls.Add(Me.Txt_Taraf)
        Me.Frame1.Controls.Add(Me.cmdTanim)
        Me.Frame1.Controls.Add(Me.Label10)
        Me.Frame1.Controls.Add(Me.Label9)
        Me.Frame1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(960, 155)
        Me.Frame1.TabIndex = 10
        Me.Frame1.TabStop = False
        '
        'dtmBitis
        '
        '
        '
        '
        Me.dtmBitis.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtmBitis.DropDownCalendar.Name = ""
        Me.dtmBitis.DropDownCalendar.Size = New System.Drawing.Size(170, 173)
        Me.dtmBitis.DropDownCalendar.TabIndex = 0
        Me.dtmBitis.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        Me.dtmBitis.Location = New System.Drawing.Point(88, 72)
        Me.dtmBitis.Name = "dtmBitis"
        Me.dtmBitis.Size = New System.Drawing.Size(92, 20)
        Me.dtmBitis.TabIndex = 24
        Me.dtmBitis.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        '
        'dtmBaslangic
        '
        '
        '
        '
        Me.dtmBaslangic.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtmBaslangic.DropDownCalendar.Name = ""
        Me.dtmBaslangic.DropDownCalendar.Size = New System.Drawing.Size(170, 173)
        Me.dtmBaslangic.DropDownCalendar.TabIndex = 0
        Me.dtmBaslangic.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        Me.dtmBaslangic.Location = New System.Drawing.Point(88, 46)
        Me.dtmBaslangic.Name = "dtmBaslangic"
        Me.dtmBaslangic.Size = New System.Drawing.Size(92, 20)
        Me.dtmBaslangic.TabIndex = 23
        Me.dtmBaslangic.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        '
        'btnOlustur
        '
        Me.btnOlustur.Image = Global.SyteLineEdi.My.Resources.Resources.add_file_128x128
        Me.btnOlustur.ImageSize = New System.Drawing.Size(48, 48)
        Me.btnOlustur.Location = New System.Drawing.Point(382, 72)
        Me.btnOlustur.Name = "btnOlustur"
        Me.btnOlustur.Size = New System.Drawing.Size(116, 53)
        Me.btnOlustur.TabIndex = 22
        Me.btnOlustur.Text = "Oluþtur"
        Me.btnOlustur.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnSorgula
        '
        Me.btnSorgula.Image = Global.SyteLineEdi.My.Resources.Resources.search_128x128
        Me.btnSorgula.ImageSize = New System.Drawing.Size(48, 48)
        Me.btnSorgula.Location = New System.Drawing.Point(234, 72)
        Me.btnSorgula.Name = "btnSorgula"
        Me.btnSorgula.Size = New System.Drawing.Size(116, 53)
        Me.btnSorgula.TabIndex = 21
        Me.btnSorgula.Text = "Sorgula"
        Me.btnSorgula.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'chkAralik
        '
        Me.chkAralik.AutoSize = True
        Me.chkAralik.Location = New System.Drawing.Point(186, 48)
        Me.chkAralik.Name = "chkAralik"
        Me.chkAralik.Size = New System.Drawing.Size(81, 17)
        Me.chkAralik.TabIndex = 17
        Me.chkAralik.Text = "Tarih Aralýðý"
        Me.chkAralik.UseVisualStyleBackColor = True
        '
        'Txt_Taraf
        '
        Me.Txt_Taraf.AcceptsReturn = True
        Me.Txt_Taraf.BackColor = System.Drawing.SystemColors.Window
        Me.Txt_Taraf.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Txt_Taraf.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Txt_Taraf.Location = New System.Drawing.Point(88, 20)
        Me.Txt_Taraf.MaxLength = 0
        Me.Txt_Taraf.Name = "Txt_Taraf"
        Me.Txt_Taraf.ReadOnly = True
        Me.Txt_Taraf.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_Taraf.Size = New System.Drawing.Size(225, 20)
        Me.Txt_Taraf.TabIndex = 1
        '
        'cmdTanim
        '
        Me.cmdTanim.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTanim.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTanim.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTanim.Location = New System.Drawing.Point(313, 20)
        Me.cmdTanim.Name = "cmdTanim"
        Me.cmdTanim.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTanim.Size = New System.Drawing.Size(24, 20)
        Me.cmdTanim.TabIndex = 0
        Me.cmdTanim.Text = "..."
        Me.cmdTanim.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(8, 50)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(74, 13)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "Tarih Aralýðý...:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(8, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(53, 13)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Müþteri...:"
        '
        'FAna
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(960, 585)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Location = New System.Drawing.Point(111, 83)
        Me.Name = "FAna"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Tofaþ Faturasý EDI"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Frame2.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chkAralik As System.Windows.Forms.CheckBox
    Friend WithEvents btnSorgula As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnOlustur As Janus.Windows.EditControls.UIButton
    Friend WithEvents dtmBaslangic As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtmBitis As Janus.Windows.CalendarCombo.CalendarCombo
#End Region 
End Class