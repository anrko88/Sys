<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmImpuestoVehicularCargarMasivo.aspx.vb" Inherits="GestionBien_ImpuestoVehicular_frmImpuestoVehicularCargarMasivo" %>

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
    <script type='text/javascript' src="frmImpuestoVehicularCargarMasivo.aspx.js"> </script>

</head>
<body>
    <form id="frmImpuestoVehicularCargaMasivo" runat="server">
    <asp:Button ID="btnGrabar" runat="server" Text="Button" style="display: none" runat="server" />
    <div id="dv_cuerpoModal">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="icono">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Util/images/ico_mdl_impuesto.gif" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Gestión del Bien</div>
                    <div class="css_lbl_titulo">
                        Impuesto Vehicular : Carga Masiva</div>
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
                        <a href="javascript:fn_GrabarMasivoCarga();">
                            <img alt="" src="../../Util/images/ico_acc_grabar.gif" border="0" /><br />
                            Procesar </a>
                    </div>
                </td>
            </tr>
        </table>
        
    <table id="tb_formulario" border="0" cellpadding="0">
            <tr id="tr_nombre" runat="server">
                <td class="label" valign="top">
                    Periodo
                </td>
                <td class="input">
                    <input id="txtPeriodo" type="text" class="css_input" runat="server" />
                </td>
            </tr>
            <tr id="tr_adjuntar" runat="server">
                <td class="label" valign="top">
                    Adjuntar Archivo
                </td>
                <td class="input">
                    <asp:FileUpload ID="txtArchivoDocumentos" runat="server" />
                    <asp:Label ID="lblNoArchivo" runat="server" CssClass="inputform" width="250px" Text="(No se adjuntó archivo)"  Visible="false"></asp:Label>
                    <asp:HyperLink ID="hlkArchivo" style ="cursor:hand;" target="_blank" CssClass="css_link"
                    Width="200px" runat="server" Text="(Ver Archivo Adjunto)" Visible="false" ></asp:HyperLink>     
                </td>
            </tr>
            
        </table>
    </div>
    </form>
</body>
</html>
