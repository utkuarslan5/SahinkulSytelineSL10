Imports System.Collections.ObjectModel
Imports System.Data.SqlClient

Public Class FieldAndValue

    #Region "Fields"

    Private _Name As String
    Private _Value As Object

    #End Region 'Fields

    #Region "Constructors"

    Public Sub New(ByVal name As String, ByVal value As Object)
        Me._Name = name
        Me._Value = value
    End Sub

    #End Region 'Constructors

    #Region "Properties"

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            _Name = Value
        End Set
    End Property

    Public Property Value() As Object
        Get
            Return _Value
        End Get
        Set(ByVal Value As Object)
            _Value = Value
        End Set
    End Property

    #End Region 'Properties

End Class