<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mdifrmMenu
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
        Me.components = New System.ComponentModel.Container
        Dim ButtonBarGroup1 As Janus.Windows.ButtonBar.ButtonBarGroup = New Janus.Windows.ButtonBar.ButtonBarGroup
        Dim ButtonBarItem1 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarItem2 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarItem3 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarGroup2 As Janus.Windows.ButtonBar.ButtonBarGroup = New Janus.Windows.ButtonBar.ButtonBarGroup
        Dim ButtonBarItem4 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarItem5 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarItem6 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarItem7 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarGroup3 As Janus.Windows.ButtonBar.ButtonBarGroup = New Janus.Windows.ButtonBar.ButtonBarGroup
        Dim ButtonBarItem8 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarItem9 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarItem10 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarItem11 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarGroup4 As Janus.Windows.ButtonBar.ButtonBarGroup = New Janus.Windows.ButtonBar.ButtonBarGroup
        Dim ButtonBarItem12 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarItem13 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarItem14 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarItem15 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarGroup5 As Janus.Windows.ButtonBar.ButtonBarGroup = New Janus.Windows.ButtonBar.ButtonBarGroup
        Dim ButtonBarItem16 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarItem17 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarItem18 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarGroup6 As Janus.Windows.ButtonBar.ButtonBarGroup = New Janus.Windows.ButtonBar.ButtonBarGroup
        Dim ButtonBarGroup7 As Janus.Windows.ButtonBar.ButtonBarGroup = New Janus.Windows.ButtonBar.ButtonBarGroup
        Dim ButtonBarItem19 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarItem20 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarItem21 As Janus.Windows.ButtonBar.ButtonBarItem = New Janus.Windows.ButtonBar.ButtonBarItem
        Dim ButtonBarLayout1 As Janus.Windows.ButtonBar.ButtonBarLayout = New Janus.Windows.ButtonBar.ButtonBarLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mdifrmMenu))
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ButtonBar1 = New Janus.Windows.ButtonBar.ButtonBar
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ýmageList1 = New System.Windows.Forms.ImageList(Me.components)
        CType(Me.ButtonBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonBar1
        '
        Me.ButtonBar1.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonBar1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ButtonBar1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.ButtonBar1.GroupAppearance = Janus.Windows.ButtonBar.GroupAppearance.PopUp
        ButtonBarItem1.Key = "Item1"
        ButtonBarItem1.TagString = "100"
        ButtonBarItem1.Text = "Edi Aktarým Ýþlemi"
        ButtonBarItem2.Key = "Item2"
        ButtonBarItem2.TagString = "101"
        ButtonBarItem2.Text = "Edi Syteline Aktarým Ýþlemi"
        ButtonBarItem3.Key = "Item3"
        ButtonBarItem3.TagString = "102"
        ButtonBarItem3.Text = "Edi Asn Gönder"
        ButtonBarGroup1.Items.AddRange(New Janus.Windows.ButtonBar.ButtonBarItem() {ButtonBarItem1, ButtonBarItem2, ButtonBarItem3})
        ButtonBarGroup1.Key = "Group2"
        ButtonBarGroup1.Text = "Edi"
        ButtonBarItem4.Key = "Item3"
        ButtonBarItem4.Text = "Edi Kümülatif Sevkiyat Bilgileri"
        ButtonBarItem5.Key = "Item4"
        ButtonBarItem5.Text = "Edi Nakliyeci Bilgileri"
        ButtonBarItem6.Key = "Item5"
        ButtonBarItem6.TagString = "100"
        ButtonBarItem6.Text = "Edi Sevkiyat Takvimi"
        ButtonBarItem7.Key = "Item6"
        ButtonBarItem7.Text = "Edi Tatil Günleri"
        ButtonBarGroup2.Items.AddRange(New Janus.Windows.ButtonBar.ButtonBarItem() {ButtonBarItem4, ButtonBarItem5, ButtonBarItem6, ButtonBarItem7})
        ButtonBarGroup2.Key = "Group1"
        ButtonBarGroup2.Text = "Edi Tanýmlar"
        ButtonBarItem8.Key = "Item1"
        ButtonBarItem8.TagString = "104"
        ButtonBarItem8.Text = "Çekme Listesi Oluþturma"
        ButtonBarItem9.Key = "Item2"
        ButtonBarItem9.TagString = "105"
        ButtonBarItem9.Text = "Çekme Listesi Yazdýrma"
        ButtonBarItem10.Key = "Item3"
        ButtonBarItem10.TagString = "106"
        ButtonBarItem10.Text = "Etiket Basma"
        ButtonBarItem11.Key = "Item4"
        ButtonBarItem11.TagString = "113"
        ButtonBarItem11.Text = "Ambalaj Ýþemri Oluþturma"
        ButtonBarGroup3.Items.AddRange(New Janus.Windows.ButtonBar.ButtonBarItem() {ButtonBarItem8, ButtonBarItem9, ButtonBarItem10, ButtonBarItem11})
        ButtonBarGroup3.Key = "Group5"
        ButtonBarGroup3.Text = "Çekme Listesi"
        ButtonBarItem12.Key = "Item1"
        ButtonBarItem12.TagString = "107"
        ButtonBarItem12.Text = "Ýrsaliye Basýmý"
        ButtonBarItem13.Key = "Item2"
        ButtonBarItem13.TagString = "108"
        ButtonBarItem13.Text = "Ýrsaliye Ýptal"
        ButtonBarItem14.Key = "Item3"
        ButtonBarItem14.TagString = "109"
        ButtonBarItem14.Text = "Fatura Oluþturma"
        ButtonBarItem15.Key = "Item4"
        ButtonBarItem15.TagString = "110"
        ButtonBarItem15.Text = "Fatura Yazdýrma"
        ButtonBarGroup4.Items.AddRange(New Janus.Windows.ButtonBar.ButtonBarItem() {ButtonBarItem12, ButtonBarItem13, ButtonBarItem14, ButtonBarItem15})
        ButtonBarGroup4.Key = "Group6"
        ButtonBarGroup4.Text = "Fatura Ve Ýrsaliye Basýmý"
        ButtonBarItem16.Key = "Item1"
        ButtonBarItem16.TagString = "111"
        ButtonBarItem16.Text = "Ýhracat  Gümrük Evraklarý"
        ButtonBarItem17.Key = "Item2"
        ButtonBarItem17.TagString = "112"
        ButtonBarItem17.Text = "Gümrük Bildirimi"
        ButtonBarItem18.Key = "Item3"
        ButtonBarItem18.TagString = "103"
        ButtonBarItem18.Text = "Delivery Note"
        ButtonBarGroup5.Items.AddRange(New Janus.Windows.ButtonBar.ButtonBarItem() {ButtonBarItem16, ButtonBarItem17, ButtonBarItem18})
        ButtonBarGroup5.Key = "Group7"
        ButtonBarGroup5.Text = "Raporlar"
        ButtonBarGroup6.Key = "Group3"
        ButtonBarGroup6.Text = "Sistem Parametreleri"
        ButtonBarItem19.Key = "Item1"
        ButtonBarItem19.TagString = "97"
        ButtonBarItem19.Text = "Kullanicilar"
        ButtonBarItem20.Key = "Item2"
        ButtonBarItem20.TagString = "98"
        ButtonBarItem20.Text = "Programlar"
        ButtonBarItem21.Key = "Item3"
        ButtonBarItem21.TagString = "99"
        ButtonBarItem21.Text = "Yetki Verme"
        ButtonBarGroup7.Items.AddRange(New Janus.Windows.ButtonBar.ButtonBarItem() {ButtonBarItem19, ButtonBarItem20, ButtonBarItem21})
        ButtonBarGroup7.Key = "Group4"
        ButtonBarGroup7.Text = "Yetkilendirme"
        Me.ButtonBar1.Groups.AddRange(New Janus.Windows.ButtonBar.ButtonBarGroup() {ButtonBarGroup1, ButtonBarGroup2, ButtonBarGroup3, ButtonBarGroup4, ButtonBarGroup5, ButtonBarGroup6, ButtonBarGroup7})
        Me.ButtonBar1.GroupsFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!)
        ButtonBarLayout1.IsCurrentLayout = True
        ButtonBarLayout1.Key = "EdiList"
        ButtonBarLayout1.LayoutString = resources.GetString("ButtonBarLayout1.LayoutString")
        Me.ButtonBar1.Layouts.AddRange(New Janus.Windows.ButtonBar.ButtonBarLayout() {ButtonBarLayout1})
        Me.ButtonBar1.Location = New System.Drawing.Point(0, 0)
        Me.ButtonBar1.Name = "ButtonBar1"
        Me.ButtonBar1.SelectionArea = Janus.Windows.ButtonBar.SelectionArea.FullItem
        Me.ButtonBar1.Size = New System.Drawing.Size(180, 453)
        Me.ButtonBar1.TabIndex = 11
        Me.ButtonBar1.VisualStyle = Janus.Windows.ButtonBar.VisualStyle.Office2003
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(111, 17)
        Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'ýmageList1
        '
        Me.ýmageList1.ImageStream = CType(resources.GetObject("ýmageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ýmageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ýmageList1.Images.SetKeyName(0, "NetByte Design Studio - 0500.png")
        Me.ýmageList1.Images.SetKeyName(1, "folder.ico")
        Me.ýmageList1.Images.SetKeyName(2, "folder_closed.ico")
        '
        'mdifrmMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.logo1
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(632, 453)
        Me.Controls.Add(Me.ButtonBar1)
        Me.IsMdiContainer = True
        Me.Name = "mdifrmMenu"
        Me.Text = "Ekip MAPICS EDI Programý"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.ButtonBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ButtonBar1 As Janus.Windows.ButtonBar.ButtonBar
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ýmageList1 As System.Windows.Forms.ImageList

End Class
