<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMntClienteModificacion.aspx.vb"
    Inherits="Mantenimiento_frmMntClienteModificacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SGL :: Sistema de Gestión de Leasing</title>
    <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="../Util/images/PV16x16.ico" />
    <!-- Estilos -->
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery.jscrollpane.css"
        media="all" />
    <link type="text/css" rel="stylesheet" href="../Util/css/css_global.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/css_formulario.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/css_fuente.css" />
    <!-- JavaScript -->

    <script type='text/javascript' src="../Util/js/jquery/jquery-1.6.2.min.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.jscrollpane.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.jscrollpane.min.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.mousewheel.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.ui.global.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.validText.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.validNumber.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.maxlength.js"> </script>

    <script type="text/javascript" src="../Util/js/js_global.js"></script>

    <script type='text/javascript' src="../Util/js/js_util.modal.js"> </script>

    <script type='text/javascript' src="../Util/js/js_util.funcion.js"> </script>

    <script type='text/javascript' src="../Util/js/js_util.date.js"> </script>

    <script type='text/javascript' src="../Util/js/js_util.ajax.js"> </script>

    <script type="text/javascript" src="../Util/js/jquery/jquery.dateFormat-1.0.js"></script>

    <script type="text/javascript" src="../Util/js/jquery/jshashtable.js"></script>

    <script type="text/javascript" src="../Util/js/js_util.Grilla.js"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmMntClienteModificacion.aspx.js"> </script>

 

</head>
<body>
    <form id="frmMntModificacionCliente" runat="server">
    <div id="dv_cuerpoModal">
        <input id="hidCodSup" type="hidden" runat="server" />
        <input id="hidDir" type="hidden" runat="server" />
        <input id="hidCodUni" type="hidden" runat="server" />
        <input id="HidCodigoContacto" type="hidden" runat="server" />
        <div style="display: none">
        <asp:Button ID="btnCargarContacto" runat="server"  />    
        </div>
        
        
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="icono">
                      <img alt="" src="../Util/images/ico_mdl_contrato.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                     <div class="css_lbl_subTitulo">
                        Mantenimiento</div>
                    <div class="css_lbl_titulo">
                        Cliente :: Editar</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:parent.fn_util_CierraModal();">
                            <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Cerrar </a>
                    </div>
                    <div id="dv_separacion" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_grabar" class="dv_img_boton">
                        <a href="javascript:fn_grabar();">
                            <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0" /><br />
                            Grabar </a>
                    </div>
                </td>
            </tr>
        </table>
       
        <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="lineas">
                </td>
            </tr>
     
            <tr>
                <td>
                     <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos de Cliente
                                </td>
                               
                            </tr>
                        </table>
                </td>
            </tr>
        </table>
        
         <div class="dv_tabla_contenedora">
        <table id="tb_formulario" border="0" cellpadding="0">
            <tr>
                <td class="label" valign="top">
                    Dirección
                </td>
                <td class="input">
                    <input id="txtDireccion" type="text" class="css_input" runat="server" style="width: 350px" />
                </td>
                <td >
                   
                </td>
                <td >
                    
                </td>
            </tr>
            
            
        </table>
                 
        </div>
        <br />
        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Contacto Preferencial
                                </td>
                           
                            </tr>
                        </table>
                           <div class="dv_tabla_contenedora">
             <table id="tb_formulario" border="0" cellpadding="0">
            <tr>
                <td class="label" valign="top">
                    Nombre del Contacto
                </td>
                <td class="input">
                    <input id="txtNombreContactoP" type="text" class="css_input_inactivo" runat="server" disabled="disabled" readonly="readonly" />
                    <img id="imgBuscarContactoP" alt="" src="../Util/images/ico_buscar.jpg" style="cursor:pointer; vertical-align:middle;" runat="server" />
                </td>
              <td class="label" valign="top">
                    Correo del Contacto
                </td>
                <td class="input">
                    <input id="txtCorreoContactoP" type="text" class="css_input_inactivo" runat="server" disabled="disabled" readonly="readonly" runat="server" />
                </td>
            </tr> 
              <tr>
                <td class="label" valign="top">
                    Teléfono del Contacto
                </td>
                <td class="input">
                    <input id="txtTelefonoContactoP" type="text" class="css_input_inactivo" runat="server" disabled="disabled" readonly="readonly"  runat="server" />
                </td>
              
            </tr>  
            </table>   
             </div>
    </div>
    </form>
</body>
</html>
