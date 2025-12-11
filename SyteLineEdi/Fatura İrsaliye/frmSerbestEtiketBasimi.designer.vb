<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class frmSerbestEtiketBasimi
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Friend  WithEvents btnTemizle As Janus.Windows.EditControls.UIButton
    Friend  WithEvents btnYazdir As Janus.Windows.EditControls.UIButton
    Friend  WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend  WithEvents Label1 As System.Windows.Forms.Label
    Friend  WithEvents Label2 As System.Windows.Forms.Label
    Friend  WithEvents Label3 As System.Windows.Forms.Label
    Friend  WithEvents Label4 As System.Windows.Forms.Label
    Friend  WithEvents txtBoxEtiketMiktari As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend  WithEvents txtBoxKopya As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend  WithEvents txtBoxLot As Janus.Windows.GridEX.EditControls.EditBox
    Friend  WithEvents txtBoxMalzemeNo As Janus.Windows.GridEX.EditControls.EditBox
    Friend  WithEvents txtBoxMalzemeTanim As Janus.Windows.GridEX.EditControls.EditBox

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    #End Region 'Fields

    #Region "Methods"

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnTemizle = New Janus.Windows.EditControls.UIButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnYazdir = New Janus.Windows.EditControls.UIButton()
        Me.txtBoxLot = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtBoxMalzemeTanim = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtBoxMalzemeNo = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBoxKopya = New Janus.Windows.GridEX.EditControls.NumericEditBox()
        Me.txtBoxEtiketMiktari = New Janus.Windows.GridEX.EditControls.NumericEditBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtBoxEtiketMiktari)
        Me.GroupBox1.Controls.Add(Me.txtBoxKopya)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnTemizle)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btnYazdir)
        Me.GroupBox1.Controls.Add(Me.txtBoxLot)
        Me.GroupBox1.Controls.Add(Me.txtBoxMalzemeTanim)
        Me.GroupBox1.Controls.Add(Me.txtBoxMalzemeNo)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(855, 190)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(23, 163)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 71
        Me.Label2.Text = "Kopya Sayısı :"
        '
        'btnTemizle
        '
        Me.btnTemizle.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnTemizle.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnTemizle.Location = New System.Drawing.Point(326, 90)
        Me.btnTemizle.Name = "btnTemizle"
        Me.btnTemizle.Size = New System.Drawing.Size(93, 36)
        Me.btnTemizle.TabIndex = 40
        Me.btnTemizle.Text = "Temizle"
        Me.btnTemizle.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(22, 90)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 70
        Me.Label4.Text = "Lot                :"
        '
        'btnYazdir
        '
        Me.btnYazdir.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnYazdir.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnYazdir.Location = New System.Drawing.Point(326, 140)
        Me.btnYazdir.Name = "btnYazdir"
        Me.btnYazdir.Size = New System.Drawing.Size(93, 36)
        Me.btnYazdir.TabIndex = 45
        Me.btnYazdir.Text = "Yazdır"
        Me.btnYazdir.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'txtBoxLot
        '
        Me.txtBoxLot.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtBoxLot.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtBoxLot.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtBoxLot.Location = New System.Drawing.Point(101, 87)
        Me.txtBoxLot.Name = "txtBoxLot"
        Me.txtBoxLot.Size = New System.Drawing.Size(94, 20)
        Me.txtBoxLot.TabIndex = 10
        Me.txtBoxLot.TabStop = False
        Me.txtBoxLot.Tag = "1"
        Me.txtBoxLot.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtBoxMalzemeTanim
        '
        Me.txtBoxMalzemeTanim.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtBoxMalzemeTanim.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtBoxMalzemeTanim.Enabled = False
        Me.txtBoxMalzemeTanim.Location = New System.Drawing.Point(264, 48)
        Me.txtBoxMalzemeTanim.Name = "txtBoxMalzemeTanim"
        Me.txtBoxMalzemeTanim.ReadOnly = True
        Me.txtBoxMalzemeTanim.Size = New System.Drawing.Size(155, 20)
        Me.txtBoxMalzemeTanim.TabIndex = 10
        Me.txtBoxMalzemeTanim.TabStop = False
        Me.txtBoxMalzemeTanim.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtBoxMalzemeNo
        '
        Me.txtBoxMalzemeNo.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtBoxMalzemeNo.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtBoxMalzemeNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.txtBoxMalzemeNo.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtBoxMalzemeNo.Location = New System.Drawing.Point(100, 48)
        Me.txtBoxMalzemeNo.Name = "txtBoxMalzemeNo"
        Me.txtBoxMalzemeNo.Size = New System.Drawing.Size(144, 20)
        Me.txtBoxMalzemeNo.TabIndex = 5
        Me.txtBoxMalzemeNo.Tag = ""
        Me.txtBoxMalzemeNo.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(22, 127)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 56
        Me.Label3.Text = "Etiket Miktarı :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(22, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 48
        Me.Label1.Text = "Malzeme No :"
        '
        'txtBoxKopya
        '
        Me.txtBoxKopya.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtBoxKopya.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtBoxKopya.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtBoxKopya.Location = New System.Drawing.Point(100, 160)
        Me.txtBoxKopya.Name = "txtBoxKopya"
        Me.txtBoxKopya.Size = New System.Drawing.Size(95, 20)
        Me.txtBoxKopya.TabIndex = 72
        Me.txtBoxKopya.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtBoxKopya.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'txtBoxEtiketMiktari
        '
        Me.txtBoxEtiketMiktari.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtBoxEtiketMiktari.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtBoxEtiketMiktari.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtBoxEtiketMiktari.Location = New System.Drawing.Point(101, 123)
        Me.txtBoxEtiketMiktari.Name = "txtBoxEtiketMiktari"
        Me.txtBoxEtiketMiktari.Size = New System.Drawing.Size(95, 20)
        Me.txtBoxEtiketMiktari.TabIndex = 72
        Me.txtBoxEtiketMiktari.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtBoxEtiketMiktari.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'frmSerbestEtiketBasimi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(855, 496)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "frmSerbestEtiketBasimi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Serbest Etiket Basımı"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    #End Region 'Methods

End Class