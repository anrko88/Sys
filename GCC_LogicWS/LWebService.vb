Imports GCC.LogicWS.wsUltimus
Imports GCC.Entity

Public Class LWebService

#Region "Consultar WebService "

    '-----------------------------------------------------------------------------
    'Nombre             : fstrConsultarClienteRM
    'Objetivo           : Consulta al Cliente en RM del Webservice
    'Parámetros         : pstrConsulta : Tipo de consulta 1 - codigo unico y 2 - Nro Docuemntro
    '                   : pstrCodUnico : Codigo Unico del cliente
    '                   : pstrTipoDoc  : Tipo Documento codigo de IBK por defecto debe ser 2
    '                   : pstrNroRuc   : Numero Ruc del Cliente
    '                   : pstrCodigoProd : Codigo del Producto por defecto debe ser - 000 
    '                   : pstrUrlws  : Url donde se ejecutara el webservice
    'Creado Por         : TSF - JRC
    'Fecha de Creación  : 15/05/2012
    '-----------------------------------------------------------------------------
    Public Function fstrConsultarClienteRM(ByVal pstrConsulta As String, ByVal pstrCodUnico As String, ByVal pstrTipoDoc As String, ByVal pstrNroRuc As String, ByVal pstrCodProd As String, ByVal pstrUrlws As String) As String
        Dim wsFCD002 As New bseFCDO002.bseFCDO002
        wsFCD002.Url = pstrUrlws
        Try
            Return wsFCD002.fConsultarCliente(pstrConsulta, pstrCodUnico.Trim.PadLeft(14, "0"c), pstrTipoDoc, pstrNroRuc, pstrCodProd)
        Catch ex As Exception
            Throw ex
        Finally
            wsFCD002.Dispose()
        End Try
    End Function


    Public Function fstrConsultarCuenta(ByVal argFCDTIPOCUENTA As String, ByVal argFCDCODMONEDA As String, ByVal argFCDCODTIENDA As String, ByVal argFCDCODCATEGORIA As String, ByVal argFCDNUMCUENTA As String, ByVal pstrUrlws As String) As String
        Dim wsFCD003 As New bseFCDO003.bseFCDO003
        wsFCD003.Url = pstrUrlws
        Try
            Return wsFCD003.fConsultarCuenta(argFCDTIPOCUENTA, argFCDCODMONEDA, argFCDCODTIENDA, argFCDCODCATEGORIA, argFCDNUMCUENTA)

        Catch ex As Exception
            Throw ex
        Finally
            wsFCD003.Dispose()
        End Try
    End Function

#End Region

