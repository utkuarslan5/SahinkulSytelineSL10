<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFaturaSeri
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
        Me.components = New System.ComponentModel.Container()
        Me.txtFaturaSeriNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnVazgec = New Janus.Windows.EditControls.UIButton()
        Me.btnTamam = New Janus.Windows.EditControls.UIButton()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFaturaNotu = New System.Windows.Forms.TextBox()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtFaturaSeriNo
        '
        Me.txtFaturaSeriNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFaturaSeriNo.Location = New System.Drawing.Point(126, 12)
        Me.txtFaturaSeriNo.MaxLength = 16
        Me.txtFaturaSeriNo.Name = "txtFaturaSeriNo"
        Me.txtFaturaSeriNo.Size = New System.Drawing.Size(155, 20)
        Me.txtFaturaSeriNo.TabIndex = 35
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(30, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 13)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "Fatura Seri No....:"
        '
        'btnVazgec
        '
        Me.btnVazgec.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnVazgec.Image = Global.SyteLineEdi.My.Resources.Resources.Error1
        Me.btnVazgec.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnVazgec.Location = New System.Drawing.Point(248, 79)
        Me.btnVazgec.Name = "btnVazgec"
        Me.btnVazgec.Size = New System.Drawing.Size(82, 32)
        Me.btnVazgec.TabIndex = 38
        Me.btnVazgec.Text = "Vazgeç"
        Me.btnVazgec.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnTamam
        '
        Me.btnTamam.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnTamam.Image = Global.SyteLineEdi.My.Resources.Resources._Select
        Me.btnTamam.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnTamam.Location = New System.Drawing.Point(133, 79)
        Me.btnTamam.Name = "btnTamam"
        Me.btnTamam.Size = New System.Drawing.Size(83, 32)
        Me.btnTamam.TabIndex = 37
        Me.btnTamam.Text = "Tamam"
        Me.btnTamam.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Fatura Notu....:"
        '
        'txtFaturaNotu
        '
        Me.txtFaturaNotu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFaturaNotu.Location = New System.Drawing.Point(126, 38)
        Me.txtFaturaNotu.MaxLength = 100
        Me.txtFaturaNotu.Name = "txtFaturaNotu"
        Me.txtFaturaNotu.Size = New System.Drawing.Size(207, 20)
        Me.txtFaturaNotu.TabIndex = 39
        '
        'frmFaturaSeri
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(342, 123)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtFaturaNotu)
        Me.Controls.Add(Me.btnVazgec)
        Me.Controls.Add(Me.btnTamam)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtFaturaSeriNo)
        Me.Name = "frmFaturaSeri"
        Me.Text = "Fatura Seri No Girişi"
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtFaturaSeriNo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnVazgec As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnTamam As Janus.Windows.EditControls.UIButton
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFaturaNotu As System.Windows.Forms.TextBox
End Class
