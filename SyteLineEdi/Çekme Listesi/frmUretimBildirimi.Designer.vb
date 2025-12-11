<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUretimBildirimi
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
        Dim GridEXLayout1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUretimBildirimi))
        Me.lblYer = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblLot = New System.Windows.Forms.Label()
        Me.txtYer = New System.Windows.Forms.TextBox()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.txtItem = New System.Windows.Forms.TextBox()
        Me.lblItem = New System.Windows.Forms.Label()
        Me.txtLot = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnTemizle = New System.Windows.Forms.Button()
        Me.txtSayi = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtJob = New System.Windows.Forms.TextBox()
        Me.lblJob = New System.Windows.Forms.Label()
        Me.cmbOperNo = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblYer
        '
        Me.lblYer.Location = New System.Drawing.Point(10, 139)
        Me.lblYer.Name = "lblYer"
        Me.lblYer.Size = New System.Drawing.Size(64, 16)
        Me.lblYer.TabIndex = 4
        Me.lblYer.Text = "Yer"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(10, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Oper. No :"
        '
        'lblLot
        '
        Me.lblLot.Location = New System.Drawing.Point(10, 113)
        Me.lblLot.Name = "lblLot"
        Me.lblLot.Size = New System.Drawing.Size(64, 20)
        Me.lblLot.TabIndex = 9
        Me.lblLot.Text = "Lot"
        '
        'txtYer
        '
        Me.txtYer.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtYer.Location = New System.Drawing.Point(80, 139)
        Me.txtYer.MaxLength = 15
        Me.txtYer.Name = "txtYer"
        Me.txtYer.ReadOnly = True
        Me.txtYer.Size = New System.Drawing.Size(152, 20)
        Me.txtYer.TabIndex = 6
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(80, 87)
        Me.txtQty.MaxLength = 6
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(152, 20)
        Me.txtQty.TabIndex = 4
        Me.txtQty.Text = "0"
        '
        'lblQty
        '
        Me.lblQty.Location = New System.Drawing.Point(8, 87)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(72, 20)
        Me.lblQty.TabIndex = 5
        Me.lblQty.Text = "Miktar"
        '
        'txtItem
        '
        Me.txtItem.Location = New System.Drawing.Point(80, 9)
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(152, 20)
        Me.txtItem.TabIndex = 1
        '
        'lblItem
        '
        Me.lblItem.Location = New System.Drawing.Point(8, 9)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(72, 20)
        Me.lblItem.TabIndex = 7
        Me.lblItem.Text = "Malzeme"
        '
        'txtLot
        '
        Me.txtLot.BackColor = System.Drawing.SystemColors.Window
        Me.txtLot.Location = New System.Drawing.Point(80, 113)
        Me.txtLot.Name = "txtLot"
        Me.txtLot.ReadOnly = True
        Me.txtLot.Size = New System.Drawing.Size(152, 20)
        Me.txtLot.TabIndex = 5
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnTemizle)
        Me.Panel2.Controls.Add(Me.txtSayi)
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnSend)
        Me.Panel2.Location = New System.Drawing.Point(16, 193)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(240, 27)
        Me.Panel2.TabIndex = 11
        '
        'btnTemizle
        '
        Me.btnTemizle.Location = New System.Drawing.Point(3, 2)
        Me.btnTemizle.Name = "btnTemizle"
        Me.btnTemizle.Size = New System.Drawing.Size(77, 20)
        Me.btnTemizle.TabIndex = 3
        Me.btnTemizle.Text = "Sýfýrla"
        '
        'txtSayi
        '
        Me.txtSayi.Enabled = False
        Me.txtSayi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtSayi.ForeColor = System.Drawing.Color.Red
        Me.txtSayi.Location = New System.Drawing.Point(80, 3)
        Me.txtSayi.Name = "txtSayi"
        Me.txtSayi.Size = New System.Drawing.Size(40, 20)
        Me.txtSayi.TabIndex = 2
        Me.txtSayi.Text = "0"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(128, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(48, 20)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Kapat"
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(176, 3)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(56, 20)
        Me.btnSend.TabIndex = 0
        Me.btnSend.Text = "Gönder"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmbOperNo)
        Me.Panel1.Controls.Add(Me.lblLot)
        Me.Panel1.Controls.Add(Me.lblYer)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtLot)
        Me.Panel1.Controls.Add(Me.txtQty)
        Me.Panel1.Controls.Add(Me.txtYer)
        Me.Panel1.Controls.Add(Me.lblQty)
        Me.Panel1.Controls.Add(Me.txtItem)
        Me.Panel1.Controls.Add(Me.lblItem)
        Me.Panel1.Controls.Add(Me.txtJob)
        Me.Panel1.Controls.Add(Me.lblJob)
        Me.Panel1.Location = New System.Drawing.Point(14, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(240, 175)
        Me.Panel1.TabIndex = 8
        '
        'txtJob
        '
        Me.txtJob.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtJob.Location = New System.Drawing.Point(80, 35)
        Me.txtJob.Name = "txtJob"
        Me.txtJob.ReadOnly = True
        Me.txtJob.Size = New System.Drawing.Size(112, 20)
        Me.txtJob.TabIndex = 2
        '
        'lblJob
        '
        Me.lblJob.Location = New System.Drawing.Point(10, 35)
        Me.lblJob.Name = "lblJob"
        Me.lblJob.Size = New System.Drawing.Size(72, 20)
        Me.lblJob.TabIndex = 9
        Me.lblJob.Text = "Ýþemri No"
        '
        'cmbOperNo
        '
        Me.cmbOperNo.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.cmbOperNo.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.cmbOperNo.ComboStyle = Janus.Windows.GridEX.ComboStyle.DropDownList
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.cmbOperNo.DesignTimeLayout = GridEXLayout1
        Me.cmbOperNo.DisplayMember = "oper_num"
        Me.cmbOperNo.Location = New System.Drawing.Point(80, 61)
        Me.cmbOperNo.Name = "cmbOperNo"
        Me.cmbOperNo.Size = New System.Drawing.Size(106, 20)
        Me.cmbOperNo.TabIndex = 31
        Me.cmbOperNo.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.cmbOperNo.ValueMember = "oper_num"
        '
        'frmUretimBildirimi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(268, 222)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmUretimBildirimi"
        Me.Text = "Üretim Bildirimi"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblYer As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblLot As System.Windows.Forms.Label
    Friend WithEvents txtYer As System.Windows.Forms.TextBox
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents txtItem As System.Windows.Forms.TextBox
    Friend WithEvents lblItem As System.Windows.Forms.Label
    Friend WithEvents txtLot As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnTemizle As System.Windows.Forms.Button
    Friend WithEvents txtSayi As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtJob As System.Windows.Forms.TextBox
    Friend WithEvents lblJob As System.Windows.Forms.Label
    Friend WithEvents cmbOperNo As Janus.Windows.GridEX.EditControls.MultiColumnCombo
End Class
