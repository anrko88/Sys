Imports ADODB

Module Functions
    Dim intRes As Integer

    Public Function fTranslateType(ByVal argTipo As Type) As ADODB.DataTypeEnum

        Select Case argTipo.UnderlyingSystemType.ToString

            Case "System.Boolean"
                Return ADODB.DataTypeEnum.adBoolean

            Case "System.Byte"
                Return ADODB.DataTypeEnum.adUnsignedTinyInt

            Case "System.Char"
                Return ADODB.DataTypeEnum.adChar

            Case "System.DateTime"
                Return ADODB.DataTypeEnum.adDate

            Case "System.TimeSpan"
                Return ADODB.DataTypeEnum.adChar

            Case "System.Decimal"
                Return ADODB.DataTypeEnum.adCurrency

            Case "System.Double"
                Return ADODB.DataTypeEnum.adDouble

            Case "System.Int16"
                Return ADODB.DataTypeEnum.adSmallInt

            Case "System.Int32"
                Return ADODB.DataTypeEnum.adInteger

            Case "System.Int64"
                Return ADODB.DataTypeEnum.adBigInt

            Case "System.SByte"
                Return ADODB.DataTypeEnum.adTinyInt

            Case "System.Single"
                Return ADODB.DataTypeEnum.adSingle

            Case "System.UInt16"
                Return ADODB.DataTypeEnum.adUnsignedSmallInt

            Case "System.UInt32"
                Return ADODB.DataTypeEnum.adUnsignedInt

            Case "System.UInt64"
                Return ADODB.DataTypeEnum.adUnsignedBigInt

            Case "System.String"
                Return ADODB.DataTypeEnum.adVarChar

            Case Else
                Return ADODB.DataTypeEnum.adVarChar

        End Select
    End Function

End Module
