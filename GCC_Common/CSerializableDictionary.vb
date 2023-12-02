Imports System.Xml.Serialization

<XmlRoot("Dictionary")> _
Public Class CSerializableDictionary(Of TKey, TValue)
    Inherits Dictionary(Of TKey, TValue)
    Implements IXmlSerializable

    Public Function GetSchema() As System.Xml.Schema.XmlSchema Implements System.Xml.Serialization.IXmlSerializable.GetSchema
        Return Nothing
    End Function

    Public Sub ReadXml(ByVal reader As System.Xml.XmlReader) Implements System.Xml.Serialization.IXmlSerializable.ReadXml
        Dim key_serializer As New XmlSerializer(GetType(TKey))
        Dim value_serializer As New XmlSerializer(GetType(TValue))

        Dim was_empty As Boolean = reader.IsEmptyElement
        reader.Read()
        If was_empty Then Exit Sub

        Do While reader.NodeType <> Xml.XmlNodeType.EndElement
            reader.ReadStartElement("Item")
            reader.ReadStartElement("Key")
            Dim key As TKey = DirectCast(key_serializer.Deserialize(reader), TKey)
            reader.ReadEndElement()
            reader.ReadStartElement("Value")
            Dim value As TValue = DirectCast(value_serializer.Deserialize(reader), TValue)
            reader.ReadEndElement()
            Me.Add(key, value)
            reader.ReadEndElement()
            reader.MoveToContent()
        Loop

        reader.ReadEndElement()
    End Sub

    Public Sub WriteXml(ByVal writer As System.Xml.XmlWriter) Implements System.Xml.Serialization.IXmlSerializable.WriteXml
        Dim key_serializer As XmlSerializer = New XmlSerializer(GetType(TKey))
        Dim value_serializer As XmlSerializer = New XmlSerializer(GetType(TValue))

        For Each key As TKey In Me.Keys
            writer.WriteStartElement("Item")
            writer.WriteStartElement("Key")
            key_serializer.Serialize(writer, key)
            writer.WriteEndElement()
            writer.WriteStartElement("Value")
            Dim value As TValue = Me(key)
            value_serializer.Serialize(writer, value)
            writer.WriteEndElement()
            writer.WriteEndElement()
        Next key
    End Sub
End Class
