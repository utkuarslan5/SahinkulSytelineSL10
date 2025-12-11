Imports System
Imports System.Data
Imports System.Data.OleDb

Namespace Core

    Public Class DataForDB2

        #Region "Fields"

        Public Result As New ReturnValue

        Private bTransaction As Boolean = False
        Private conn As New OleDbConnection
        Private tran As OleDbTransaction

        #End Region 'Fields

        #Region "Constructors"

        '
        Public Sub New(ByVal sConnString As String)
            conn.ConnectionString = sConnString
        End Sub

        #End Region 'Constructors

        #Region "Properties"

        '
        Public ReadOnly Property Transaction() As Boolean
            Get
                Return bTransaction
            End Get
        End Property

        #End Region 'Properties

        #Region "Methods"

        '
        Public Sub BeginTransaction()
            '
            OpenConnection()
            '
            If Not bTransaction Then
                bTransaction = True
            End If
            '
            tran = conn.BeginTransaction
        End Sub

        '
        Public Sub CommitTransaction()
            '
            If Not tran Is Nothing Then
                tran.Commit()
            End If
            '
            If bTransaction Then
                bTransaction = False
            End If
        End Sub

        '
        Public Sub Dispose()
            '
            tran.Dispose()
            conn.Dispose()
            '
        End Sub

        '
        Public Sub RollbackTransaction()
            '
            If Not tran Is Nothing Then
                tran.Rollback()
            End If
            '
            If bTransaction Then
                bTransaction = False
            End If
        End Sub

        '
        '*******************************************************************************
        'MethodName :RunSp
        'WhatDoes   :Verilen Proc. çalýþtýrýr
        'Inputs     :sProcName  :Proc Ýsmi
        '            param      :sqlParameterler
        '            rowsAffected: Proc iþlemleri Sonucu Etkilenen Satýr Sayýsý
        'Outputs    :
        'Return     :returnValue
        '*******************************************************************************
        Public Function RunSp(ByVal sProcName As String, _
            ByVal parameter As ArrayList, _
            ByRef rowsAffected As Integer) As Integer
            Dim returnValue As Integer

            Dim cmd As New OleDbCommand

            cmd = BuildIntCommand(sProcName, parameter)

            Try
                'Conn Close ise Açýlacak
                OpenConnection()
                '
                'ExeCute
                '
                cmd.ExecuteNonQuery()

                'rowsAffected = cmd.Parameters("@rowsAffected").Value

                'returnValue = cmd.Parameters("@returnValue").Value
                '
                Return returnValue

            Catch ex As Exception
                '
                Result.Add(ex.Message)
                '
                returnValue = -1
                Return returnValue
            Finally
                'Conn Kapalý Deðilse Kapatýlýyor
                CloseConnection()
            End Try
        End Function

        '
        '*******************************************************************************
        'MethodName :RunSp
        'WhatDoes   :Verilen Proc. çalýþtýrýr
        'Inputs     :sProcName  :Proc Ýsmi
        '            param      :sqlParameterler
        '            rowsAffected: Proc iþlemleri Sonucu Etkilenen Satýr Sayýsý
        'Outputs    :
        'Return     :returnValue
        '*******************************************************************************
        Public Function RunSp(ByVal sProcName As String, _
            ByVal parameter As ArrayList, _
            ByRef rowsAffected As Integer, ByVal rowRetInclude As Boolean) As Integer
            Dim returnValue As Integer
            Dim cmd As New OleDbCommand

            cmd = BuildIntCommand(sProcName, parameter)

            Try
                'Conn Close ise Açýlacak
                OpenConnection()
                '
                'ExeCute
                '
                cmd.ExecuteNonQuery()

                If rowRetInclude Then

                    rowsAffected = cmd.Parameters("@rowsAffected").Value

                    returnValue = cmd.Parameters("@returnValue").Value

                    Return returnValue

                Else

                    Return 0

                End If

            Catch ex As Exception
                '
                Result.Add(ex.Message)
                '
                returnValue = -1

                Return returnValue

            Finally
                'Conn Kapalý Deðilse Kapatýlýyor

                CloseConnection()

            End Try
        End Function

        '
        '*******************************************************************************
        'MethodName :RunSp
        'WhatDoes   :Verilen Proc. çalýþtýrýr
        'Inputs     :sProcName  :Proc Ýsmi
        '            param      :sqlParameterler
        'Outputs    :
        'Return     :OleDbDataReader
        '*******************************************************************************
        Public Function RunSp(ByVal sProcName As String, _
            ByVal parameter As ArrayList) As OleDbDataReader
            Dim rd As OleDbDataReader

            Dim cmd As New OleDbCommand

            cmd = BuildQueryCommand(sProcName, parameter)

            Try
                'Conn Close ise Açýlacak
                OpenConnection()

                'ExeCute
                '
                rd = cmd.ExecuteReader(CommandBehavior.CloseConnection)

                Return rd
                '
            Catch ex As Exception
                '
                Result.Add(ex.Message)
                '
            Finally
                'Conn Kapalý Deðilse Kapatýlýyor
                CloseConnection()

            End Try
        End Function

        '
        '*******************************************************************************
        'MethodName :RunSp
        'WhatDoes   :Verilen Proc. çalýþtýrýr
        'Inputs     :sProcName  :Proc Ýsmi
        '            param      :sqlParameterler
        '            tableName  :Table Ýsimleri
        'Outputs    :
        'Return     :ds: dataset
        '*******************************************************************************
        Public Function RunSp(ByVal sProcName As String, _
            ByVal parameter As ArrayList, _
            ByVal sTableName As String) As DataSet
            Dim ds As New DataSet

            Dim da As New OleDbDataAdapter

            da.SelectCommand = BuildQueryCommand(sProcName, parameter)

            Try
                'Conn Close ise Açýlacak
                OpenConnection()

                'Fill Dataset
                da.Fill(ds, sTableName)
                '
                If Not FillBlank_ForNull(ds) Then
                    '
                    'Throw New exection("Hata")
                End If
                '
                Me.Result.ReturnValue = True

                Return ds

            Catch ex As Exception
                '
                Result.Add(ex.Message)
                '
                Me.Result.ReturnValue = False

            Finally
                'Conn Kapalý Deðilse Kapatýlýyor

                CloseConnection()

            End Try
        End Function

        '
        '*******************************************************************************
        'MethodName :RunSp
        'WhatDoes   :Verilen Proc. çalýþtýrýr
        'Inputs     :sProcName  :Proc Ýsmi
        '            param      :sqlParameterler
        '            tableName  :Table Ýsimleri
        'Outputs    :ds : DataSet
        'Return     :
        '*******************************************************************************
        Public Sub RunSp(ByVal sProcName As String, _
            ByVal parameter As ArrayList, _
            ByVal sTableName As String, _
            ByRef ds As DataSet)
            Dim da As New OleDbDataAdapter

            da.SelectCommand = BuildQueryCommand(sProcName, parameter)

            Try
                'Conn Close ise Açýlacak
                OpenConnection()

                'Fill Dataset
                da.Fill(ds, sTableName)
                '
                If Not FillBlank_ForNull(ds) Then
                    '
                    'Throw New exection("Hata")
                End If
                '
            Catch ex As Exception
                '
                Result.Add(ex.Message)
                '
            Finally
                'Conn Kapalý Deðilse Kapatýlýyor
                CloseConnection()

            End Try
        End Sub

        '
        '*******************************************************************************
        'MethodName :RunSp
        'WhatDoes   :Verilen Proc. çalýþtýrýr
        'Inputs     :sProcName  :Proc Ýsmi
        '            tableName  :Table Ýsimleri
        'Outputs    :ds : DataSet
        'Return     :
        '*******************************************************************************
        Public Sub RunSp(ByVal sProcName As String, _
            ByVal sTableName As String, _
            ByRef ds As DataSet)
            Dim da As New OleDbDataAdapter

            da.SelectCommand = BuildQueryCommand(sProcName)

            Try
                'Conn Close ise Açýlacak
                OpenConnection()

                'Fill Dataset
                da.Fill(ds, sTableName)
                '
                If Not FillBlank_ForNull(ds) Then
                    '
                    'Throw New exection("Hata")
                End If
                '
            Catch ex As Exception
                '
                Result.Add(ex.Message)
                '
            Finally
                'Conn Kapalý Deðilse Kapatýlýyor
                CloseConnection()
            End Try
        End Sub

        '
        '*******************************************************************************
        'MethodName :RunSp
        'WhatDoes   :Verilen Proc. çalýþtýrýr
        'Inputs     :sProcName  :Proc Ýsmi
        '            tableName  :Table Ýsimleri
        'Outputs    :
        'Return     :ds: dataset
        '*******************************************************************************
        Public Function RunSp(ByVal sProcName As String, _
            ByVal sTableName As String) As DataSet
            Dim ds As New DataSet
            Dim da As New OleDbDataAdapter

            da.SelectCommand = BuildQueryCommand(sProcName)

            Try
                'Conn Close ise Açýlacak
                OpenConnection()

                'Fill Dataset
                da.Fill(ds, sTableName)
                '
                If Not FillBlank_ForNull(ds) Then
                    '
                    'Throw New exection("Hata")
                End If
                '
                Return ds
            Catch ex As Exception
                '
                Result.Add(ex.Message)
                '
            Finally
                'Conn Kapalý Deðilse Kapatýlýyor

                CloseConnection()

            End Try
        End Function

        '
        '*******************************************************************************
        'MethodName :RunSp ; scalar sonuçlar için kullanýlacak
        'WhatDoes   :Verilen Proc. çalýþtýrýr
        'Inputs     :sProcName  :Proc Ýsmi
        'Outputs    :
        'Return     :Object
        '*******************************************************************************
        Public Function RunSp(ByVal sProcName As String) As Object
            '
            Dim cmd As New OleDbCommand
            '
            cmd = BuildQueryCommand(sProcName)

            Try
                'Conn Close ise Açýlacak
                OpenConnection()
                '
                'ExeCute
                Return cmd.ExecuteScalar
                '
            Catch ex As Exception
                '
                Result.Add(ex.Message)
                '
                Return Nothing
            Finally
                'Conn Kapalý Deðilse Kapatýlýyor
                CloseConnection()
            End Try
        End Function

        '
        '*******************************************************************************
        'MethodName :RunSql
        'WhatDoes   :Verilen Sql Ýfadesini çalýþtýrýr
        'Inputs     :sSql  :Sql Ýfadesi
        '            tableName  :Table Ýsimleri
        'Outputs    :
        'Return     :ds: dataset
        '*******************************************************************************
        Public Function RunSql(ByVal sSql As String, _
            ByVal sTableName As String) As DataSet
            Dim ds As New DataSet
            Dim da As New OleDbDataAdapter()
            sSql = sSql.Replace("=N'", "='")
            da.SelectCommand = BuildSqlQueryCommand(sSql)
            '
            Try
                'Conn Close ise Açýlacak
                OpenConnection()
                '
                'Fill Dataset
                da.Fill(ds, sTableName)
                '
                If Not FillBlank_ForNull(ds) Then
                    '
                    'Throw New exection("Hata")
                End If
                '
                Me.Result.ReturnValue = True

                Return ds

            Catch ex As Exception
                '

                Me.Result.ReturnValue = False

                Result.Add(ex.Message)
                '
                'MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                OlayYonetici.HataYaz(ex.Message, "RunSql", sSql)

                Throw ex

            Finally
                '
                'Conn Kapalý Deðilse Kapatýlýyor
                CloseConnection()

            End Try
        End Function

        '
        '*******************************************************************************
        'MethodName :RunSql
        'WhatDoes   :Verilen Sql Ýfadesini çalýþtýrýr
        'Inputs     :sSql  :Sql Ýfadesi
        'Outputs    :
        'Return     :dt: dataTable
        '*******************************************************************************
        Public Function RunSql(ByVal sSql As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OleDbDataAdapter
            sSql = sSql.Replace("=N'", "='")
            sSql = sSql.Replace(",N'", ",'")
            sSql = sSql.Replace("(N'", "('")

            da.SelectCommand = BuildSqlQueryCommand(sSql)

            Try
                'Conn Close ise Açýlacak
                OpenConnection()
                '
                'Fill Dataset
                da.Fill(dt)
                '
                If Not FillBlank_ForNull(dt) Then
                    '
                    'Throw New exection("Hata")
                End If
                '
                Result.ReturnValue = True

                Return dt

            Catch ex As Exception
                '
                Result.ReturnValue = False

                Result.Add(ex.Message)
                '
                'MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                OlayYonetici.HataYaz(ex.Message, "RunSql", sSql)

                Throw ex

            Finally
                '
                'Conn Kapalý Deðilse Kapatýlýyor
                CloseConnection()

            End Try
        End Function

        '
        '*******************************************************************************
        'MethodName :RunSql ; Scalar sonuçlar için kullanýlacak
        'WhatDoes   :Verilen Sql Ýfadesini çalýþtýrýr
        'Inputs     :sSql  :Sql Ýfadesi
        'Outputs    :
        'Return     :object
        '*******************************************************************************
        Public Function RunSql(ByVal sSql As String, ByVal Scalar As Boolean) As Object
            '
            Dim cmd As OleDbCommand
            sSql = sSql.Replace("=N'", "='")
            cmd = BuildSqlQueryCommand(sSql)
            '
            Try
                'Conn Close ise Açýlacak
                OpenConnection()
                '
                'Execute Scalar

                Result.ReturnValue = True

                Return cmd.ExecuteScalar()
                '
            Catch ex As Exception
                '
                Result.Add(ex.Message)

                Result.ReturnValue = False

                'MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                OlayYonetici.HataYaz(ex.Message, "RunSql", sSql)

                '
                Throw ex

                Return Nothing

            Finally
                '
                'Conn Kapalý Deðilse Kapatýlýyor
                CloseConnection()
            End Try
        End Function

        '
        '
        '*******************************************************************************
        'MethodName :RunSql ; Scalar sonuçlar için kullanýlacak
        'WhatDoes   :Verilen Sql Ýfadesini çalýþtýrýr
        'Inputs     :sSql  :Sql Ýfadesi
        'Outputs    :
        'Return     :object
        '*******************************************************************************
        Public Function RunSql(ByVal sSql As String, ByVal Scalar As Boolean, ByVal Proc As Boolean) As Object
            '
            Dim cmd As OleDbCommand
            sSql = sSql.Replace("=N'", "='")
            cmd = BuildSqlQueryCommand(sSql)
            '
            Try
                'Conn Close ise Açýlacak
                OpenConnection()
                '
                'Execute Scalar
                cmd.CommandType = CommandType.StoredProcedure

                Result.ReturnValue = True

                Return cmd.ExecuteScalar()
                '
            Catch ex As Exception
                '
                Result.Add(ex.Message)

                Result.ReturnValue = False

                'MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                OlayYonetici.HataYaz(ex.Message, "RunSql", sSql)
                '
                Throw ex

                Return Nothing

            Finally
                '
                'Conn Kapalý Deðilse Kapatýlýyor
                CloseConnection()
            End Try
        End Function

        '
        '*******************************************************************************
        'MethodName :RunSql ; Scalar sonuçlar için kullanýlacak
        'WhatDoes   :Verilen Sql Ýfadesini çalýþtýrýr
        'Inputs     :sSql  :Sql Ýfadesi
        'Outputs    :
        'Return     :object
        '*******************************************************************************
        Public Sub RunSqlx(ByVal sSql As String)
            '
            Dim cmd As OleDbCommand
            sSql = sSql.Replace("=N'", "='")
            cmd = BuildSqlQueryCommand(sSql)
            '
            Try
                'Conn Close ise Açýlacak
                OpenConnection()
                '
                'Execute Scalar
                cmd.CommandType = CommandType.Text

                cmd.ExecuteNonQuery()
                '
            Catch ex As Exception
                '
                Result.Add(ex.Message)

                Result.ReturnValue = False

                'MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                OlayYonetici.HataYaz(ex.Message, "RunSql", sSql)
                '
                Throw ex

            Finally
                '
                'Conn Kapalý Deðilse Kapatýlýyor
                CloseConnection()

            End Try
        End Sub

        '
        '**************************************************************************
        'Function   :gfFillBlank_ForNull ; Null Olan Alanlara Tipine Göre
        '                        "" Veya 0 Atar
        '           Tarih Deðerlerini Deðiþtiremez
        'Input      :ds     : Alanlarý Kontrol edilmek Ýstenen DataSet
        'Output     :
        'Return     :Ok/Err
        '**************************************************************************
        Protected Function FillBlank_ForNull(ByRef ds As DataSet) As Boolean
            Dim iDt As Integer

            Try
                '
                'Table Sayýsý Kadar Döner
                For iDt = 0 To ds.Tables.Count - 1
                    '
                    FillBlank_ForNull(ds.Tables(iDt))
                Next
                '
                Return True
            Catch ex As Exception

                MsgBox(ex.Message)
                Return False
            End Try
        End Function

        '
        '**************************************************************************
        'Function   :gfFillBlank_ForNull ; Null Olan Alanlara Tipine Göre
        '                        "" Veya 0 Atar
        '           Tarih Deðerlerini Deðiþtiremez
        'Input      :dt     : Alanlarý Kontrol edilmek Ýstenen DataTable
        'Output     :
        'Return     :Ok/Err
        '**************************************************************************
        Protected Function FillBlank_ForNull(ByRef dt As DataTable) As Boolean
            Dim iRow As Integer
            Dim iCol As Integer
            '
            Try
                '
                'Satýr Sayýsý Kadar Döner
                For iRow = 0 To dt.Rows.Count - 1
                    '
                    'Kolon Sayýsý Kadar Döner
                    For iCol = 0 To dt.Columns.Count - 1
                        '
                        'Eðer Null Ýse
                        If dt.Rows(iRow).IsNull(iCol) = True Then
                            '
                            'Tipine Göre Deðer Atanacak
                            Select Case dt.Columns(iCol).DataType.FullName.Trim
                                Case "System.Decimal"
                                    dt.Rows(iRow).Item(iCol) = 0
                                Case "System.String"
                                    dt.Rows(iRow).Item(iCol) = ""
                                Case "System.DateTime"
                                    'Throw New Exception("DateTime Alanlar Null Olmamalý.")
                            End Select
                        End If
                    Next
                Next
                '
                Return True
            Catch ex As Exception

                MsgBox(ex.Message)
                Return False
            End Try
        End Function

        '
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        '
        '*******************************************************************************
        'MethodName :BuildIntCommand
        'WhatDoes   :Verilen Parametreler ve Procismini kullanarak
        '            Bir Command Oluþturur ve ReturnValue Ekler
        'Inputs     :sProcName  :Proc Ýsmi
        '            param      :sqlParameterler
        'Outputs    :
        'Return     :OleDbCommand
        '*******************************************************************************
        Private Function BuildIntCommand(ByVal sProcName As String, _
            ByVal parameter As ArrayList) As OleDbCommand
            Dim cmd As New OleDbCommand

            'BuildQueryCommand
            cmd = BuildQueryCommand(sProcName, parameter)

            'ReturnValue Parametresi Ekleniyor
            'cmd.Parameters.Add("@ReturnValue", OleDbType.Integer)

            'cmd.Parameters("@ReturnValue").Direction = ParameterDirection.ReturnValue

            Return cmd
        End Function

        '
        '*******************************************************************************
        'MethodName :BuildQueryCommand
        'WhatDoes   :Verilen Parametreler ve Procismini kullanarak
        '            Bir Command Oluþturur
        'Inputs     :sProcName  :Proc Ýsmi
        '            param      :sqlParameterler
        'Outputs    :
        'Return     :OleDbCommand
        '*******************************************************************************
        Private Function BuildQueryCommand(ByVal sProcName As String, _
            ByVal parameter As ArrayList) As OleDbCommand
            '
            'Command Oluþturuluyor
            Dim cmd As New OleDbCommand(sProcName, conn)

            cmd.CommandType = CommandType.StoredProcedure
            '
            If bTransaction Then

                cmd.Transaction = tran

            End If
            '
            'Parametreler Ekleniyor
            For Each param As OleDbParameter In parameter

                cmd.Parameters.Add(param)

            Next

            Return cmd
        End Function

        '
        '*******************************************************************************
        'MethodName :BuildQueryCommand
        'WhatDoes   :Verilen Procismini kullanarak
        '            Bir Command Oluþturur
        'Inputs     :sProcName  :Proc Ýsmi
        'Outputs    :
        'Return     :OleDbCommand
        '*******************************************************************************
        Private Function BuildQueryCommand(ByVal sProcName As String) As OleDbCommand
            '
            'Command Oluþturuluyor
            Dim cmd As New OleDbCommand(sProcName, conn)

            cmd.CommandType = CommandType.StoredProcedure
            '
            If bTransaction Then

                cmd.Transaction = tran

            End If

            Return cmd
        End Function

        '
        '*******************************************************************************
        'MethodName :BuildSqlIntCommand
        'WhatDoes   :Verilen Parametreler ve Sql Ýfadesi kullanarak
        '            Bir Command Oluþturur ve ReturnValue Ekler
        'Inputs     :sSql  :Sql Ýfadesi
        '            param      :sqlParameterler
        'Outputs    :
        'Return     :OleDbCommand
        '*******************************************************************************
        Private Function BuildSqlIntCommand(ByVal sSql As String, _
            ByVal parameter As ArrayList) As OleDbCommand
            Dim cmd As New OleDbCommand

            'BuildSqlQueryCommand
            cmd = BuildSqlQueryCommand(sSql, parameter)

            'ReturnValue Parametresi Ekleniyor
            cmd.Parameters.Add(("@ReturnValue"), OleDbType.Integer)

            cmd.Parameters("@ReturnValue").Direction = ParameterDirection.ReturnValue

            Return cmd
        End Function

        '
        '
        '*******************************************************************************
        'MethodName :BuildSqlQueryCommand
        'WhatDoes   :Verilen Parametreler ve Ýfadeyi kullanarak
        '            Bir Command Oluþturur
        'Inputs     :sSql   :Sql Ýfadesi
        '            param      :sqlParameterler
        'Outputs    :
        'Return     :OleDbCommand
        '*******************************************************************************
        Private Function BuildSqlQueryCommand(ByVal sSql As String, _
            ByVal parameter As ArrayList) As OleDbCommand
            '
            'Command Oluþturuluyor
            Dim cmd As New OleDbCommand(sSql, conn)

            cmd.CommandType = CommandType.Text
            '
            If bTransaction Then

                cmd.Transaction = tran

            End If
            '
            'Parametreler Ekleniyor
            For Each param As OleDbParameter In parameter

                cmd.Parameters.Add(param)

            Next

            Return cmd
        End Function

        '
        '*******************************************************************************
        'MethodName :BuildSqlQueryCommand
        'WhatDoes   :Verilen Parametreler ve Ýfadeyi kullanarak
        '            Bir Command Oluþturur
        'Inputs     :sSql   :Sql Ýfadesi
        'Outputs    :
        'Return     :OleDbCommand
        '*******************************************************************************
        Private Function BuildSqlQueryCommand(ByVal sSql As String) As OleDbCommand
            '
            'Command Oluþturuluyor
            Dim cmd As New OleDbCommand(sSql, conn)

            cmd.CommandType = CommandType.Text
            '
            If bTransaction Then

                cmd.Transaction = tran

            End If

            Return cmd
        End Function

        '
        Private Sub CloseConnection()
            CloseConnection(False)
        End Sub

        '
        Private Sub CloseConnection(ByVal Trans As Boolean)
            If Not bTransaction Then
                If conn.State <> ConnectionState.Closed Then
                    conn.Close()
                End If
            End If
        End Sub

        '
        Private Sub OpenConnection()
            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If
        End Sub

        #End Region 'Methods

        #Region "Other"

        '
        '
        '
        '
        '
        '
        '
        '
        '
        '
        '

        #End Region 'Other

    End Class

End Namespace

