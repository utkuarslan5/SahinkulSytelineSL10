<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDetay
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
        Dim GridEXLayout1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDetay))
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.btnTamam = New Janus.Windows.EditControls.UIButton()
        Me.btnIptal = New Janus.Windows.EditControls.UIButton()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridEX1
        '
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEXLayout1
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GridEX1.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.Location = New System.Drawing.Point(0, 0)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(902, 262)
        Me.GridEX1.TabIndex = 8
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'btnTamam
        '
        Me.btnTamam.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnTamam.Image = Global.SyteLineEdi.My.Resources.Resources._Select
        Me.btnTamam.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnTamam.Location = New System.Drawing.Point(773, 276)
        Me.btnTamam.Name = "btnTamam"
        Me.btnTamam.Size = New System.Drawing.Size(83, 32)
        Me.btnTamam.TabIndex = 38
        Me.btnTamam.Text = "Kaydet"
        Me.btnTamam.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnIptal
        '
        Me.btnIptal.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnIptal.Image = Global.SyteLineEdi.My.Resources.Resources.Close1
        Me.btnIptal.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnIptal.Location = New System.Drawing.Point(665, 276)
        Me.btnIptal.Name = "btnIptal"
        Me.btnIptal.Size = New System.Drawing.Size(83, 32)
        Me.btnIptal.TabIndex = 39
        Me.btnIptal.Text = "İptal"
        Me.btnIptal.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'frmDetay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(902, 320)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.btnIptal)
        Me.Controls.Add(Me.btnTamam)
        Me.Name = "frmDetay"
        Me.Text = "Palet Detay Ekranı"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnTamam As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnIptal As Janus.Windows.EditControls.UIButton
End Class
