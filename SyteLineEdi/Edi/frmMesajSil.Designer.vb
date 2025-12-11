<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMesajSil
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
        Me.chkUyari1 = New System.Windows.Forms.CheckBox
        Me.chkUyari2 = New System.Windows.Forms.CheckBox
        Me.chkError = New System.Windows.Forms.CheckBox
        Me.chkTumu = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnIptal = New Janus.Windows.EditControls.UIButton
        Me.btnTamam = New Janus.Windows.EditControls.UIButton
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkUyari1
        '
        Me.chkUyari1.AutoSize = True
        Me.chkUyari1.ForeColor = System.Drawing.Color.MediumSeaGreen
        Me.chkUyari1.Location = New System.Drawing.Point(40, 12)
        Me.chkUyari1.Name = "chkUyari1"
        Me.chkUyari1.Size = New System.Drawing.Size(157, 17)
        Me.chkUyari1.TabIndex = 0
        Me.chkUyari1.Text = "Uyarý veren tüm mesajlarý Sil"
        Me.chkUyari1.UseVisualStyleBackColor = True
        '
        'chkUyari2
        '
        Me.chkUyari2.AutoSize = True
        Me.chkUyari2.ForeColor = System.Drawing.Color.Yellow
        Me.chkUyari2.Location = New System.Drawing.Point(40, 39)
        Me.chkUyari2.Name = "chkUyari2"
        Me.chkUyari2.Size = New System.Drawing.Size(203, 17)
        Me.chkUyari2.TabIndex = 1
        Me.chkUyari2.Text = "Kümülatif Uyarý veren tüm mesajlarý Sil"
        Me.chkUyari2.UseVisualStyleBackColor = True
        '
        'chkError
        '
        Me.chkError.AutoSize = True
        Me.chkError.ForeColor = System.Drawing.Color.Red
        Me.chkError.Location = New System.Drawing.Point(40, 66)
        Me.chkError.Name = "chkError"
        Me.chkError.Size = New System.Drawing.Size(130, 17)
        Me.chkError.TabIndex = 2
        Me.chkError.Text = "Hatalý tüm mesajlarý Sil"
        Me.chkError.UseVisualStyleBackColor = True
        '
        'chkTumu
        '
        Me.chkTumu.AutoSize = True
        Me.chkTumu.ForeColor = System.Drawing.Color.DodgerBlue
        Me.chkTumu.Location = New System.Drawing.Point(40, 93)
        Me.chkTumu.Name = "chkTumu"
        Me.chkTumu.Size = New System.Drawing.Size(104, 17)
        Me.chkTumu.TabIndex = 3
        Me.chkTumu.Text = "Tüm mesajlarý Sil"
        Me.chkTumu.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnIptal)
        Me.GroupBox1.Controls.Add(Me.btnTamam)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox1.Location = New System.Drawing.Point(0, 128)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(292, 61)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'btnIptal
        '
        Me.btnIptal.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnIptal.Image = Global.SyteLineEdi.My.Resources.Resources.Close
        Me.btnIptal.Location = New System.Drawing.Point(166, 19)
        Me.btnIptal.Name = "btnIptal"
        Me.btnIptal.Size = New System.Drawing.Size(106, 26)
        Me.btnIptal.TabIndex = 5
        Me.btnIptal.Text = "Ýptal"
        Me.btnIptal.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnTamam
        '
        Me.btnTamam.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnTamam.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnTamam.Image = Global.SyteLineEdi.My.Resources.Resources.Options
        Me.btnTamam.Location = New System.Drawing.Point(40, 19)
        Me.btnTamam.Name = "btnTamam"
        Me.btnTamam.Size = New System.Drawing.Size(96, 26)
        Me.btnTamam.TabIndex = 4
        Me.btnTamam.Text = "Tamam"
        Me.btnTamam.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'frmMesajSil
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 189)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.chkTumu)
        Me.Controls.Add(Me.chkError)
        Me.Controls.Add(Me.chkUyari2)
        Me.Controls.Add(Me.chkUyari1)
        Me.Name = "frmMesajSil"
        Me.Text = "frmMesajSil"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkUyari1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkUyari2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkError As System.Windows.Forms.CheckBox
    Friend WithEvents chkTumu As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnIptal As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnTamam As Janus.Windows.EditControls.UIButton
End Class
