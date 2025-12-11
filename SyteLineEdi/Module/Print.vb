Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Print

    #Region "Fields"

    Private sConn As String
    Private sUId, sPassword, sDataSource, sInitialCatalog As String

    #End Region 'Fields

    #Region "Constructors"

    Public Sub New(ByVal tip As String)
        Try

            If tip = "sql" Then
                sConn = My.Settings.ConnectionString
            Else
                sConn = My.Settings.ConnectionString
            End If
            If sConn = "" Then
                Throw New Exception("Baðlantý Cümlesi Hatalý !")
            Else
                sUId = sConn.Split(";")(2).Split("=")(1).ToString
                sPassword = sConn.Split(";")(3).Split("=")(1).ToString
                sDataSource = sConn.Split(";")(0).Split("=")(1).ToString
                sInitialCatalog = sConn.Split(";")(1).Split("=")(1).ToString
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    #End Region 'Constructors

    #Region "Methods"

    Public Function AddParameter(ByVal paramName As String, _
        ByVal paramValue As String, ByVal paramFields As ParameterFields) As ParameterFields
        ' bu kýsýmda ise parametreyi tanýmlýyorsun.
        Dim paramField As New CrystalDecisions.Shared.ParameterField
        Dim paramDiscreteValue As New CrystalDecisions.Shared.ParameterDiscreteValue
        Dim paramValues As New CrystalDecisions.Shared.ParameterValues

        paramField.ParameterFieldName = paramName
        paramDiscreteValue.Value = paramValue
        paramValues.Add(paramDiscreteValue)

        paramField.CurrentValues = paramValues

        paramFields.Add(paramField)
        Return paramFields
    End Function

    'Bu kýsýmda ise database baglanýrken þifre sormayý pas ediyorsunönce ReportDocument1 diye bir components ekliyorsun
    'eger  crystal tarafýnda database logon oluyorsun bu iþlemide yapmalýsýn.
    Public Function LogOnDatabase(ByVal Rpt As String, Optional ByVal dt As DataTable = Nothing) As ReportDocument
        Dim table As Table

        Dim logonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim reportDocument1 = New ReportDocument

        reportDocument1.Load(Rpt)

        If dt Is Nothing Then
            reportDocument1.SetDatabaseLogon(sUId, sPassword, sDataSource, sInitialCatalog)
            For Each table In reportDocument1.Database.Tables
                ' logonInfo = table.LogOnInfo
                logonInfo = New TableLogOnInfo
                logonInfo.ConnectionInfo.ServerName = sDataSource
                logonInfo.ConnectionInfo.DatabaseName = sInitialCatalog
                logonInfo.ConnectionInfo.UserID = sUId
                logonInfo.ConnectionInfo.Password = sPassword
                '' logonInfo.TableName = "ZED_SDK_FATURA_RPT_SP;1"
                table.ApplyLogOnInfo(logonInfo)
            Next
        Else

            reportDocument1.SetDatabaseLogon(sUId, sPassword, sDataSource, sInitialCatalog)

            reportDocument1.Database.Tables(0).SetDataSource(dt)

            'For Each table In reportDocument1.Database.Tables
            '    ' logonInfo = table.LogOnInfo
            '    logonInfo = New TableLogOnInfo
            '    logonInfo.ConnectionInfo.ServerName = sDataSource
            '    logonInfo.ConnectionInfo.DatabaseName = sInitialCatalog
            '    logonInfo.ConnectionInfo.UserID = sUId
            '    logonInfo.ConnectionInfo.Password = sPassword
            '    '' logonInfo.TableName = "ZED_SDK_FATURA_RPT_SP;1"
            '    ' reportDocument1.VerifyDatabase()
            '    table.ApplyLogOnInfo(logonInfo)
            'Next

        End If

        Return reportDocument1 ' frmRapor.CrystalReportViewer1.ReportSource
    End Function

    #End Region 'Methods

End Class