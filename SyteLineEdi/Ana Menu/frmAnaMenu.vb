'****************************************************
'23.09.2025  16:04
'Rümeyda GÖNÜL
'v.9.19
'****************************************************

Imports System.IO
Imports System.Windows.Forms

Public Class frmAnaMenu

#Region "Fields"

    Public frm As Form

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dt As New DataTable
    Private m_ChildFormNumber As Integer = 0
    Dim str As String

#End Region 'Fields

#Region "Methods"

    Public Sub formac()
        frm.MdiParent = Me
        frm.Show()
        frm.WindowState = FormWindowState.Maximized
        frm.Activate()
    End Sub

    Public Sub FormKapa()
        If Not Me.ActiveMdiChild Is Nothing Then

            For Each _frm As Form In My.Application.OpenForms

                If _frm.IsMdiChild Then

                    _frm.Close()

                End If
            Next

        End If
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub ButtonBar1_ItemClick(ByVal sender As System.Object, ByVal e As Janus.Windows.ButtonBar.ItemEventArgs)
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Global.System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub ExplorerBar1_ItemClick(ByVal sender As System.Object, ByVal e As Janus.Windows.ExplorerBar.ItemEventArgs) Handles ExplorerBar1.ItemClick
        Try

            If Not frm Is Nothing Then

                frm.Close()

                frm = Nothing

            End If

            FormKapa()

            Select Case e.Item.Group.Index
                ''''uygulamalar
                Case 0

                    Select Case e.Item.Index

                        Case 0

                            frm = frmEdiAktarimIslemi

                        Case 1

                            frm = frmEdiSyteLineAktarimIslemi

                        Case 2

                            frm = frmAsnGonder

                        Case 3

                            frm = frmTofasIrsaliye

                        Case 4

                            frm = frmSayimHareketleri
                        Case 5

                            frm = frmSatinalmaSiparisMektubu

                        Case 6
                            frm = FAna

                        Case 7
                            frm = frmKonsinye

                        Case 8

                            frm = frmKonsinyeSatis
                    End Select
                Case 1

                    Select Case e.Item.Index
                        Case 0

                            frm = frmCekmeListesi 'frmCekmeListesi

                        Case 1

                            frm = frmCLYenidenBasim

                        Case 2

                            frm = frmAmbalajIsemriOlustur

                        Case 3

                            frm = frmSatinAlmaGirisEtiketi

                        Case 4

                            frm = frmIsEmriEtiketi
                    End Select
                Case 2

                    Select Case e.Item.Index

                        Case 0

                            frm = frmIrsaliye

                        Case 1

                            frm = frmIrsaliyeIptal

                        Case 2

                            frm = frmFatura

                        Case 3

                            frm = frmFaturaYazdirma

                        Case 4

                            frm = frmSatinAlmaIadeIrsaliyesi

                        Case 5

                            frm = frmSatinAlmaIadeFaturasi

                        Case 6

                            frm = frmNakilIrsaliyesi

                        Case 7

                            frm = frmKutuEtiketBasim

                        Case 8

                            frm = frmSerbestEtiketBasimi

                        Case 9

                            frm = frmFiyatFarkiFaturasi

                        Case 10

                            frm = frmFaturaBirimFiyatGuncelleme

                        Case 11

                            frm = frmFaturaGuncelleme

                        Case 12

                            frm = frmKismiFatura
                    End Select
                Case 3

                    Select Case e.Item.Index

                        Case 0

                            frm = frmIhracEvraklari

                        Case 1

                            frm = frmGumrukBildirimi

                        Case 2

                            frm = frmDeliveryNoteBasimi

                        Case 3
                            'Dim startInfo As System.Diagnostics.ProcessStartInfo
                            'Dim pStart As New System.Diagnostics.Process

                            'startInfo = New System.Diagnostics.ProcessStartInfo(System.Windows.Forms.Application.StartupPath & "\RaporGoruntule\RptGoruntule.exe")

                            'pStart.StartInfo = startInfo
                            'pStart.Start()
                            ''pStart.WaitForExit() 'Your code will halt until the exe file has executed.
                            'Exit Sub
                            frm = frmRaporGor
                    End Select

                Case 4

                    Select Case e.Item.Index

                        Case 0

                            frm = frmSevkiyat

                        Case 1

                            frm = frmPaletlemeIptal

                        Case 2
                            frm = frmSevkiyatOnay
                    End Select

                Case 5

                    Select Case e.Item.Index

                        Case 0

                            frm = frmMiktarIlerlet

                        Case 1

                            frm = frmUretimBildirimi

                        Case 2

                            frm = frmFasonUretimBildirimi
                    End Select

                Case 6

                    Select Case e.Item.Index

                        Case 0

                            frm = frmTakvimGirisi

                        Case 1

                            frm = frmTatilGunleri

                        Case 2

                            frm = frmEdiPrmGiris
                    End Select

                Case 7

                    Select Case e.Item.Index

                        Case 0

                            frm = frmKullanicilar

                        Case 1

                            frm = frmProgramEkle

                        Case 2

                            frm = frmYetkilendirme
                    End Select

            End Select

            If Not frm Is Nothing Then

                formac()

            End If

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub frmAnaMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Me.Cursor = Cursors.WaitCursor

            BolgeselAyar()

            For i As Integer = 0 To ExplorerBar1.Groups.Count - 1

                ExplorerBar1.Groups(i).Visible = False

                For j As Integer = 0 To ExplorerBar1.Groups(i).Items.Count - 1

                    str = ""
                    str = " select * " & _
                            " from EDIYETKI " & _
                             " where YetkiID=" & (ExplorerBar1.Groups(i).Items(j).Key) & _
                             " and KullaniciId=" & KullaniciId

                    dt = db.RunSql(str)

                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        ExplorerBar1.Groups(i).Visible = True
                        ExplorerBar1.Groups(i).Items(j).Visible = True
                    Else
                        ExplorerBar1.Groups(i).Items(j).Visible = False
                    End If

                Next j

            Next i

            'Me.Text = Me.Text & "  Kullanici:" & KullaniciAdi.Trim & "  " & Application.ProductVersion

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    Private Sub mdifrmMenu_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        End
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub

    'Test
    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)
        ' Create a new instance of the child form.
        Dim ChildForm As New System.Windows.Forms.Form
        ' Make it a child of this MDI form before showing it.
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1

        ChildForm.Text = "Window " & m_ChildFormNumber

        ChildForm.Show()
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub TileVerticleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

#End Region 'Methods

End Class