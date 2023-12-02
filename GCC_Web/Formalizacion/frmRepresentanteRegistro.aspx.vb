Imports System.Data
Imports System.Web.Services

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS


Partial Class Formalizacion_frmRepresentanteRegistro
    Inherits GCCBase


    ReadOnly objLog As New GCCLog("frmRepresentanteRegistro.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                hflagtipoObs.Value = Request.QueryString("TipoObs")
                hdnTitulo.Value = Request.QueryString("Titulo")
                hddCodigoTipoRepresentante.Value = Request.QueryString("CodigoTipoRepresentante")
                hdnCodigoRepresentante.Value = Request.QueryString("CodigoRepresentante")
                hdnCodigoContrato.Value = Request.QueryString("CodigoContrato")
                If Not IsNothing(Request.QueryString("CodUnico")) Then
                    hddCodUnico.Value = Request.QueryString("CodUnico")
                End If

                Call InicializarListas()
            End If

        Catch ex As ApplicationException
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "')", True)
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If
        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try

    End Sub

    Protected Sub cmdGuardarRepresentante_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardarRepresentante.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Listado", "fn_ListaRepresentantesCerrar();", True)
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Inicializa el contenido de los Select (html controls) y dropdwonlist con los datos de sus respectivas tablas genericas.
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub InicializarListas()
        GCCUtilitario.CargarDepartamento(Me.cmbDepartamento)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoDocumento, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
    End Sub

#End Region

