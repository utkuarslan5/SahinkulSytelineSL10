<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFaturaGuncelleme
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
        Me.txtMusteriAdi1 = New System.Windows.Forms.TextBox()
        Me.txtMusteri1 = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnGuncelle = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtMusteriAdi1
        '
        Me.txtMusteriAdi1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMusteriAdi1.Enabled = False
        Me.txtMusteriAdi1.Location = New System.Drawing.Point(111, 44)
        Me.txtMusteriAdi1.Name = "txtMusteriAdi1"
        Me.txtMusteriAdi1.Size = New System.Drawing.Size(209, 20)
        Me.txtMusteriAdi1.TabIndex = 37
        '
        'txtMusteri1
        '
        Me.txtMusteri1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtMusteri1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtMusteri1.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtMusteri1.Location = New System.Drawing.Point(111, 21)
        Me.txtMusteri1.Name = "txtMusteri1"
        Me.txtMusteri1.Size = New System.Drawing.Size(103, 20)
        Me.txtMusteri1.TabIndex = 35
        Me.txtMusteri1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(12, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "Müşteri.....:"
        '
        'btnGuncelle
        '
        Me.btnGuncelle.Location = New System.Drawing.Point(111, 100)
        Me.btnGuncelle.Name = "btnGuncelle"
        Me.btnGuncelle.Size = New System.Drawing.Size(140, 51)
        Me.btnGuncelle.TabIndex = 38
        Me.btnGuncelle.Text = "Güncelle"
        Me.btnGuncelle.UseVisualStyleBackColor = True
        '
        'frmFaturaGuncelleme
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(404, 179)
        Me.Controls.Add(Me.btnGuncelle)
        Me.Controls.Add(Me.txtMusteriAdi1)
        Me.Controls.Add(Me.txtMusteri1)
        Me.Controls.Add(Me.Label2)
        Me.Name = "frmFaturaGuncelleme"
        Me.Text = "frmFaturaGuncelleme"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtMusteriAdi1 As System.Windows.Forms.TextBox
    Friend WithEvents txtMusteri1 As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnGuncelle As System.Windows.Forms.Button
End Class
