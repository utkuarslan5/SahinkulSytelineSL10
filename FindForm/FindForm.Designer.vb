<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FindForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FindForm))
        Me.txtFindID = New System.Windows.Forms.TextBox()
        Me.txtFindDescription = New System.Windows.Forms.TextBox()
        Me.lblID = New System.Windows.Forms.Label()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.grpUst = New Janus.Windows.EditControls.UIGroupBox()
        Me.UıGroupBox1 = New Janus.Windows.EditControls.UIGroupBox()
        Me.btnIptal = New Janus.Windows.EditControls.UIButton()
        Me.btnOk = New Janus.Windows.EditControls.UIButton()
        Me.grdFindResult = New Janus.Windows.GridEX.GridEX()
        Me.txtFindCriteria = New System.Windows.Forms.TextBox()
        Me.lblCriteria = New System.Windows.Forms.Label()
        CType(Me.grpUst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpUst.SuspendLayout()
        CType(Me.UıGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UıGroupBox1.SuspendLayout()
        CType(Me.grdFindResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtFindID
        '
        Me.txtFindID.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtFindID.Location = New System.Drawing.Point(159, 12)
        Me.txtFindID.Name = "txtFindID"
        Me.txtFindID.Size = New System.Drawing.Size(284, 27)
        Me.txtFindID.TabIndex = 5
        '
        'txtFindDescription
        '
        Me.txtFindDescription.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtFindDescription.Location = New System.Drawing.Point(159, 45)
        Me.txtFindDescription.Name = "txtFindDescription"
        Me.txtFindDescription.Size = New System.Drawing.Size(285, 27)
        Me.txtFindDescription.TabIndex = 10
        '
        'lblID
        '
        Me.lblID.AutoSize = True
        Me.lblID.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.lblID.Location = New System.Drawing.Point(4, 45)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(35, 19)
        Me.lblID.TabIndex = 5
        Me.lblID.Text = "lbl1"
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.lblDescription.Location = New System.Drawing.Point(4, 12)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(35, 19)
        Me.lblDescription.TabIndex = 6
        Me.lblDescription.Text = "lbl2"
        '
        'grpUst
        '
        Me.grpUst.Controls.Add(Me.txtFindCriteria)
        Me.grpUst.Controls.Add(Me.lblCriteria)
        Me.grpUst.Controls.Add(Me.txtFindID)
        Me.grpUst.Controls.Add(Me.lblDescription)
        Me.grpUst.Controls.Add(Me.txtFindDescription)
        Me.grpUst.Controls.Add(Me.lblID)
        Me.grpUst.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpUst.Location = New System.Drawing.Point(0, 0)
        Me.grpUst.Name = "grpUst"
        Me.grpUst.Size = New System.Drawing.Size(455, 116)
        Me.grpUst.TabIndex = 21
        '
        'UıGroupBox1
        '
        Me.UıGroupBox1.Controls.Add(Me.btnIptal)
        Me.UıGroupBox1.Controls.Add(Me.btnOk)
        Me.UıGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UıGroupBox1.Location = New System.Drawing.Point(0, 247)
        Me.UıGroupBox1.Name = "UıGroupBox1"
        Me.UıGroupBox1.Size = New System.Drawing.Size(455, 80)
        Me.UıGroupBox1.TabIndex = 22
        '
        'btnIptal
        '
        Me.btnIptal.Image = Global.General.My.Resources.Resources.NetByte_Design_Studio___0957
        Me.btnIptal.ImageSize = New System.Drawing.Size(48, 48)
        Me.btnIptal.Location = New System.Drawing.Point(333, 14)
        Me.btnIptal.Name = "btnIptal"
        Me.btnIptal.Size = New System.Drawing.Size(110, 54)
        Me.btnIptal.TabIndex = 22
        Me.btnIptal.Text = "İptal"
        '
        'btnOk
        '
        Me.btnOk.Image = Global.General.My.Resources.Resources.NetByte_Design_Studio___1124
        Me.btnOk.ImageSize = New System.Drawing.Size(48, 48)
        Me.btnOk.Location = New System.Drawing.Point(214, 14)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(111, 54)
        Me.btnOk.TabIndex = 21
        Me.btnOk.Text = "Tamam"
        '
        'grdFindResult
        '
        GridEXLayout1.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition ID="""" /></RootTable></GridEXLayoutDa" &
    "ta>"
        Me.grdFindResult.DesignTimeLayout = GridEXLayout1
        Me.grdFindResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdFindResult.GroupByBoxVisible = False
        Me.grdFindResult.Location = New System.Drawing.Point(0, 116)
        Me.grdFindResult.Name = "grdFindResult"
        Me.grdFindResult.Size = New System.Drawing.Size(455, 131)
        Me.grdFindResult.TabIndex = 23
        '
        'txtFindCriteria
        '
        Me.txtFindCriteria.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtFindCriteria.Location = New System.Drawing.Point(158, 78)
        Me.txtFindCriteria.Name = "txtFindCriteria"
        Me.txtFindCriteria.Size = New System.Drawing.Size(285, 27)
        Me.txtFindCriteria.TabIndex = 14
        '
        'lblCriteria
        '
        Me.lblCriteria.AutoSize = True
        Me.lblCriteria.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.lblCriteria.Location = New System.Drawing.Point(3, 78)
        Me.lblCriteria.Name = "lblCriteria"
        Me.lblCriteria.Size = New System.Drawing.Size(35, 19)
        Me.lblCriteria.TabIndex = 13
        Me.lblCriteria.Text = "lbl3"
        '
        'FindForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(455, 327)
        Me.Controls.Add(Me.grdFindResult)
        Me.Controls.Add(Me.UıGroupBox1)
        Me.Controls.Add(Me.grpUst)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "FindForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bul"
        CType(Me.grpUst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpUst.ResumeLayout(False)
        Me.grpUst.PerformLayout()
        CType(Me.UıGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UıGroupBox1.ResumeLayout(False)
        CType(Me.grdFindResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtFindID As System.Windows.Forms.TextBox
    Friend WithEvents txtFindDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents grpUst As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents UıGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents btnOk As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnIptal As Janus.Windows.EditControls.UIButton
    Friend WithEvents grdFindResult As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtFindCriteria As Windows.Forms.TextBox
    Friend WithEvents lblCriteria As Windows.Forms.Label
End Class
