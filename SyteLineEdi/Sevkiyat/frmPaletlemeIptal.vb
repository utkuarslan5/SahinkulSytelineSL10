Public Class frmPaletlemeIptal
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Public Sorgu As String

    Friend  WithEvents btnKapat As System.Windows.Forms.Button
    Friend  WithEvents btnPaletIptal As System.Windows.Forms.Button
    Friend  WithEvents lblPaletNo As System.Windows.Forms.Label
    Friend  WithEvents Panel1 As System.Windows.Forms.Panel
    Friend  WithEvents txtPickNo As System.Windows.Forms.TextBox

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbcore As New Core.Data(My.Settings.ConnectionString)
    Private ds As DataSet
    Dim dt As New DataTable
    Private etkBilg As EtiketBilgisi
    Private palet As Boolean
    Private Qty As Decimal
    Private QtyMax As Integer = 0
    Dim RetVal As New ReturnValue
    Private sL3p As String = ""
    Private Surum As String = "1.0.0.1"

    #End Region 'Fields

    #Region "Constructors"

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub

    #End Region 'Constructors

    #Region "Methods"

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        MyBase.Dispose(disposing)
    End Sub

    '
    Private Sub btnCreatePalet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaletIptal.Click
        '
        Try

            Cursor.Current = Cursors.WaitCursor

            If MessageBox.Show(txtPickNo.Text & " nolu Paleti Ýptal Edilecek Onaylýyor musunuz?", "Ekip Mapics", MessageBoxButtons.YesNo) = MsgBoxResult.No Then

                txtPickNo.Text = ""

                Exit Sub

            End If

            If Not CekmeKontrol(txtPickNo.Text) Then

                txtPickNo.Text = ""

                Exit Sub

            End If

            Sorgu = " Update ETIKETDTY" & _
                        " Set PLTNO=0" & _
                        " Where Pickno=" & txtPickNo.Text

            db.RunSql(Sorgu, True)

            Sorgu = " Delete From ETIKETDTY" & _
                        " Where PickNo=" & txtPickNo.Text & _
                        " And PLTETK=1"

            db.RunSql(Sorgu, True)

            MessageBox.Show(txtPickNo.Text & " nolu Çekmenin Paletlemesi Ýptal Edilmiþtir", "Ekip Mapics", MessageBoxButtons.OK)

            txtPickNo.Text = ""

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Cursor.Current = Cursors.Default

        End Try
    End Sub

    Private Sub btnKapat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKapat.Click
        '
        Me.Close()
        ''
    End Sub

    Private Function CekmeKontrol(ByVal Pickno As String) As Boolean
        Try

            Sorgu = "Select Max(Durum) As Durum" & _
                        " From ETIKETDTY" & _
                        " Where Pickno =" & sTirnakEkle(Pickno)

            dt = dbcore.RunSql(Sorgu)

            If Not dt Is Nothing AndAlso _
                dt.Rows.Count > 0 Then

                If dt.Rows(0).Item("Durum").ToString = "3" Then

                    MessageBox.Show(Pickno & " nolu Çekme Sevk Edilmiþ . Paletleme Ýptal Edilemez...", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    CekmeKontrol = False

                Else

                    CekmeKontrol = True

                End If

            Else

                MessageBox.Show(Pickno & " nolu Çekme Sistemde Tanýmlý Deðil...", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                CekmeKontrol = False

            End If

            Return CekmeKontrol

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Function

    '
    Private Sub frmSevkiyat_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor.Current = Cursors.WaitCursor

        '
        Me.Text = Me.Text & " "

        Cursor.Current = Cursors.Default
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPaletlemeIptal))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtPickNo = New System.Windows.Forms.TextBox
        Me.lblPaletNo = New System.Windows.Forms.Label
        Me.btnPaletIptal = New System.Windows.Forms.Button
        Me.btnKapat = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtPickNo)
        Me.Panel1.Controls.Add(Me.lblPaletNo)
        Me.Panel1.Controls.Add(Me.btnPaletIptal)
        Me.Panel1.Controls.Add(Me.btnKapat)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(240, 69)
        Me.Panel1.TabIndex = 1
        '
        'txtPickNo
        '
        Me.txtPickNo.Location = New System.Drawing.Point(80, 32)
        Me.txtPickNo.Name = "txtPickNo"
        Me.txtPickNo.Size = New System.Drawing.Size(73, 20)
        Me.txtPickNo.TabIndex = 2
        '
        'lblPaletNo
        '
        Me.lblPaletNo.Location = New System.Drawing.Point(5, 32)
        Me.lblPaletNo.Name = "lblPaletNo"
        Me.lblPaletNo.Size = New System.Drawing.Size(69, 20)
        Me.lblPaletNo.TabIndex = 3
        Me.lblPaletNo.Text = "Çekme No"
        '
        'btnPaletIptal
        '
        Me.btnPaletIptal.Location = New System.Drawing.Point(154, 8)
        Me.btnPaletIptal.Name = "btnPaletIptal"
        Me.btnPaletIptal.Size = New System.Drawing.Size(84, 30)
        Me.btnPaletIptal.TabIndex = 6
        Me.btnPaletIptal.Text = "Paletleme Ýptal"
        '
        'btnKapat
        '
        Me.btnKapat.Location = New System.Drawing.Point(154, 37)
        Me.btnKapat.Name = "btnKapat"
        Me.btnKapat.Size = New System.Drawing.Size(84, 32)
        Me.btnKapat.TabIndex = 7
        Me.btnKapat.Text = "Kapat"
        '
        'frmPaletlemeIptal
        '
        Me.ClientSize = New System.Drawing.Size(240, 76)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(248, 110)
        Me.Name = "frmPaletlemeIptal"
        Me.Text = "Paletleme Ýptali"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    #End Region 'Methods

End Class