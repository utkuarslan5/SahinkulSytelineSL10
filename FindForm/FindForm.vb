Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class FindForm

    #Region "Fields"

    Dim dvFind As DataView


    Dim _FindSelectedID As String
    Dim _FindSelectedDescription As String
    Dim _FindSelectedCriteria As String

    Dim _FindSelectedItem_1 As String
    Dim _FindSelectedItem_2 As String
    Dim _FindSelectedItem_3 As String
    Dim _FindSelectedItem_4 As String
    Dim _FindSelectedItem_5 As String
    Dim _FindSelectedItem_6 As String
    Dim _FindSelectedItem_7 As String
    Dim _NumFindId As Boolean = False

    '= "Data Source=localhost;Initial Catalog=Vesbo_App;pwd=1453;uid=sa"
    Dim _FindQuerySelect As String
    Dim _ConString As String
    Dim _labelID As String
    Dim _labelDescription As String
    Dim _labelCriteria As String
    Dim _Onay As Boolean

#End Region 'Fields

#Region "Properties"
    Public WriteOnly Property NumFindId() As Boolean
        Set(ByVal value As Boolean)
            _NumFindId = value
        End Set
    End Property
    Public WriteOnly Property labelID() As String
        Set(ByVal value As String)
            _labelID = value
        End Set
    End Property
    Public WriteOnly Property labelDescription() As String
        Set(ByVal value As String)
            _labelDescription = value
        End Set
    End Property
    Public WriteOnly Property labelCriteria() As String
        Set(value As String)
            _labelCriteria = value
        End Set
    End Property
    Public Property Onay() As Boolean
        Get
            Return _Onay
        End Get
        Set(ByVal value As Boolean)
            _Onay = value
        End Set
    End Property
    Public Property FindSelectedID() As String
        Get
            Return _FindSelectedID
        End Get
        Set(ByVal value As String)
            _FindSelectedID = value
        End Set
    End Property
    Public Property FindSelectedDescription() As String
        Get
            Return _FindSelectedDescription
        End Get
        Set(ByVal value As String)
            _FindSelectedDescription = value
        End Set
    End Property
    Public Property FindSelectedCriteria() As String
        Get
            Return _FindSelectedCriteria
        End Get
        Set(ByVal value As String)
            _FindSelectedCriteria = value
        End Set
    End Property
    Public Property FindSelectedItem_1() As String
        Get
            Return _FindSelectedItem_1
        End Get
        Set(ByVal value As String)
            _FindSelectedItem_1 = value
        End Set
    End Property
    Public Property FindSelectedItem_2() As String
        Get
            Return _FindSelectedItem_2
        End Get
        Set(ByVal value As String)
            _FindSelectedItem_2 = value
        End Set
    End Property
    Public Property FindSelectedItem_3() As String
        Get
            Return _FindSelectedItem_3
        End Get
        Set(ByVal value As String)
            _FindSelectedItem_3 = value
        End Set
    End Property
    Public Property FindSelectedItem_4() As String
        Get
            Return _FindSelectedItem_4
        End Get
        Set(ByVal value As String)
            _FindSelectedItem_4 = value
        End Set
    End Property
    Public Property FindSelectedItem_5() As String
        Get
            Return _FindSelectedItem_5
        End Get
        Set(ByVal value As String)
            _FindSelectedItem_5 = value
        End Set
    End Property
    Public Property FindSelectedItem_6() As String
        Get
            Return _FindSelectedItem_6
        End Get
        Set(ByVal value As String)
            _FindSelectedItem_6 = value
        End Set
    End Property
    Public Property FindSelectedItem_7() As String
        Get
            Return _FindSelectedItem_7
        End Get
        Set(ByVal value As String)
            _FindSelectedItem_7 = value
        End Set
    End Property
    Public Property FindQuerySelect() As String
        Get
            Return _FindQuerySelect
        End Get
        Set(ByVal value As String)
            _FindQuerySelect = value
        End Set
    End Property
    Public Property ConnectionString() As String
        Get
            Return _ConString
        End Get
        Set(ByVal value As String)
            _ConString = value
        End Set
    End Property



#End Region 'Properties

