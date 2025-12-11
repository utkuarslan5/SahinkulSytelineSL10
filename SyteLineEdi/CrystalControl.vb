Public Class CryRepControl
    Inherits System.Windows.Forms.UserControl

    #Region "Fields"

    Public ParamFields As New CrystalDecisions.Shared.ParameterFields()

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend  WithEvents CrystalControl As System.Windows.Forms.Panel

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Dim m_ParaterFileds(1000) As String
    Dim m_ReportFileName As String

    #End Region 'Fields

    #Region "Constructors"

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub

    #End Region 'Constructors

    #Region "Properties"

    Public Property ParameterFields() As String()
        Get
            ParameterFields = m_ParaterFileds
        End Get
        Set(ByVal Value As String())
            m_ParaterFileds = Value
        End Set
    End Property

    Public Property ReportFileName() As String
        Get
            ReportFileName = m_ReportFileName
        End Get

        Set(ByVal New_ReportFileName As String)
            m_ReportFileName = New_ReportFileName
        End Set
    End Property

    #End Region 'Properties

    #Region "Methods"

    Public Function CrySet() As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim Para As Integer
        Dim crReportDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
        crReportDocument.Load(ReportFileName)

        Dim ParamField As New CrystalDecisions.Shared.ParameterField()
        Dim DiscreteVal As New CrystalDecisions.Shared.ParameterDiscreteValue()

        With crReportDocument.DataDefinition
            Dim ArrValues() As String
            Dim ParamRow As Integer
            For Para = 0 To .ParameterFields().Count - 1

                For ParamRow = 0 To UBound(ParameterFields)
                    ArrValues = Split(ParameterFields(ParamRow), ";")
                    If .ParameterFields(Para).Name = ArrValues(0) Then
                        ' Set the name of the parameter field, this must match a
                        ' parameter in the report.
                        ParamField.ParameterFieldName = .ParameterFields(Para).Name

                        Select Case .ParameterFields(Para).ParameterValueKind
                            Case CrystalDecisions.[Shared].ParameterValueKind.BooleanParameter
                                '"Boolean"
                                DiscreteVal.Value = CBool(ArrValues(1))
                                ParamField.CurrentValues.Add(DiscreteVal)

                                ' Add the parameter to the parameter fields collection.
                                ParamFields.Add(ParamField)
                                DiscreteVal = New CrystalDecisions.Shared.ParameterDiscreteValue()
                                ParamField = New CrystalDecisions.Shared.ParameterField()
                            Case CrystalDecisions.[Shared].ParameterValueKind.CurrencyParameter
                                '"Currency"
                                DiscreteVal.Value = ArrValues(1)
                                ParamField.CurrentValues.Add(DiscreteVal)

                                ' Add the parameter to the parameter fields collection.
                                ParamFields.Add(ParamField)
                                DiscreteVal = New CrystalDecisions.Shared.ParameterDiscreteValue()
                                ParamField = New CrystalDecisions.Shared.ParameterField()
                            Case CrystalDecisions.[Shared].ParameterValueKind.DateParameter
                                '"Date"
                                DiscreteVal.Value = CDate(ArrValues(1))
                                ParamField.CurrentValues.Add(DiscreteVal)

                                ' Add the parameter to the parameter fields collection.
                                ParamFields.Add(ParamField)
                                DiscreteVal = New CrystalDecisions.Shared.ParameterDiscreteValue()
                                ParamField = New CrystalDecisions.Shared.ParameterField()
                            Case CrystalDecisions.[Shared].ParameterValueKind.DateTimeParameter
                                '"DateTime"
                                DiscreteVal.Value = CDate(ArrValues(1))
                                ParamField.CurrentValues.Add(DiscreteVal)

                                ' Add the parameter to the parameter fields collection.
                                ParamFields.Add(ParamField)
                                DiscreteVal = New CrystalDecisions.Shared.ParameterDiscreteValue()
                                ParamField = New CrystalDecisions.Shared.ParameterField()
                            Case CrystalDecisions.[Shared].ParameterValueKind.NumberParameter
                                '"Number"
                                DiscreteVal.Value = CInt(ArrValues(1))
                                ParamField.CurrentValues.Add(DiscreteVal)

                                ' Add the parameter to the parameter fields collection.
                                ParamFields.Add(ParamField)
                                DiscreteVal = New CrystalDecisions.Shared.ParameterDiscreteValue()
                                ParamField = New CrystalDecisions.Shared.ParameterField()
                            Case CrystalDecisions.[Shared].ParameterValueKind.StringParameter
                                '"String"
                                DiscreteVal.Value = ArrValues(1)
                                ParamField.CurrentValues.Add(DiscreteVal)

                                ' Add the parameter to the parameter fields collection.
                                ParamFields.Add(ParamField)
                                DiscreteVal = New CrystalDecisions.Shared.ParameterDiscreteValue()
                                ParamField = New CrystalDecisions.Shared.ParameterField()
                            Case CrystalDecisions.[Shared].ParameterValueKind.TimeParameter
                                '"Time"
                                DiscreteVal.Value = CDate(ArrValues(1))
                                ParamField.CurrentValues.Add(DiscreteVal)

                                ' Add the parameter to the parameter fields collection.
                                ParamFields.Add(ParamField)
                                DiscreteVal = New CrystalDecisions.Shared.ParameterDiscreteValue()
                                ParamField = New CrystalDecisions.Shared.ParameterField()
                        End Select
                    End If
                Next
            Next Para

        End With

        CrySet = crReportDocument
    End Function

    'UserControl1 overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    <System.Diagnostics.DebuggerStepThrough> _
    Private Sub InitializeComponent()
        Me.CrystalControl = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'CrystalControl
        '
        Me.CrystalControl.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(192, Byte))
        Me.CrystalControl.ForeColor = System.Drawing.Color.RosyBrown
        Me.CrystalControl.Location = New System.Drawing.Point(8, 8)
        Me.CrystalControl.Name = "CrystalControl"
        Me.CrystalControl.Size = New System.Drawing.Size(32, 32)
        Me.CrystalControl.TabIndex = 0
        '
        'CryRepControl
        '
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.CrystalControl})
        Me.Name = "CryRepControl"
        Me.Size = New System.Drawing.Size(48, 48)
        Me.ResumeLayout(False)
    End Sub

    #End Region 'Methods

End Class