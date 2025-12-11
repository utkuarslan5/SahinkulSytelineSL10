Public Class Parametre

    #Region "Fields"

    Private sAlanAdi As String
    Private sDeger As String

    #End Region 'Fields

    #Region "Constructors"

    Public Sub New(ByVal tmpAlanAdi As String, ByVal tmpDeger As String)
        Me.AlanAdi = tmpAlanAdi
        Me.Deger = tmpDeger
    End Sub

    #End Region 'Constructors

    #Region "Properties"

    Public Property AlanAdi() As String
        Get
            Return sAlanAdi
        End Get
        Set(ByVal value As String)
            sAlanAdi = value
        End Set
    End Property

    Public Property Deger() As String
        Get
            Return sDeger
        End Get
        Set(ByVal value As String)
            sDeger = value
        End Set
    End Property

    #End Region 'Properties

End Class