
#Region "Clase Transaccional"

Public Class LTipoCambioTx
    Public Function InsertaTipoCambio(ByVal pETipoCambio As String) As Boolean

        Dim objLTipoCambioTx As Object = CreateObject("GCC.Logic.LTipoCambioTx")
        Try
            Return objLTipoCambioTx.InsertaTipoCambio(pETipoCambio)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ActualizaTipoCambio(ByVal pETipoCambio As String) As Boolean

        Dim objLTipoCambioTx As Object = CreateObject("GCC.Logic.LTipoCambioTx")
        Try
            Return objLTipoCambioTx.ActualizaTipoCambio(pETipoCambio)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function EliminaTipoCambio(ByVal pETipoCambio As String) As Boolean

        Dim objLTipoCambioTx As Object = CreateObject("GCC.Logic.LTipoCambioTx")
        Try
            Return objLTipoCambioTx.EliminaTipoCambio(pETipoCambio)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

#End Region

#Region "Clase No Transaccional"

Public Class LTipoCambioNTx

    Public Function ListaTipoCambio(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pETipocambio As String) As String

        Dim objLTipoCambioNTx As Object = CreateObject("GCC.Logic.LTipoCambioNTx")
        Try
            Return objLTipoCambioNTx.ListaTipoCambio(pPageSize, _
                                                                   pCurrentPage, _
                                                                   pSortColumn, _
                                                                   pSortOrder, _
                                                                   pETipocambio)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ValidaTipoCambio(ByVal pETipocambio As String) As String

        Dim objLTipoCambioNTx As Object = CreateObject("GCC.Logic.LTipoCambioNTx")
        Try
            Return objLTipoCambioNTx.ValidaTipoCambio(pETipocambio)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

#End Region
