<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmxmlkaydet
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtImalatciKodu = New System.Windows.Forms.TextBox()
        Me.txtAlici = New System.Windows.Forms.TextBox()
        Me.txtSipNo = New System.Windows.Forms.TextBox()
        Me.btnKaydet = New Janus.Windows.EditControls.UIButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtKlasoryolu = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS Reference Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label1.Location = New System.Drawing.Point(35, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Ýmalatçý Kodu"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS Reference Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label2.Location = New System.Drawing.Point(35, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Alýcý"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS Reference Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label3.Location = New System.Drawing.Point(35, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Sipariþ No"
        Me.Label3.Visible = False
        '
        'txtImalatciKodu
        '
        Me.txtImalatciKodu.BackColor = System.Drawing.Color.Yellow
        Me.txtImalatciKodu.Location = New System.Drawing.Point(138, 19)
        Me.txtImalatciKodu.Name = "txtImalatciKodu"
        Me.txtImalatciKodu.Size = New System.Drawing.Size(100, 20)
        Me.txtImalatciKodu.TabIndex = 3
        '
        'txtAlici
        '
        Me.txtAlici.BackColor = System.Drawing.Color.Yellow
        Me.txtAlici.Location = New System.Drawing.Point(138, 45)
        Me.txtAlici.Name = "txtAlici"
        Me.txtAlici.Size = New System.Drawing.Size(100, 20)
        Me.txtAlici.TabIndex = 4
        '
        'txtSipNo
        '
        Me.txtSipNo.BackColor = System.Drawing.Color.Yellow
        Me.txtSipNo.Location = New System.Drawing.Point(138, 100)
        Me.txtSipNo.Name = "txtSipNo"
        Me.txtSipNo.Size = New System.Drawing.Size(100, 20)
        Me.txtSipNo.TabIndex = 5
        Me.txtSipNo.Visible = False
        '
        'btnKaydet
        '
        Me.btnKaydet.Font = New System.Drawing.Font("MS Reference Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnKaydet.Image = Global.SyteLineEdi.My.Resources.Resources.kaydet
        Me.btnKaydet.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnKaydet.Location = New System.Drawing.Point(138, 153)
        Me.btnKaydet.Name = "btnKaydet"
        Me.btnKaydet.Size = New System.Drawing.Size(91, 37)
        Me.btnKaydet.TabIndex = 6
        Me.btnKaydet.Text = "Kaydet"
        Me.btnKaydet.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.GroupBox1.Controls.Add(Me.btnKaydet)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtKlasoryolu)
        Me.GroupBox1.Controls.Add(Me.txtSipNo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtAlici)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtImalatciKodu)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.GroupBox1.Location = New System.Drawing.Point(33, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(356, 254)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Sabit Parametreler"
        '
        'txtKlasoryolu
        '
        Me.txtKlasoryolu.BackColor = System.Drawing.Color.Yellow
        Me.txtKlasoryolu.Location = New System.Drawing.Point(138, 72)
        Me.txtKlasoryolu.Name = "txtKlasoryolu"
        Me.txtKlasoryolu.Size = New System.Drawing.Size(100, 20)
        Me.txtKlasoryolu.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS Reference Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label4.Location = New System.Drawing.Point(35, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 16)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Klasör Yolu"
        '
        'frmxmlkaydet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.ClientSize = New System.Drawing.Size(428, 316)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "frmxmlkaydet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtImalatciKodu As System.Windows.Forms.TextBox
    Friend WithEvents txtAlici As System.Windows.Forms.TextBox
    Friend WithEvents txtSipNo As System.Windows.Forms.TextBox
    Friend WithEvents btnKaydet As Janus.Windows.EditControls.UIButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtKlasoryolu As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
