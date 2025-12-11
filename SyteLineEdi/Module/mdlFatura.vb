Module mdlFatura

    Public structFaturaTmp As New structFatura
    Public structMalzemeTmp As New structMalzeme

    Structure structFatura
        Dim do_num As String
        Dim co_num As String
        Dim co_line As Integer
        Dim item As String
        Dim qty As Decimal
        Dim kontrat_birim_fiyati As Decimal
        Dim kontrat_parabirimi As String
        Dim fatura_birim_fiyati As Decimal
        Dim fatura_parabirimi As String
        Dim kur As Decimal
        Dim VergiKodu As String
        Dim VergiOrani As Decimal
        Dim VergiKodu2 As String
        Dim VergiOrani2 As Decimal
        Dim SHPNO As Integer
        Dim murnkod As String

    End Structure

    Structure structMalzeme
        Dim malzeme As String
        Dim must_malzeme As String
        Dim kontrat_birim_fiyati As Decimal
        Dim kontrat_parabirimi As String
        Dim fatura_birim_fiyati As Decimal
        Dim fatura_parabirimi As String
        Dim kur As Decimal

    End Structure


End Module
