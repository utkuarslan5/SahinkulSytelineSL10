Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text


Public Class DataTableHelper

    #Region "Methods"

    Public Shared Sub ExcelExportGeneral(ByVal dt As DataTable, ByVal DosyaAdi As String, ByVal i As Integer)
        Dim objExc As New Microsoft.Office.Interop.Excel.Application()

        If objExc Is Nothing Then

            Throw New Exception("Office Kurulumunu Kontrol Ediniz...")

        End If

        Dim oldCI As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")

        Dim objWorkBook As Microsoft.Office.Interop.Excel.Workbook

        objExc.Visible = False

        If IO.File.Exists(DosyaAdi) Then

            objWorkBook = objExc.Workbooks.Open(DosyaAdi)

        Else

            objWorkBook = objExc.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet)

        End If

        Dim objWorkSheet As Microsoft.Office.Interop.Excel.Worksheet = DirectCast(objWorkBook.Worksheets(1), Microsoft.Office.Interop.Excel.Worksheet)

        Try

            'objWorkSheet.Rows.Delete(False)
            'objWorkSheet.Columns.Delete(False)

            Dim j As Integer

            For j = 0 To dt.Columns.Count - 1 'grid.RootTable.Columns.Count - 1

                'If j = 0 Then

                If IsNumeric(dt.Rows(0).Item(j).ToString) Then

                    objExc.Sheets(1).Cells(i + 1, j + 1).Value = CDbl(dt.Rows(0).Item(j).ToString)

                ElseIf j = 11 Or j = 12 Then

                    objExc.Sheets(1).Cells(i + 1, j + 1).Value = DateSerial(dt.Rows(0).Item(j).ToString.Split(".")(2), _
                                                                            dt.Rows(0).Item(j).ToString.Split(".")(1), _
                                                                            dt.Rows(0).Item(j).ToString.Split(".")(0))

                Else

                    objExc.Sheets(1).Cells(i + 1, j + 1).Value = dt.Rows(0).Item(j).ToString

                End If

            Next j

            objExc.Sheets(1).Columns.Autofit()
            objExc.Sheets(1).Select()
            objExc.Range("A1").Select()

            'MessageBox.Show("EXCEL'e taşındı")

            'If IO.File.Exists(DosyaAdi) Then

            '    IO.File.Delete(DosyaAdi)

            'End If

            objExc.DisplayAlerts = False

            objWorkSheet.SaveAs(DosyaAdi)

            objExc.DisplayAlerts = True

            objExc.Workbooks.Close()

            objExc.Quit()

            objExc = Nothing

        Catch ex As Exception

            objExc.Workbooks.Close()

            objExc.Quit()

            objExc = Nothing

            Throw ex

        End Try
    End Sub

    Public Shared Function ExcelImportGeneral(ByVal DosyaAdi As String, _
        ByRef DosyaAdiMrp As String, _
        ByVal HareketTipi As String) As DataTable
        Dim sSatirNo As String = String.Empty
        Dim sMalzeme As String = String.Empty
        Dim sAmbar As String = String.Empty
        Dim sDokuman As String = String.Empty
        Dim sNedenKodu As String = String.Empty
        Dim sLokasyon As String = String.Empty
        Dim sLot As String = String.Empty
        Dim sMiktar As String = String.Empty

        Dim objExc As New Microsoft.Office.Interop.Excel.Application()

        If objExc Is Nothing Then

            Throw New Exception("Office Kurulumunu Kontrol Ediniz...")

        End If

        Dim oldCI As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")

        Dim objWorkBook As Microsoft.Office.Interop.Excel.Workbook = Nothing

        objExc.Visible = False

        objExc.DisplayAlerts = False

        If IO.File.Exists(DosyaAdi) Then

            objWorkBook = objExc.Workbooks.Open(DosyaAdi)

        End If

        'Debug.WriteLine("Sleeping...")
        'System.Threading.Thread.Sleep(5000)
        'Debug.WriteLine("End Excel")

        Dim objWorkSheet As Microsoft.Office.Interop.Excel.Worksheet = DirectCast(objWorkBook.Worksheets(1), Microsoft.Office.Interop.Excel.Worksheet)

        Dim table As New DataTable("Hurda")

        Try

            'objWorkSheet.Rows.Delete(False)
            'objWorkSheet.Columns.Delete(False)

            table.Columns.Add(New DataColumn("Satir_No", Type.GetType("System.String")))

            table.Columns.Add(New DataColumn("Malzeme", Type.GetType("System.String")))

            table.Columns.Add(New DataColumn("Lokasyon", Type.GetType("System.String")))

            table.Columns.Add(New DataColumn("Lot", Type.GetType("System.String")))

            table.Columns.Add(New DataColumn("Miktar", Type.GetType("System.String")))

            table.Columns.Add(New DataColumn("Ambar", Type.GetType("System.String")))

            table.Columns.Add(New DataColumn("Dokuman", Type.GetType("System.String")))

            table.Columns.Add(New DataColumn("NedenKodu", Type.GetType("System.String")))

            table.Columns.Add(New DataColumn("Kayit", Type.GetType("System.String")))

            Dim i As Integer
            Dim j As Integer
            'Dim sMalzeme As Malzeme = Nothing
            'For j = 0 To 38 'grid.RootTable.Columns.Count - 1

            For i = 2 To 65657

                sSatirNo = i - 1
                sMalzeme = IIf(objExc.Sheets(1).Cells(i, 1).Value Is Nothing, "", objExc.Sheets(1).Cells(i, 1).Value)

                If sMalzeme = "" Then
                    Exit For
                End If

                sLokasyon = IIf(objExc.Sheets(1).Cells(i, 2).Value Is Nothing, "", objExc.Sheets(1).Cells(i, 2).Value)
                sLot = IIf(objExc.Sheets(1).Cells(i, 3).Value Is Nothing, "", objExc.Sheets(1).Cells(i, 3).Value)
                sMiktar = IIf(objExc.Sheets(1).Cells(i, 4).Value Is Nothing, "", objExc.Sheets(1).Cells(i, 4).Value)
                sAmbar = IIf(objExc.Sheets(1).Cells(i, 5).Value Is Nothing, "", objExc.Sheets(1).Cells(i, 5).Value)
                sDokuman = IIf(objExc.Sheets(1).Cells(i, 6).Value Is Nothing, "", objExc.Sheets(1).Cells(i, 6).Value)
                sNedenKodu = IIf(objExc.Sheets(1).Cells(i, 6).Value Is Nothing, "", objExc.Sheets(1).Cells(i, 7).Value)
                table.Rows.Add(New Object() {sSatirNo, sMalzeme, sLokasyon, sLot, sMiktar, sAmbar, sDokuman, sNedenKodu, String.Empty})

            Next i

            'If bCikis Then

            '    Exit For

            'End If

            'Next j

            'objWorkSheet.SaveAs(DosyaAdi)

            objExc.DisplayAlerts = True

            Return table

        Catch ex As Exception

            Throw ex

        Finally

            objExc.Workbooks.Close()

            objExc.Quit()

            objExc = Nothing

            KillExcel()

            'Debug.WriteLine("Sleeping...")
            'System.Threading.Thread.Sleep(5000)
            'Debug.WriteLine("End Excel")

        End Try
    End Function

    Public Shared Sub ExcelUpdateGeneral(ByVal DosyaAdi As String, _
        ByVal SatirNo As Integer, _
        ByVal sSiparisNo As String)
        Dim MyConnection As System.Data.OleDb.OleDbConnection
        Dim myCommand As New System.Data.OleDb.OleDbCommand()
        Dim sql As String = Nothing

        Try

            MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & DosyaAdi & "';Extended Properties=Excel 8.0;")

            MyConnection.Open()

            myCommand.Connection = MyConnection

            sql = "Update [Sheet1$] " & _
                    " set Hata = 'İşlem Tamamlandı' " & _
                    " , Ref='" & sSiparisNo & "'" & _
                    " where Id= " & SatirNo - 1
            myCommand.CommandText = sql
            myCommand.ExecuteNonQuery()

            'objExc.Sheets(1).Cells(SatirNo, 39).Value = "Aktarıldı"
            'objExc.Sheets(1).Cells(SatirNo, 9).Value = sSiparisNo
            'objExc.Sheets(1).Cells(SatirNo, 10).Value = 1

        Catch ex As Exception

            Throw ex

        Finally

            MyConnection.Close()

        End Try
    End Sub

    Public Shared Function GetWriteableValue(ByVal o As Object) As String
        Try

            If o Is Nothing OrElse IsDBNull(o) Then
                Return ""
            ElseIf (o.ToString().IndexOf(",") = -1) Then
                Return o.ToString()
            Else
                Return "\"" + o.ToString() + " \ ""

            End If

        Catch ex As Exception

            Throw ex

        End Try
    End Function

    Public Shared Sub NAR(ByVal o As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(o)
        Catch
        Finally
            o = Nothing
        End Try
    End Sub

    Public Shared Sub ProduceCSV(ByVal dt As DataTable, _
        ByVal file As System.IO.StreamWriter, _
        ByVal WriteHeader As Boolean)
        Try

            Dim i As Int32

            Dim j As Int32

            If (WriteHeader) Then

                Dim arr(dt.Columns.Count - 1) As String

                For i = 0 To dt.Columns.Count - 1

                    arr(i) = dt.Columns(i).ColumnName

                    arr(i) = GetWriteableValue(arr(i))

                Next

                file.WriteLine(String.Join(";", arr))

            End If

            For j = 0 To dt.Rows.Count - 1

                Dim dataArr(dt.Columns.Count - 1) As String

                For i = 0 To dt.Columns.Count - 1

                    Dim o As Object = dt.Rows(j)(i)

                    Dim sMalzeme As String

                    If i = 3 Then

                        sMalzeme = CStr(GetWriteableValue(o)).Split("-")(0)

                    Else

                        sMalzeme = GetWriteableValue(o)

                    End If

                    dataArr(i) = IIf(i = 3, sMalzeme, GetWriteableValue(o))

                Next

                file.WriteLine(String.Join(";", dataArr))

            Next

        Catch ex As Exception

            Throw ex

        End Try
    End Sub

    Private Shared Sub KillExcel()
        Dim excelProcesses As Process() = System.Diagnostics.Process.GetProcessesByName("Excel")

        For Each p As Process In excelProcesses

            p.Kill()

        Next
    End Sub

    #End Region 'Methods

End Class