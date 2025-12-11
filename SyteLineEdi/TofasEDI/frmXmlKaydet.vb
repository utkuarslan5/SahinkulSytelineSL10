Imports System
Imports System.IO
Imports System.Text
Imports System.Xml

Public Class frmxmlkaydet

    #Region "Methods"

    Private Sub btnKaydet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKaydet.Click
        Try
            My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\parametre.xml")

            Dim writer As XmlWriter = New XmlTextWriter(Application.StartupPath & "\parametre.xml", System.Text.Encoding.UTF8)
            writer.WriteStartDocument()
            writer.WriteStartElement("Edi")
            writer.WriteElementString("imalatkodu", txtImalatciKodu.Text)
            writer.WriteElementString("alici", txtAlici.Text)
            writer.WriteElementString("siparisno", txtSipNo.Text)
            writer.WriteElementString("dosyayolu", txtKlasoryolu.Text)

            writer.WriteEndElement()
            writer.Flush()
            writer.Close()

            MessageBox.Show("Sabit Deðerler Güncellendi")

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub xmlkaydet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim imalatkodu As String = ""

            Dim reader As XmlReader = XmlReader.Create(Application.StartupPath & "\parametre.xml")

            While Not reader.EOF
                If reader.IsStartElement And reader.LocalName = "imalatkodu" Then
                    txtImalatciKodu.Text = reader.ReadElementContentAsString
                End If
                If reader.IsStartElement And reader.LocalName = "alici" Then
                    txtAlici.Text = reader.ReadElementContentAsString
                End If
                If reader.IsStartElement And reader.LocalName = "siparisno" Then
                    txtSipNo.Text = reader.ReadElementContentAsString
                End If
                If reader.IsStartElement And reader.LocalName = "dosyayolu" Then
                    txtKlasoryolu.Text = reader.ReadElementContentAsString
                End If
                reader.Read()
            End While
            reader.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    #End Region 'Methods

End Class