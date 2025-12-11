Imports System.Windows.Forms

Public Class mdifrmMenu

    #Region "Fields"

    Public frm As Form

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dt As New DataTable
    Private m_ChildFormNumber As Integer = 0
    Dim str As String

    #End Region 'Fields

    #Region "Methods"

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

    Private Sub ButtonBar1_ItemClick(ByVal sender As System.Object, ByVal e As Janus.Windows.ButtonBar.ItemEventArgs) Handles ButtonBar1.ItemClick
        Try

            If Not frm Is Nothing Then

                frm.Close()

                frm = Nothing

            End If

            If e.Item.Key <> "" Then

                FormKapa()

            End If

            Select Case e.Item.Group.Key

                Case "Group1"

                    Select Case e.Item.Key

                        'Case "Item2"

                        '    frmAmbalaj.MdiParent = Me

                        '    frmAmbalaj.Show(False)

                        'Case "Item4"

                        '    frmMusteriRef.MdiParent = Me

                        '    frmMusteriRef.Show(False)

                        Case "Item5"

                            frm = frmTakvimGirisi

                    End Select

                Case "Group2"

                    Select Case e.Item.Key

                        Case "Item1"

                            frm = frmEdiAktarimIslemi

                        Case "Item2"

                            frm = frmEdiSyteLineAktarimIslemi

                        Case "Item3"

                            frm = frmAsnGonder

                    End Select

                Case "Group4"

                    Select Case e.Item.Key

                        Case "Item1"

                            frm = frmKullanicilar

                        Case "Item2"

                            frm = frmProgramEkle

                        Case "Item3"

                            frm = frmYetkilendirme

                    End Select

                Case "Group5"

                    Select Case e.Item.Key

                        Case "Item1"

                            frm = frmCekmeListesi 'frmCekmeListesi

                        Case "Item2"

                            frm = frmCLYenidenBasim

                        Case "Item3"

                            frm = frmKutuEtiketBasim

                        Case "Item4"

                            frm = frmAmbalajIsemriOlustur

                    End Select

                Case "Group6"

                    Select Case e.Item.Key

                        Case "Item1"

                            frm = frmIrsaliye

                        Case "Item2"

                            frm = frmIrsaliyeIptal

                        Case "Item3"

                            frm = frmFatura

                        Case "Item4"

                            frm = frmFaturaYazdirma

                    End Select

                Case "Group7"

                    Select Case e.Item.Key

                        Case "Item1"

                            frm = frmIhracEvraklari

                        Case "Item2"

                            frm = frmGumrukBildirimi

                    End Select

            End Select

            If Not frm Is Nothing Then

                frm.MdiParent = Me

                frm.WindowState = FormWindowState.Maximized

                frm.Show()

            End If

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
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

    Private Sub mdifrmMenu_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        End
    End Sub

    'Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    '    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
    'End Sub
    Private Sub mdifrmMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor

            For i As Integer = 0 To ButtonBar1.Groups.Count - 1
                For j As Integer = 0 To ButtonBar1.Groups(i).Items.Count - 1

                    Str = ""
                    str = " select * " & _
                            " from EDIYETKI " & _
                             " where YetkiID=" & IIf(ButtonBar1.Groups(i).Items(j).TagString = "", 0, ButtonBar1.Groups(i).Items(j).TagString) & _
                             " and KullaniciId=" & KullaniciId
                    dt = db.RunSql(Str)

                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        ButtonBar1.Groups(i).Items(j).Enabled = True
                    Else
                        ButtonBar1.Groups(i).Items(j).Enabled = False
                    End If
                Next
            Next
        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            Me.Cursor = Cursors.Arrow
        End Try
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