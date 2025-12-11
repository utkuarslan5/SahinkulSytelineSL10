Imports System.Data.SqlClient

Public Class SaIadeFaturasi
    Inherits Core.Data

    #Region "Constructors"

    '
    Public Sub New(ByVal ConnectionString As String)
        MyBase.New(ConnectionString)
    End Sub

    #End Region 'Constructors

    #Region "Methods"

    '
    '
    Public Function CreateInvoice( _
        ByVal sVendor As String, _
        ByVal sInvNum As String, _
        ByVal invDate As DateTime, _
        ByVal dPurchAmt As Decimal, _
        ByVal dTaxAmount As Decimal, _
        ByVal SessionId As String, _
        ByVal sRef As String, _
        ByRef iVoucher As Integer) As Boolean
        '
        Try
          '@Vendor VendNumType,
          '@InvNum VendInvNumType,
          '@InvDate DateTime,
          '@PurchAmt AmountType,
          '@TaxAmount AMountTYpe,
          '@SessionID uniqueidentifier,
          '@Ref ReferenceType,
          '@Voucher int OUTPUT
          Dim p As New ArrayList
          '
          Dim px As New SqlClient.SqlParameter

          px.ParameterName = "@SessionID"
          px.DbType = DbType.String 'System.Data.SqlDbType.UniqueIdentifier
          px.Value = SessionId

          p.Add(px)
          '
          p.Add(New SqlParameter("@InvNum", sInvNum))

          p.Add(New SqlParameter("@Vendor", sVendor))
          p.Add(New SqlParameter("@InvDate", invDate))
          p.Add(New SqlParameter("@PurchAmt", dPurchAmt))
          p.Add(New SqlParameter("@TaxAmount", dTaxAmount))
          p.Add(New SqlParameter("@Ref", sRef))
          '
          Dim p2 As New SqlClient.SqlParameter
          p2.DbType = DbType.Int32
          p2.ParameterName = "@Voucher"
          p2.Direction = ParameterDirection.Output
          p.Add(p2)
          '
          If MyBase.RunSp("TUR_CreateApInvoiceSp", p, 1, False) = 0 Then
        '
        iVoucher = IIf(IsDBNull(p2.Value), 0, p2.Value)
        '
        Return True
          Else
        Return False
          End If
          '
        Catch ex As Exception
          '
          Me.Result.Add(ex.Message)
          Return False
        End Try
    End Function

    '
    Public Function EmptyApInvoice( _
        ByVal SessionId As String) As Boolean
        '
        Try
          Dim p As New ArrayList

          Dim px As New SqlClient.SqlParameter

          px.ParameterName = "@SessionID"
          px.DbType = DbType.String 'System.Data.SqlDbType.UniqueIdentifier
          px.Value = SessionId

          p.Add(px)
          '
            If MyBase.RunSp("TRM_IadeFaturasiTemizleSp", p, 1, False) = 0 Then
                Return True
            Else
                Return False
            End If
          '
        Catch ex As Exception
          '
          Me.Result.Add(ex.Message)
          Return False
        End Try
    End Function

    '
    Public Function FisOlustur( _
        ByVal PVendNum As String, _
        ByVal PVoucher As String, _
        ByVal PDistDate As DateTime, _
        ByVal PPurchAmt As Decimal, _
        ByVal PSalesTax As Decimal, _
        ByVal PInvNum As String, _
        ByVal PInvDate As DateTime, _
        ByVal PTermsCode As String, _
        ByVal PAuthorizer As String, _
        ByVal PProcessId As String, _
        ByRef PoVoucher As String, _
        ByRef Infobar As String, _
        Optional ByVal PVchAdj As String = "A", _
        Optional ByVal PPreRegister As String = "", _
        Optional ByVal PIsEdi As Integer = 0, _
        Optional ByVal PSeqNum As Integer = 0, _
        Optional ByVal PFreight As Decimal = 0, _
        Optional ByVal PMiscCharges As Decimal = 0, _
        Optional ByVal PSalesTax2 As Decimal = 0, _
        Optional ByVal PFixedRate As Integer = 0, _
        Optional ByVal PExchRate As Decimal = 0, _
        Optional ByVal PFormTitle As String = "", _
        Optional ByVal CalledFrom As String = "GenerateAPTransactions" _
        ) As Boolean
        '
        Try

          'DECLARE @PVendNum VendNumType
          'DECLARE @PVchAdj VchPrStatusType
          'DECLARE @PVoucher VoucherType
          'DECLARE @PDistDate DateType
          'DECLARE @PIsEdi FlagNyType
          'DECLARE @PSeqNum EdiSeqType
          'DECLARE @PPreRegister PreRegisterType
          'DECLARE @PPurchAmt AmountType
          'DECLARE @PFreight AmountType
          'DECLARE @PMiscCharges AmountType
          'DECLARE @PSalesTax AmountType
          'DECLARE @PSalesTax2 AmountType
          'DECLARE @PInvNum VendInvNumType
          'DECLARE @PInvDate DateType
          'DECLARE @PTermsCode TermsCodeType
          'DECLARE @PAuthorizer NameType
          'DECLARE @PFixedRate ListYesNoType
          'DECLARE @PExchRate ExchRateType
          'DECLARE @PFormTitle InfobarType
          'DECLARE @CalledFrom nvarchar(25)
          'DECLARE @PProcessId RowPointerType
          'DECLARE @PoVoucher VoucherType
          'DECLARE @Infobar InfobarType

          Dim p As New ArrayList
          '
          'Dim px As New SqlClient.SqlParameter

          'px.ParameterName = "@PProcessId"
          'px.SqlDbType = SqlDbType.NVarChar 'System.Data.SqlDbType.UniqueIdentifier
          'px.Value = PProcessId

          'p.Add(px)
          '
          p.Add(New SqlParameter("@PProcessId", PProcessId))
          p.Add(New SqlParameter("@PVendNum", PVendNum))
          p.Add(New SqlParameter("@PVchAdj", PVchAdj))
          p.Add(New SqlParameter("@PVoucher", PVoucher))
          p.Add(New SqlParameter("@PDistDate", PDistDate.ToString("yyyy-MM-dd")))
          p.Add(New SqlParameter("@PIsEdi", PIsEdi))
          p.Add(New SqlParameter("@PSeqNum", PSeqNum))
          p.Add(New SqlParameter("@PPreRegister", PPreRegister))
          p.Add(New SqlParameter("@PPurchAmt", PPurchAmt))
          p.Add(New SqlParameter("@PFreight", PFreight))
          p.Add(New SqlParameter("@PMiscCharges", PMiscCharges))
          p.Add(New SqlParameter("@PSalesTax", PSalesTax))
          p.Add(New SqlParameter("@PSalesTax2", PSalesTax2))
          p.Add(New SqlParameter("@PInvNum", PInvNum))
          p.Add(New SqlParameter("@PInvDate", PInvDate.ToString("yyyy-MM-dd")))
          p.Add(New SqlParameter("@PTermsCode", PTermsCode))
          p.Add(New SqlParameter("@PAuthorizer", PAuthorizer))
          p.Add(New SqlParameter("@PFixedRate", PFixedRate))
          p.Add(New SqlParameter("@PExchRate", PExchRate))
          p.Add(New SqlParameter("@PFormTitle", PFormTitle))
          p.Add(New SqlParameter("@CalledFrom", CalledFrom))

          '
          Dim p2 As New SqlClient.SqlParameter
          p2.SqlDbType = SqlDbType.Int
          p2.ParameterName = "@PoVoucher"
          p2.Direction = ParameterDirection.Output
          p.Add(p2)

          Dim p3 As New SqlClient.SqlParameter
          p3.SqlDbType = SqlDbType.NVarChar
          p3.ParameterName = "@Infobar"
          p3.Size = 4000
          p3.Direction = ParameterDirection.Output
          p.Add(p3)

          '
          'MyBase.RunSql("update tt_voucher" & _
          '                " Set connection_id = " & sTirnakEkle(PProcessId), True)

            If MyBase.RunSp("Tr_IadeFaturasiPost", p, 1, False) = 0 Then
                '
                PoVoucher = IIf(IsDBNull(p2.Value), 0, p2.Value)
                '
                Return True
            Else
                Return False
            End If
          '
        Catch ex As Exception
          '
          Me.Result.Add(ex.Message)
          Return False
        End Try
    End Function

    '
    Public Function loadRecords( _
        ByVal sVendNum As String, _
        ByVal sDocNum As String, _
        ByVal SessionId As String, _
        ByVal tarih As Date, _
        ByVal pcname As String, _
        ByVal voucher As Integer) As DataSet
        '
        Try
          '@Vendor VendNumType,
          '@DocNum DocumentNumType,
          '@SessionID uniqueidentifier,
          '@EmptyID ListYesNoType
          '
          Dim p As New ArrayList
          p.Add(New SqlParameter("@Vendor", sVendNum))
          p.Add(New SqlParameter("@DocNum", sDocNum))
          p.Add(New SqlParameter("@Tarih", tarih))
          p.Add(New SqlParameter("@PcName", pcname))
          p.Add(New SqlParameter("@VoucherNo", voucher))
          '
          Dim px As New SqlClient.SqlParameter

          px.ParameterName = "@SessionID"
          px.DbType = DbType.String 'System.Data.SqlDbType.UniqueIdentifier
          px.Value = SessionId

          p.Add(px)

          'Execute
            Return MyBase.RunSp("TrM_IadeFaturasiYukleSP", p, "TrM_IadeFaturasiYukleSP")
          '

        Catch ex As Exception
          Me.Result.Add(ex.Message)
          '
          Return Nothing
        End Try
        ''
    End Function

    '
    Public Function updateRecords( _
        ByVal SessionId As String, _
        ByVal iTransNum As Integer, _
        ByVal Amount As Decimal) As Boolean
        '
        Try
          Dim p As New ArrayList
          Dim px As New SqlClient.SqlParameter

          px.ParameterName = "@SessionID"
          px.DbType = DbType.String 'System.Data.SqlDbType.UniqueIdentifier
          px.Value = SessionId

          p.Add(px)

          p.Add(New SqlParameter("@Amount", Amount))
          p.Add(New SqlParameter("@TransNum", iTransNum))
          '
            If MyBase.RunSp("TRM_IadeFaturasiGuncelemeSP", p, 1, False) = 0 Then
                Return True
            Else
                Return False
            End If
          '
        Catch ex As Exception
          '
          Me.Result.Add(ex.Message)
          Return False
        End Try
    End Function

    #End Region 'Methods

End Class