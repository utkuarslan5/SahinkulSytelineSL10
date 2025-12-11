<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class frmDeliveryNoteBasimi
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Friend  WithEvents btnSorgula As Janus.Windows.EditControls.UIButton
    Friend  WithEvents btnYazdir As Janus.Windows.EditControls.UIButton
    Friend  WithEvents cbYenidenBasim As System.Windows.Forms.CheckBox
    Friend  WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend  WithEvents EkleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend  WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend  WithEvents Label1 As System.Windows.Forms.Label
    Friend  WithEvents Label2 As System.Windows.Forms.Label
    Friend  WithEvents Label3 As System.Windows.Forms.Label
    Friend  WithEvents Label4 As System.Windows.Forms.Label
    Friend  WithEvents SilToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend  WithEvents SilToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend  WithEvents txtIrsaliyeNo1 As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend  WithEvents txtIrsaliyeNo2 As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend  WithEvents txtMusteriNo As Janus.Windows.GridEX.EditControls.EditBox
    Friend  WithEvents txtTanim As Janus.Windows.GridEX.EditControls.EditBox
    Friend  WithEvents UıGroupBox1 As Janus.Windows.EditControls.UIGroupBox

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
        Me.components = New System.ComponentModel.Container
        Dim GridEXLayout1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDeliveryNoteBasimi))
        Me.btnYazdir = New Janus.Windows.EditControls.UIButton
        Me.btnSorgula = New Janus.Windows.EditControls.UIButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EkleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SilToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SilToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.txtMusteriNo = New Janus.Windows.GridEX.EditControls.EditBox
        Me.txtTanim = New Janus.Windows.GridEX.EditControls.EditBox
        Me.UıGroupBox1 = New Janus.Windows.EditControls.UIGroupBox
        Me.cbYenidenBasim = New System.Windows.Forms.CheckBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtIrsaliyeNo2 = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.txtIrsaliyeNo1 = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.UıGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UıGroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnYazdir
        '
        Me.btnYazdir.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnYazdir.Location = New System.Drawing.Point(682, 14)
        Me.btnYazdir.Name = "btnYazdir"
        Me.btnYazdir.Size = New System.Drawing.Size(116, 43)
        Me.btnYazdir.TabIndex = 25
        Me.btnYazdir.Text = "Yazdır"
        Me.btnYazdir.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnSorgula
        '
        Me.btnSorgula.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnSorgula.Location = New System.Drawing.Point(549, 14)
        Me.btnSorgula.Name = "btnSorgula"
        Me.btnSorgula.Size = New System.Drawing.Size(116, 43)
        Me.btnSorgula.TabIndex = 24
        Me.btnSorgula.Text = "Sorgula"
        Me.btnSorgula.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(220, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Tanımı...:"
        '
        'GridEX1
        '
        Me.GridEX1.BackColor = System.Drawing.Color.White
        Me.GridEX1.ContextMenuStrip = Me.ContextMenuStrip1
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEXLayout1
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.Location = New System.Drawing.Point(0, 99)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(969, 392)
        Me.GridEX1.TabIndex = 29
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EkleToolStripMenuItem, Me.SilToolStripMenuItem, Me.SilToolStripMenuItem1})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(122, 70)
        '
        'EkleToolStripMenuItem
        '
        Me.EkleToolStripMenuItem.Name = "EkleToolStripMenuItem"
        Me.EkleToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
        Me.EkleToolStripMenuItem.Text = "Ekle"
        '
        'SilToolStripMenuItem
        '
        Me.SilToolStripMenuItem.Name = "SilToolStripMenuItem"
        Me.SilToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
        Me.SilToolStripMenuItem.Text = "Değiştir"
        '
        'SilToolStripMenuItem1
        '
        Me.SilToolStripMenuItem1.Name = "SilToolStripMenuItem1"
        Me.SilToolStripMenuItem1.Size = New System.Drawing.Size(121, 22)
        Me.SilToolStripMenuItem1.Text = "Sil"
        '
        'txtMusteriNo
        '
        Me.txtMusteriNo.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtMusteriNo.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtMusteriNo.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtMusteriNo.Location = New System.Drawing.Point(100, 14)
        Me.txtMusteriNo.Name = "txtMusteriNo"
        Me.txtMusteriNo.Size = New System.Drawing.Size(107, 20)
        Me.txtMusteriNo.TabIndex = 9
        Me.txtMusteriNo.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtTanim
        '
        Me.txtTanim.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtTanim.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtTanim.Location = New System.Drawing.Point(311, 14)
        Me.txtTanim.Name = "txtTanim"
        Me.txtTanim.Size = New System.Drawing.Size(207, 20)
        Me.txtTanim.TabIndex = 2
        Me.txtTanim.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'UıGroupBox1
        '
        Me.UıGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(175, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.UıGroupBox1.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.UıGroupBox1.Controls.Add(Me.cbYenidenBasim)
        Me.UıGroupBox1.Controls.Add(Me.Label3)
        Me.UıGroupBox1.Controls.Add(Me.Label4)
        Me.UıGroupBox1.Controls.Add(Me.txtIrsaliyeNo2)
        Me.UıGroupBox1.Controls.Add(Me.txtIrsaliyeNo1)
        Me.UıGroupBox1.Controls.Add(Me.btnYazdir)
        Me.UıGroupBox1.Controls.Add(Me.btnSorgula)
        Me.UıGroupBox1.Controls.Add(Me.txtMusteriNo)
        Me.UıGroupBox1.Controls.Add(Me.Label2)
        Me.UıGroupBox1.Controls.Add(Me.txtTanim)
        Me.UıGroupBox1.Controls.Add(Me.Label1)
        Me.UıGroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UıGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.UıGroupBox1.Name = "UıGroupBox1"
        Me.UıGroupBox1.Size = New System.Drawing.Size(969, 99)
        Me.UıGroupBox1.TabIndex = 31
        '
        'cbYenidenBasim
        '
        Me.cbYenidenBasim.AutoSize = True
        Me.cbYenidenBasim.BackColor = System.Drawing.Color.Transparent
        Me.cbYenidenBasim.Location = New System.Drawing.Point(223, 76)
        Me.cbYenidenBasim.Name = "cbYenidenBasim"
        Me.cbYenidenBasim.Size = New System.Drawing.Size(96, 17)
        Me.cbYenidenBasim.TabIndex = 32
        Me.cbYenidenBasim.Text = "Yeniden Basım"
        Me.cbYenidenBasim.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(220, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "İrsaliye No ...:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(3, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "İrsaliye No ...:"
        '
        'txtIrsaliyeNo2
        '
        Me.txtIrsaliyeNo2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtIrsaliyeNo2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtIrsaliyeNo2.FormatString = "#"
        Me.txtIrsaliyeNo2.Location = New System.Drawing.Point(311, 41)
        Me.txtIrsaliyeNo2.Name = "txtIrsaliyeNo2"
        Me.txtIrsaliyeNo2.Size = New System.Drawing.Size(107, 20)
        Me.txtIrsaliyeNo2.TabIndex = 28
        Me.txtIrsaliyeNo2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'txtIrsaliyeNo1
        '
        Me.txtIrsaliyeNo1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtIrsaliyeNo1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtIrsaliyeNo1.FormatString = "#"
        Me.txtIrsaliyeNo1.Location = New System.Drawing.Point(100, 41)
        Me.txtIrsaliyeNo1.Name = "txtIrsaliyeNo1"
        Me.txtIrsaliyeNo1.Size = New System.Drawing.Size(107, 20)
        Me.txtIrsaliyeNo1.TabIndex = 27
        Me.txtIrsaliyeNo1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(3, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Müşteri"
        '
        'frmDeliveryNoteBasimi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(969, 491)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.UıGroupBox1)
        Me.Name = "frmDeliveryNoteBasimi"
        Me.Text = "Delivery Note Basımı"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.UıGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UıGroupBox1.ResumeLayout(False)
        Me.UıGroupBox1.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    #End Region 'Methods

End Class