#Region "Web Métodos"

    ''' <summary>
    ''' Verifica si el Dni ya existe asignado para algún representante del cliente, 
    ''' cuando se edita el representante
    ''' </summary>
    ''' <param name="pCodigoRepresentante">Código del representante</param>
    ''' <param name="pCodigoContrato">Número de contrato.</param>
    ''' <param name="pCodUnico">Código único del cliente</param>
    ''' <param name="pNroDocumento">Número de documento de identidad.</param>
    ''' <returns></returns> 
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ExisteDniOtroRep(ByVal pCodigoRepresentante As String, _
                                            ByVal pCodigoContrato As String, _
                                            ByVal pCodUnico As String, _
                                            ByVal pCodigoTipoDocumento As String, _
                                            ByVal pNroDocumento As String) As String
        Dim objLCheckListNTx As New LCheckListNTx()
        Dim blnResult As Boolean = False

        Dim dtRepresentante As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLCheckListNTx.RepresentantesCliente(1000, _
                                                                                                                                1, _
                                                                                                                                "CodigoRepresentante", _
                                                                                                                                "desc", _
                                                                                                                                pCodUnico))

        For Each oRow As DataRow In dtRepresentante.Rows
            If Not oRow.Item("NroDocumento") Is DBNull.Value AndAlso _
                Not oRow.Item("CodigoRepresentante") Is DBNull.Value AndAlso _
                Not oRow.Item("CodigoTipoDocumento") Is DBNull.Value Then
                If oRow.Item("NroDocumento").ToString() = pNroDocumento AndAlso _
                    oRow.Item("CodigoTipoDocumento") = pCodigoTipoDocumento AndAlso _
                    oRow.Item("CodigoRepresentante").ToString() <> pCodigoRepresentante Then
                    blnResult = True

                    Exit For
                End If
            End If
        Next oRow

        If blnResult Then
            Return "0"
        Else
            Return "1"
        End If
    End Function

    ''' <summary>
    ''' Verifica si el Dni ya existe asignado para algún representante del cliente, cuando se agrega uno nuevo.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <param name="pCodUnico">Código único del cliente</param>
    ''' <param name="NroDocumento">Número del documento</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ExisteDni(ByVal pCodigoContrato As String, _
                                     ByVal pCodUnico As String, _
                                     ByVal pCodigoTipoDocumento As String, _
                                     ByVal NroDocumento As String) As String
        Dim objLCheckListNTx As New LCheckListNTx()
        Dim blnResult As Boolean = False

        Dim dtRepresentante As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLCheckListNTx.RepresentantesCliente(1000, _
                                                                                                                                1, _
                                                                                                                                "CodigoRepresentante", _
                                                                                                                                "desc", _
                                                                                                                                pCodUnico))

        For Each oRow As DataRow In dtRepresentante.Rows
            If Not (oRow.Item("NroDocumento") Is DBNull.Value AndAlso oRow.Item("CodigoTipoDocumento") Is DBNull.Value) Then
                If oRow.Item("NroDocumento") = NroDocumento Then
                    blnResult = True
                    Exit For
                End If
            End If
        Next oRow

        If blnResult Then
            Return "0"
        Else
            Return "1"
        End If
    End Function

    ''' <summary>
    ''' Lista los representantes del cliente que no pertenecen al contrato indicado (que no se encuentran asignados).
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pCodigoContrato">Número de contrato.</param>
    ''' <param name="pCodUnico">Código de identificación del cliente.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListarRepresentantes(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pCodigoContrato As String, _
                                                ByVal pCodUnico As String) As JQGridJsonResponse
        Dim objLContratoNTx As New LContratoNTx()

        Dim dtRepresentante As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLContratoNTx.ListarRepresentesDelCliente(pPageSize, _
                                                                                                                                     pCurrentPage, _
                                                                                                                                     pSortColumn, _
                                                                                                                                     pSortOrder, _
                                                                                                                                     pCodigoContrato, _
                                                                                                                                     pCodUnico))

        'Número total de páginas
        Dim oJQGridJsonResponse As New JQGridJsonResponse
        'Total de registros a mostrar.
        Dim totalRecords As Integer
        Dim totalCurrent As Integer

        If dtRepresentante.Rows.Count = 0 Then
            totalRecords = 0
            totalCurrent = 0
        Else
            totalRecords = Convert.ToInt32(dtRepresentante.Rows(0)("RecordCount"))
            totalCurrent = Convert.ToInt32(dtRepresentante.Rows(0)("TOTAL_PAGINA"))
        End If
        If pCurrentPage > totalCurrent Then
            pCurrentPage = totalCurrent
        End If

        'Número total de páginas
        Dim totalPages As Integer = oJQGridJsonResponse.TotalPaginas(totalRecords, _
                                                                     pPageSize)
        Return oJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, _
                                                               pCurrentPage, _
                                                               totalRecords, _
                                                               dtRepresentante)
    End Function

    ''' <summary>
    ''' Adiciona un nuevo representante a la lista de representantes y de los representantes del cliente.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato.</param>
    ''' <param name="strCodigoTipoRepresentante">Tipo de representante.</param>
    ''' <param name="strNroDocumento">Número de documento.</param>
    ''' <param name="strNombreRepresentante">Nombre completo del representante.</param>
    ''' <param name="strPartidaRegistral">Partida registral</param>
    ''' <param name="strOficinaRegistral">Oficina registral</param>
    ''' <param name="strDepartamento">Código de departamento.</param>
    ''' <param name="strProvincia">Código de provincia.</param>
    ''' <param name="strDistrito">Código de distrito.</param>
    ''' <param name="strCodUnico">Código único del cliente.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GuardarRepresentanteNuevo(ByVal pCodigoContrato As String, _
                                                     ByVal strCodigoTipoRepresentante As String, _
                                                     ByVal strNroDocumento As String, _
                                                     ByVal strNombreRepresentante As String, _
                                                     ByVal strPartidaRegistral As String, _
                                                     ByVal strOficinaRegistral As String, _
                                                     ByVal strDepartamento As String, _
                                                     ByVal strProvincia As String, _
                                                     ByVal strDistrito As String, _
                                                     ByVal strCodUnico As String, _
                                                     ByVal strCodigoTipoDeDocumento As String) As String
        Try
            Dim objLContratoTx As New LContratoTx

            Dim objEGccRepresentante As New EGcc_representante
            Dim oEGccContratorepresentante As New EGcc_contratorepresentante

            Dim pEGccRepresentante As String
            Dim pEGccContratorepresentante As String

            Dim objESolicitudCredito As New ESolicitudcredito
            Dim pESolicitudCredito As String


            With objEGccRepresentante
                .Codigotiporepresentante = strCodigoTipoRepresentante
                .Nrodocumento = GCCUtilitario.NullableString(strNroDocumento)
                .Nombrerepresentante = GCCUtilitario.NullableString(strNombreRepresentante)
                .Partidaregistral = GCCUtilitario.NullableString(strPartidaRegistral)
                .Oficinaregistral = GCCUtilitario.NullableString(strOficinaRegistral)
                .Codigoubigeo = GCCUtilitario.CodigoUbigeo(strDepartamento, strProvincia, strDistrito)
                .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .CodUnico = strCodUnico
                .CodigoTipoDocumento = strCodigoTipoDeDocumento
            End With

            oEGccContratorepresentante.Numerocontrato = pCodigoContrato
            oEGccContratorepresentante.Codigotiporepresentante = strCodigoTipoRepresentante
            oEGccContratorepresentante.Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            oEGccContratorepresentante.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
            objESolicitudCredito.Modificado = True
            objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            pEGccRepresentante = GCCUtilitario.SerializeObject(objEGccRepresentante)
            pEGccContratorepresentante = GCCUtilitario.SerializeObject(oEGccContratorepresentante)
            pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

            Dim result As Boolean = objLContratoTx.RepresentanteClienteContratoIns(pESolicitudCredito, _
                                                                                   pEGccRepresentante, _
                                                                                   pEGccContratorepresentante)

            If result Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Actualiza los datos del representante seleccioando.
    ''' </summary>
    ''' <param name="strCodigoRepresentante">Código del representante</param>
    ''' <param name="strCodigoTipoRepresentante">Código del tipo de representante</param>
    ''' <param name="strNroDocumento">Número de documento.</param>
    ''' <param name="strNombreRepresentante">Nombre completo del representante.</param>
    ''' <param name="strPartidaRegistral">Número de partida registral</param>
    ''' <param name="strOficinaRegistra">Número de oficina registral</param>
    ''' <param name="strDepartamento">Código de departamento.</param>
    ''' <param name="strProvincia">Código de provincia.</param>
    ''' <param name="strDistrito">Código de distrito.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GuardarRepresentanteEditar(ByVal strCodigoRepresentante As String, _
                                                      ByVal strCodigoTipoRepresentante As String, _
                                                      ByVal strNroDocumento As String, _
                                                      ByVal strNombreRepresentante As String, _
                                                      ByVal strPartidaRegistral As String, _
                                                      ByVal strOficinaRegistra As String, _
                                                      ByVal strDepartamento As String, _
                                                      ByVal strProvincia As String, _
                                                      ByVal strDistrito As String, _
                                                      ByVal strCodigoTipoDeDocumento As String) As String
        Try
            Dim objLCheckListTx As New LCheckListTx

            Dim objEGccRepresentante As New EGcc_representante
            Dim pEGccRepresentante As String

            With objEGccRepresentante
                .Codigorepresentante = CInt(strCodigoRepresentante)

                .Codigotiporepresentante = strCodigoTipoRepresentante
                .Nrodocumento = GCCUtilitario.NullableString(strNroDocumento)
                .Nombrerepresentante = GCCUtilitario.NullableString(strNombreRepresentante)
                .Partidaregistral = GCCUtilitario.NullableString(strPartidaRegistral)
                .Oficinaregistral = GCCUtilitario.NullableString(strOficinaRegistra)
                .Codigoubigeo = GCCUtilitario.CodigoUbigeo(strDepartamento, strProvincia, strDistrito)
                .CodigoTipoDocumento = strCodigoTipoDeDocumento

                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With

            pEGccRepresentante = GCCUtilitario.SerializeObject(objEGccRepresentante)

            Dim result As Boolean = objLCheckListTx.RepresentanteUpd(pEGccRepresentante)

            If result Then
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Elimina uno o varios representantes del cliente.
    ''' </summary>
    ''' <param name="pRepresentantesEliminar">Lista de los códigos de los representantes a eliminar.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function RepresentantesEliminar(ByVal pRepresentantesEliminar As String) As String
        Dim objCheckListTx As New LCheckListTx
        Dim oListEGccRepresentante As New ListEGcc_representante
        Dim pListEGccRepresentante As String

        Dim vRepresentantesEliminar As String() = pRepresentantesEliminar.Split(New Char() {"|"c})

        For i As Integer = 0 To vRepresentantesEliminar.Length - 1
            Dim oEGccRepresentante As New EGcc_representante
            oEGccRepresentante.Codigorepresentante = CInt(vRepresentantesEliminar(i))
            oEGccRepresentante.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            oListEGccRepresentante.Add(oEGccRepresentante)
        Next i

        pListEGccRepresentante = GCCUtilitario.SerializeObject(oListEGccRepresentante)
        Dim blnResult As Boolean = objCheckListTx.fblnEliminarRepresentante(pListEGccRepresentante)

        If blnResult Then
            Return "0"
        Else
            Return "1"
        End If
    End Function

    ''' <summary>
    ''' Le permite agregar uno o varios representantes ya asociados al cliente, al presente contrato.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de representante</param>
    ''' <param name="pCodigoTipoRepresentante">Tipo de representante</param>
    ''' <param name="pRepresentantesAAgregar">Código del representante a agregar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function RepresentantesClienteAgregarAContrato(ByVal pCodigoContrato As String, _
                                                                 ByVal pCodigoTipoRepresentante As String, _
                                                                 ByVal pRepresentantesAAgregar As String) As String
        Dim objCheckListTx As New LCheckListTx

        Dim oListEGccContratorepresentante As New ListEGcc_contratorepresentante
        Dim pListEGccContratorepresentante As String

        Dim objESolicitudCredito As New ESolicitudcredito
        Dim pESolicitudCredito As String

        Dim vEGccContratorepresentanteAgregar As String() = pRepresentantesAAgregar.Split(New Char() {"|"c})

        For i As Integer = 0 To vEGccContratorepresentanteAgregar.Length - 1
            Dim oEGccContratorepresentante As New EGcc_contratorepresentante

            oEGccContratorepresentante.Numerocontrato = pCodigoContrato
            oEGccContratorepresentante.Codigotiporepresentante = pCodigoTipoRepresentante
            oEGccContratorepresentante.Codigorepresentante = CInt(vEGccContratorepresentanteAgregar(i))
            oEGccContratorepresentante.Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            oEGccContratorepresentante.Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

            oListEGccContratorepresentante.Add(oEGccContratorepresentante)
        Next i

        objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
        objESolicitudCredito.Modificado = True
        objESolicitudCredito.AudUsuarioModificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

        pListEGccContratorepresentante = GCCUtilitario.SerializeObject(oListEGccContratorepresentante)
        pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

        Dim blnResult As Boolean = objCheckListTx.RepresentanteContratoListIns(pESolicitudCredito, _
                                                                               pListEGccContratorepresentante)

        If blnResult Then
            Return "0"
        Else
            Return "1"
        End If
    End Function

    ''' <summary>
    ''' Verifica si el representante seleccionado se encuentra asociado con algún contrato diferente al enviado por parámetro.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato.</param>
    ''' <param name="pCodigoTipoRepresentante">Tipo de representante</param>
    ''' <param name="pCodigoRepresentante">Código de representante.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function RepresentanteAsociadoAOtroContrato(ByVal pCodigoContrato As String, _
                                                              ByVal pCodigoTipoRepresentante As String, _
                                                              ByVal pCodigoRepresentante As String) As Boolean
        ' '' '' ''Try
        ' '' '' ''    Dim objLCheckListTx As New LCheckListNTx

        ' '' '' ''    Dim objEGccRepresentante As New EGcc_representante
        ' '' '' ''    Dim pEGccRepresentante As String

        ' '' '' ''    Dim objESolicitudCredito As New ESolicitudcredito
        ' '' '' ''    Dim pESolicitudCredito As String

        ' '' '' ''    With objEGccRepresentante
        ' '' '' ''        .Codigorepresentante = CInt(pCodigoRepresentante)
        ' '' '' ''        .Codigotiporepresentante = pCodigoTipoRepresentante
        ' '' '' ''    End With

        ' '' '' ''    objESolicitudCredito.Codsolicitudcredito = pCodigoContrato

        ' '' '' ''    pEGccRepresentante = GCCUtilitario.SerializeObject(objEGccRepresentante)
        ' '' '' ''    pESolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)

        ' '' '' ''    Return objLCheckListTx.EsRepresentanteAsociadoAOtroContrato(pESolicitudCredito, _
        ' '' '' ''                                                                pEGccRepresentante)
        ' '' '' ''Catch ex As Exception
        ' '' '' ''    Return False
        ' '' '' ''End Try
        Return True
    End Function

#End Region

End Class
