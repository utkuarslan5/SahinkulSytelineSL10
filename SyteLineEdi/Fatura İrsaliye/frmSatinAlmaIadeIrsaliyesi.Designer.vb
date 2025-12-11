<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSatinAlmaIadeIrsaliyesi
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
        Dim GridEXLayout1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSatinAlmaIadeIrsaliyesi))
        Dim UıStatusBarPanel1 As Janus.Windows.UI.StatusBar.UIStatusBarPanel = New Janus.Windows.UI.StatusBar.UIStatusBarPanel
        Dim UıStatusBarPanel2 As Janus.Windows.UI.StatusBar.UIStatusBarPanel = New Janus.Windows.UI.StatusBar.UIStatusBarPanel
        Dim GridEXLayout2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.txtSatici = New Janus.Windows.GridEX.EditControls.EditBox
        Me.txtSaticiTanim = New Janus.Windows.GridEX.EditControls.EditBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkYenidenBasim = New System.Windows.Forms.CheckBox
        Me.btnYazdir = New Janus.Windows.EditControls.UIButton
        Me.btnSorgula = New Janus.Windows.EditControls.UIButton
        Me.txtIrsNum = New Janus.Windows.GridEX.EditControls.EditBox
        Me.cmdDocNum = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.dtIrsDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.stBar = New Janus.Windows.UI.StatusBar.UIStatusBar
        Me.grdMain = New Janus.Windows.GridEX.GridEX
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtSatici
        '
        Me.txtSatici.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtSatici.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtSatici.BackColor = System.Drawing.Color.Yellow
        Me.txtSatici.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis
        Me.txtSatici.Location = New System.Drawing.Point(104, 17)
        Me.txtSatici.Name = "txtSatici"
        Me.txtSatici.Size = New System.Drawing.Size(108, 20)
        Me.txtSatici.TabIndex = 0
        Me.txtSatici.Tag = "1"
        Me.txtSatici.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtSatici.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'txtSaticiTanim
        '
        Me.txtSaticiTanim.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtSaticiTanim.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtSaticiTanim.Enabled = False
        Me.txtSaticiTanim.Location = New System.Drawing.Point(104, 43)
        Me.txtSaticiTanim.Name = "txtSaticiTanim"
        Me.txtSaticiTanim.Size = New System.Drawing.Size(222, 20)
        Me.txtSaticiTanim.TabIndex = 8
        Me.txtSaticiTanim.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtSaticiTanim.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkYenidenBasim)
        Me.GroupBox1.Controls.Add(Me.txtSatici)
        Me.GroupBox1.Controls.Add(Me.txtSaticiTanim)
        Me.GroupBox1.Controls.Add(Me.btnYazdir)
        Me.GroupBox1.Controls.Add(Me.btnSorgula)
        Me.GroupBox1.Controls.Add(Me.txtIrsNum)
        Me.GroupBox1.Controls.Add(Me.cmdDocNum)
        Me.GroupBox1.Controls.Add(Me.dtIrsDate)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1009, 171)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'chkYenidenBasim
        '
        Me.chkYenidenBasim.AutoSize = True
        Me.chkYenidenBasim.Location = New System.Drawing.Point(228, 132)
        Me.chkYenidenBasim.Name = "chkYenidenBasim"
        Me.chkYenidenBasim.Size = New System.Drawing.Size(96, 17)
        Me.chkYenidenBasim.TabIndex = 4
        Me.chkYenidenBasim.Text = "Yeniden Basım"
        Me.chkYenidenBasim.UseVisualStyleBackColor = True
        '
        'btnYazdir
        '
        Me.btnYazdir.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnYazdir.ImageSize = New System.Drawing.Size(60, 60)
        Me.btnYazdir.Location = New System.Drawing.Point(529, 17)
        Me.btnYazdir.Name = "btnYazdir"
        Me.btnYazdir.Size = New System.Drawing.Size(126, 47)
        Me.btnYazdir.TabIndex = 6
        Me.btnYazdir.Text = "&Yazdir"
        Me.btnYazdir.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnSorgula
        '
        Me.btnSorgula.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnSorgula.ImageSize = New System.Drawing.Size(32, 32)
        Me.btnSorgula.Location = New System.Drawing.Point(369, 17)
        Me.btnSorgula.Name = "btnSorgula"
        Me.btnSorgula.Size = New System.Drawing.Size(126, 47)
        Me.btnSorgula.TabIndex = 5
        Me.btnSorgula.Text = "&Sorgula"
        Me.btnSorgula.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'txtIrsNum
        '
        Me.txtIrsNum.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.txtIrsNum.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.txtIrsNum.BackColor = System.Drawing.Color.Yellow
        Me.txtIrsNum.Location = New System.Drawing.Point(104, 75)
        Me.txtIrsNum.Name = "txtIrsNum"
        Me.txtIrsNum.Size = New System.Drawing.Size(108, 20)
        Me.txtIrsNum.TabIndex = 1
        Me.txtIrsNum.Tag = "1"
        Me.txtIrsNum.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.txtIrsNum.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'cmdDocNum
        '
        Me.cmdDocNum.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.cmdDocNum.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        GridEXLayout1.LayoutString = resources.GetString("GridEXLayout1.LayoutString")
        Me.cmdDocNum.DesignTimeLayout = GridEXLayout1
        Me.cmdDocNum.DisplayMember = "document_num"
        Me.cmdDocNum.Location = New System.Drawing.Point(228, 75)
        Me.cmdDocNum.Name = "cmdDocNum"
        Me.cmdDocNum.Size = New System.Drawing.Size(108, 20)
        Me.cmdDocNum.TabIndex = 2
        Me.cmdDocNum.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.cmdDocNum.ValueMember = "document_num"
        Me.cmdDocNum.Visible = False
        Me.cmdDocNum.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'dtIrsDate
        '
        Me.dtIrsDate.BackColor = System.Drawing.Color.Yellow
        '
        '
        '
        Me.dtIrsDate.DropDownCalendar.Location = New System.Drawing.Point(0, 0)
        Me.dtIrsDate.DropDownCalendar.Name = ""
        Me.dtIrsDate.DropDownCalendar.Size = New System.Drawing.Size(170, 173)
        Me.dtIrsDate.DropDownCalendar.TabIndex = 0
        Me.dtIrsDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        Me.dtIrsDate.EditStyle = Janus.Windows.CalendarCombo.EditStyle.Free
        Me.dtIrsDate.Location = New System.Drawing.Point(104, 127)
        Me.dtIrsDate.Name = "dtIrsDate"
        Me.dtIrsDate.NullButtonText = "Yok"
        Me.dtIrsDate.Size = New System.Drawing.Size(108, 20)
        Me.dtIrsDate.TabIndex = 3
        Me.dtIrsDate.Tag = "1"
        Me.dtIrsDate.TodayButtonText = "Bugün"
        Me.dtIrsDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(6, 79)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(92, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "İrsaliye Numarası :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(12, 21)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Satıcı :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(6, 132)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "İrsaliye Tarihi :"
        '
        'stBar
        '
        Me.stBar.Location = New System.Drawing.Point(0, 470)
        Me.stBar.Name = "stBar"
        UıStatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        UıStatusBarPanel1.BorderColor = System.Drawing.Color.Empty
        UıStatusBarPanel1.Key = "Pn1"
        UıStatusBarPanel1.ProgressBarValue = 0
        UıStatusBarPanel1.Width = 745
        UıStatusBarPanel2.BorderColor = System.Drawing.Color.Empty
        UıStatusBarPanel2.Key = ""
        UıStatusBarPanel2.ProgressBarValue = 0
        Me.stBar.Panels.AddRange(New Janus.Windows.UI.StatusBar.UIStatusBarPanel() {UıStatusBarPanel1, UıStatusBarPanel2})
        Me.stBar.PanelsBorderColor = System.Drawing.SystemColors.ControlDark
        Me.stBar.Size = New System.Drawing.Size(1009, 23)
        Me.stBar.TabIndex = 5
        '
        'grdMain
        '
        Me.grdMain.AlternatingColors = True
        Me.grdMain.BuiltInTextsData = "<LocalizableData ID=""LocalizableStrings"" Collection=""true""><GroupByBoxInfo>Grupla" & _
            "mak istediğiniz kolonu buraya taşıyın</GroupByBoxInfo></LocalizableData>"
        Me.grdMain.ColumnAutoResize = True
        GridEXLayout2.LayoutString = resources.GetString("GridEXLayout2.LayoutString")
        Me.grdMain.DesignTimeLayout = GridEXLayout2
        Me.grdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdMain.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grdMain.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdMain.GroupByBoxVisible = False
        Me.grdMain.Location = New System.Drawing.Point(0, 171)
        Me.grdMain.Name = "grdMain"
        Me.grdMain.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.grdMain.Size = New System.Drawing.Size(1009, 299)
        Me.grdMain.TabIndex = 4
        Me.grdMain.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'frmSatinAlmaIadeIrsaliyesi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1009, 493)
        Me.Controls.Add(Me.grdMain)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.stBar)
        Me.Name = "frmSatinAlmaIadeIrsaliyesi"
        Me.Text = "Satinalma İade İrsaliyesi"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtSatici As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents txtSaticiTanim As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnYazdir As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnSorgula As Janus.Windows.EditControls.UIButton
    Friend WithEvents txtIrsNum As Janus.Windows.GridEX.EditControls.EditBox
    Friend WithEvents cmdDocNum As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents dtIrsDate As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents stBar As Janus.Windows.UI.StatusBar.UIStatusBar
    Friend WithEvents grdMain As Janus.Windows.GridEX.GridEX
    Friend WithEvents chkYenidenBasim As System.Windows.Forms.CheckBox

End Class
