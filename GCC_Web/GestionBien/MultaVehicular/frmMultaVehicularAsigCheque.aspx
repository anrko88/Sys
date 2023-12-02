<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMultaVehicularAsigCheque.aspx.vb" Inherits="GestionBien_MultaVehicular_frmMultaVehicularAsigCheque" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SGL :: Sistema de Gestión de Leasing</title>
    
    <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="../../Util/images/PV16x16.ico" />
    
    <!-- Estilos --> 
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery.jscrollpane.css" media="all" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_global.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_formulario.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_fuente.css" />
    <!-- JavaScript -->

    <script type='text/javascript' src="../../Util/js/jquery/jquery-1.6.2.min.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.jscrollpane.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.jscrollpane.min.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.mousewheel.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.ui.global.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.validText.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.validNumber.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.maxlength.js"> </script>
    
    <script type="text/javascript" src="../../Util/js/js_global.js"></script>

    <script type='text/javascript' src="../../Util/js/js_util.modal.js"> </script>

    <script type='text/javascript' src="../../Util/js/js_util.funcion.js"> </script>

    <script type='text/javascript' src="../../Util/js/js_util.date.js"> </script>

    <script type='text/javascript' src="../../Util/js/js_util.ajax.js"> </script>

    <script type="text/javascript" src="../../Util/js/jquery/jquery.dateFormat-1.0.js"></script>

    <script type="text/javascript" src="../../Util/js/jquery/jshashtable.js"></script>    

    <script type="text/javascript" src="../../Util/js/js_util.Grilla.js"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmMultaVehicularAsigCheque.aspx.js"> </script>

</head>
<body>
    <form id="frmImpuestoVehicularAsignarCheque" runat="server">
    <div id="dv_cuerpoModal">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="icono">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Util/images/ico_mdl_multa.gif" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Gestión del Bien</div>
                    <div class="css_lbl_titulo">
                        Multa Vehicular : Asignar Cheque</div>
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
                    <div id="dv_separacion" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_grabar" class="dv_img_boton">
                        <a href="javascript:fn_asignaCheque();">
                            <img alt="" src="../../Util/images/ico_acc_grabar.gif" border="0" /><br />
                            Asignar </a>
                    </div>
                </td>
            </tr>
        </table>
        
        <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="lineas">
                    </td>
                </tr>
                </table>
     <br/>   
    <table id="tb_formulario" border="0" cellpadding="0">
            <tr>
                <td class="label">
                    N° Lote
                </td>
                <td class="input">
                    <input id="txtNroLote" type="text" class="css_input" runat="server" />
                </td>
                <td class="label" valign="top">
                    Municipalidad
                </td>
                <td class="input">
                    <input id="txtMunicipalidad" type="text" class="css_input" runat="server" value="" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    N° Cheque
                </td>
                <td class="input">
                    <input id="txtCheque" type="text" class="css_input" runat="server" />
                </td>
                <td class="label" valign="top">
                    Importe Total
                </td>
                <td class="input">
                    <input id="txtImporteTotal" type="text" class="css_input" runat="server" value="" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    Fecha de Pago
                </td>
                <td class="input">
                    <input id="txtFechaPago" type="text" class="css_input" runat="server" size="12" />
                </td>
                <td class="label" valign="top">
                    Fecha de Generación
                </td>
                <td class="input">
                    <input id="txtFechaGeneracion" type="text" class="css_input" runat="server" value="" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
