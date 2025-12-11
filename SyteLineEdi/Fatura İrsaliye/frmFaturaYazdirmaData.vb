Imports System.Xml

Public Class frmFaturaYazdirmaData

    #Region "Methods"

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (txtBrut.Text.Trim() = String.Empty AndAlso txtNet.Text.Trim() = String.Empty) Then
            MessageBox.Show("Net ve Brüt ağırlıkları giriniz!")
            Exit Sub
        End If

        sFaturaData.Detay1 = txtDetay1.Text
        sFaturaData.Detay2 = txtDetay2.Text
        sFaturaData.BankDetails1 = txtBankDetails1.Text
        sFaturaData.BankDetails2 = txtBankDetails2.Text
        sFaturaData.AccountNo = txtAccountNo.Text
        sFaturaData.AccountNo2 = txtAccountNo2.Text
        sFaturaData.Brut = txtBrut.Text
        sFaturaData.Net = txtNet.Text
        sFaturaData.SevkIrsaliyesi = txtSevkIrsaliyesi.Text
        sFaturaData.Iptal = False

        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        sFaturaData.Iptal = True
        Me.Close()
    End Sub

    Private Sub frmFaturaYazdirmaData_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim reader As XmlReader = XmlReader.Create(Application.StartupPath & "\fatura.xml")

        While Not reader.EOF
            If reader.IsStartElement And reader.LocalName = "BankaBilgi1" Then
                txtBankDetails1.Text = reader.ReadElementContentAsString
            End If
            If reader.IsStartElement And reader.LocalName = "BankaBilgi2" Then
                txtBankDetails2.Text = reader.ReadElementContentAsString
            End If
            reader.Read()
        End While
        reader.Close()
        txtBrut.Text = sFaturaData.Brut
        txtNet.Text = sFaturaData.Net
    End Sub

    #End Region 'Methods

End Class