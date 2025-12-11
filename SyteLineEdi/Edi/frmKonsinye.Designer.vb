<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKonsinye
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmKonsinye))
        Dim GridEXLayout1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnKapat = New Janus.Windows.EditControls.UIButton()
        Me.btnMesajSil = New Janus.Windows.EditControls.UIButton()
        Me.btnListele = New Janus.Windows.EditControls.UIButton()
        Me.btnMesajAl = New Janus.Windows.EditControls.UIButton()
        Me.btnDizin = New Janus.Windows.EditControls.UIButton()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.OpenDir = New System.Windows.Forms.FolderBrowserDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Panel1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(46, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Mesaj :"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnKapat)
        Me.Panel1.Controls.Add(Me.btnMesajSil)
        Me.Panel1.Controls.Add(Me.btnListele)
        Me.Panel1.Controls.Add(Me.btnMesajAl)
        Me.Panel1.Controls.Add(Me.btnDizin)
        Me.Panel1.Controls.Add(Me.txtPath)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1085, 91)
        Me.Panel1.TabIndex = 1
        '
        'btnKapat
        '
        Me.btnKapat.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnKapat.Image = My.Resources.Resources.Close1
        Me.btnKapat.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnKapat.Location = New System.Drawing.Point(890, 12)
        Me.btnKapat.Name = "btnKapat"
        Me.btnKapat.Size = New System.Drawing.Size(100, 45)
        Me.btnKapat.TabIndex = 15
        Me.btnKapat.Text = "Çıkış"
        Me.btnKapat.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnMesajSil
        '
        Me.btnMesajSil.Image = My.Resources.Resources.edit_clear
        Me.btnMesajSil.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnMesajSil.Location = New System.Drawing.Point(764, 23)
        Me.btnMesajSil.Name = "btnMesajSil"
        Me.btnMesajSil.Size = New System.Drawing.Size(120, 34)
        Me.btnMesajSil.TabIndex = 11
        Me.btnMesajSil.Text = "Tüm Mesajları Sil"
        Me.btnMesajSil.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnListele
        '
        Me.btnListele.Image = My.Resources.Resources.NetByte_Design_Studio___0001
        Me.btnListele.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnListele.Location = New System.Drawing.Point(667, 23)
        Me.btnListele.Name = "btnListele"
        Me.btnListele.Size = New System.Drawing.Size(83, 34)
        Me.btnListele.TabIndex = 10
        Me.btnListele.Text = "Listele"
        Me.btnListele.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnMesajAl
        '
        Me.btnMesajAl.Image = My.Resources.Resources._3617_5784
        Me.btnMesajAl.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnMesajAl.Location = New System.Drawing.Point(570, 23)
        Me.btnMesajAl.Name = "btnMesajAl"
        Me.btnMesajAl.Size = New System.Drawing.Size(83, 34)
        Me.btnMesajAl.TabIndex = 9
        Me.btnMesajAl.Text = "İşle"
        Me.btnMesajAl.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnDizin
        '
        Me.btnDizin.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnDizin.Image = My.Resources.Resources.NetByte_Design_Studio___0483
        Me.btnDizin.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnDizin.Location = New System.Drawing.Point(472, 23)
        Me.btnDizin.Name = "btnDizin"
        Me.btnDizin.Size = New System.Drawing.Size(93, 34)
        Me.btnDizin.TabIndex = 2
        Me.btnDizin.Text = "Mesaj Al"
        Me.btnDizin.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(93, 37)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(365, 20)
        Me.txtPath.TabIndex = 1
        '
        'GridEX1
        '
        Me.GridEX1.BuiltInTextsData = resources.GetString("GridEX1.BuiltInTextsData")
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEXLayout1
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.Location = New System.Drawing.Point(0, 91)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(1085, 369)
        Me.GridEX1.TabIndex = 3
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmKonsinye
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1085, 460)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmKonsinye"
        Me.Text = "frmKonsinye"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnDizin As Janus.Windows.EditControls.UIButton
    Friend WithEvents OpenDir As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btnMesajSil As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnListele As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnMesajAl As Janus.Windows.EditControls.UIButton
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnKapat As Janus.Windows.EditControls.UIButton
End Class
