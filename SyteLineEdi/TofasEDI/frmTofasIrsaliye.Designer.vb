<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTofasIrsaliye
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTofasIrsaliye))
        Me.btnGoster = New Janus.Windows.EditControls.UIButton()
        Me.btnAktar = New Janus.Windows.EditControls.UIButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkWebSrvFrm = New System.Windows.Forms.CheckBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.btnSabit = New Janus.Windows.EditControls.UIButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtIrsNo = New System.Windows.Forms.TextBox()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnGoster
        '
        Me.btnGoster.Image = Global.SyteLineEdi.My.Resources.Resources.Search
        Me.btnGoster.ImageSize = New System.Drawing.Size(46, 46)
        Me.btnGoster.Location = New System.Drawing.Point(294, 10)
        Me.btnGoster.Name = "btnGoster"
        Me.btnGoster.Size = New System.Drawing.Size(112, 46)
        Me.btnGoster.TabIndex = 0
        Me.btnGoster.Text = "Göster"
        Me.btnGoster.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnAktar
        '
        Me.btnAktar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnAktar.Image = Global.SyteLineEdi.My.Resources.Resources.notepad2
        Me.btnAktar.ImageSize = New System.Drawing.Size(46, 46)
        Me.btnAktar.Location = New System.Drawing.Point(543, 10)
        Me.btnAktar.Name = "btnAktar"
        Me.btnAktar.Size = New System.Drawing.Size(112, 46)
        Me.btnAktar.TabIndex = 2
        Me.btnAktar.Text = "Aktar"
        Me.btnAktar.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'GroupBox1
        '
        Me.GroupBox1.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.GroupBox1.Controls.Add(Me.chkWebSrvFrm)
        Me.GroupBox1.Controls.Add(Me.ComboBox1)
        Me.GroupBox1.Controls.Add(Me.btnSabit)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtIrsNo)
        Me.GroupBox1.Controls.Add(Me.btnAktar)
        Me.GroupBox1.Controls.Add(Me.btnGoster)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1005, 106)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'chkWebSrvFrm
        '
        Me.chkWebSrvFrm.AutoSize = True
        Me.chkWebSrvFrm.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.chkWebSrvFrm.Location = New System.Drawing.Point(413, 27)
        Me.chkWebSrvFrm.Name = "chkWebSrvFrm"
        Me.chkWebSrvFrm.Size = New System.Drawing.Size(118, 17)
        Me.chkWebSrvFrm.TabIndex = 1
        Me.chkWebSrvFrm.Text = "Web Servis Formatý"
        Me.chkWebSrvFrm.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"S - SERÝ", "N - NUMUNE", "O - ÖNSERÝ "})
        Me.ComboBox1.Location = New System.Drawing.Point(88, 59)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(148, 21)
        Me.ComboBox1.TabIndex = 5
        Me.ComboBox1.Text = "S - SERÝ"
        '
        'btnSabit
        '
        Me.btnSabit.Image = Global.SyteLineEdi.My.Resources.Resources.show_sticky
        Me.btnSabit.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnSabit.Location = New System.Drawing.Point(747, 10)
        Me.btnSabit.Name = "btnSabit"
        Me.btnSabit.Size = New System.Drawing.Size(139, 46)
        Me.btnSabit.TabIndex = 4
        Me.btnSabit.Text = "Sabit Parametreler"
        Me.btnSabit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Seri :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 16)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Ýrsaliye No :"
        '
        'txtIrsNo
        '
        Me.txtIrsNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtIrsNo.Location = New System.Drawing.Point(88, 19)
        Me.txtIrsNo.Name = "txtIrsNo"
        Me.txtIrsNo.Size = New System.Drawing.Size(200, 20)
        Me.txtIrsNo.TabIndex = 2
        '
        'grd
        '
        Me.grd.AlternatingColors = True
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.grd.DesignTimeLayout = GridEXLayout1
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(0, 106)
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(1005, 369)
        Me.grd.TabIndex = 4
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'frmTofasIrsaliye
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.ClientSize = New System.Drawing.Size(1005, 475)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmTofasIrsaliye"
        Me.Text = "Edi Ýrsaliye"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnGoster As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnAktar As Janus.Windows.EditControls.UIButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtIrsNo As System.Windows.Forms.TextBox
    Friend WithEvents btnSabit As Janus.Windows.EditControls.UIButton
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents chkWebSrvFrm As System.Windows.Forms.CheckBox

End Class
