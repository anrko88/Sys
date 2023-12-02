
#Region "Clase No Transaccional"

Public Class LMantenimientoNTX
    ''' <summary>
    ''' Listado de Temporal
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - IJM
    ''' Fecha de Creación  : 02/05/2012
    ''' </remarks>
    Public Function ListarTablaGenerica(ByVal pstrTablaGenerica) As String

        Dim objValorgenericaNTx As Object = CreateObject("GCC.Logic.LValorGenericaNTx")
        Try
            'Return objUtilNTx.ListadoTemporal()
            Return objValorgenericaNTx.LeerValorGenerica(pstrTablaGenerica)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ListarTablaGenericaAnidada(ByVal pstrTablaGenerica As String, ByVal pstrCodigo As String) As String

        Dim objValorGenericaAnidadaNTx As Object = CreateObject("GCC.Logic.LValorGenericaNTx")
        Try
            'Return objUtilNTx.ListadoTemporal()
            Return objValorGenericaAnidadaNTx.LeerValorGenericaAnidada(pstrTablaGenerica, pstrCodigo)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '10/01/2013 IBK RPR
    Public Function ListadoValorGenericaEspecial(ByVal pEValorGenerica As String, ByVal type As Integer) As String

        Dim objValorgenericaNTx As Object = CreateObject("GCC.Logic.LValorGenericaNTx")
        Try
            Return objValorgenericaNTx.ListadoValorGenericaEspecial(pEValorGenerica, type)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'FIN RPR

End Class

#End Region