<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKonsinyeBilgileri
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmKonsinyeBilgileri))
        Dim GridEXLayout1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnKapat = New Janus.Windows.EditControls.UIButton()
        Me.btnIsle = New Janus.Windows.EditControls.UIButton()
        Me.Panel1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GridEX1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(797, 271)
        Me.Panel1.TabIndex = 0
        '
        'GridEX1
        '
        Me.GridEX1.BuiltInTextsData = resources.GetString("GridEX1.BuiltInTextsData")
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEXLayout1
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.Location = New System.Drawing.Point(0, 0)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(797, 271)
        Me.GridEX1.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnKapat)
        Me.Panel2.Controls.Add(Me.btnIsle)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 271)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(797, 102)
        Me.Panel2.TabIndex = 1
        '
        'btnKapat
        '
        Me.btnKapat.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnKapat.Image = My.Resources.Resources._Error
        Me.btnKapat.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnKapat.Location = New System.Drawing.Point(638, 27)
        Me.btnKapat.Name = "btnKapat"
        Me.btnKapat.Size = New System.Drawing.Size(126, 40)
        Me.btnKapat.TabIndex = 14
        Me.btnKapat.Text = "Kapat"
        Me.btnKapat.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnIsle
        '
        Me.btnIsle.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnIsle.Image = My.Resources.Resources.kaydet
        Me.btnIsle.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnIsle.Location = New System.Drawing.Point(462, 27)
        Me.btnIsle.Name = "btnIsle"
        Me.btnIsle.Size = New System.Drawing.Size(126, 40)
        Me.btnIsle.TabIndex = 13
        Me.btnIsle.Text = "İşle"
        Me.btnIsle.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'frmKonsinyeBilgileri
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(797, 373)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmKonsinyeBilgileri"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Konsinye Bilgileri"
        Me.Panel1.ResumeLayout(False)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnIsle As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnKapat As Janus.Windows.EditControls.UIButton
End Class
