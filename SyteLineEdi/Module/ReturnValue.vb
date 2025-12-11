Public Class ReturnValue

    #Region "Fields"

    Public iInfo As Integer
    Public ReturnValue As Boolean

    '
    Public sInfo As String

    Private mMessages As New ArrayList

    '
    Private mOthers As New Hashtable

    #End Region 'Fields

    #Region "Properties"

    '
    Public Default Property Items(ByVal index As Integer) As String
        Get
            Return mMessages(index)
        End Get
        Set(ByVal Value As String)
            mMessages(index) = Value
        End Set
    End Property

    '
    Public Property Others(ByVal key As String) As String
        Get
            Return mOthers.Item(key)
        End Get
        Set(ByVal Value As String)
            mOthers.Item(key) = Value
        End Set
    End Property

    #End Region 'Properties

    #Region "Methods"

    '
    Public Sub Add(ByVal msg As String)
        mMessages.Add(msg)
    End Sub

    '
    Public Sub AddOther(ByVal key As String, ByVal value As String)
        '
        '10.10 tarinde eklendi
        If mOthers.Contains(key) Then
            mOthers.Remove(key)
        End If
        '
        mOthers.Add(key, value)
        '
    End Sub

    '
    Public Function GetMessages() As String
        '
        Dim returnMsg As String

        returnMsg = ""

        For Each msg As String In mMessages
            '
            returnMsg &= msg & vbNewLine
            '
        Next
        '
        Return returnMsg
    End Function

    #End Region 'Methods

    #Region "Other"

    '

    #End Region 'Other

End Class