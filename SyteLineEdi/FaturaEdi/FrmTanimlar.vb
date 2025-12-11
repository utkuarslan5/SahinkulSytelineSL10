Option Strict Off
Option Explicit On

Friend Class FTanimlar
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dt As New DataTable
    Dim dtTemp As New DataTable

    #End Region 'Fields

    #Region "Methods"

    Private Sub CancelButton_Renamed_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CancelButton_Renamed.Click
        Me.Close()
    End Sub

    Private Sub FTanimlar_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim LstItm As System.Windows.Forms.ListViewItem

        dt = db.RunSql("select A.Cust_Num, A.Name , A.curr_code " &
                        " from CustAddr A " &
                        " LEft Join Customer C On A.Cust_num = C.Cust_num And A.Cust_Seq=C.Cust_Seq" &
                        " where A.Cust_Seq=0" &
                        " Order by A.cust_num")

        If Not dt Is Nothing Then

            For Each row As DataRow In dt.Rows

                LstItm = New ListViewItem

                LstItm.Text = Trim(row.Item(0).ToString)

                If LstItm.SubItems.Count > 1 Then
                    LstItm.SubItems(1).Text = Trim(row.Item(1).ToString)
                Else
                    LstItm.SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, Trim(row.Item(1).ToString)))
                End If

                If LstItm.SubItems.Count > 2 Then
                    LstItm.SubItems(2).Text = Trim(row.Item(2).ToString)
                Else
                    LstItm.SubItems.Insert(2, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, Trim(row.Item(2).ToString)))
                End If

                ListView1.Items.Add(LstItm)

            Next row

        End If
    End Sub

    Private Sub listele(Optional ByRef sTarafNo As String = vbNullString)
        Dim LstItm As System.Windows.Forms.ListViewItem

        Dim sQuery As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        sQuery = "select A.Cust_Num, A.Name , A.curr_code " &
                        " from CustAddr A " &
                        " LEft Join Customer C On A.Cust_num = C.Cust_num And A.Cust_Seq=C.Cust_Seq" &
                        " where A.Cust_Seq=0"

        If sTarafNo <> vbNullString Then
            sQuery = sQuery & " and A.Cust_Num like '" & sTarafNo & "%' or A.Cust_Num like '" & UCase(sTarafNo) & "%'"
        End If
        sQuery = sQuery & " order by A.Cust_Num"

        dt = db.RunSql(sQuery)

        ListView1.Items.Clear()

        If Not dt Is Nothing Then

            For Each row As DataRow In dt.Rows

                LstItm = New ListViewItem

                LstItm.Text = Trim(row.Item(0).ToString)

                If LstItm.SubItems.Count > 1 Then
                    LstItm.SubItems(1).Text = Trim(row.Item(1).ToString)
                Else
                    LstItm.SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, Trim(row.Item(1).ToString)))
                End If

                If LstItm.SubItems.Count > 2 Then
                    LstItm.SubItems(2).Text = Trim(row.Item(2).ToString)
                Else
                    LstItm.SubItems.Insert(2, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, Trim(row.Item(2).ToString)))
                End If

                ListView1.Items.Add(LstItm)

            Next row

        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ListView1_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ListView1.DoubleClick
        OKButton_Click(OKButton, New System.EventArgs())
    End Sub

    Private Sub OKButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OKButton.Click
        If ListView1.FocusedItem.Text <> vbNullString Then

            FAna.sMusNoTum = ListView1.FocusedItem.Text

            FAna.Txt_Taraf.Text = ListView1.FocusedItem.Text & " - " & ListView1.FocusedItem.SubItems.Item(1).Text

            FAna.sMusAdi = ListView1.FocusedItem.SubItems.Item(1).Text

            FAna.sTPC = ListView1.FocusedItem.SubItems(2).Text

            Me.Close()

        End If
    End Sub

    Private Sub txt_TarafNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txt_TarafNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode

        Dim Shift As Short = eventArgs.KeyData \ &H10000

        listele((txt_TarafNo.Text))
    End Sub

    #End Region 'Methods

End Class