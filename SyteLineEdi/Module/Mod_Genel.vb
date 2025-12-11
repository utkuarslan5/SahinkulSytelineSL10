Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic

Module Mdl_Genel

    #Region "Fields"

    Public Const sFormat0 As String = "0"
    Public Const sFormat1 As String = "0.0"
    Public Const sFormat2 As String = "0.00"
    Public Const sFormat3 As String = "0.000"
    Public Const sFormat4 As String = "0.0000"

    Public BuLdu As Boolean
    Public dKagGenCm As Double
    Public dKagYukCm As Double
    Public TbaKiye As Decimal 'bora bakiye toplamý için
    Public TytL As Decimal 'bora ytl toplamý için

    #End Region 'Fields

    #Region "Methods"

    Public Function Conv(ByRef N As Double) As String
        Dim l, r As Double

        Dim s As String = ""

        Conv = ""

        If N > 0 Then

            GetStr(N, l, r, s)

            If l = 0 And r = 0 Then

                Conv = s

            Else

                Conv = Conv(l) & s & Conv(r)

            End If

        End If

        Return Conv
    End Function

    Public Sub errLogger(ByVal lNum As Integer, ByVal sDesc As String, ByVal sQuery As String, ByVal sForm As String)
        Dim FileNum As Short
        FileNum = FreeFile()
        FileOpen(FileNum, My.Application.Info.DirectoryPath & "\Errors.log", OpenMode.Append)
        WriteLine(FileNum, lNum, sDesc, sQuery, sForm, Now)
        FileClose(FileNum)
    End Sub

    Public Declare Function GetPrivateProfileString Lib "Kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer

    Public Declare Function GetSystemDirectory Lib "Kernel32" Alias "GetSystemDirectoryA" (ByVal lpBuffer As String, ByVal nSize As Integer) As Integer

    Public Sub PrintPicture(ByRef ObjOut As Object, ByRef sPicture As System.Drawing.Image, ByRef sX1 As Object, ByRef sY1 As Double)
        Dim ICarpan As Integer
        If dKagYukCm > 0 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.Height. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            ICarpan = Int(ObjOut.Height / dKagYukCm)
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.Width. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            ICarpan = Int(ObjOut.Width / dKagGenCm)
        End If
        '    ICarpan = Int(ObjOut.Height / dKagYukCm)
        '    ICarpan = 567
        'UPGRADE_WARNING: Couldn't resolve default property of object sX1. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        sX1 = sX1 * ICarpan
        sY1 = sY1 * ICarpan
        'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.PaintPicture. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        ObjOut.PaintPicture(sPicture, sX1, sY1)
    End Sub

    Public Sub PrintText(ByRef ObjOut As Object, ByRef LMarg As Object, ByRef TMarg As Object, ByRef RWid As Double, ByRef sFontName As String, ByRef bFontSize As Byte, ByRef InptStr As String, Optional ByRef bBold As Boolean = False, Optional ByRef bJustify As Byte = 0, Optional ByRef bUnderLine As Boolean = False)
        'bJustify 0 left, 1 Middle, 2 Right
        Dim ITxtLen As Object
        Dim ICarpan As Integer
        Dim TMargTemp As Double

        'UPGRADE_WARNING: Couldn't resolve default property of object TMarg. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        TMargTemp = TMarg
        If dKagYukCm > 0 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.Height. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            ICarpan = Int(ObjOut.Height / dKagYukCm)
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.Width. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            ICarpan = Int(ObjOut.Width / dKagGenCm)
        End If
        '    ICarpan = Int(ObjOut.Height / dKagYukCm)
        '    ICarpan = 567
        'UPGRADE_WARNING: Couldn't resolve default property of object LMarg. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        LMarg = LMarg * ICarpan
        'UPGRADE_WARNING: Couldn't resolve default property of object TMarg. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        TMarg = TMarg * ICarpan
        RWid = RWid * ICarpan

        'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.FontName. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        ObjOut.FontName = sFontName
        'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.FontSize. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        ObjOut.FontSize = bFontSize
        'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.FontBold. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        ObjOut.FontBold = bBold
        'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.FontUnderline. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        ObjOut.FontUnderline = bUnderLine
        'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.CurrentY. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        'UPGRADE_WARNING: Couldn't resolve default property of object TMarg. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        ObjOut.CurrentY = TMarg
        Select Case bJustify
            Case 0
                'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.TextWidth. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                While (ObjOut.TextWidth(InptStr) > RWid)
                    InptStr = Left(InptStr, Len(InptStr) - 1)
                End While
                'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.CurrentX. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object LMarg. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                ObjOut.CurrentX = LMarg
            Case 1
                'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.CurrentX. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.TextWidth. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object LMarg. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                ObjOut.CurrentX = Int(LMarg + Int((RWid - (ObjOut.TextWidth(InptStr))) / 2))
            Case 2
                'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.CurrentX. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.TextWidth. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object LMarg. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                ObjOut.CurrentX = LMarg + (RWid - ObjOut.TextWidth(InptStr))
        End Select
        'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.Print. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        ObjOut.Print(InptStr)
        'UPGRADE_WARNING: Couldn't resolve default property of object TMarg. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        TMarg = TMargTemp
    End Sub

    Public Function StringFillLeftChar(ByVal Value As String, ByVal Length As Short, ByVal pad_text As String) As String
        StringFillLeftChar = Right(New String(pad_text, Length) & Value, Length)
    End Function

    Public Function StringFillRightChar(ByVal Value As String, ByVal Length As Short, ByVal pad_text As String) As String
        StringFillRightChar = Left(Value & New String(pad_text, Length), Length)
    End Function

    Public Sub sWordWrap(ByRef ObjOut As Object, ByRef LMarg As Object, ByRef TMarg As Object, ByRef RgWid As Object, ByRef RgHgt As Double, ByRef InptStr As String, Optional ByRef sFontName As String = "Arial TUR", Optional ByRef bFontSize As Byte = 9, Optional ByRef bBold As Boolean = False, Optional ByRef bJustify As Byte = 0, Optional ByRef bUnderLine As Boolean = False)
        'bJustify 0 left, 1 Middle, 2 Right
        Dim IEndPos, IStrtPos, ITxtLen As Object
        Dim ITxtHgt As Integer
        Dim sPrntLn As Object
        Dim sPrntIn As String
        Dim ICarpan As Integer

        If dKagYukCm > 0 Then
            ICarpan = Int(ObjOut.Height / dKagYukCm)
        Else
            ICarpan = Int(ObjOut.Width / dKagGenCm)
        End If

        LMarg = LMarg * ICarpan
        TMarg = TMarg * ICarpan
        RgWid = RgWid * ICarpan
        RgHgt = RgHgt * ICarpan

        sPrntIn = InptStr
        ObjOut.CurrentY = TMarg
        ObjOut.FontName = sFontName
        ObjOut.FontSize = bFontSize
        ObjOut.FontBold = bBold
        ObjOut.FontUnderline = bUnderLine
        ITxtHgt = ObjOut.TextHeight(sPrntIn)

        Do

            IEndPos = 0

            ITxtLen = 0

            sPrntLn = vbNullString
            Do

                IStrtPos = IEndPos + 1

                IEndPos = InStr(IStrtPos, sPrntIn, " ")

                sPrntLn = Left(sPrntIn, IEndPos)

                ITxtLen = ObjOut.TextWidth(sPrntLn)
            Loop Until (ITxtLen > (RgWid - 10)) Or (IEndPos = 0)

            If IEndPos = 0 Then

                sPrntLn = sPrntIn

                sPrntIn = vbNullString

            Else
                sPrntLn = Left(sPrntIn, IStrtPos - 1)
                sPrntIn = LTrim(Mid(sPrntIn, IStrtPos))
            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object TMarg. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.CurrentY. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            If (ObjOut.CurrentY + ITxtHgt) <= (RgHgt + TMarg) Then
                'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.CurrentX. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object LMarg. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                ObjOut.CurrentX = LMarg
                'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.Print. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                ObjOut.Print(sPrntLn)
            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object TMarg. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object ObjOut.CurrentY. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object IStrtPos. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        Loop While (Len(sPrntIn) > 0) And (IStrtPos > 1) And ((ObjOut.CurrentY + ITxtHgt) <= (RgHgt + TMarg))
    End Sub

    Public Declare Function WritePrivateProfileString Lib "Kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Object, ByVal lpString As Object, ByVal lpFileName As String) As Integer

    Function AyAdi(ByRef bAySiraNo As Byte) As String
        AyAdi = vbNullString
        Select Case bAySiraNo
            Case 1 : AyAdi = "Ocak"
            Case 2 : AyAdi = "Þubat"
            Case 3 : AyAdi = "Mart"
            Case 4 : AyAdi = "Nisan"
            Case 5 : AyAdi = "Mayýs"
            Case 6 : AyAdi = "Haziran"
            Case 7 : AyAdi = "Temmuz"
            Case 8 : AyAdi = "Aðustos"
            Case 9 : AyAdi = "Eylül"
            Case 10 : AyAdi = "Ekim"
            Case 11 : AyAdi = "Kasým"
            Case 12 : AyAdi = "Aralýk"
        End Select
    End Function

    Function DateFormatToDate(ByRef sDate As String) As Date
        Dim sYear, sMonth As Object
        Dim sDay As String

        If Len(sDate) = 7 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object sYear. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            sYear = "20" & Mid(sDate, 2, 2)
            'UPGRADE_WARNING: Couldn't resolve default property of object sMonth. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            sMonth = Mid(sDate, 4, 2)
            sDay = Mid(sDate, 6, 2)
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object sYear. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            sYear = "19" & Mid(sDate, 1, 2)
            'UPGRADE_WARNING: Couldn't resolve default property of object sMonth. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            sMonth = Mid(sDate, 3, 2)
            sDay = Mid(sDate, 5, 2)
        End If
        'UPGRADE_WARNING: Couldn't resolve default property of object sYear. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        'UPGRADE_WARNING: Couldn't resolve default property of object sMonth. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        If IsDate(sDay & "." & sMonth & "." & sYear) = False Then
            'UPGRADE_WARNING: Couldn't resolve default property of object sYear. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object sMonth. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            If InStr(CStr(Today), "/") > 0 Then DateFormatToDate = CDate(sDay & "/" & sMonth & "/" & sYear)
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object sYear. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object sMonth. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            DateFormatToDate = CDate(sDay & "." & sMonth & "." & sYear)
        End If
    End Function

    Function DateToDateFormat(ByRef dDate As Date) As String
        Dim sYear, sDate, sMonth As Object
        Dim sDay As String
        DateToDateFormat = vbNullString

        'UPGRADE_WARNING: Couldn't resolve default property of object sYear. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        sYear = Year(dDate)
        'UPGRADE_WARNING: Couldn't resolve default property of object sMonth. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        sMonth = Month(dDate)
        sDay = CStr(VB.Day(dDate))
        'UPGRADE_WARNING: Couldn't resolve default property of object sDate. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        sDate = vbNullString

        'UPGRADE_WARNING: Couldn't resolve default property of object sYear. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        If CDbl(Left(sYear, 2)) = 20 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object sYear. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object sDate. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            sDate = "1" & Right(sYear, 2)
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object sYear. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object sDate. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            sDate = Right(sYear, 2)
        End If

        If Len(sMonth) < 2 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object sMonth. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            sMonth = "0" & sMonth
        End If
        If Len(sDay) < 2 Then
            sDay = "0" & sDay
        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object sMonth. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        'UPGRADE_WARNING: Couldn't resolve default property of object sDate. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        sDate = sDate & sMonth & sDay
        'UPGRADE_WARNING: Couldn't resolve default property of object sDate. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        DateToDateFormat = sDate
    End Function

    Function FileExists(ByRef strFile As String) As Short
        Dim lSize As Integer

        On Error Resume Next

        lSize = -1
        lSize = FileLen(strFile)
        If lSize = 0 Then
            FileExists = 0
        ElseIf lSize > 0 Then
            FileExists = 1
        Else
            FileExists = -1
        End If
    End Function

    Private Sub GetStr(ByRef N As Double, ByRef l As Double, ByRef r As Double, ByRef st As String)
        l = 0
        r = 0
        If N >= 1.0E+15 Then
            st = "Katrilyon"
            r = N - (l * 1.0E+15)
        ElseIf N >= 1000000000000.0# Then
            st = "Trilyon"
            l = Int(N / 1000000000000.0#)
            r = N - (l * 1000000000000.0#)
        ElseIf N >= 1000000000.0# Then
            st = "Milyar"
            l = Int(N / 1000000000.0#)
            r = N - (l * 1000000000.0#)
        ElseIf N >= 1000000.0# Then
            st = "Milyon"
            l = Int(N / 1000000)
            r = N - (l * 1000000.0#)
        ElseIf N >= 1000.0# Then
            st = "Bin"
            l = Int(N / 1000)
            If l = 1 Then l = 0
            'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            r = N Mod 1000.0#
        ElseIf N >= 100.0# Then
            st = "Yüz"
            l = Int(N / 100)
            If l = 1 Then l = 0
            'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            r = N Mod 100.0#
        ElseIf N >= 90.0# Then
            st = "Doksan"
            l = 0
            'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            r = N Mod 90.0#
        ElseIf N >= 80.0# Then
            st = "Seksen"
            l = 0
            'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            r = N Mod 80.0#
        ElseIf N >= 70.0# Then
            st = "Yetmiþ"
            l = 0
            'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            r = N Mod 70.0#
        ElseIf N >= 60.0# Then
            st = "Altmýþ"
            l = 0
            'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            r = N Mod 60.0#
        ElseIf N >= 50.0# Then
            st = "Elli"
            l = 0
            'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            r = N Mod 50.0#
        ElseIf N >= 40.0# Then
            st = "Kýrk"
            l = 0
            'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            r = N Mod 40.0#
        ElseIf N >= 30.0# Then
            st = "Otuz"
            l = 0
            'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            r = N Mod 30.0#
        ElseIf N >= 20.0# Then
            st = "Yirmi"
            l = 0
            'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            r = N Mod 20.0#
        ElseIf N >= 10.0# Then
            st = "On"
            l = 0
            'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            r = N Mod 10.0#
        ElseIf N = 9 Then
            st = "Dokuz"
        ElseIf N = 8 Then
            st = "Sekiz"
        ElseIf N = 7 Then
            st = "Yedi"
        ElseIf N = 6 Then
            st = "Altý"
        ElseIf N = 5 Then
            st = "Beþ"
        ElseIf N = 4 Then
            st = "Dört"
        ElseIf N = 3 Then
            st = "Üç"
        ElseIf N = 2 Then
            st = "Ýki"
        ElseIf N = 1 Then
            st = "Bir"
        End If
    End Sub

    Function HesapAdi(ByRef cConn As ADODB.Connection, ByRef sHesapNo As String, Optional ByRef sLibraryName As String = vbNullString) As String
        Dim Connection_ExecuteAs400 As Object
        Dim ResSQL As New ADODB.Recordset
        Dim sEK As String

        If sLibraryName = vbNullString Then sEK = vbNullString Else sEK = sLibraryName & "."

        ResSQL = Connection_ExecuteAs400("select AHADNA" & " from " & sEK & "YAAHREP" & " where AHAFCD='" & sHesapNo & "'")

        If ResSQL.EOF = True Then
            HesapAdi = vbNullString
        Else
            HesapAdi = Trim(ResSQL.Fields("AHADNA").Value)
        End If
    End Function

    Function KarakterEkle(ByRef sTEXT As String, ByRef iTotCharCount As Short, Optional ByRef sChar As String = " ", Optional ByRef bRight As Boolean = True) As String
        Dim i As Short
        If Len(sTEXT) >= iTotCharCount Then
            KarakterEkle = Left(sTEXT, iTotCharCount)
            Exit Function
        End If
        If bRight Then
            For i = 1 To (iTotCharCount - Len(sTEXT))
                sTEXT = sTEXT & sChar
            Next
        Else
            For i = 1 To (iTotCharCount - Len(sTEXT))
                sTEXT = sChar & sTEXT
            Next
        End If
        KarakterEkle = sTEXT
    End Function

    Function KuraCevir(ByRef cConn As ADODB.Connection, ByRef sHoldingNo As Object, ByRef sFromPC As Object, ByRef sToPC As Object, ByRef dDate As String, ByRef dTutar As Double, Optional ByRef sLibraryName As String = vbNullString, Optional ByRef bGCarpBol As Byte = 0) As Double
        Dim Connection_ExecuteAs400 As Object
        Dim ResSQL As New ADODB.Recordset
        Dim sEK As String
        Dim bCarpBolen As Byte

        If sLibraryName = vbNullString Then sEK = vbNullString Else sEK = sLibraryName & "."

        If bGCarpBol = 0 Then
            ' Kur carpan mi, bolen mi?
            ResSQL = Connection_ExecuteAs400("select BLFTST from " & sEK & "YABLREP" & " where BLASCD ='" & sHoldingNo & "'" & " and BLF3CD ='MAPICS'" & " and BLI5CD ='" & sFromPC & "'" & " and BLI6CD ='" & sToPC & "'")
            bCarpBolen = 0
            If ResSQL.EOF = False Then
                bCarpBolen = ResSQL.Fields("BLFTST").Value
            End If
        Else
            bCarpBolen = bGCarpBol
        End If

        ResSQL = Connection_ExecuteAs400("select BKD5NB from " & sEK & "YABKREP" & " where BKI5CD='" & sFromPC & "'" & " and BKI6CD='" & sToPC & "'" & " and BKA6DT= (select max(BKA6DT) from " & sEK & "YABKREP" & " where BKI5CD='" & sFromPC & "'" & " and BKI6CD='" & sToPC & "'" & " and BKA6DT<=" & dDate & ")")
        KuraCevir = 0

        If ResSQL.EOF = False Then
            If bCarpBolen = 1 Then
                KuraCevir = dTutar * ResSQL.Fields("BKD5NB").Value
            ElseIf bCarpBolen = 2 Then
                KuraCevir = dTutar / ResSQL.Fields("BKD5NB").Value
            End If
        End If
    End Function

    Function KurGetir(ByRef cConn As ADODB.Connection, ByRef sFromPC As Object, ByRef sToPC As Object, ByRef dDate As String, Optional ByRef bYoksaKurGetir As Boolean = True, Optional ByRef bYoksaMsjVer As Boolean = False, Optional ByRef sLibraryName As String = vbNullString) As Double
        Dim Connection_ExecuteAs400 As Object
        Dim ResSQL As New ADODB.Recordset
        Dim sEK As String

        If sLibraryName = vbNullString Then sEK = vbNullString Else sEK = sLibraryName & "."

        'UPGRADE_WARNING: Couldn't resolve default property of object sToPC. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        'UPGRADE_WARNING: Couldn't resolve default property of object sFromPC. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        ResSQL = Connection_ExecuteAs400("select BKD5NB from " & sEK & "YABKREP" & " where BKI5CD='" & sFromPC & "'" & " and BKI6CD='" & sToPC & "'" & " and BKA6DT= " & dDate)

        If ResSQL.EOF = True Then
            If bYoksaKurGetir = True Then
                'UPGRADE_WARNING: Couldn't resolve default property of object sToPC. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object sFromPC. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                ResSQL = Connection_ExecuteAs400("select BKD5NB from " & sEK & "YABKREP" & " where BKI5CD='" & sFromPC & "'" & " and BKI6CD='" & sToPC & "'" & " and BKA6DT= (select max(BKA6DT) from " & sEK & "YABKREP" & " where BKI5CD='" & sFromPC & "'" & " and BKI6CD='" & sToPC & "'" & " and BKA6DT<=" & dDate & ")")
                If ResSQL.EOF = True Then
                    KurGetir = 0
                    'UPGRADE_WARNING: Couldn't resolve default property of object sToPC. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object sFromPC. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    If bYoksaMsjVer Then MsgBox(sFromPC & " PC den " & sToPC & " e " & DateFormatToDate(dDate) & " tarihinde kayýt bulunanadý")
                Else
                    KurGetir = ResSQL.Fields("BKD5NB").Value
                End If
            Else
                KurGetir = 0
                'UPGRADE_WARNING: Couldn't resolve default property of object sToPC. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object sFromPC. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If bYoksaMsjVer Then MsgBox(sFromPC & " PC den " & sToPC & " e " & DateFormatToDate(dDate) & " tarihinde kayýt bulunanadý")
            End If
        Else
            KurGetir = ResSQL.Fields("BKD5NB").Value
        End If
    End Function

    Function PBBoleni(ByRef cConn As ADODB.Connection, ByRef sPB As String, Optional ByRef sLibraryName As String = vbNullString) As Byte
        Dim Connection_ExecuteAs400 As Object
        Dim ResSQL As New ADODB.Recordset
        Dim sEK As String

        If sLibraryName = vbNullString Then sEK = vbNullString Else sEK = sLibraryName & "."

        ResSQL = Connection_ExecuteAs400("select AECJST from " & sEK & "YAAEREP" & " where AEACCD='" & sPB & "'")
        If ResSQL.EOF = True Then
            PBBoleni = 1
        Else
            Select Case ResSQL.Fields("AECJST").Value
                Case 0
                    PBBoleni = 1
                Case 1
                    PBBoleni = 10
                Case 2
                    PBBoleni = 100
            End Select
        End If
    End Function

    Function SirketPC(ByRef cConn As ADODB.Connection, ByRef sHoldingNo As Object, ByRef sSirketNo As String, Optional ByRef sLibraryName As String = vbNullString) As String
        Dim Connection_ExecuteAs400 As Object
        Dim ResSQL As New ADODB.Recordset
        Dim sEK As String

        If sLibraryName = vbNullString Then sEK = vbNullString Else sEK = sLibraryName & "."

        'UPGRADE_WARNING: Couldn't resolve default property of object sHoldingNo. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        ResSQL = Connection_ExecuteAs400("select C3IXCD" & " from " & sEK & "YAC3REP" & " where C3ASCD='" & sHoldingNo & "'" & " and C3F7CD='" & sSirketNo & "'")

        If ResSQL.EOF = True Then
            SirketPC = vbNullString
        Else
            SirketPC = Trim(ResSQL.Fields("C3IXCD").Value)
        End If
    End Function

    Function TarafAdi(ByRef cConn As ADODB.Connection, ByRef sTaraf As String, Optional ByRef sLibraryName As String = vbNullString) As String
        Dim Connection_ExecuteAs400 As Object
        Dim ResSQL As New ADODB.Recordset
        Dim sEK As String

        If sLibraryName = vbNullString Then sEK = vbNullString Else sEK = sLibraryName & "."

        ResSQL = Connection_ExecuteAs400("select AFAKNA from " & sEK & "YAAFREP" & " where AFADCD='" & sTaraf & "'")
        If ResSQL.EOF = True Then
            TarafAdi = vbNullString
        Else
            TarafAdi = Trim(ResSQL.Fields("AFAKNA").Value)
        End If
    End Function

    Function YaziIle(ByRef dDeger As Double) As String
        YaziIle = vbNullString

        If dDeger < 0 Then dDeger = dDeger * -1
        If (dDeger - Int(dDeger)) = 0 Then
            YaziIle = Conv(dDeger)
        Else
            YaziIle = Conv(Int(dDeger)) & " / " & CDbl(VB.Format(dDeger - Int(dDeger), sFormat2)) * 100
        End If
    End Function

    Function YaziIle2(ByRef dDeger As Double) As String
        YaziIle2 = vbNullString

        If dDeger < 0 Then dDeger = dDeger * -1
        If (dDeger - Int(dDeger)) = 0 Then
            YaziIle2 = Conv(dDeger)
        Else
            YaziIle2 = Conv(Int(dDeger)) & " % " & CDbl(VB.Format(dDeger - Int(dDeger), sFormat2)) * 100
        End If
    End Function

    #End Region 'Methods

End Module