#Region "Methods"

    Private Sub FindForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If _labelCriteria = "" Then
                lblCriteria.Visible = False
                txtFindCriteria.Visible = False
            Else
                lblCriteria.Visible = True
                txtFindCriteria.Visible = True
            End If

            lblID.Text = _labelID
            lblDescription.Text = _labelDescription
            lblCriteria.Text = _labelCriteria

            grdFindResult.RootTable.Columns.Add(_labelDescription, Janus.Windows.GridEX.ColumnType.Text, Janus.Windows.GridEX.EditType.NoEdit)

            grdFindResult.RootTable.Columns.Add(_labelID, Janus.Windows.GridEX.ColumnType.Text, Janus.Windows.GridEX.EditType.NoEdit)

            If _labelCriteria <> "" Then
                grdFindResult.RootTable.Columns.Add(_labelCriteria, Janus.Windows.GridEX.ColumnType.Text, Janus.Windows.GridEX.EditType.NoEdit)
            End If

            If _FindSelectedItem_1 <> "" Then

                grdFindResult.RootTable.Columns.Add(_FindSelectedItem_1, Janus.Windows.GridEX.ColumnType.Text, Janus.Windows.GridEX.EditType.NoEdit)

            End If

            If _FindSelectedItem_2 <> "" Then

                grdFindResult.RootTable.Columns.Add(_FindSelectedItem_2, Janus.Windows.GridEX.ColumnType.Text, Janus.Windows.GridEX.EditType.NoEdit)

            End If
            If _FindSelectedItem_3 <> "" Then

                grdFindResult.RootTable.Columns.Add(_FindSelectedItem_3, Janus.Windows.GridEX.ColumnType.Text, Janus.Windows.GridEX.EditType.NoEdit)

            End If

            If _FindSelectedItem_4 <> "" Then

                grdFindResult.RootTable.Columns.Add(_FindSelectedItem_4, Janus.Windows.GridEX.ColumnType.Text, Janus.Windows.GridEX.EditType.NoEdit)

            End If

            If _FindSelectedItem_5 <> "" Then

                grdFindResult.RootTable.Columns.Add(_FindSelectedItem_5, Janus.Windows.GridEX.ColumnType.Text, Janus.Windows.GridEX.EditType.NoEdit)

            End If

            If _FindSelectedItem_6 <> "" Then

                grdFindResult.RootTable.Columns.Add(_FindSelectedItem_6, Janus.Windows.GridEX.ColumnType.Text, Janus.Windows.GridEX.EditType.NoEdit)

            End If

            If _FindSelectedItem_7 <> "" Then

                grdFindResult.RootTable.Columns.Add(_FindSelectedItem_7, Janus.Windows.GridEX.ColumnType.Text, Janus.Windows.GridEX.EditType.NoEdit)

            End If

            'If _FindSelectedItem_5 <> "" And _FindSelectedItem_6 <> "" Then
            If _FindSelectedItem_4 <> "" Then
                Me.Width = 650
                grdFindResult.RootTable.Columns(0).Width = 120
                grdFindResult.RootTable.Columns(1).Width = 80
                grdFindResult.RootTable.Columns(2).Width = 100
                grdFindResult.RootTable.Columns(3).Width = 60
                grdFindResult.RootTable.Columns(4).Width = 100
                ' grdFindResult.RootTable.Columns(5).Width = 30
                ' grdFindResult.RootTable.Columns(6).Width = 50

                'If _FindSelectedItem_7 <> "" Then
                'grdFindResult.RootTable.Columns(7).Width = 50
                'End If

            End If
            If _labelCriteria <> "" Then

                grdFindResult.RootTable.Columns(0).Width = 50
                grdFindResult.RootTable.Columns(1).Width = 200
                If _labelCriteria = "Purplast Kodu" Then
                    grdFindResult.RootTable.Columns(2).Width = 90
                End If
                grdFindResult.RootTable.Columns(3).Width = 30
                'grdFindResult.RootTable.Columns(4).Width = 30
            End If

            Me.Show()

            'Data Bind
            dvFind = New DataView(FindSelect(_FindQuerySelect))
            grdFindResult.DataSource = dvFind

            grdFindResult.ColumnAutoResize = True

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Hata")
        End Try
    End Sub

    Private Sub grdFindResult_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'Select Case e.KeyValue
        '    Case 38, 40
        '        e.Handled = True
        'End Select
        If grdFindResult.RowCount > 0 Then

            Select Case e.KeyValue
                Case 38 'UP
                        'If grdFindResult.ActiveRow > 0 Then
                        '    grdFindResult.Rows(grdFindResult.ActiveRow.Index).Selected = False
                        '    grdFindResult.ActiveRow = grdFindResult.Rows(grdFindResult.ActiveRow.Index - 1)
                        '    grdFindResult.ActiveRow = grdFindResult.Rows(grdFindResult.ActiveRow.Index)
                        '    grdFindResult.Rows(grdFindResult.ActiveRow.Index).Selected = True

                        'End If
                Case 40 'DOWN

                        'If txtFindDescription.Focus = False And txtFindID.Focus = False Then
                        '    txtFindID.Focus()
                        'End If

                        'If grdFindResult.ActiveRow.Index = 0 _
                        '    And grdFindResult.Rows(grdFindResult.ActiveRow.Index).Selected = False Then
                        '    grdFindResult.Rows(grdFindResult.ActiveRow.Index).Selected = True
                        'ElseIf grdFindResult.ActiveRow.Index < grdFindResult.Rows.Count - 1 Then
                        '    grdFindResult.Rows(grdFindResult.ActiveRow.Index).Selected = False
                        '    grdFindResult.ActiveRow = grdFindResult.Rows(grdFindResult.ActiveRow.Index + 1)
                        '    grdFindResult.ActiveRow = grdFindResult.Rows(grdFindResult.ActiveRow.Index)
                        '    grdFindResult.Rows(grdFindResult.ActiveRow.Index).Selected = True
                        'End If
                Case 13 'ENTER
                    btnOk_Click(sender, e)
            End Select
        End If
    End Sub

    Public Function FindSelect(ByVal QueryString As String) As DataTable
        Dim objSqlConnection As New SqlConnection
        Try
            Dim dtFindResult As New DataTable
            objSqlConnection = New SqlConnection(_ConString)

            If Not String.IsNullOrEmpty(_ConString.Trim) Then
                Dim sorgu As String
                sorgu = "DECLARE @return_value int, @Infobar InfobarType " &
               " EXEC  @return_value = [dbo].[SetSiteSp] @Site = 'Default', @Infobar = @Infobar OUTPUT "
                'OpenConnection()
                'RunSql(sorgu, True)
                If objSqlConnection.State <> ConnectionState.Open Then
                    objSqlConnection.Open()
                End If
                Dim cmd As New SqlCommand(sorgu, objSqlConnection)
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 0
                cmd.ExecuteScalar()
                'objSqlConnection.Close()
                'CloseConnection()
            End If

            Dim adapFind As New SqlDataAdapter
            adapFind.SelectCommand = New SqlCommand
            adapFind.SelectCommand.Connection = objSqlConnection
            adapFind.SelectCommand.CommandText = QueryString
            adapFind.Fill(dtFindResult)

            Return dtFindResult
        Catch ex As SqlClient.SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objSqlConnection.Dispose()
        End Try
    End Function
    Private Sub btnIptal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIptal.Click
        _Onay = False

        Me.Close()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try

            If grdFindResult.SelectedItems.Item(0).GetRow.Cells.Count > 0 Then
                With grdFindResult.SelectedItems.Item(0).GetRow

                    If IsDBNull(.Cells(0).Text) = False Then
                        FindSelectedID = .Cells(0).Text
                    End If
                    If IsDBNull(.Cells(1).Text) = False Then
                        FindSelectedDescription = .Cells(1).Text
                    Else
                        FindSelectedDescription = ""
                    End If

                    If grdFindResult.RootTable.Columns.Count > 2 Then
                        FindSelectedItem_1 = .Cells(2).Text

                    Else
                        FindSelectedItem_1 = ""
                    End If

                    If grdFindResult.RootTable.Columns.Count > 3 Then
                        FindSelectedItem_2 = .Cells(3).Text

                    Else
                        FindSelectedItem_2 = ""
                    End If

                End With

                'DialogResult = Windows.Forms.DialogResult.OK

                Onay = True

                Me.Close()

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Hata")
        End Try
    End Sub

    Private Function CheckFindID() As Boolean
        If _NumFindId Then
            If Not IsNumeric(txtFindID.Text) Then
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub FindForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If grdFindResult.RowCount > 0 Then

                Select Case e.KeyValue
                    Case 38 'UP
                        'If grdFindResult.ActiveRow > 0 Then
                        '    grdFindResult.Rows(grdFindResult.ActiveRow.Index).Selected = False
                        '    grdFindResult.ActiveRow = grdFindResult.Rows(grdFindResult.ActiveRow.Index - 1)
                        '    grdFindResult.ActiveRow = grdFindResult.Rows(grdFindResult.ActiveRow.Index)
                        '    grdFindResult.Rows(grdFindResult.ActiveRow.Index).Selected = True

                        'End If
                    Case 40 'DOWN

                        'If txtFindDescription.Focus = False And txtFindID.Focus = False Then
                        '    txtFindID.Focus()
                        'End If

                        'If grdFindResult.ActiveRow.Index = 0 _
                        '    And grdFindResult.Rows(grdFindResult.ActiveRow.Index).Selected = False Then
                        '    grdFindResult.Rows(grdFindResult.ActiveRow.Index).Selected = True
                        'ElseIf grdFindResult.ActiveRow.Index < grdFindResult.Rows.Count - 1 Then
                        '    grdFindResult.Rows(grdFindResult.ActiveRow.Index).Selected = False
                        '    grdFindResult.ActiveRow = grdFindResult.Rows(grdFindResult.ActiveRow.Index + 1)
                        '    grdFindResult.ActiveRow = grdFindResult.Rows(grdFindResult.ActiveRow.Index)
                        '    grdFindResult.Rows(grdFindResult.ActiveRow.Index).Selected = True
                        'End If
                    Case 13 'ENTER
                        btnOk_Click(sender, e)
                End Select
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Hata")
        End Try
    End Sub

    Private Sub grdFindResult_RowDoubleClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdFindResult.RowDoubleClick
        Try
            If e.Row.Cells.Count > 0 Then
                With e.Row

                    If IsDBNull(.Cells(0).Text) = False Then
                        FindSelectedID = .Cells(0).Text
                    End If

                    If IsDBNull(.Cells(1).Text) = False Then
                        FindSelectedDescription = .Cells(1).Text
                    Else
                        FindSelectedDescription = ""
                    End If

                    If _labelCriteria = "" Then

                        FindSelectedCriteria = ""

                        If grdFindResult.RootTable.Columns.Count > 2 Then
                            FindSelectedItem_1 = .Cells(2).Text
                        Else
                            FindSelectedItem_1 = ""
                        End If

                        If grdFindResult.RootTable.Columns.Count > 3 Then
                            FindSelectedItem_2 = .Cells(3).Text
                        Else
                            FindSelectedItem_2 = ""
                        End If

                        If grdFindResult.RootTable.Columns.Count > 4 Then
                            FindSelectedItem_3 = .Cells(4).Text
                        Else
                            FindSelectedItem_3 = ""
                        End If

                        If grdFindResult.RootTable.Columns.Count > 5 Then
                            FindSelectedItem_4 = .Cells(5).Text
                        Else
                            FindSelectedItem_4 = ""
                        End If

                        If grdFindResult.RootTable.Columns.Count > 6 Then
                            FindSelectedItem_5 = .Cells(6).Text
                        Else
                            FindSelectedItem_5 = ""
                        End If

                        If grdFindResult.RootTable.Columns.Count > 7 Then
                            FindSelectedItem_6 = .Cells(7).Text
                        Else
                            FindSelectedItem_6 = ""
                        End If

                    Else

                        FindSelectedCriteria = .Cells(2).Text

                        If grdFindResult.RootTable.Columns.Count > 3 Then
                            FindSelectedItem_1 = .Cells(3).Text
                        Else
                            FindSelectedItem_1 = ""
                        End If

                        If grdFindResult.RootTable.Columns.Count > 4 Then
                            FindSelectedItem_2 = .Cells(4).Text
                        Else
                            FindSelectedItem_2 = ""
                        End If

                        If grdFindResult.RootTable.Columns.Count > 5 Then
                            FindSelectedItem_3 = .Cells(5).Text
                        Else
                            FindSelectedItem_3 = ""
                        End If

                        If grdFindResult.RootTable.Columns.Count > 6 Then
                            FindSelectedItem_4 = .Cells(6).Text
                        Else
                            FindSelectedItem_4 = ""
                        End If

                        If grdFindResult.RootTable.Columns.Count > 7 Then
                            FindSelectedItem_5 = .Cells(7).Text
                        Else
                            FindSelectedItem_5 = ""
                        End If

                        If grdFindResult.RootTable.Columns.Count > 8 Then
                            FindSelectedItem_6 = .Cells(7).Text
                        Else
                            FindSelectedItem_6 = ""
                        End If

                    End If

                End With

                Onay = True

                Me.Close()

            End If

            'If e.Row.Cells.Count > 0 Then
            '    With e.Row

            '        If IsDBNull(.Cells(0).Text) = False Then
            '            FindSelectedID = .Cells(0).Text
            '        End If
            '        If IsDBNull(.Cells(1).Text) = False Then
            '            FindSelectedDescription = .Cells(1).Text
            '        Else
            '            FindSelectedDescription = ""
            '        End If

            '        If grdFindResult.RootTable.Columns.Count > 2 Then
            '            FindSelectedItem_1 = .Cells(2).Text

            '        Else
            '            FindSelectedItem_1 = ""
            '        End If

            '        If grdFindResult.RootTable.Columns.Count > 3 Then
            '            FindSelectedItem_2 = .Cells(3).Text

            '        Else
            '            FindSelectedItem_2 = ""
            '        End If

            '    End With

            '    'DialogResult = Windows.Forms.DialogResult.OK

            '    Onay = True

            '    Me.Close()
            'End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Hata")
        End Try
    End Sub

    Private Sub txtFindDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFindDescription.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFindDescription_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFindDescription.TextChanged
        Try

            Dim strKosul As String = ""

            If Not (txtFindDescription.Text = "") Then

                strKosul = "[" & dvFind.Table.Columns(1).ToString & "]" & " LIKE '%" & txtFindDescription.Text & "%'"

            End If

            If Not strKosul = "" Then
                dvFind.RowFilter = strKosul
            Else
                dvFind.RowFilter = Nothing
            End If

            'FindGridRowSelection()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Hata")
        End Try
    End Sub

    Private Sub txtFindID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFindID.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFindID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFindID.KeyPress
        If Asc(e.KeyChar) < 48 OrElse Asc(e.KeyChar) > 57 Then

            If Not CheckFindID() Then
                e.Handled = True
            End If

        End If
    End Sub

    Private Sub txtFindID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFindID.TextChanged
        Try

            'If Not CheckFindID() Then
            '    Exit Sub
            'End If

            Dim strKosul As String = ""

            If Not (txtFindID.Text = "") Then

                If Not _NumFindId Then
                    strKosul = "[" & dvFind.Table.Columns(0).ToString & "]" & " LIKE '" & txtFindID.Text & "%'"
                Else
                    strKosul = "[" & dvFind.Table.Columns(0).ToString & "]" & "=" & txtFindID.Text
                End If

            End If

            If Not strKosul = "" Then
                dvFind.RowFilter = strKosul
            Else
                dvFind.RowFilter = Nothing
            End If

            If Not CheckFindID() Then
                strKosul = "[" & dvFind.Table.Columns(0).ToString & "]" & "<>0"
                dvFind.RowFilter = Nothing
            End If

            'FindGridRowSelection()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Hata")
        End Try
    End Sub

    #End Region 'Methods

    #Region "Other"

    'Private Sub FindGridRowSelection()
    '    Try
    '        If grdFindResult.RowCount > 0 Then
    '            Dim gridRow As Janus.Windows.GridEX.GridEXRow
    '            For intRow As Int16 = 0 To grdFindResult.RowCount - 1
    '                grdFindResult.GetRow(intRow).
    '            Next intRow
    '            grdFindResult.ActiveRow = grdFindResult.GetRow(0)
    '            grdFindResult.Rows(grdFindResult.ActiveRow.Index).Selected = True
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    #End Region 'Other

End Class