<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTatilGunleri
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
        Dim GridEXLayout3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTatilGunleri))
        Me.dtmTatilGunu = New System.Windows.Forms.MonthCalendar
        Me.btnTatilGunuEkle = New Janus.Windows.EditControls.UIButton
        Me.btnTatilGunuSil = New Janus.Windows.EditControls.UIButton
        Me.btnTatilGunuIsle = New Janus.Windows.EditControls.UIButton
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtmTatilGunu
        '
        Me.dtmTatilGunu.Location = New System.Drawing.Point(97, 18)
        Me.dtmTatilGunu.Name = "dtmTatilGunu"
        Me.dtmTatilGunu.TabIndex = 0
        Me.dtmTatilGunu.TodayDate = New Date(2009, 8, 11, 0, 0, 0, 0)
        '
        'btnTatilGunuEkle
        '
        Me.btnTatilGunuEkle.Location = New System.Drawing.Point(108, 185)
        Me.btnTatilGunuEkle.Name = "btnTatilGunuEkle"
        Me.btnTatilGunuEkle.Size = New System.Drawing.Size(136, 23)
        Me.btnTatilGunuEkle.TabIndex = 2
        Me.btnTatilGunuEkle.Text = "Tatil Günü Olarak Ekle"
        '
        'btnTatilGunuSil
        '
        Me.btnTatilGunuSil.Location = New System.Drawing.Point(108, 387)
        Me.btnTatilGunuSil.Name = "btnTatilGunuSil"
        Me.btnTatilGunuSil.Size = New System.Drawing.Size(136, 23)
        Me.btnTatilGunuSil.TabIndex = 3
        Me.btnTatilGunuSil.Text = "Tatil Gününü Sil"
        '
        'btnTatilGunuIsle
        '
        Me.btnTatilGunuIsle.Location = New System.Drawing.Point(108, 416)
        Me.btnTatilGunuIsle.Name = "btnTatilGunuIsle"
        Me.btnTatilGunuIsle.Size = New System.Drawing.Size(136, 23)
        Me.btnTatilGunuIsle.TabIndex = 4
        Me.btnTatilGunuIsle.Text = "Tatil Günlerini İşle"
        '
        'GridEX1
        '
        GridEXLayout3.LayoutString = resources.GetString("GridEXLayout3.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEXLayout3
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.Location = New System.Drawing.Point(97, 222)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(164, 159)
        Me.GridEX1.TabIndex = 5
        '
        'frmTatilGunleri
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(393, 497)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.btnTatilGunuIsle)
        Me.Controls.Add(Me.btnTatilGunuSil)
        Me.Controls.Add(Me.btnTatilGunuEkle)
        Me.Controls.Add(Me.dtmTatilGunu)
        Me.Name = "frmTatilGunleri"
        Me.Text = "Tatil Günleri"
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtmTatilGunu As System.Windows.Forms.MonthCalendar
    Friend WithEvents btnTatilGunuEkle As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnTatilGunuSil As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnTatilGunuIsle As Janus.Windows.EditControls.UIButton
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
End Class
