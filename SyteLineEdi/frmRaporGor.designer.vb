<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class frmRaporGor
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Friend  WithEvents btnAc As System.Windows.Forms.Button
    Friend  WithEvents btnListele As System.Windows.Forms.Button
    Friend  WithEvents btnSil As System.Windows.Forms.Button
    Friend  WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend  WithEvents fldDizin As System.Windows.Forms.FolderBrowserDialog
    Friend  WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend  WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend  WithEvents Label1 As System.Windows.Forms.Label
    Friend  WithEvents lstListe As System.Windows.Forms.ListBox
    Friend  WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend  WithEvents reportDocument1 As CrystalDecisions.CrystalReports.Engine.ReportDocument
    Friend  WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend  WithEvents txtDosyaYol As System.Windows.Forms.TextBox

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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRaporGor))
        Me.txtDosyaYol = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnAc = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btnSil = New System.Windows.Forms.Button
        Me.btnListele = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.fldDizin = New System.Windows.Forms.FolderBrowserDialog
        Me.reportDocument1 = New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lstListe = New System.Windows.Forms.ListBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtDosyaYol
        '
        Me.txtDosyaYol.Location = New System.Drawing.Point(6, 34)
        Me.txtDosyaYol.Name = "txtDosyaYol"
        Me.txtDosyaYol.Size = New System.Drawing.Size(168, 20)
        Me.txtDosyaYol.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Dizin Yolu"
        '
        'btnAc
        '
        Me.btnAc.Location = New System.Drawing.Point(180, 34)
        Me.btnAc.Name = "btnAc"
        Me.btnAc.Size = New System.Drawing.Size(40, 21)
        Me.btnAc.TabIndex = 2
        Me.btnAc.Text = "Aç"
        Me.btnAc.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.btnListele)
        Me.GroupBox1.Controls.Add(Me.txtDosyaYol)
        Me.GroupBox1.Controls.Add(Me.btnAc)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnSil)
        Me.GroupBox1.Controls.Add(Me.lstListe)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(254, 662)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(180, 62)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(39, 22)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'btnSil
        '
        Me.btnSil.Location = New System.Drawing.Point(9, 61)
        Me.btnSil.Name = "btnSil"
        Me.btnSil.Size = New System.Drawing.Size(64, 23)
        Me.btnSil.TabIndex = 4
        Me.btnSil.Text = "Temizle"
        Me.btnSil.UseVisualStyleBackColor = True
        '
        'btnListele
        '
        Me.btnListele.Location = New System.Drawing.Point(79, 61)
        Me.btnListele.Name = "btnListele"
        Me.btnListele.Size = New System.Drawing.Size(95, 23)
        Me.btnListele.TabIndex = 3
        Me.btnListele.Text = "Listele"
        Me.btnListele.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.CrystalReportViewer1)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(254, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(697, 662)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.DisplayGroupTree = False
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(3, 16)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.SelectionFormula = ""
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(691, 643)
        Me.CrystalReportViewer1.TabIndex = 0
        Me.CrystalReportViewer1.ViewTimeSelectionFormula = ""
        '
        'lstListe
        '
        Me.lstListe.FormattingEnabled = True
        Me.lstListe.Location = New System.Drawing.Point(9, 96)
        Me.lstListe.Name = "lstListe"
        Me.lstListe.Size = New System.Drawing.Size(220, 550)
        Me.lstListe.Sorted = True
        Me.lstListe.TabIndex = 6
        '
        'frmRaporGor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(951, 662)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmRaporGor"
        Me.Text = "Rapor Görüntüleme"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    #End Region 'Methods

End Class