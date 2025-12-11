<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSevkiyatKalemDetay
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSevkiyatKalemDetay))
        Me.txtCoLine = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCoNum = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtItem = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.txtDescription = New Janus.Windows.GridEX.EditControls.EditBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnVazgec = New Janus.Windows.EditControls.UIButton()
        Me.btnTamam = New Janus.Windows.EditControls.UIButton()
        Me.UıGroupBox1 = New Janus.Windows.EditControls.UIGroupBox()
        Me.txtBoxQty = New Janus.Windows.GridEX.EditControls.NumericEditBox()
        Me.txtShipQty = New Janus.Windows.GridEX.EditControls.NumericEditBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbPackingCode = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.UıGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UıGroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtCoLine
        '
        Me.txtCoLine.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtCoLine.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtCoLine.Location = New System.Drawing.Point(130, 43)
        Me.txtCoLine.MaxLength = 20
        Me.txtCoLine.Name = "txtCoLine"
        Me.txtCoLine.ReadOnly = True
        Me.txtCoLine.Size = New System.Drawing.Size(43, 20)
        Me.txtCoLine.TabIndex = 1
        Me.txtCoLine.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.txtCoLine.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 47)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 13)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "Sipariş Kalemi....:"
        '
        'txtCoNum
        '
        Me.txtCoNum.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtCoNum.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtCoNum.Location = New System.Drawing.Point(130, 15)
        Me.txtCoNum.MaxLength = 15
        Me.txtCoNum.Name = "txtCoNum"
        Me.txtCoNum.ReadOnly = True
        Me.txtCoNum.Size = New System.Drawing.Size(108, 20)
        Me.txtCoNum.TabIndex = 0
        Me.txtCoNum.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtCoNum.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Sipariş No....:"
        '
        'txtItem
        '
        Me.txtItem.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtItem.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtItem.Location = New System.Drawing.Point(130, 71)
        Me.txtItem.MaxLength = 20
        Me.txtItem.Name = "txtItem"
        Me.txtItem.ReadOnly = True
        Me.txtItem.Size = New System.Drawing.Size(169, 20)
        Me.txtItem.TabIndex = 2
        Me.txtItem.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtItem.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'txtDescription
        '
        Me.txtDescription.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtDescription.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtDescription.Location = New System.Drawing.Point(130, 98)
        Me.txtDescription.MaxLength = 30
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReadOnly = True
        Me.txtDescription.Size = New System.Drawing.Size(301, 20)
        Me.txtDescription.TabIndex = 3
        Me.txtDescription.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtDescription.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Malzeme....:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 102)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 13)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Malzeme Tanımı....:"
        '
        'btnVazgec
        '
        Me.btnVazgec.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnVazgec.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnVazgec.Image = Global.SyteLineEdi.My.Resources.Resources.Error1
        Me.btnVazgec.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnVazgec.Location = New System.Drawing.Point(322, 231)
        Me.btnVazgec.Name = "btnVazgec"
        Me.btnVazgec.Size = New System.Drawing.Size(90, 44)
        Me.btnVazgec.TabIndex = 8
        Me.btnVazgec.Text = "Vazgeç"
        Me.btnVazgec.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnTamam
        '
        Me.btnTamam.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnTamam.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnTamam.Image = Global.SyteLineEdi.My.Resources.Resources._Select
        Me.btnTamam.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnTamam.Location = New System.Drawing.Point(198, 231)
        Me.btnTamam.Name = "btnTamam"
        Me.btnTamam.Size = New System.Drawing.Size(90, 44)
        Me.btnTamam.TabIndex = 7
        Me.btnTamam.Text = "Tamam"
        Me.btnTamam.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'UıGroupBox1
        '
        Me.UıGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.UıGroupBox1.Controls.Add(Me.txtBoxQty)
        Me.UıGroupBox1.Controls.Add(Me.txtShipQty)
        Me.UıGroupBox1.Controls.Add(Me.Label7)
        Me.UıGroupBox1.Controls.Add(Me.Label5)
        Me.UıGroupBox1.Controls.Add(Me.cmbPackingCode)
        Me.UıGroupBox1.Controls.Add(Me.Label4)
        Me.UıGroupBox1.Controls.Add(Me.txtCoNum)
        Me.UıGroupBox1.Controls.Add(Me.btnVazgec)
        Me.UıGroupBox1.Controls.Add(Me.Label1)
        Me.UıGroupBox1.Controls.Add(Me.btnTamam)
        Me.UıGroupBox1.Controls.Add(Me.txtItem)
        Me.UıGroupBox1.Controls.Add(Me.txtCoLine)
        Me.UıGroupBox1.Controls.Add(Me.Label6)
        Me.UıGroupBox1.Controls.Add(Me.Label3)
        Me.UıGroupBox1.Controls.Add(Me.txtDescription)
        Me.UıGroupBox1.Controls.Add(Me.Label2)
        Me.UıGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UıGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.UıGroupBox1.Name = "UıGroupBox1"
        Me.UıGroupBox1.Size = New System.Drawing.Size(434, 285)
        Me.UıGroupBox1.TabIndex = 29
        Me.UıGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'txtBoxQty
        '
        Me.txtBoxQty.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtBoxQty.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtBoxQty.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtBoxQty.Location = New System.Drawing.Point(130, 155)
        Me.txtBoxQty.Name = "txtBoxQty"
        Me.txtBoxQty.Size = New System.Drawing.Size(67, 20)
        Me.txtBoxQty.TabIndex = 5
        Me.txtBoxQty.Text = "0"
        Me.txtBoxQty.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.txtBoxQty.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'txtShipQty
        '
        Me.txtShipQty.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtShipQty.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtShipQty.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtShipQty.Location = New System.Drawing.Point(130, 127)
        Me.txtShipQty.Name = "txtShipQty"
        Me.txtShipQty.Size = New System.Drawing.Size(67, 20)
        Me.txtShipQty.TabIndex = 4
        Me.txtShipQty.Text = "0"
        Me.txtShipQty.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.txtShipQty.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 159)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(87, 13)
        Me.Label7.TabIndex = 34
        Me.Label7.Text = "Kutu İçi Miktar...:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 189)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 13)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Ambalaj Kodu....:"
        '
        'cmbPackingCode
        '
        Me.cmbPackingCode.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.cmbPackingCode.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.cmbPackingCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.cmbPackingCode.DesignTimeLayout = GridEXLayout1
        Me.cmbPackingCode.DisplayMember = "AMBKOD"
        Me.cmbPackingCode.Location = New System.Drawing.Point(130, 185)
        Me.cmbPackingCode.Name = "cmbPackingCode"
        Me.cmbPackingCode.Size = New System.Drawing.Size(67, 20)
        Me.cmbPackingCode.TabIndex = 6
        Me.cmbPackingCode.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.cmbPackingCode.ValueMember = "AMBKOD"
        Me.cmbPackingCode.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 131)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 13)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Sevkiyat Miktarı....:"
        '
        'frmSevkiyatKalemDetay
        '
        Me.AcceptButton = Me.btnTamam
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnVazgec
        Me.ClientSize = New System.Drawing.Size(434, 285)
        Me.Controls.Add(Me.UıGroupBox1)
        Me.Name = "frmSevkiyatKalemDetay"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sevkiyat Kalem Detayı"
        CType(Me.UıGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UıGroupBox1.ResumeLayout(False)
        Me.UıGroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtCoLine As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCoNum As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtItem As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtDescription As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnVazgec As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnTamam As Janus.Windows.EditControls.UIButton
    Friend WithEvents UıGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbPackingCode As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtBoxQty As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents txtShipQty As Janus.Windows.GridEX.EditControls.NumericEditBox
End Class