#Region "Consultas Externas"

    ''' <summary>
    ''' Obtiene Linea
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 29/02/2012
    ''' </remarks>
    Public Function fObtenerLineaOP(ByVal pstrCodigoUnico As String, ByVal pintTipo As Integer, ByVal pintCodigoProducto As Integer) As String
        Try
            Dim objLConsulta As Object = CreateObject("F15.Logic.LLineaOPNTx")
            Return objLConsulta.fObtenerLineaOP(pstrCodigoUnico, pintTipo, pintCodigoProducto)
        Catch ex As Exception
            Throw ex
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Obtiene Datos Lineas OP
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 29/02/2012
    ''' </remarks>
    Public Function fObtenerDatosLineaOP(ByVal pstrCodigoLinea As String) As String
        Try
            Dim objLConsulta As Object = CreateObject("F15.Logic.LLineaOPNTx")
            Return objLConsulta.fObtenerDatosLineaOP(pstrCodigoLinea)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene Datos de las tasas de Lineas OP
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 18/06/2012
    ''' </remarks>
    Public Function fObtenerTasasLineas(ByVal pstrCodUnico As String, ByVal pstrCodigoLinea As String) As String
        Try
            Dim objLConsulta As Object = CreateObject("F15.Logic.LLineaOPNTx")
            Return objLConsulta.fObtenerTasaComisiones(pstrCodUnico, pstrCodigoLinea)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los datos de parametro dominio de WIO
    ''' </summary>
    ''' <param name="pstrCodDominio"></param>
    ''' <param name="pstrCodParam"></param>
    ''' <param name="pstrTipo"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 18/06/2012
    ''' </remarks>
    Public Function fObtenerParamDomWio(ByVal pstrCodDominio As String, ByVal pstrCodParam As String, ByVal pstrTipo As String) As String
        Try
            Dim objLConsultaParametro As Object = CreateObject("F15.Logic.LConsultaNTx")
            Return objLConsultaParametro.fObtenerParametroDominio(pstrCodDominio, pstrCodParam, pstrTipo)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene Secuencia Maxima de una instruccion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 10/07/2012
    ''' </remarks>
    Public Function fintObtenerSecuenciaLs(ByVal pstrNumeroIO As String) As Integer
        Try
            Dim objLConsulta As Object = CreateObject("F15.Logic.LInstruccionOperativaNTx")
            Return objLConsulta.fintObtenerSecuenciaLs(pstrNumeroIO)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Ultimus"

    '-----------------------------------------------------------------------------
    'Nombre             : pObtenerDepartamentoZona
    'Objetivo           :
    'Parámetros         :
    'Creado Por         : TSF - JRC
    'Fecha de Creación  : 08/06/2012
    '------------------------------------------------------------------------------
    Public Sub pObtenerDepartamentoZona(ByVal pUserName As String, ByRef pDepartamento As String, ByRef pZona As String, _
                                         ByVal sURLwsUltimusWbc As String, ByRef pStrError As String)

        Try
            Dim wsUltimus As New wsUltimos
            Dim pObjUser As User = Nothing
            wsUltimus.Url = sURLwsUltimusWbc

            Dim aZona() As String
            Dim aDepa() As String

            Dim book As Boolean = wsUltimus.FindUser(pUserName, "", 0, pObjUser, pStrError)

            If book Then
                If pObjUser.strDepartmentName <> "" Then
                    Dim mZona As String = pObjUser.strDepartmentName.Replace(Chr(150), Chr(45))
                    aZona = mZona.Split("-") 'pObjUser.strDepartmentName.Split("-")
                    If aZona.Length > 1 Then
                        pZona = aZona(1).Trim
                    Else
                        If aZona(0).ToUpper.ToString.Contains("ZON") Then pZona = aZona(0).ToString.Trim
                    End If
                End If

                If pObjUser.strJobFunction <> "" Then
                    aDepa = pObjUser.strJobFunction.Split("-")
                    If aDepa.Length > 1 Then
                        pDepartamento = aDepa(1)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    '-----------------------------------------------------------------------------
    'Nombre             : fboolObtenerPrimerGrupoLogico
    'Objetivo           : Retorna el primer grupo lógico del usuario distinto de
    '                     "pstrDistintoDeGrupo"
    'Parámetros         : pstrUserName,pintJobID,pstrGroupName,pStrError
    'Creado Por         : TSF - JRC
    'Fecha de Creación  : 08/06/2012
    '------------------------------------------------------------------------------
    Function fboolObtenerPrimerGrupoLogico(ByVal pstrUserName As String, _
                                    ByVal pintJobID As Integer, _
                                    ByVal pstrDistintoDeGrupo As String, _
                                    ByVal pstrNemonicoGrupo As String, _
                                    ByRef pstrGroupName As String, _
                                    ByVal sURLwsUltimusWbc As String, _
                                    ByRef pStrError As String) As Boolean
        Try
            Dim blnOK As Boolean = False
            Dim blnRetorno As Boolean = False
            Dim wsUltimus As New wsUltimos
            Dim strPrefijoBuscar As String
            wsUltimus.Url = sURLwsUltimusWbc
            Dim arrpstrUserName As String() = pstrUserName.Split("/")
            If arrpstrUserName(1) = "" Then
                Return False
                Exit Function
            End If

            Dim strGroupArr() As String = Nothing

            pstrGroupName = ""
            'If sPreFijoRM <> "" Then
            'strPrefijoBuscar = pstrNemonicoGrupo & sPreFijoRM
            'Else
            strPrefijoBuscar = pstrNemonicoGrupo
            'End If

            blnOK = wsUltimus.GetGroupNames(strGroupArr, pStrError)

            If blnOK Then
                For Each strGrupoName As String In strGroupArr
                    'Buscamos por Nemonico y el prefijo de RM
                    If strGrupoName.StartsWith(strPrefijoBuscar) Then
                        If wsUltimus.GroupIsMemberOfGroup(pstrUserName, pintJobID, strGrupoName, "", pStrError) Then
                            If Not String.IsNullOrEmpty(pstrDistintoDeGrupo) Then
                                If pstrDistintoDeGrupo.Trim.ToUpper <> strGrupoName.Trim.ToUpper Then
                                    'If Not String.IsNullOrEmpty(pstrNemonicoGrupo) Then
                                    'If strGrupoName.Trim.ToUpper.IndexOf(pstrNemonicoGrupo.Trim.ToUpper) > -1 Then
                                    blnRetorno = True
                                    pstrGroupName = strGrupoName
                                    Exit For
                                    'End If
                                    'Else
                                    'blnRetorno = True
                                    'pstrGroupName = strGrupoName
                                    'Exit For
                                    'End If
                                End If
                                'Else
                                'blnRetorno = True
                                'pstrGroupName = strGrupoName
                                'Exit For
                            End If
                        End If
                    End If
                Next
                If pstrGroupName = "" Then
                    For Each strGrupoName As String In strGroupArr
                        'Buscamos por Nemonico y el prefijo de RM
                        If strGrupoName.StartsWith(strPrefijoBuscar) Then
                            If wsUltimus.GroupIsMemberOfGroup(pstrUserName, pintJobID, strGrupoName, "", pStrError) Then
                                If Not String.IsNullOrEmpty(pstrDistintoDeGrupo) Then
                                    If pstrDistintoDeGrupo.Trim.ToUpper <> strGrupoName.Trim.ToUpper Then
                                        If Not String.IsNullOrEmpty(pstrNemonicoGrupo) Then
                                            If strGrupoName.Trim.ToUpper.IndexOf(pstrNemonicoGrupo.Trim.ToUpper) > -1 Then
                                                blnRetorno = True
                                                pstrGroupName = strGrupoName
                                                Exit For
                                            End If
                                        Else
                                            blnRetorno = True
                                            pstrGroupName = strGrupoName
                                            Exit For
                                        End If
                                    End If
                                Else
                                    blnRetorno = True
                                    pstrGroupName = strGrupoName
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                End If

            End If

            strGroupArr = Nothing

            Return blnRetorno
        Catch ex As Exception
            Throw ex
            pStrError = ex.ToString
        End Try
    End Function

    Function fboolObtenerUsuariosxGrupo(ByVal pstrGrupoName As String, _
                                        ByRef objListEUsuarioUltimus As ListEUsuarioUltimus, _
                                        ByVal sURLwsUltimusWbc As String, _
                                        ByRef pStrError As String) As Boolean

        Dim wsUltimus As New wsUltimos
        wsUltimus.Url = sURLwsUltimusWbc
        Dim arrGrupoName() As String = pstrGrupoName.Split(";")
        Dim strGrupoName As String = ""
        Dim objGroup As New Group
        Dim objEUsuarioUltimus As EUsuarioUltimus
        Dim strGroupArr() As String = Nothing
        Dim obj As Object

        Try

            'Dim arrGrupoName() As String = pstrGrupoName.Split(";")
            Dim blnOK As Boolean = False
            Dim intRows As Integer = 0

            For i As Integer = 0 To arrGrupoName.Length - 1

                strGrupoName = arrGrupoName(i)
                blnOK = False
                intRows = 0
                blnOK = wsUltimus.GetGroup(strGrupoName, objGroup, pStrError)

                If objGroup IsNot Nothing Then
                    For intIndex As Integer = 0 To objGroup.GroupMembers.Length - 1
                        obj = New Object
                        obj = (objGroup.GroupMembers().GetValue(intIndex))
                        objEUsuarioUltimus = New EUsuarioUltimus

                        With objEUsuarioUltimus
                            .Codigousuario = CStr(obj.strMemberName)
                            .Nombreusuario = CStr(obj.strFullName)
                        End With
                        objListEUsuarioUltimus.Add(objEUsuarioUltimus)

                    Next
                End If
                'Return blnOK

            Next

            Return blnOK

        Catch ex As Exception
            Throw ex
            pStrError = ex.ToString
        Finally
            strGroupArr = Nothing
            wsUltimus = Nothing
            'objListEUsuarioUltimus = Nothing
            strGroupArr = Nothing
            obj = Nothing
        End Try

    End Function

    ''' <summary>
    ''' Obtiene los Miembros de un Grupo logico
    ''' </summary>
    ''' <param name="pstrGroupName">Nombre del Grupo Logico</param>
    ''' <param name="pObjstructGroupMembers">Esructura que se envia de retorno, datos de los miembros</param>
    ''' <param name="sURLwsUltimus">Url donde se ejecutara el procedimiento</param>
    ''' <param name="pStrError">valor de error si trae</param>
    ''' <returns>Boolean</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AGC
    ''' Fecha de Creacin   : 26/03/2010 
    ''' </remarks>
    Function fblnObtenerMemberGroup(ByVal pstrGroupName As String, _
                                    ByRef pObjstructGroupMembers() As structGroupMembers, _
                                    ByVal sURLwsUltimus As String, _
                                    ByRef pStrError As String) As Boolean

        Dim wsUltimus As New wsUltimos
        Try
            wsUltimus.Url = sURLwsUltimus
            Return wsUltimus.GetGroupMembers(pstrGroupName, pObjstructGroupMembers)
        Catch ex As Exception
            Throw ex
            pStrError = ex.ToString
        End Try
    End Function
#End Region

End Class
