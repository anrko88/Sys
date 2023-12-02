Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class GestionBien_ImpuestoMultasInmueble_frmImpuestoMultaInmuebleListadoAsigCheque
    Inherits GCCBase

    Dim objLog As New GCCLog("frmImpuestoMultaInmuebleListadoAsigCheque.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/02/2011
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
                'txtFechaPago.Value = Now.Date.ToShortDateString()
                'Inicio IBK - AAE
                txtLote.Value = IIf(String.IsNullOrEmpty(Request.QueryString("NroLote")), "", Request.QueryString("NroLote"))
                'Fin IbK
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

#End Region

#Region "WebMethods"

    ''' <summary>
    ''' Asigna Cheque
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 27/11/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function AsignarCheque(ByVal pstrLote As String, _
                                            ByVal pstrNroCheque As String, _
                                            ByVal pstrFechaPago As String) As String

        Try
            Dim blnResult As Boolean = False
            pstrLote = pstrLote.Trim.PadLeft(8, "0"c)

            'Valida Fech
            Dim objLCobroNTx As New LCobroNTx
            Dim strMensaje As String()
            Dim strMensajeResultado As String = ""
            Dim strFechaCobro As String = ""
            'strFechaCobro = Convert.ToDateTime(pstrFechaPago).AddDays(GCCConstante.C_DIAS_HABILES_COBRO_MULTA_MUNICIPAL).ToString("yyyy-MM-dd")
            strFechaCobro = Convert.ToDateTime(Convert.ToDateTime(GCCUtilitario.CheckDateString(pstrFechaPago, "yyyy-MM-dd"))).AddDays(GCCConstante.C_DIAS_HABILES_COBRO_MULTA_VEHICULAR).ToString("yyyy-MM-dd")
            strMensaje = objLCobroNTx.ValidarFeriado(strFechaCobro).Split(Convert.ToChar("*"))
            strMensajeResultado = strMensaje(1)
            If strMensajeResultado <> "" Then
                strFechaCobro = strMensaje(0)
            End If

            'For i As Integer = 0 To GCCConstante.C_DIAS_HABILES_COBRO_MULTA_MUNICIPAL
            '    If i = 0 Then
            '        strFechaCobro = Date.Now.ToString("yyyy-MM-dd")
            '    Else
            '        strFechaCobro = Convert.ToDateTime(strFechaCobro).ToString("yyyy-MM-dd")
            '    End If
            '    strMensaje = objLCobroNTx.ValidarFeriado(strFechaCobro).Split(Convert.ToChar("*"))
            '    strMensajeResultado = strMensaje(1)
            '    If strMensajeResultado = "" Then
            '        If i = GCCConstante.C_DIAS_HABILES_COBRO_MULTA_MUNICIPAL Then
            '            Exit For
            '        Else
            '            strFechaCobro = Convert.ToDateTime(strFechaCobro).AddDays(1).ToString("yyyy-MM-dd")
            '        End If
            '    Else
            '        strFechaCobro = strMensaje(0)
            '        i = i - 1
            '    End If
            'Next
            strFechaCobro = Convert.ToDateTime(strFechaCobro).ToString("yyyyMMdd")


            'Inicializa Objeto
            Dim objEImpuestomunicipal As New EImpuestomunicipal
            Dim strEImpuestomunicipal As String
            With objEImpuestomunicipal
                .Departamento = Nothing
                .Provincia = Nothing
                .Distrito = Nothing
                .NroContrato = Nothing
                .RazonSocial = Nothing
                .TipoDocumento = Nothing
                .NroDocumento = Nothing
                .Periodo = Nothing
                .Lote = GCCUtilitario.NullableString(pstrLote)
                .EstadoPago = Nothing
                .EstadoCobro = Nothing
                .Nrocheque = pstrNroCheque
                .FecpagoStr = Convert.ToDateTime(Convert.ToDateTime(GCCUtilitario.CheckDateString(pstrFechaPago, "yyyyMMdd"))).ToString("yyyyMMdd")
                .FechacobroStr = strFechaCobro
                .Usuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                'Inicio IBK - AAE
                .CantDias = GCCConstante.C_DIAS_HABILES_COBRO_MULTA_MUNICIPAL
                'Fin IBK
            End With
            strEImpuestomunicipal = GCCUtilitario.SerializeObject(Of EImpuestomunicipal)(objEImpuestomunicipal)

            'Ejecuta Consulta
            'Dim objLImpuestoMunicipalNTx As New LImpuestoMunicipalNTx
            'Dim dtImpMunicipal As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLImpuestoMunicipalNTx.ListadoImpuestoMunicipal(999, _
            '                                                                                                                               1, _
            '                                                                                                                               "FecModificacion", _
            '                                                                                                                               "desc", _
            '                                                                                                                               strEImpuestomunicipal))

            'Ejecuta Transaccion                
            Dim objLImpuestoMunicipalTx As New LImpuestoMunicipalTx
            blnResult = objLImpuestoMunicipalTx.AsignarChequeImpuestoMunicipal(GCCUtilitario.SerializeObject(objEImpuestomunicipal))
            'Verifica si el Lote Existe
            'If dtImpMunicipal.Rows.Count > 0 Then

            '    Dim strChequeExiste As String = dtImpMunicipal.Rows(0).Item("NroCheque").ToString
            '    If Not strChequeExiste.Trim.Equals("") Then
            '        Return "3"
            '    Else
            '        objEImpuestomunicipal = New EImpuestomunicipal
            '        With objEImpuestomunicipal
            '            .Lote = GCCUtilitario.NullableString(pstrLote)
            '            .Nrocheque = GCCUtilitario.NullableString(pstrNroCheque)
            '            .Usuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            '            .FechacobroStr = strFechaCobro
            '            .FecpagoStr = pstrFechaPago
            '        End With

            '        'Ejecuta Transaccion                
            '        Dim objLImpuestoMunicipalTx As New LImpuestoMunicipalTx
            '        blnResult = objLImpuestoMunicipalTx.AsignarChequeImpuestoMunicipal(GCCUtilitario.SerializeObject(objEImpuestomunicipal))
            '    End If

            'Else
            '    'No existe el LOTE
            '    Return "2"
            'End If



            'Valida Resultado
            If blnResult Then
                Return "1"
            Else
                Return "0"
            End If

        Catch ex As Exception
            Return "0"
        End Try

    End Function

    <WebMethod()> _
    Public Shared Function ObtieneDatosLote(ByVal pstrLote As String) As EImpuestomunicipal
        Try

            Dim dtImpuestoMunicipal As New DataTable
            Dim oImpuestoMunicipal As New EImpuestomunicipal
            Dim objLImpuestoMunicipalNTx As New LImpuestoMunicipalNTx
            dtImpuestoMunicipal = GCCUtilitario.DeserializeObject(Of DataTable)(objLImpuestoMunicipalNTx.GetImpuestoMultasInmueble(pstrLote))

            If dtImpuestoMunicipal.Rows.Count > 0 Then
                oImpuestoMunicipal.Municipalidad = IIf(String.IsNullOrEmpty(dtImpuestoMunicipal.Rows(0).Item("Municipalidad").ToString()), "Sin Municipalidad", dtImpuestoMunicipal.Rows(0).Item("Municipalidad").ToString())
                oImpuestoMunicipal.Total = GCCUtilitario.CheckDecimal(dtImpuestoMunicipal.Rows(0).Item("Total").ToString())
                oImpuestoMunicipal.FechacobroStr = dtImpuestoMunicipal.Rows(0).Item("FechaRegistro").ToString()
            End If

            Return oImpuestoMunicipal
        Catch ex As Exception
            Dim oImpuestoMunicipal As New EImpuestomunicipal
            'oESolicitudcredito.MsgError = "No se pudo cargar los datos del contrato."
            Return oImpuestoMunicipal
        End Try
    End Function
    <WebMethod()> _
   Public Shared Function ValidaLotesCheque(ByVal pstrLote As String) As String
        Try
            Dim blnResult As Boolean = False
            pstrLote = pstrLote.Trim.PadLeft(8, "0"c)

            Dim objEImpuestomunicipal As New EImpuestomunicipal
            Dim strEImpuestomunicipal As String
            With objEImpuestomunicipal
                .Departamento = Nothing
                .Provincia = Nothing
                .Distrito = Nothing
                .NroContrato = Nothing
                .RazonSocial = Nothing
                .TipoDocumento = Nothing
                .NroDocumento = Nothing
                .Periodo = Nothing
                .Lote = GCCUtilitario.NullableString(pstrLote)
                .EstadoPago = Nothing
                .EstadoCobro = Nothing
            End With
            strEImpuestomunicipal = GCCUtilitario.SerializeObject(Of EImpuestomunicipal)(objEImpuestomunicipal)

            'Ejecuta Consulta
            Dim objLImpuestoMunicipalNTx As New LImpuestoMunicipalNTx
            Dim dtImpMunicipal As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLImpuestoMunicipalNTx.ListadoImpuestoMunicipal(999, _
                                                                                                                                           1, _
                                                                                                                                           "FecModificacion", _
                                                                                                                                           "desc", _
                                                                                                                                           strEImpuestomunicipal))
            If dtImpMunicipal.Rows.Count > 0 Then

                Dim strChequeExiste As String = dtImpMunicipal.Rows(0).Item("NroCheque").ToString
                If Not String.IsNullOrEmpty(strChequeExiste) Then
                    Return "3" ' tiene cheque
                Else
                    Return "0" 'No tiene
                End If
            Else
                Return "2" 'No existe el LOTE
            End If

        Catch ex As Exception
            Return "-1" 'Error
        End Try
    End Function

#End Region

End Class
