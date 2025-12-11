<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class frmEdiAktarimIslemi
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Friend  WithEvents btnDizin As Janus.Windows.EditControls.UIButton
    Friend  WithEvents btnExit As Janus.Windows.EditControls.UIButton
    Friend  WithEvents btnMesajAl As Janus.Windows.EditControls.UIButton
    Friend  WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend  WithEvents grpAltBilgi As Janus.Windows.EditControls.UIGroupBox
    Friend  WithEvents grpUstBilgi As Janus.Windows.EditControls.UIGroupBox
    Friend  WithEvents lblPath As System.Windows.Forms.Label
    Friend  WithEvents openDir As System.Windows.Forms.FolderBrowserDialog
    Friend  WithEvents txtPath As Janus.Windows.GridEX.EditControls.EditBox

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    #End Region 'Fields

    #Region "Methods"

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough> _
    Private Sub InitializeComponent()
        Dim GridEXLayout2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEdiAktarimIslemi))
        Me.openDir = New System.Windows.Forms.FolderBrowserDialog
        Me.btnDizin = New Janus.Windows.EditControls.UIButton
        Me.txtPath = New Janus.Windows.GridEX.EditControls.EditBox
        Me.grpUstBilgi = New Janus.Windows.EditControls.UIGroupBox
        Me.lblPath = New System.Windows.Forms.Label
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.grpAltBilgi = New Janus.Windows.EditControls.UIGroupBox
        Me.btnExit = New Janus.Windows.EditControls.UIButton
        Me.btnMesajAl = New Janus.Windows.EditControls.UIButton
        CType(Me.grpUstBilgi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpUstBilgi.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpAltBilgi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAltBilgi.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnDizin
        '
        Me.btnDizin.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Ellipsis
        Me.btnDizin.Location = New System.Drawing.Point(485, 37)
        Me.btnDizin.Name = "btnDizin"
        Me.btnDizin.Size = New System.Drawing.Size(39, 20)
        Me.btnDizin.TabIndex = 0
        Me.btnDizin.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'txtPath
        '
        Me.txtPath.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtPath.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtPath.Location = New System.Drawing.Point(90, 37)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(389, 20)
        Me.txtPath.TabIndex = 1
        Me.txtPath.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '
        'grpUstBilgi
        '
        Me.grpUstBilgi.BackgroundImage = Global.SyteLineEdi.My.Resources.Resources.arkaalan
        Me.grpUstBilgi.Controls.Add(Me.lblPath)
        Me.grpUstBilgi.Controls.Add(Me.btnDizin)
        Me.grpUstBilgi.Controls.Add(Me.txtPath)
        Me.grpUstBilgi.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpUstBilgi.Location = New System.Drawing.Point(0, 0)
        Me.grpUstBilgi.Name = "grpUstBilgi"
        Me.grpUstBilgi.Size = New System.Drawing.Size(796, 95)
        Me.grpUstBilgi.TabIndex = 3
        '
        'lblPath
        '
        Me.lblPath.AutoSize = True
        Me.lblPath.BackColor = System.Drawing.Color.Transparent
        Me.lblPath.Location = New System.Drawing.Point(11, 41)
        Me.lblPath.Name = "lblPath"
        Me.lblPath.Size = New System.Drawing.Size(63, 13)
        Me.lblPath.TabIndex = 2
        Me.lblPath.Text = "Dizin..........:"
        '
        'GridEX1
        '
        Me.GridEX1.BuiltInTextsData = "<LocalizableData ID=""LocalizableStrings"" Collection=""true""><GroupByBoxInfo>Grupla" & _
            "mak istediðiniz alaný buraya sürükleyip býrakýn</GroupByBoxInfo></LocalizableDat" & _
            "a>"
        GridEXLayout2.LayoutString = resources.GetString("GridEXLayout2.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEXLayout2
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.Location = New System.Drawing.Point(0, 95)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(796, 358)
        Me.GridEX1.TabIndex = 4
        '
        'grpAltBilgi
        '
        Me.grpAltBilgi.Controls.Add(Me.btnExit)
        Me.grpAltBilgi.Controls.Add(Me.btnMesajAl)
        Me.grpAltBilgi.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grpAltBilgi.Location = New System.Drawing.Point(0, 453)
        Me.grpAltBilgi.Name = "grpAltBilgi"
        Me.grpAltBilgi.Size = New System.Drawing.Size(796, 92)
        Me.grpAltBilgi.TabIndex = 5
        '
        'btnExit
        '
        Me.btnExit.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnExit.Image = Global.SyteLineEdi.My.Resources.Resources._exit
        Me.btnExit.ImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Location = New System.Drawing.Point(618, 16)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(156, 64)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = "Çýkýþ"
        Me.btnExit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnMesajAl
        '
        Me.btnMesajAl.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnMesajAl.Image = Global.SyteLineEdi.My.Resources.Resources.email
        Me.btnMesajAl.ImageSize = New System.Drawing.Size(48, 48)
        Me.btnMesajAl.Location = New System.Drawing.Point(45, 16)
        Me.btnMesajAl.Name = "btnMesajAl"
        Me.btnMesajAl.Size = New System.Drawing.Size(156, 64)
        Me.btnMesajAl.TabIndex = 0
        Me.btnMesajAl.Text = "Mesaj Al"
        Me.btnMesajAl.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'frmEdiAktarimIslemi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(796, 545)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.grpUstBilgi)
        Me.Controls.Add(Me.grpAltBilgi)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmEdiAktarimIslemi"
        Me.Text = " Edi Aktarým Ýþlemi"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grpUstBilgi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpUstBilgi.ResumeLayout(False)
        Me.grpUstBilgi.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpAltBilgi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAltBilgi.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    #End Region 'Methods

End Class