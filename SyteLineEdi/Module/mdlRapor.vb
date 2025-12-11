Imports System.Configuration
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Resources
Imports System.Threading
Imports System.Xml

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Module mdlRapor

    #Region "Fields"

    Public boolTabHatasi As Boolean = False
    Public Id As Integer '= 5
    Public MalEtkBsm As New MalzemeEtiketBasımı
    Public MessageType As enumMessageType = enumMessageType.SystemHatası
    Public sDataSource As String = My.Settings.ConnectionString.Split(";")(0).Split("=")(1).ToString
    Public SetDataBaseLogon As Boolean = True
    Public sInitialCatalog As String = My.Settings.ConnectionString.Split(";")(1).Split("=")(1).ToString
    Public sPassword As String = My.Settings.ConnectionString.Split(";")(3).Split("=")(1).ToString
    Public sUId As String = My.Settings.ConnectionString.Split(";")(2).Split("=")(1).ToString

    'T01 String
    Public T01IpAdress As String '= ReadConnectionValues("T00001")

    'T02 String
    Public T02IpAdress As String '= ReadConnectionValues("T00002")

    'T03 String
    Public T03IpAdress As String '= ReadConnectionValues("T00003")

    'T04 String
    Public T04IpAdress As String '= ReadConnectionValues("T00004")

    'T05 String
    'Public T05IpAdress As String = ReadConnectionValues("T00005")
    'Terazi Portu
    Public TeraziPortu As String '= ReadConnectionValues("Port")

    'Tolerans String
    Public ToleransValue As String '= ReadConnectionValues("Tolerans")
    Public UserCode As String '= "1"
    Public UserLang As String '= "TR"
    Public UserName As String '= "deneme"
    Public UserTypeCode As String '= "A"

    Dim _AccessConnectionString As String
    Dim _Mandt As String
    Dim _Port As String
    Dim _SapConnectionString As String
    Dim _Sirket As String
    Dim _SqlConnectionString As String
    Dim _T01 As String
    Dim _T02 As String
    Dim _T03 As String
    Dim _T04 As String
    Dim _T05 As String
    Dim _Tolerans As String

    #End Region 'Fields

    #Region "Enumerations"

    Enum enumMessageType
        SystemHatası = 0
        GirisKontrol = 1
        Onay = 2
    End Enum

    Enum enumReportType
        Summary = 0
        Detail = 1
    End Enum

    #End Region 'Enumerations

    #Region "Properties"

    Public ReadOnly Property AccessConnectionString() As String
        Get
            Return _AccessConnectionString
        End Get
    End Property

    Public ReadOnly Property Mandt() As String
        Get
            Return _Mandt
        End Get
    End Property

    Public ReadOnly Property Port() As String
        Get
            Return _Port
        End Get
    End Property

    Public ReadOnly Property SapConnectionString() As String
        Get
            Return _SapConnectionString
        End Get
    End Property

    Public ReadOnly Property Sirket() As String
        Get
            Return _Sirket
        End Get
    End Property

    Public ReadOnly Property SqlConnectionString() As String
        Get
            Return _SqlConnectionString
        End Get
    End Property

    Public ReadOnly Property T01() As String
        Get
            Return _T01
        End Get
    End Property

    Public ReadOnly Property T02() As String
        Get
            Return _T02
        End Get
    End Property

    Public ReadOnly Property T03() As String
        Get
            Return _T03
        End Get
    End Property

    Public ReadOnly Property T04() As String
        Get
            Return _T04
        End Get
    End Property

    Public ReadOnly Property T05() As String
        Get
            Return _T05
        End Get
    End Property

    Public ReadOnly Property Tolerans() As String
        Get
            Return _Tolerans
        End Get
    End Property

    #End Region 'Properties

    #Region "Methods"

    Public Sub AccessLogOnDatabase(ByVal Rpt As String, _
        ByVal Dt As DataTable, _
        Optional ByVal FormType As enumReportType = enumReportType.Summary)
        Try

            MessageType = enumMessageType.SystemHatası

            If FormType = enumReportType.Summary Then
                frmRaporOzet.reportDocument1 = New ReportDocument
                frmRaporOzet.reportDocument1.Load(System.AppDomain.CurrentDomain.BaseDirectory & Rpt)
                frmRaporOzet.reportDocument1.Database.Tables(0).SetDataSource(Dt)
                frmRaporOzet.CrystalReportViewer1.ReportSource = frmRaporOzet.reportDocument1
            ElseIf FormType = enumReportType.Detail Then
                frmRaporOzet.reportDocument1 = New ReportDocument
                frmRaporOzet.reportDocument1.Load(System.AppDomain.CurrentDomain.BaseDirectory & Rpt)
                frmRaporOzet.reportDocument1.Database.Tables(0).SetDataSource(Dt)
                frmRaporOzet.CrystalReportViewer1.ReportSource = frmRaporOzet.reportDocument1
            End If

        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Public Sub AccessLogOnDatabase2(ByVal Rpt As String, _
       ByVal Dt As DataTable, _
       Optional ByVal FormType As enumReportType = enumReportType.Summary)
        Try

            MessageType = enumMessageType.SystemHatası

            If FormType = enumReportType.Summary Then
                frmRaporOzet2.reportDocument2 = New ReportDocument
                frmRaporOzet2.reportDocument2.Load(System.AppDomain.CurrentDomain.BaseDirectory & Rpt)
                frmRaporOzet2.reportDocument2.Database.Tables(0).SetDataSource(Dt)
                frmRaporOzet2.CrystalReportViewer2.ReportSource = frmRaporOzet2.reportDocument2
            ElseIf FormType = enumReportType.Detail Then
                frmRaporOzet2.reportDocument2 = New ReportDocument
                frmRaporOzet2.reportDocument2.Load(System.AppDomain.CurrentDomain.BaseDirectory & Rpt)
                frmRaporOzet2.reportDocument2.Database.Tables(0).SetDataSource(Dt)
                frmRaporOzet2.CrystalReportViewer2.ReportSource = frmRaporOzet2.reportDocument2
            End If

        Catch ex As Exception
            Throw ex

        End Try
    End Sub


    Public Function AddParameter(ByVal paramName As String, _
        ByVal paramValue As String, _
        ByVal paramFields As ParameterFields) As ParameterFields
        Try

            MessageType = enumMessageType.SystemHatası

            ' bu kısımda ise parametreyi tanımlıyorsun.
            Dim paramField As New CrystalDecisions.Shared.ParameterField
            Dim paramDiscreteValue As New CrystalDecisions.Shared.ParameterDiscreteValue
            Dim paramValues As New CrystalDecisions.Shared.ParameterValues

            paramField.ParameterFieldName = paramName
            paramDiscreteValue.Value = paramValue
            paramValues.Add(paramDiscreteValue)

            paramField.CurrentValues = paramValues

            paramFields.Add(paramField)
            Return paramFields

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    Public Function LabelNoCal(ByVal SıraNo As Integer) As Long
        Try

            MessageType = enumMessageType.SystemHatası

            Dim LabelNoCalSet As String
            Dim ColCnt As Integer

            LabelNoCalSet = "1" & DateTime.Now.Year.ToString.Substring(2, 2)

            For ColCnt = 1 To 10 - (Len(LabelNoCalSet) + Len(SıraNo.ToString))
                LabelNoCalSet = LabelNoCalSet & "0"
            Next ColCnt

            LabelNoCalSet = LabelNoCalSet & SıraNo.ToString
            LabelNoCal = CLng(LabelNoCalSet)
            Return LabelNoCal

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    Public Sub LogOn(ByVal Rpt As String, _
        Optional ByVal FormType As enumReportType = enumReportType.Summary)
        Try

            MessageType = enumMessageType.SystemHatası

            Dim table As Table
            Dim logonInfo As CrystalDecisions.Shared.TableLogOnInfo

            If FormType = enumReportType.Summary Then
                frmRaporOzet.reportDocument1 = New ReportDocument
                frmRaporOzet.reportDocument1.Load(System.AppDomain.CurrentDomain.BaseDirectory & Rpt)
                frmRaporOzet.reportDocument1.SetDatabaseLogon(sUId, sPassword, sDataSource, sInitialCatalog)
                For Each table In frmRaporOzet.reportDocument1.Database.Tables
                    ' logonInfo = table.LogOnInfo
                    logonInfo = New TableLogOnInfo
                    logonInfo.ConnectionInfo.ServerName = sDataSource
                    logonInfo.ConnectionInfo.DatabaseName = sInitialCatalog
                    logonInfo.ConnectionInfo.UserID = sUId
                    logonInfo.ConnectionInfo.Password = sPassword
                    '' logonInfo.TableName = "ZED_SDK_FATURA_RPT_SP;1"
                    table.ApplyLogOnInfo(logonInfo)
                Next
            ElseIf FormType = enumReportType.Detail Then
                frmRaporOzet.reportDocument1 = New ReportDocument
                frmRaporOzet.reportDocument1.Load(System.AppDomain.CurrentDomain.BaseDirectory & Rpt)
                frmRaporOzet.reportDocument1.SetDatabaseLogon(sUId, sPassword, sDataSource, sInitialCatalog)
                For Each table In frmRaporOzet.reportDocument1.Database.Tables
                    ' logonInfo = table.LogOnInfo
                    logonInfo = New TableLogOnInfo
                    logonInfo.ConnectionInfo.ServerName = sDataSource
                    logonInfo.ConnectionInfo.DatabaseName = sInitialCatalog
                    logonInfo.ConnectionInfo.UserID = sUId
                    logonInfo.ConnectionInfo.Password = sPassword
                    '' logonInfo.TableName = "ZED_SDK_FATURA_RPT_SP;1"
                    table.ApplyLogOnInfo(logonInfo)
                Next
            End If

            If FormType = enumReportType.Summary Then

                For Each sec As Section In frmRaporOzet.reportDocument1.ReportDefinition.Sections
                    For Each ro As ReportObject In sec.ReportObjects
                        If ro.Kind = ReportObjectKind.SubreportObject Then
                            Dim subRepo As SubreportObject
                            Dim subRep As ReportDocument
                            subRepo = ro
                            subRep = subRepo.OpenSubreport(subRepo.SubreportName)
                            '
                            If SetDataBaseLogon Then
                                Dim lgInfo As TableLogOnInfo
                                'Dim lgInfos As New TableLogOnInfos
                                '
                                subRep.SetDatabaseLogon(sUId, sPassword, sDataSource, sInitialCatalog)
                                '
                                '  subRep.Refresh()
                                '
                                For Each tb As Table In subRep.Database.Tables
                                    '
                                    lgInfo = New TableLogOnInfo
                                    lgInfo.ConnectionInfo.DatabaseName = sInitialCatalog
                                    lgInfo.ConnectionInfo.ServerName = sDataSource
                                    lgInfo.ConnectionInfo.UserID = sUId
                                    lgInfo.ConnectionInfo.Password = sPassword
                                    '
                                    tb.ApplyLogOnInfo(lgInfo)
                                    If tb.Location <> sDataSource & ".sop." & tb.Name Then
                                        '
                                        If tb.Location.Split(".").Length > 1 Then
                                            tb.Location = sInitialCatalog & ".sop." & tb.Name  ' tb.Location.Remove(0, tb.Location.Split(".")(0).Length)
                                        End If
                                        '
                                    End If
                                    '
                                Next
                                '
                            End If
                        End If
                    Next
                Next

            ElseIf FormType = enumReportType.Detail Then

                For Each sec As Section In frmRaporOzet.reportDocument1.ReportDefinition.Sections
                    For Each ro As ReportObject In sec.ReportObjects
                        If ro.Kind = ReportObjectKind.SubreportObject Then
                            Dim subRepo As SubreportObject
                            Dim subRep As ReportDocument
                            subRepo = ro
                            subRep = subRepo.OpenSubreport(subRepo.SubreportName)
                            '
                            If SetDataBaseLogon Then
                                Dim lgInfo As TableLogOnInfo
                                'Dim lgInfos As New TableLogOnInfos
                                '
                                subRep.SetDatabaseLogon(sUId, sPassword, sDataSource, sInitialCatalog)
                                '
                                '  subRep.Refresh()
                                '
                                For Each tb As Table In subRep.Database.Tables
                                    '
                                    lgInfo = New TableLogOnInfo
                                    lgInfo.ConnectionInfo.DatabaseName = sInitialCatalog
                                    lgInfo.ConnectionInfo.ServerName = sDataSource
                                    lgInfo.ConnectionInfo.UserID = sUId
                                    lgInfo.ConnectionInfo.Password = sPassword
                                    '
                                    tb.ApplyLogOnInfo(lgInfo)
                                    If tb.Location <> sDataSource & ".sop." & tb.Name Then
                                        '
                                        If tb.Location.Split(".").Length > 1 Then
                                            tb.Location = sInitialCatalog & ".sop." & tb.Name  ' tb.Location.Remove(0, tb.Location.Split(".")(0).Length)
                                        End If
                                        '
                                    End If
                                    '
                                Next
                                '
                            End If
                        End If
                    Next
                Next

            End If

            If FormType = enumReportType.Summary Then
                frmRaporOzet.CrystalReportViewer1.ReportSource = frmRaporOzet.reportDocument1
            ElseIf FormType = enumReportType.Detail Then
                frmRaporOzet.CrystalReportViewer1.ReportSource = frmRaporOzet.reportDocument1
            End If

        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    'Bu kısımda ise database baglanırken şifre sormayı pas ediyorsunönce ReportDocument1 diye bir components ekliyorsun
    'eger  crystal tarafında database logon oluyorsun bu işlemide yapmalısın.
    Public Sub LogOnDatabase(ByVal Rpt As String, _
        Optional ByVal FormType As enumReportType = enumReportType.Summary)
        'Application.StartupPath
        'System.AppDomain.CurrentDomain.BaseDirectory
        Try

            MessageType = enumMessageType.SystemHatası

            Dim table As Table
            Dim logonInfo As CrystalDecisions.Shared.TableLogOnInfo

            If FormType = enumReportType.Summary Then
                frmRaporOzet.reportDocument1 = New ReportDocument
                frmRaporOzet.reportDocument1.Load(System.AppDomain.CurrentDomain.BaseDirectory & Rpt)
                frmRaporOzet.reportDocument1.SetDatabaseLogon(sUId, sPassword, sDataSource, sInitialCatalog)

                For Each table In frmRaporOzet.reportDocument1.Database.Tables
                    ' logonInfo = table.LogOnInfo
                    logonInfo = New TableLogOnInfo
                    logonInfo.ConnectionInfo.ServerName = sDataSource
                    logonInfo.ConnectionInfo.DatabaseName = sInitialCatalog
                    logonInfo.ConnectionInfo.UserID = sUId
                    logonInfo.ConnectionInfo.Password = sPassword
                    '' logonInfo.TableName = "ZED_SDK_FATURA_RPT_SP;1"
                    table.ApplyLogOnInfo(logonInfo)
                Next

            ElseIf FormType = enumReportType.Detail Then
                frmRaporOzet.reportDocument1 = New ReportDocument
                frmRaporOzet.reportDocument1.Load(System.AppDomain.CurrentDomain.BaseDirectory & Rpt)
                frmRaporOzet.reportDocument1.SetDatabaseLogon(sUId, sPassword, sDataSource, sInitialCatalog)

                For Each table In frmRaporOzet.reportDocument1.Database.Tables
                    ' logonInfo = table.LogOnInfo
                    logonInfo = New TableLogOnInfo
                    logonInfo.ConnectionInfo.ServerName = sDataSource
                    logonInfo.ConnectionInfo.DatabaseName = sInitialCatalog
                    logonInfo.ConnectionInfo.UserID = sUId
                    logonInfo.ConnectionInfo.Password = sPassword
                    '' logonInfo.TableName = "ZED_SDK_FATURA_RPT_SP;1"
                    table.ApplyLogOnInfo(logonInfo)
                Next

            End If

            If FormType = enumReportType.Summary Then
                frmRaporOzet.CrystalReportViewer1.ReportSource = frmRaporOzet.reportDocument1
            ElseIf FormType = enumReportType.Detail Then
                frmRaporOzet.CrystalReportViewer1.ReportSource = frmRaporOzet.reportDocument1
            End If

        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Public Function PaletLabelNoCal(ByVal SıraNo As Integer) As Long
        Try

            MessageType = enumMessageType.SystemHatası

            Dim PaletLabelNoCalSet As String
            Dim ColCnt As Integer

            PaletLabelNoCalSet = "5" & DateTime.Now.Year.ToString.Substring(2, 2)

            For ColCnt = 1 To 10 - (Len(PaletLabelNoCalSet) + Len(SıraNo.ToString))
                PaletLabelNoCalSet = PaletLabelNoCalSet & "0"
            Next ColCnt

            PaletLabelNoCalSet = PaletLabelNoCalSet & SıraNo.ToString
            PaletLabelNoCal = CLng(PaletLabelNoCalSet)
            Return PaletLabelNoCal

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    Public Function RandomNumber() As String
        Dim a As New Random
        Return a.Next
    End Function

    Public Function SapConvertDate(ByVal DateTime As Date) As String
        Try

            MessageType = enumMessageType.SystemHatası

            Return DateTime.Year.ToString & Right("0" & DateTime.Month.ToString, 2) & Right("0" & DateTime.Day.ToString, 2)

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    Public Function SapTodayDate() As String
        Try

            MessageType = enumMessageType.SystemHatası

            Return Now.Year.ToString & Right("0" & Now.Month.ToString, 2) & Right("0" & Now.Day.ToString, 2)

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    Public Function SapTodayTime() As String
        Try

            MessageType = enumMessageType.SystemHatası

            Return Right("0" & Now.Hour.ToString, 2) & Right("0" & Now.Minute.ToString, 2) & Right("0" & Now.Second.ToString, 2)

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    Public Function SpecialCharDelete(ByVal Word As String) As String
        Try
            MessageType = enumMessageType.SystemHatası

            Dim WordClear As String = Word

            WordClear = WordClear.Replace("'", String.Empty)

            Return WordClear

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    Public Function TeraziIpAdress(ByVal TeraziKodu As String) As String
        Try
            MessageType = enumMessageType.SystemHatası

            TeraziKodu = UCase(TeraziKodu)
            If TeraziKodu = "T00001" Then
                Return T01
            ElseIf TeraziKodu = "T00002" Then
                Return T02
            ElseIf TeraziKodu = "T00003" Then
                Return T03
            ElseIf TeraziKodu = "T00004" Then
                Return T04
                'ElseIf TeraziKodu = "T00005" Then
                'Return T05
            Else
                Return String.Empty
            End If

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    #End Region 'Methods

    #Region "Nested Types"

    Public Structure MalzemeEtiketBasımı

        #Region "Fields"

        Dim Blandcat As String
        Dim Boxqty As Decimal
        Dim Crtuser As String
        Dim Docno As String
        Dim Expdate As String
        Dim Issuedqty As Decimal
        Dim Itdsc As String
        Dim Itnbr As String
        Dim Labeltyp As String
        Dim Location As String
        Dim Lotno As String
        Dim Mandt As String
        Dim Manufacturer As String
        Dim Ordno As String
        Dim Packageid As String
        Dim Recdate As String
        Dim Status As String
        Dim Syrupid As String
        Dim Tdate As String
        Dim Totalqty As Decimal
        Dim Trnno As String
        Dim Ttime As String
        Dim Unmsr As String
        Dim Vname As String
        Dim Vndnr As String

        #End Region 'Fields

        #Region "Other"

        'Dim Matnr As String

        #End Region 'Other

    End Structure

    #End Region 'Nested Types

    #Region "Other"

    'Dim _AccessConnectionString As String
    'Public ReadOnly Property AccessConnectionString() As String
    '    Get
    '        Return _AccessConnectionString
    '    End Get
    'End Property

    #End Region 'Other

End Module