<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultaDocumentoBienContrato.aspx.vb"
    Inherits="Consultas_frmConsultaDocumentoBienContrato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Documentos y Comentarios</title>
    <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="../../Util/images/PV16x16.ico" />
    <!-- Estilos -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery.jscrollpane.css" media="all" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_global.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_formulario.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_fuente.css" />
    
    <!-- JavaScript -->
    <script src="../../Util/js/jquery/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"></script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.jscrollpane.min.js"></script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.mousewheel.js"></script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.ui.global.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.validText.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.validNumber.js"> </script>
    <script type='text/javascript' src="../../Util/js/jquery/jquery.maxlength.js"> </script>
    <script type="text/javascript" src="../../Util/js/jquery/json2.js"></script>
    <script type="text/javascript" src="../../Util/js/js_global.js"></script>
    <script type='text/javascript' src="../../Util/js/js_util.modal.js"></script>
    <script type='text/javascript' src="../../Util/js/js_util.funcion.js"></script>
    <script type='text/javascript' src="../../Util/js/js_util.date.js"></script>
    <script type='text/javascript' src="../../Util/js/js_util.ajax.js"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />
    <script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>
    <script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmConsultaDocumentoBienContrato.aspx.js"></script>

</head>
<body>
    <form id="frmMotivoRechazo" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="icono">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Util/images/ico_mdl_mantenimiento.jpg" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Consultas</div>
                    <div class="css_lbl_titulo">
                        Documentos y Comentarios</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:parent.fn_util_CierraModal();">
                            <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Cerrar </a>
                    </div>
                    <%--<div id="dv_separacion" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_grabar" class="dv_img_boton">
                        <a href="javascript:fn_Guardar();">
                            <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0" /><br />
                            Grabar </a>
                    </div>--%>
                </td>
            </tr>
        </table>
        
        <input type="hidden" name="hddCodigoContrato" id="hddCodigoContrato" value="" runat="server" />
        <input type="hidden" name="hddCodigoDocumento" id="hddCodigoDocumento" value="" runat="server" />
        <input type="hidden" id="hddCodigoBien" name="hddCodigoBien" value="" runat="server" />
        <input type="hidden" id="hddTipoTransaccion" name="hddTipoTransaccion" value="NUEVO" runat="server" />
        <input type="hidden" id="hddEdicion" name="hddEdicion" value="" runat="server" />
        <input type="hidden" id="hddObservacion" name="hddObservacion" value="" runat="server" />
        <input type="hidden" id="hddArchivo" name="hddArchivo" value="" runat="server" />
        <input type="hidden" id="hddAdjunto" name="hddAdjunto" value="" runat="server" />
        <input type="hidden" id="hidDetalle" value="" runat="server" />
        <input type="hidden" id="hidSecFinanciamiento" value="" runat="server" />
        
        <asp:Button ID="btnGrabar" runat="server" Style="display: none;" Text="Graba" />
        
        <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" style="width:100%; padding-right: 0px;" >
		    <tr>
			    <td class="lineas"></td>			
		    </tr>
	    </table>
	    <br/>
        <table id="tb_formulario" border="0" cellpadding="0">
            <tr id="tr_nombre" runat="server">
                <td class="label" valign="top">
                    Nombre Archivo
                </td>
                <td class="input" runat="server" id="txtNombreArchivo">
                    <%--<input id="txtNombreArchivo" type="text" class="css_input" size="50" runat="server" />--%>
                </td>
            </tr>
            <tr id="tr_adjuntar" runat="server">
                <td class="label" valign="top">
                    Adjuntar Archivo
                </td>
                <td class="input">
                    <%--<asp:FileUpload ID="txtArchivoDocumentos" runat="server" />--%>
                    <asp:Label ID="lblNoArchivo" runat="server" CssClass="inputform" width="250px" Text="(No se adjuntó archivo)"  Visible="false"></asp:Label>
                    <asp:HyperLink ID="hlkArchivo" style ="cursor:hand;" target="_blank" CssClass="css_link"
                    Width="200px" runat="server" Text="(Ver Archivo Adjunto)" Visible="false" ></asp:HyperLink>     
                </td>
            </tr>
            <tr id="tr_comentario" runat="server">
                <td class="label" valign="top" style="vertical-align: top">
                    Comentario
                </td>
                <td class="input">
                    <textarea name="txtComentario" id="txtComentario" cols="75" rows="6" runat="server" readonly="readonly" disabled="disabled ></textarea>
                </td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>
