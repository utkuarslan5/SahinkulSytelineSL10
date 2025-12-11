Imports System.Drawing.Printing
Imports System.IO
Imports System.Runtime.InteropServices

Public Class RawPrinterHelper

    #Region "Methods"

    <DllImport("winspool.Drv", EntryPoint:="ClosePrinter", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function ClosePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="EndDocPrinter", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function EndDocPrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="EndPagePrinter", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function EndPagePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="OpenPrinterW", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function OpenPrinter(ByVal src As String, ByRef hPrinter As IntPtr, ByVal pd As Long) As Boolean
    End Function

    ' SendBytesToPrinter()
    ' İşleve bir yazıcı adı ve yönetilmeyen bir bayt dizisi verildiğinde,
    ' işlev bu baytları yazdırma sırasına gönderir.
    ' Başarılı olduğunda True, hata durumunda False döndürür.
    Public Shared Function SendBytesToPrinter(ByVal szPrinterName As String, ByVal pBytes As IntPtr, ByVal dwCount As Int32) As Boolean
        Dim hPrinter As IntPtr      ' Yazıcı tanıtıcı.
        Dim dwError As Int32        ' Son hata - bir sorun ortaya çıkarsa.
        Dim di As DOCINFOW          ' Belgenizi tanımlar (ad, bağlantı noktası, veri türü).
        Dim dwWritten As Int32      ' WritePrinter() tarafından yazılan bayt sayısı.
        Dim bSuccess As Boolean     ' Başarı kodunuz.

        ' DOCINFO yapısını hazırla.
        With di
            .pDocName = "Visual Basic .NET RAW Belgem"
            .pDataType = "RAW"
        End With
        ' Başarılı olduğu belirlenmedikçe başarısız olduğunu varsay.
        bSuccess = False
        If OpenPrinter(szPrinterName, hPrinter, 0) Then
            If StartDocPrinter(hPrinter, 1, di) Then
                If StartPagePrinter(hPrinter) Then
                    ' Yazıcıya özel baytları yazıcıya yaz.
                    bSuccess = WritePrinter(hPrinter, pBytes, dwCount, dwWritten)
                    EndPagePrinter(hPrinter)
                End If
                EndDocPrinter(hPrinter)
            End If
            ClosePrinter(hPrinter)
        End If
        ' Başarılı olmadıysa, GetLastError neden başarısız olunduğu
        ' hakkında daha fazla bilgi verebilir.
        If bSuccess = False Then
            dwError = Marshal.GetLastWin32Error()
        End If
        Return bSuccess
    End Function

    ' SendFileToPrinter()
    ' İşleve bir dosya adı ve bir yazıcı adı verildiğinde,
    ' işlev dosyanın içeriğini okur ve
    ' içeriği yazıcıya gönderir.
    ' Dosyanın yazdırılmaya hazır veri içerdiğini kabul eder.
    ' SendBytesToPrinter işlevinin nasıl kullanılacağını gösterir.
    ' Başarılı olduğunda True, hata durumunda False döndürür.
    Public Shared Function SendFileToPrinter(ByVal szPrinterName As String, ByVal szFileName As String) As Boolean
        ' Dosyayı aç.
        Dim fs As New FileStream(szFileName, FileMode.Open)
        ' Dosya için bir BinaryReader oluştur.
        Dim br As New BinaryReader(fs)
        ' Dosya içeriğini alacak kadar büyük bir bayt dizisi oluştur.
        Dim bytes(fs.Length) As Byte
        Dim bSuccess As Boolean
        ' Yönetilmeyen işaretçiniz.
        Dim pUnmanagedBytes As IntPtr

        ' Dosya içeriğini dizi içine oku.
        bytes = br.ReadBytes(fs.Length)
        ' Bu baytlar için bir miktar yönetilmeyen bellek ayır.
        pUnmanagedBytes = Marshal.AllocCoTaskMem(fs.Length)
        ' Yönetilen bayt dizini yönetilmeyen diziye kopyala.
        Marshal.Copy(bytes, 0, pUnmanagedBytes, fs.Length)
        ' Yönetilmeyen baytları yazıcıya gönder.
        bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, fs.Length)
        ' Daha önce ayrılan yönetilmeyen belleği serbest bırak.
        Marshal.FreeCoTaskMem(pUnmanagedBytes)
        Return bSuccess
    End Function

    ' İşleve bir dize ve bir yazıcı adı verildiğinde,
    ' işlev dizeyi ham bayt olarak yazıcıya gönderir.
    Public Shared Function SendStringToPrinter(ByVal szPrinterName As String, ByVal szString As String)
        Dim pBytes As IntPtr
        Dim dwCount As Int32
        ' Dizede kaç karakter var?
        dwCount = szString.Length()
        ' Yazıcının ANSI metin beklediğini kabul et ve
        ' dizeyi ANSI metne dönüştür.
        pBytes = Marshal.StringToCoTaskMemAnsi(szString)
        ' Dönüştürülmüş ANSI dizeyi yazıcıya gönder.
        SendBytesToPrinter(szPrinterName, pBytes, dwCount)
        Marshal.FreeCoTaskMem(pBytes)
    End Function

    <DllImport("winspool.Drv", EntryPoint:="StartDocPrinterW", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function StartDocPrinter(ByVal hPrinter As IntPtr, ByVal level As Int32, ByRef pDI As DOCINFOW) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="StartPagePrinter", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function StartPagePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="WritePrinter", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function WritePrinter(ByVal hPrinter As IntPtr, ByVal pBytes As IntPtr, ByVal dwCount As Int32, ByRef dwWritten As Int32) As Boolean
    End Function

    #End Region 'Methods

    #Region "Nested Types"

    ' Yapı ve API bildirimleri:
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    Structure DOCINFOW

        <MarshalAs(UnmanagedType.LPWStr)> _
        Public pDocName As String
        <MarshalAs(UnmanagedType.LPWStr)> _
        Public pOutputFile As String
        <MarshalAs(UnmanagedType.LPWStr)> _
        Public pDataType As String

    End Structure

    #End Region 'Nested Types

End Class