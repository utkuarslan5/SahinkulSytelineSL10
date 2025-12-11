<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAyniyatIade
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
        Dim GridEXLayout1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAyniyatIade))
        Me.btnTamam = New Janus.Windows.EditControls.UIButton
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.btnCikis = New Janus.Windows.EditControls.UIButton
        Me.UıGroupBox2 = New Janus.Windows.EditControls.UIGroupBox
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UıGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UıGroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnTamam
        '
        Me.btnTamam.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnTamam.Image = Global.SyteLineEdi.My.Resources.Resources._Select
        Me.btnTamam.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnTamam.Location = New System.Drawing.Point(205, 19)
        Me.btnTamam.Name = "btnTamam"
        Me.btnTamam.Size = New System.Drawing.Size(83, 32)
        Me.btnTamam.TabIndex = 29
        Me.btnTamam.Text = "Tamam"
        Me.btnTamam.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'GridEX1
        '
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEXLayout1
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.Location = New System.Drawing.Point(0, 0)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(458, 259)
        Me.GridEX1.TabIndex = 3
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'btnCikis
        '
        Me.btnCikis.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnCikis.Image = Global.SyteLineEdi.My.Resources.Resources.Error1
        Me.btnCikis.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnCikis.Location = New System.Drawing.Point(320, 19)
        Me.btnCikis.Name = "btnCikis"
        Me.btnCikis.Size = New System.Drawing.Size(82, 32)
        Me.btnCikis.TabIndex = 30
        Me.btnCikis.Text = "Vazgeç"
        Me.btnCikis.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'UıGroupBox2
        '
        Me.UıGroupBox2.Controls.Add(Me.btnCikis)
        Me.UıGroupBox2.Controls.Add(Me.btnTamam)
        Me.UıGroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UıGroupBox2.Location = New System.Drawing.Point(0, 259)
        Me.UıGroupBox2.Name = "UıGroupBox2"
        Me.UıGroupBox2.Size = New System.Drawing.Size(458, 74)
        Me.UıGroupBox2.TabIndex = 4
        '
        'frmAyniyatIade
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(458, 333)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.UıGroupBox2)
        Me.Name = "frmAyniyatIade"
        Me.Text = "Ayniyat İade"
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UıGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UıGroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnTamam As Janus.Windows.EditControls.UIButton
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnCikis As Janus.Windows.EditControls.UIButton
    Friend WithEvents UıGroupBox2 As Janus.Windows.EditControls.UIGroupBox
End Class
