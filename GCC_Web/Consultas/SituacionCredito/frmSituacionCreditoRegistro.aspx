<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSituacionCreditoRegistro.aspx.vb"
    Inherits="SituacionCredito_frmSituacionCreditoRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GCC :: Sistema de Gestión de Leasing</title>
    <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="../../Util/images/PV16x16.ico" />
    <!-- Estilos -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery.jscrollpane.css"
        media="all" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_global.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_formulario.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_fuente.css" />
    <!-- JavaScript -->

    <script type='text/javascript' src="../../Util/js/jquery/jquery-1.6.2.min.js"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.jscrollpane.min.js"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.mousewheel.js"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.ui.global.js"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.validText.js"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.validNumber.js"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.maxlength.js"></script>

    <script type="text/javascript" src="../../Util/js/js_global.js"></script>

    <script type='text/javascript' src="../../Util/js/js_util.modal.js"></script>

    <script type='text/javascript' src="../../Util/js/js_util.funcion.js"></script>

    <script type='text/javascript' src="../../Util/js/js_util.date.js"></script>

    <script type='text/javascript' src="../../Util/js/js_util.ajax.js"></script>

    <script src="../../Util/js/jquery/jquery.dateFormat-1.0.js" type="text/javascript"></script>

    <script src="../../Util/js/jquery/jshashtable.js" type="text/javascript"></script>

    <script src="../../Util/js/js_util.Grilla.js" type="text/javascript"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->


    <script src="frmSituacionCreditoRegistro.aspx.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmContratoRegistro" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!--
        BOTONES
        -->
        <table id="tb_cuerpoCabecera" style="border: 0;" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_contrato.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Consultas</div>
                    <div class="css_lbl_titulo">
                        Situación del Crédito :: Detalle</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_Volver" class="dv_img_boton">
                        <a href="javascript:fn_Cancelar();">
                            <img alt="Volver" src="../../Util/images/ico_acc_cancelar.gif" style="border: 0" title="Volver" /><br />
                            Volver </a>
                    </div>
                    
                    <div id="dv_btnSeguimiento" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_abreSeguimiento();">
                            <img alt="" src="../../Util/images/ico_seguimiento.gif" style="border: 0" /><br />
                            Seguimiento </a>
                    </div>
                   
                </td>
            </tr>
        </table>
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" cellpadding="0" cellspacing="0" style="width: 100%; border: 0;">
                <tr>
                    <td class="lineas">
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                        <!--
        				CABECERA DEL CONTRATO
        				-->
                        <table id="tb_tabla_comunCabecera" cellpadding="0" cellspacing="0" style="border: 0;">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Contrato
                                </td>
                                <td class="botones">
                                    <img alt="" src="../../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        <div style="display: none">
                            <textarea id="TextArea1" cols="200" rows="15" style="width: 600px;"></textarea>
                            <input id="Button1" type="button" value="button" onclick="go();" /></div>
                        <div id="dv_datos" class="dv_tabla_contenedora">
                            <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                <colgroup>
                                    <col style="width: 16.66%;" />
                                    <col style="width: 16.66%;" />
                                    <col style="width: 16.66%;" />
                                    <col style="width: 16.66%;" />
                                    <col style="width: 16.66%;" />
                                    <col style="width: 16.66%;" />
                                </colgroup>
                                <tr>
                                    <td class="label">
                                        Nº Contrato
                                    </td>
                                    <td class="input" id="txtNroContrato" runat="server" >                                        
                                    </td>
                                    <td class="label">
                                        Estado del Contrato
                                    </td>
                                    <td class="input" id="txtEstadoDelContrato" runat="server">                                       
                                    </td>
                                    <td class="label">
                                        F. Contrato
                                    </td>
                                    <td class="input" id="txtFechaContrato" runat="server">                                     
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Fecha Máx. de Disponibilidad
                                    </td>
                                    <td class="input" id="txtFechamaxdisp" runat="server">                                       
                                    </td>
                                    <td class="label">
                                        Fecha Máx. de Activación
                                    </td>
                                    <td class="input" id="txtFechaActivacion" runat="server">                                        
                                    </td>
                                    <td class="label">
                                        Periodo de Disponibilidad
                                    </td>
                                    <td class="input" id="txtPeriodoDisponible" runat="server">                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Clasificación del Bien
                                    </td>
                                    <td class="input" id="txtClasificacionDelBien" runat="server">
                                    </td>
                                    <td class="label">
                                        Tipo de Bien
                                    </td>
                                    <td class="input" id="txtTipoDeBien" runat="server">                                       
                                    </td>
                                    <td class="label">
                                        Procedencia
                                    </td>
                                    <td class="input" id="txtProcedencia" runat="server">
                                                                                
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Moneda
                                    </td>
                                    <td class="input" id="txtMoneda" runat="server">
                                       
                                    </td>
                                    <td class="label">
                                        Precio Venta
                                    </td>
                                    <td class="input" id="txtMontoFinanciado" runat="server">
                                        
                                    </td>
                                    <td class="label">
                                        Ejecutivo Leasing
                                    </td>
                                    <td class="input" id="txtEjecutivoLeasing" runat="server">                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Clasificación de Contrato
                                    </td>
                                    <td class="input" id="cmbClasificacionContrato" runat="server">
                                      
                                    </td>
                                    <td class="label" id="dv_lblRegistrosPublicos">
                                        <div id="dv_RegistrosPublicos">
                                            Registros Públicos
                                        </div>
                                    </td>
                                    <td class="input">
                                        <div id="dv_FechaRegistroPublico">
                                            <input id="chkRegistroPublico" name="chkRegistroPublico" type="checkbox" runat="server" readonly="readonly" disabled="disabled" />
                                            <input id="txtFechaRegistroPublico" name="txtFechaRegistroPublico" type="text" class="css_input_inactivo"
                                                size="11" readonly="readonly" runat="server" />
                                        </div>
                                    </td>
                                    <td class="label">
                                        Firmado en Notaría
                                    </td>
                                    <td class="input">
                                        <input id="chkFirmaNotaria" name="chkFirmaNotaria" type="checkbox" runat="server" readonly="readonly" disabled="disabled"/>
                                        <input id="txtFechaFirmaNotaria" name="txtFechaFirmaNotaria" type="text" class="css_input_inactivo"
                                            size="11" readonly="readonly" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Contrato Adjunto
                                    </td>
                                    <td class="input" colspan="5">
                                        <table style="border: 0;">
                                            <tr>
                                                <td>
                                                    <div id="dv_DescargarArchivoContrato">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <!--
        				INICIA TABS
        				-->
                        <div id="divTabs" style="border: 0; background: none;" class="dv_tabla_contenedora">
                            <ul>
                                <li><a href="#tab-6">DATOS DEL CLIENTE</a></li>
                                <li><a href="#tab-0">DATOS DEL BIEN</a></li>
                                <li><a href="#tab-1">INFORMACIÓN REQUERIDA</a></li>
                                <li><a href="#tab-2">REPRESENTANTES A FIRMAR</a></li>
                                <li><a href="#tab-3">OTROS CONCEPTOS</a></li>
                                <li><a href="#tab-4">DATOS NOTARIALES</a></li>
                                <li id="li_adenda"><a href="#tab-5">ADENDAS</a></li>
                                <li><a href="#tab-7">CRONOGRAMA</a></li>
                            </ul>
                            <!--
        			         TAB :: DATOS DEL CLIENTE
        			         !-->
                            <div id="tab-6">
                                <div id="dv_datosCliente" style="border: 0px;">
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Datos del Cliente</legend>
                                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                            <colgroup>
                                                <col style="width: 25%;" />
                                                <col style="width: 25%;" />
                                                <col style="width: 25%;" />
                                                <col style="width: 25%;" />
                                            </colgroup>
                                            <tr>
                                                <td class="label">
                                                    CU Cliente
                                                </td>
                                                <td class="input" id="txtCodUnico" runat="server">
                                                   
                                                </td>
                                                <td class="label">
                                                    Razón social o Nombre
                                                </td>
                                                <td class="input" id="txtRazonSocial" runat="server">
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Tipo Persona
                                                </td>
                                                <td class="input" id="txtTipoPersona" runat="server">
                                                    
                                                </td>
                                                <td class="label" id="td_EstadoCivil">
                                                    Estado Civil
                                                </td>
                                                <td class="input" id="cmbEstadoCivil" runat="server" >
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Tipo de Documento
                                                </td>
                                                <td class="input" id="txtTipoDocumento" runat="server">
                                                   
                                                </td>
                                                <td class="label">
                                                    Nº de Documento
                                                </td>
                                                <td class="input" id="txtNroDeDocumento" runat="server">
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Domicilio
                                                </td>
                                                <td class="input" colspan="2">
                                                    <textarea id="txtaDomicilioCliente" name="txtaDomicilioCliente" class="css_input"
                                                        rows="3" cols="90" runat="server" readonly="readonly" disabled="disabled"></textarea>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <fieldset id="fs_DatosConyugue">
                                        <legend class="css_lbl_subTitulo">Datos del Cónyuge</legend>
                                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                            <colgroup>
                                                <col style="width: 25%;" />
                                                <col style="width: 25%;" />
                                                <col style="width: 25%;" />
                                                <col style="width: 25%;" />
                                            </colgroup>
                                            <tr>
                                                <td class="label">
                                                    Nombre del Cónyuge
                                                </td>
                                                <td class="input">
                                                    <input id="txtNombreConyuge" name="txtNombreConyuge" type="text" class="css_input"
                                                        runat="server" style="width: 350px;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Tipo de Documento
                                                </td>
                                                <td class="input">
                                                    <select name="cmbTipoDocumentoConyuge" id="cmbTipoDocumentoConyuge">
                                                    </select>
                                                </td>
                                                <td class="label">
                                                    Nro. de Documento
                                                </td>
                                                <td class="input">
                                                    <input id="txtnumerodocumento" name="txtnumerodocumento" type="text" class="css_input"
                                                        size="14" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Documento de Separación
                                                </td>
                                                <td colspan="3" class="input">
                                                    <table style="border: 0;">
                                                        <tr>
                                                            <td>
                                                                <img title="Adjuntar correo" id="imgAdjuntarArchivoDocumentoSeparacion" style="cursor: pointer;
                                                                    cursor: hand;" onclick="javascript:fn_AdjuntarArchivoDocumento('ArchivoDocumentoSeparacion');"
                                                                    alt="" src="../../Util/images/ico_acc_adjuntarMini.gif" />
                                                            </td>
                                                            <td>
                                                                <div id="dv_AdjuntarArchivoDocumentoSeparacion" style="border: 0px;">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Datos de Representantes a Firmar</legend>
                                        <table id="tb_formulario" cellpadding="0" cellspacing="0" style="border: 0;">
                                            <tr>
                                                <td>
                                                  <%--  <div id="dv_AccionesRepresentantes" style="border: 0;">
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_AgregarRepresentantes();" style="display: inline;">
                                                                <img alt="" src="../../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;
                                                                    display: inline; border: 0px;" />Agregar </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_EliminarRepresentantes();">
                                                                <img alt="" src="../../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;
                                                                    border: 0px;" />Eliminar </a>
                                                        </div>
                                                    </div>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="jqGrid_lista_H">
                                                        <tr>
                                                            <td />
                                                        </tr>
                                                    </table>
                                                    <div id="jqGrid_pager_H">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </div>
                            <!--
        			         TAB :: DATOS DEL BIEN
        			         !-->
                            <div id="tab-0">
                                <!-- Datos Inmueble -->
                                <div id="dvDatosBien" style="border: 0;">
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Datos del Bien</legend>
                                        <table id="tb_DatosInmueble" border="0">
                                            <tr>
                                                <td>
                                                    <table id="tb_formulario" cellpadding="0" cellspacing="3" style="border: 0;">
                                                        <colgroup>
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="label">
                                                                Uso
                                                            </td>
                                                            <td class="input" id="txtUsoInmueble" runat="server">
                                                            
                                                            </td>
                                                            <td class="label">
                                                                Ubicación
                                                            </td>
                                                            <td class="input" id="txtUbicacionInmueble" runat="server">
                                                                <%--<input name="txtUbicacionInmueble" type="text" class="css_input"
                                                                    size="50" runat="server" />--%>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Descripción
                                                            </td>
                                                            <td class="input">
                                                                <textarea id="txtDescripcionInmueble" name="txtDescripcionInmueble" rows="2" cols="51"
                                                                    runat="server" disabled="disabled" readonly="readonly"></textarea>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Cantidad
                                                            </td>
                                                            <td class="input" id="txtCantidadInmueble" runat="server" >
                                                                
                                                            </td>
                                                            <td class="label">
                                                                Estado del Bien
                                                            </td>
                                                            <td class="input" id="cmbEstadoBienInmueble" runat="server">
                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Departamento
                                                            </td>
                                                            <td class="input" id="cmbDepartamentoInmueble" runat="server">
                                                               
                                                            </td>
                                                            <td class="label">
                                                                Provincia
                                                            </td>
                                                            <td class="input" id="cmbProvinciaInmueble" runat="server">
                                                                
                                                            </td>
                                                            <td class="label">
                                                                Distrito
                                                            </td>
                                                            <td class="input" id="cmbDistritoInmueble" runat="server">
                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Partida Registral
                                                            </td>
                                                            <td class="input" id="txtPartidaRegistralInmueble" runat="server">
                                                                
                                                            </td>
                                                            <td class="label">
                                                                Oficina Registral
                                                            </td>
                                                            <td class="input" id="txtOficinaRegistralInmueble" runat="server" >
                                                                
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td style="text-align: left;">
                                                                                                 
                                                        <div class="dv_img_boton_mini" id="btnEditarDatosBien" style="border: 0; height: 22px; float: right;">
                                                            <a href="javascript:fn_EditarDetDatosBien();">
                                                                <img alt="" src="../../Util/images/ico_acc_ver.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />&nbsp;&nbsp;&nbsp;Ver </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" id="btnCancelarDatosBien" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_CancelarBienInmueble();">
                                                                <img alt="" src="../../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />&nbsp;&nbsp;&nbsp;Cancelar </a>
                                                        </div>
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="jqGrid_lista_A">
                                                        <tr>
                                                            <td />
                                                        </tr>
                                                    </table>
                                                    <div id="jqGrid_pager_A">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                                <!-- Datos Maquinaria -->
                                <div id="dvDatosMaquinaria" style="border: 0;">
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Datos del Bien</legend>
                                        <table id="tb_DatosMaquinaria">
                                            <tr>
                                                <td>
                                                    <table id="tb_formulario" cellpadding="0" cellspacing="3" style="border: 0;">
                                                        <colgroup>
                                                            <col style="width: 10.00%;" />
                                                            <col style="width: 23.32%;" />
                                                            <col style="width: 10.00%;" />
                                                            <col style="width: 23.32%;" />
                                                            <col style="width: 10.00%;" />
                                                            <col style="width: 23.32%;" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="label">
                                                                Nº Serie
                                                            </td>
                                                            <td class="input" id="txtSerieMotorMaquina" runat="server">
                                                                
                                                            </td>
                                                            <td class="label" id="lblNumeroMotorMaquina">
                                                                Nº Motor
                                                            </td>
                                                            <td class="input" id="txtNumeroMotorMaquina" runat="server">
                                                                
                                                            </td>
                                                            <td class="label">
                                                                Año
                                                            </td>
                                                            <td class="input" id="txtFabricacionMaquina" runat="server">
                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Marca
                                                            </td>
                                                            <td class="input" id="txtMarcaMaquina" runat="server">                                                                
                                                            </td>
                                                            <td class="label">
                                                                Modelo
                                                            </td>
                                                            <td class="input" id="txtModeloMaquina" runat="server">                                                                
                                                            </td>
                                                            <td class="label">
                                                                Tipo Carrocería
                                                            </td>
                                                            <td class="input" id="txtTipoCarroceriaMaquina" runat="server">
                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Descripción
                                                            </td>
                                                            <td class="input">
                                                                <textarea id="txtDescripcionAutoMaquina" runat="server" name="txtDescripcionAutoMaquina"
                                                                    class="css_input" rows="2" cols="51" readonly="readonly" disabled="disabled"></textarea>
                                                            </td>
                                                            <td class="label">
                                                                Estado del Bien
                                                            </td>
                                                            <td class="input" id="cmbEstadobienMaquina" runat="server">
                                                               
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Cantidad
                                                            </td>
                                                            <td class="input" id="txtCantidadMaquina" runat="server">
                                                            </td>
                                                            <td class="label">
                                                                Medidas
                                                            </td>
                                                            <td class="input" id="txtMedidasMaquina" runat="server">
                                                                
                                                            </td>
                                                            <td class="label">
                                                                Placa
                                                            </td>
                                                            <td class="input" id="txtPlacaMaquina" runat="server">                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Uso
                                                            </td>
                                                            <td class="input" id="txtUsoBienMaquina" runat="server">
                                                                
                                                            </td>
                                                            <td class="label">
                                                                Ubicación
                                                            </td>
                                                            <td class="input" colspan="3" id="txtUbicacionBienMaquina" runat="server">
                                                               
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Departamento
                                                            </td>
                                                            <td class="input" id="cmbDepartamentoMaquinaria" runat="server">                                                               
                                                            </td>
                                                            <td class="label">
                                                                Provincia
                                                            </td>
                                                            <td class="input" id="cmbProvinciaMaquinaria" runat="server">
                                                               
                                                            </td>
                                                            <td class="label">
                                                                Distrito
                                                            </td>
                                                            <td class="input" id="cmbDistritoMaquinaria" runat="server">
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                   
                                                     
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;" id="btnEditarMaquina">
                                                            <a href="javascript:fn_EditarMaquina();">
                                                                <img alt="" src="../../Util/images/ico_acc_ver.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />&nbsp;&nbsp;&nbsp;Ver </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;" id="btnCancelarMaquina">
                                                            <a href="javascript:fn_CancelarMaquina();">
                                                                <img alt="" src="../../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;
                                                                    border: 0px;" />&nbsp;&nbsp;&nbsp;Cancelar </a>
                                                        </div>
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="jqGrid_lista_B">
                                                        <tr>
                                                            <td />
                                                        </tr>
                                                    </table>
                                                    <div id="jqGrid_pager_B">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                                <!-- Datos Otros -->
                                <div id="dvDatosOtros" style="border: 0;">
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Datos del Bien</legend>
                                        <table id="tb_DatosOtros">
                                            <tr>
                                                <td>
                                                    <table id="tb_formulario" cellpadding="0" cellspacing="3" style="border: 0;">
                                                        <colgroup>
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="label">
                                                                Uso
                                                            </td>
                                                            <td class="input" id="txtUsoDatosOtros" runat="server">                                                               
                                                            </td>
                                                            <td class="label">
                                                                Ubicación
                                                            </td>
                                                            <td class="input" id="txtUbicacionDatosOtros" runat="server">                                                                
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Descripción
                                                            </td>
                                                            <td class="input">
                                                                <textarea id="txtDescripcionDatosOtros" name="txtDescripcionDatosOtros" rows="2"
                                                                    class="css_input" cols="51" readonly="readonly" disabled="disabled"></textarea>
                                                            </td>
                                                            <td class="label">
                                                                Marca
                                                            </td>
                                                            <td class="input" id="txtMarcaDatosOtros" runat="server">
                                                                
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Cantidad
                                                            </td>
                                                            <td class="input" id="txtCantidadDatosOtros" runat="server">
                                                                
                                                            </td>
                                                            <td class="label">
                                                                Modelo
                                                            </td>
                                                            <td class="input" id="txtModeloDatosOtros" runat="server">                                                                
                                                            </td>
                                                            <td class="label">
                                                                Estado del Bien
                                                            </td>
                                                            <td class="input" id="cmbEstadoDatosOtros" runat="server">                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Departamento
                                                            </td>
                                                            <td class="input" id="cmbDepartamentoDatosOtros" runat="server">
                                                                
                                                            </td>
                                                            <td class="label">
                                                                Provincia
                                                            </td>
                                                            <td class="input" id="cmbProvinciaDatosOtros" runat="server">                                                                
                                                            </td>
                                                            <td class="label">
                                                                Distrito
                                                            </td>
                                                            <td class="input" id="cmbDistritoDatosOtros" runat="server">                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Partida Registral
                                                            </td>
                                                            <td class="input" id="txtPartidaRegistralDatosOtros" runat="server">                                                                
                                                            </td>
                                                            <td class="label">
                                                                Oficina Registral
                                                            </td>
                                                            <td class="input" id="txtOficinaRegistralDatosOtros" runat="server">                                                                
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td style="text-align: left;">
                                               
                                                 
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;" id="btnEditarDatosOtros">
                                                            <a href="javascript:fn_EditarDatosOtros();">
                                                                <img alt="" src="../../Util/images/ico_acc_ver.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />&nbsp;&nbsp;Ver&nbsp; </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;" id="btnCancelarDatosOtros">
                                                            <a href="javascript:fn_CancelarDatosOtros();">
                                                                <img alt="" src="../../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;
                                                                    border: 0px;" />&nbsp;&nbsp;Cancelar </a>
                                                        </div>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="jqGrid_lista_J">
                                                        <tr>
                                                            <td />
                                                        </tr>
                                                    </table>
                                                    <div id="jqGrid_pager_J">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                                <!-- Proveedores -->
                                <fieldset>
                                    <legend class="css_lbl_subTitulo">Proveedores</legend>
                                    <table id="jqGrid_lista_F">
                                        <tr>
                                            <td />
                                        </tr>
                                    </table>
                                    <div id="jqGrid_pager_F">
                                    </div>
                                </fieldset>
                            </div>
                            <!--
        			         TAB :: CONDICIONES ADICIONALES
        			         !-->
                            <div id="tab-1">
                                <div class="dv_tabla_contenedora" id="dv_info_Req_condiciones_ad">
                                    <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                        <tr>
                                            <td>
                                                <strong>CONDICIONES ADICIONALES</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="border: 0;">
                                                    <tr>
                                                        <td>
                                                            <table id="jqGrid_lista_E">
                                                                <tr>
                                                                    <td />
                                                                </tr>
                                                            </table>
                                                            <div id="jqGrid_pager_E">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="dv_tabla_contenedora">
                                    <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                        <tr>
                                            <td>
                                                <strong>DOCUMENTOS</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="border: 0;">
                                                    <tr>
                                                        <td>
                                                            <table id="jqGrid_lista_K">
                                                                <tr>
                                                                    <td />
                                                                </tr>
                                                            </table>
                                                            <div id="jqGrid_pager_K">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <!--
        			         TAB :: REPRESENTANTES A FIRMAR
        			         !-->
                            <div id="tab-2">
                                <div class="dv_tabla_contenedora">
                                    <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                        <tr>
                                            <td>
                                                <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                                    <tr>
                                                        <td class="titulo css_lbl_tituloContenido">
                                                            Representantes Interbank
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="border: 0;">
                                                    <tr>
                                                        <td>
                                                            <table id="jqGrid_lista_C">
                                                                <tr>
                                                                    <td />
                                                                </tr>
                                                            </table>
                                                            <div id="jqGrid_pager_C">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <!--
        			         TAB :: OTROS CONCEPTOS
        			         !-->
                            <div id="tab-3">
                                <div class="dv_tabla_contenedora">
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Situación del Crédito</legend>
                                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                            <colgroup>
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                            </colgroup>
                                            <tr>
                                                <td class="label">
                                                    Segmento
                                                </td>
                                                <td class="input" id="txtSegmento" runat="server">
                                                    
                                                </td>
                                                <td class="label">
                                                    Sectorista
                                                </td>
                                                <td class="input" id="txtSectorista" runat="server">
                                                    
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Fecha Desembolso
                                                </td>
                                                <td class="input" id="txtFechaDesembolso" runat="server">
                                                    
                                                </td>
                                                <td class="label">
                                                    Fecha Vcto. Cuota
                                                </td>
                                                <td class="input" id="txtFechaVctoCuota" runat="server" >
                                                    
                                                </td>
                                                <td class="label" >
                                                    Fecha Vcto. Crédito
                                                </td>
                                                <td class="input" id="txtFechaVctoCredito" runat="server">
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Tasas y Comisiones</legend>
                                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                            <colgroup>
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                            </colgroup>
                                            <tr>
                                                <td class="label">
                                                    T.E.A. %
                                                </td>
                                                <td class="input" id="txtTea" runat="server">
                                                    
                                                </td>
                                                <td class="label">
                                                    Precuota %
                                                </td>
                                                <td class="input" id="txtprecuota" runat="server">
                                                    
                                                </td>
                                                <td class="label">
                                                    Opción de Compra
                                                </td>
                                                <td class="input" id="txtOpcionCompra" runat="server">
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Comisión de Activación
                                                </td>
                                                <td class="input" id="txtComisionActivacion" runat="server">
                                                    
                                                </td>
                                                <td class="label">
                                                    Comisión de Estructuración
                                                </td>
                                                <td class="input" id="txtComisionEstructuracion" runat="server" >
                                                 
                                                </td>
                                                <td class="label">
                                                    Otras Comisiones
                                                </td>
                                                <td class="input">
                                                    <textarea id="txtaOtrasComisiones" name="txtaOtrasComisiones" cols="20" rows="2"
                                                        style="width: 350px;" runat="server" class="css_input" readonly="readonly" disabled="disabled">
                                                    
                                                        </textarea>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Penalidades</legend>
                                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                            <colgroup>
                                                <col style="width: 25.00%;" />
                                                <col style="width: 25.00%;" />
                                                <col style="width: 25.00%;" />
                                                <col style="width: 25.00%;" />
                                                
                                            </colgroup>
                                            <tr>
                                                <td class="label">
                                                    % Del Importe Pendiente de Pago, por Día de Atraso
                                                </td>
                                                <td class="input" id="txtImporteAtrasoPorc" runat="server" style="text-align:left">
                                                    
                                                </td>
                                                <td class="label">
                                                    Otros
                                                </td>
                                                <td class="input">
                                                    <textarea id="txtaOtrasPenalidades" name="txtaOtrasPenalidades" cols="20" rows="2"
                                                        style="width: 350px;" runat="server" class="css_input" readonly="readonly" disabled="disabled"></textarea>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Días desde el Vencimiento a la Fecha de Pago
                                                </td>
                                                <td class="input" id="txtdiasVencimiento" runat="server" style="text-align:left">
                                                    
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    % De la cuota
                                                </td>
                                                <td class="input" colspan="3" id="txtPorcentajeCuota" runat="server" style="text-align:left">
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Total de Importe</legend>
                                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                            <colgroup>
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                            </colgroup>
                                            <tr>
                                                <td class="label">
                                                    Desembolsado
                                                </td>
                                                <td class="input" id="txtDesembolsado" runat="server">                                                  
                                                </td>
                                                <td class="label">
                                                    Principal Pagado
                                                </td>
                                                <td class="input" id="txtPrincipalPagado" runat="server">                                                    
                                                </td>
                                                <td class="label">
                                                    Pagado en el día
                                                </td>
                                                <td class="input" id="txtPagadoDia" runat="server">                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Saldo Principal
                                                </td>
                                                <td class="input" id="txtSaldoPrincipal" runat="server">
                                                </td>
                                                <td class="label">
                                                    Saldo por Desembolsar
                                                </td>
                                                <td class="input" id="txtSaldoDesembolsar" runat="server">                                                    
                                                </td>
                                                <td class="label">
                                                    Comisiones por Cobrar
                                                </td>
                                                <td class="input" id="txtComisionesCobrar" runat="server" >
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>                                    
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Total de Cuotas</legend>
                                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                            <colgroup>
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                            </colgroup>
                                            <tr>
                                                <td class="label">
                                                    Nº de Cuotas
                                                </td>
                                                <td class="input" id="txtNroCuotas" runat="server">
                                                </td>
                                                <td class="label">
                                                    Cuotas Pagadas
                                                </td>
                                                <td class="input" id="txtCuotasPagadas" runat="server">
                                                </td>
                                                <td class="label">
                                                    Pendientes Vigentes
                                                </td>
                                                <td class="input" id="txtPendientesVigentes" runat="server">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                   Pendientes Vencidas
                                                </td>
                                                <td class="input" id="txtPendientesVencidas" runat="server">
                                                </td>
                                                <td class="label">
                                                    Opción de Compra
                                                </td>
                                                <td class="input" id="txtTotalOpcionCompra" runat="server">
                                               </td>
                                                <td class="label"> 
                                                    Seguro
                                                </td>
                                                <td class="input" id="txtSeguro" runat="server">
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </div>
                            <!--
        			         TAB :: DATOS NOTARIALES
        			         !-->
                            <div id="tab-4">
                                <div id="dv_datosNotariales" style="border: 0px;">
                                    <fieldset>
                                        <input id="hdnCodigoNotarial" type="hidden" />
                                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                            <colgroup>
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                            </colgroup>
                                            <tr>
                                                <td class="label">
                                                    Departamento
                                                </td>
                                                <td class="input" id="cmbDepartamento" runat="server">
                                                   
                                                </td>
                                                <td class="label">
                                                    Provincia
                                                </td>
                                                <td class="input" id="cmbProvincia2" runat="server">
                                                    
                                                </td>
                                                <td class="label" id="lblcmbdistrito">
                                                    Distrito
                                                </td>
                                                <td class="input" id="cmbDistrito2" runat="server">
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Notaria
                                                </td>
                                                <td class="input" id="cmbNotariaDatoNotarial" runat="server">
                                             
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <!--INICIO IBK - RPH -->
							                <tr>
							                   <td class="label">Contacto</td>
						                       <td class="input" id="txtContactoNotario" runat="server" >
						                       </td>
						                       <td class="label">Correo Contacto</td>
						                       <td class="input" colspan="3" id="txtCorreoNotaria" runat="server"/>
							                </tr>
							                <!--FIN-->
                                            <tr>
                                                <td class="label">
                                                    Contrato de
                                                </td>
                                                <td class="input" id="cmbTipoContrato" runat="server" >
                                                  
                                                </td>
                                                <td class="label">
                                                    Nº Kardex
                                                </td>
                                                <td class="input" id="txtKardex" runat="server">
                                                    
                                                </td>
                                                <td class="label">
                                                    Fecha de Envío
                                                </td>
                                                <td class="input" id="txtFechaContratoNotarial" runat="server">
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Observaciones
                                                </td>
                                                <td class="input" colspan="5">
                                                    <textarea id="txtObservacionesNotariales" name="txtObservacionesNotariales" class="css_input"
                                                        rows="3" cols="70" style="width: 400px;" readonly="readonly" disabled="disabled"></textarea>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="border: 0;">
                                            <tr>
                                                <td>
                                                    <div id="dv_AccionesDatosNotariales" style="border: 0;">
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;" id="btnEditarDatosNotariales">
                                                            <a href="javascript:fn_EditarDatosNotariales();">
                                                                <img alt="" src="../../Util/images/ico_acc_ver.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />&nbsp;&nbsp;&nbsp;Ver </a>
                                                        </div>
                                                         <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;" id="btnCancelarDatosNotariales">
                                                            <a href="javascript:fn_CancelarDatosNotariales();">
                                                                <img alt="" src="../../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />&nbsp;&nbsp;&nbsp;Cancelar </a>
                                                        </div>
                                                    </div>
                                            
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="jqGrid_lista_G">
                                                        <tr>
                                                            <td />
                                                        </tr>
                                                    </table>
                                                    <div id="jqGrid_pager_G">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </div>
                            <!--
        			         TAB :: ADENDAS
        			         !-->
                            <div id="tab-5">
                                <div id="dv_adendas" style="border: 0;">
                                    <fieldset>
                                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td class="label">
                                                    Fecha de Envio
                                                </td>
                                                <td class="input" id="txtFechaAdenda" runat="server">                                                
                                                </td>
                                                <td class="label">
                                                    Fecha de Escritura Pública
                                                </td>
                                                <td class="input" id="txtFechaEscrituraPub" runat="server">
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                         
                                            <tr>
                                                <td class="label">
                                                    Motivo
                                                </td>
                                                <td class="input" colspan="5">
                                                    <textarea id="txtaMotivo" name="txtaMotivo" cols="90" class="css_input" rows="4" readonly="readonly" disabled="disabled"></textarea>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Por Cuenta de
                                                </td>
                                                <td class="input" colspan="5" id="cmbporCuentade" runat="server">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Departamento
                                                </td>
                                                <td class="input" id="cmbDepartamentoAdenda" runat="server">
                                                </td>
                                                <td class="label">
                                                    Provincia
                                                </td>
                                                <td class="input" id="cmbProvienciaAdenda" runat="server">
                                                </td>
                                                <td class="label">
                                                    Distrito
                                                </td>
                                                <td class="input" id="cmbDistritoAdenda" runat="server">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Notaria
                                                </td>
                                                <td class="input" id="cmbNotariaAdenda" runat="server">
                                                </td>
                                                <td class="label">
                                                    Nº Kardex
                                                </td>
                                                <td class="input" colspan="3" id="txtKardexAdenda" runat="server">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <table style="border: 0;">
                                                        <tr>
                                                            <td>
                                                                
                                                                    <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;" id="btn_EditarAdenda">
                                                                        <a href="javascript:fn_EditarAdenda();">
                                                                            <img alt="" src="../../Util/images/ico_acc_ver.gif" style="width: 16px; height: 16px;
                                                                                border: 0;" />&nbsp;&nbsp;&nbsp;Ver </a>
                                                                    </div>
                                                                    <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;" id="btn_CancelarAdenda">
                                                                        <a href="javascript:fn_CancelarAdenda();">
                                                                            <img alt="" src="../../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;
                                                                                border: 0;" />&nbsp;&nbsp;&nbsp;Cancelar </a>
                                                                    </div>
                                                               
                                                                <div id="dv_ProcesoAdenda" style="border: 0;">
                                                                    <%--<div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                                        <a href="javascript:fn_GuardarAdenda();">
                                                                            <img alt="" src="../../Util/images/ico_acc_grabar.gif" style="width: 16px; height: 16px;
                                                                                border: 0;" />Guardar </a>
                                                                    </div>
                                                                    <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                                        <a href="javascript:fn_CancelarAdenda();">
                                                                            <img alt="" src="../../../../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;
                                                                                border: 0;" />&nbsp;&nbsp;&nbsp;Cancelar </a>
                                                                    </div>--%>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table id="jqGrid_lista_I">
                                                                    <tr>
                                                                        <td />
                                                                    </tr>
                                                                </table>
                                                                <div id="jqGrid_pager_I">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </div>
                            
                             <!-- ****************** -->	
					            <!-- TAB :: CRONOGRAMA  -->	
					            <!-- ****************** -->	
					            <div id="tab-7">
					                
					                <fieldset>
						                <legend  class="css_lbl_subTitulo">Datos Cronograma</legend>
    						            
       						            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3"  style="width:100%">
       						                <colgroup>                           
                                                <col style="width:16.66%" />
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                                <col style="width:16.66%"/>                                                       
                                            </colgroup>
					                        <tr>
				                                <td class="label" >Tipo de Cronograma</td>
			                                    <td class="input" id="txtTipoCronograma" runat="server" >
			                                    </td>
			                                    <td class="label" >Nro. Cuotas</td>
			                                    <td class="input" id="txtNroCuotasCronograma" runat="server" >
			                                    </td>
			                                    <td class="label">Periodicidad</td>
			                                    <td class="input" id="txtPeriodicidad" runat="server">
			                                    </td>
				                            </tr>
				                            <tr> 
				                                 <td class="label" >Frecuencia de Pago</td>
    						                     <td class="input" id="txtFrecuenciaPago"  runat="server">
    						                     </td>
    						                     <td class="label">Plazo Gracia</td>
			                                     <td class="input" id="txtPlazoGracia" runat="server">
			                                     </td>
			                                     <td class="label">Tipo de Gracia</td>
			                                     <td class="input" id="txtTipoGracia" runat="server">
			                                     </td>                               
					                        </tr>
					                        <tr>	
					                             <td class="label" >Fecha Activación</td>
		                                         <td class="input" id="txtFechaMaxActivacion" runat="server"></td>				                         	
			                                     <td class="label">Fecha 1º Vencimiento</td>
			                                     <td class="input" id="txtFechavence" runat="server">
			                                     </td>
                                            </tr>					                 					         
                                        </table>
                                        
                                    </fieldset>
                                                                    
						            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width:100%;">
						                <tr>
						                    <td id="tbCronograma" style="text-align:center">
                                                <table id="jqGrid_lista_L"><tr><td/></tr></table> 
                                                <div id="jqGrid_pager_L"></div>
						                    </td>
						                </tr>
						            </table>
					            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
    
       
        <input type="hidden" id="hddVersionCotizacion" name="hddVersionCotizacion" runat="server" />
        
        <input type="hidden" id="hddBloqueoExistente" name="hddBloqueoExistente" />
        <input type="hidden" id="hddBloqueoCodigo" name="hddBloqueoCodigo" />
        <input type="hidden" id="hddBloqueoCodUsuario" name="hddBloqueoCodUsuario" />
        <input type="hidden" id="hddBloqueoNomUsuario" name="hddBloqueoNomUsuario" />
        <input type="hidden" id="hddBloqueoFecha" name="hddBloqueoFecha" />
        <input type="hidden" id="hddUbigeoUbicacion" name="hddUbigeoUbicacion" runat="server" />
        <input type="button" name="cmdGuardarRepresentante" id="cmdGuardarRepresentante"
            onclick="javascript:fn_ListaRepresentantesClienteFromModal();" style="display: none;" />
        <input id="hddCodigoCotizacion" type="hidden" runat="server" />
        <input id="hddCorreocontacto" type="hidden" runat="server" />
        <input id="hdnCodigoAdenda" type="hidden" />
        <input id="hddCodProductoFinancieroActivo" type="hidden" runat="server" />
        <input id="hddCodMoneda" type="hidden" runat="server" />
        <input id="hddTipoDocumentoConyuge" type="hidden" runat="server" />
        <input id="hddCodigoTipoPersona" type="hidden" runat="server" />
        <input id="hddTipoDocumento" type="hidden" runat="server" />
        <input id="hddCodigoEstadoCivil" type="hidden" runat="server" />
        <input id="hddEstadoCivil" type="hidden" runat="server" />
        <input id="hddCodigoContrato" type="hidden" runat="server" />
        <input id="hddTipoRubroFinanciamiento" type="hidden" runat="server" />
        <input id="hddCodigoClasificacionContrato" type="hidden" runat="server" />
        <input id="hddCodigoTipoBien" type="hidden" runat="server" />
        <input id="hddSecFinanciamiento" type="hidden" />
        <input id="hddRowId" type="hidden" />
        <input id="hddCodProveedor" type="hidden" />
        <input id="hddCodigoEstadoBien" type="hidden" runat="server" />
        <input id="hddCodigoEstadoContrato" type="hidden" runat="server" />
        <input id="hddFechaFirmaNotaria" type="hidden" runat="server" />
        <input id="hddClasifContratoSeleccion" type="hidden" runat="server" />
        <!-- Datos del bien -->
        <!-- Detecta cambios en el contenido de los controles, antes de salir de la actual ventana. -->
        <input id="hddCambiosSinGuardar" type="hidden" />
        <!-- Indica si alguno de los datos del contrato o de otros documentos han cambiado y requieren volver a generar el contrato.
             El cambio se registra a través de la base de datos -->
        <input id="hddFlagModificado" type="hidden" runat="server" />
        <input id="hddMensajeCorreo" type="hidden" runat="server" />
        <input id="hddFechaActual" type="hidden" runat="server" />
        <!-- Datos del bien -->
        <input id="hddUso" type="hidden" runat="server" />
        <input id="hddUbicacion" type="hidden" runat="server" />
        <!-- Contrato (Anexos) -->
        <input id="btnAdjuntarArchivo" type="button" runat="server" onclick="javascript:fn_ActualizarAnexo();"
            style="visibility: hidden; display: none;" />
        <!-- Contrato Valida Modificaciones -->
        <input id="hddAdjuntarArchivo" type="hidden" runat="server" />
        <input id="hddValidaModificacion" type="hidden" />
        <input id="hddGeneraContrato_Adjunto" type="hidden" />
        <!-- Datos del conyugue -->
        <input id="btnAdjuntarArchivoDocumentoSeparacion" type="button" runat="server" onclick="javascript:fn_ActualizarDocumentoSeparacion();"
            style="visibility: hidden; display: none;" />
        <input id="hddAdjuntarArchivoDocumentoSeparacion" type="hidden" runat="server" />
        <!-- Otros conceptos -->
        <input id="btnAdjuntarArchivoOtroConcepto" type="button" runat="server" onclick="javascript:fn_ActualizarArchivoAdjunto();"
            style="visibility: hidden; display: none;" />
        <input id="hddAdjuntarArchivoOtroConcepto" type="hidden" runat="server" />
        <!-- Adenda - nuevo -->
        <input id="btnAdjuntarArchivoNotarialNuevo" type="button" runat="server" onclick="javascript:fn_ActualizarArchivoNotarialNuevo();"
            style="visibility: hidden; display: none;" />
        <input id="hddAdjuntarArchivoNotarialNuevo" type="hidden" runat="server" />
        <!-- Adenda - editar -->
        <input id="btnAdjuntarArchivoNotarialEditar" type="button" runat="server" onclick="javascript:fn_ActualizarArchivoNotarialEditar();"
            style="visibility: hidden; display: none;" />
        <input id="hddAdjuntarArchivoNotarialEditar" type="hidden" runat="server" />
        <!-- Texto predefinido -->
        <input id="btnTextoPredefinido" type="button" runat="server" onclick="javascript:fn_ActualizarTextoPredefinido();"
            style="visibility: hidden; display: none;" />
        <input id="hddTextoPredefinido" type="hidden" runat="server" />
        <!-- Retorno -->
        <input type="hidden" id="hddFlagRetorno" name="hddFlagRetorno" value="" runat="server" />
    </div>
    </form>
</body>
</html>
