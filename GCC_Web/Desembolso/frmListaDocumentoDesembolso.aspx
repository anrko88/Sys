<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaDocumentoDesembolso.aspx.vb" Inherits="Desembolso_frmListaDocumentoDesembolso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>GCC :: Sistema de Gestión de Leasing</title>
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

    <script type="text/javascript" src="../Util/js/jquery/json2.js"></script>

    <script type="text/javascript" src="../Util/js/js_global.js"></script>

    <script type='text/javascript' src="../Util/js/js_util.modal.js"> </script>

    <script type='text/javascript' src="../Util/js/js_util.funcion.js"> </script>

    <script type='text/javascript' src="../Util/js/js_util.date.js"> </script>

    <script type='text/javascript' src="../Util/js/js_util.ajax.js"> </script>

    <script src="../Util/js/jquery/jquery.dateFormat-1.0.js" type="text/javascript"></script>

    <script src="../Util/js/jquery/jshashtable.js" type="text/javascript"></script>

    <!--
    <script src="../Util/js/jquery/jquery.numberformatter-1.2.3.js" type="text/javascript"></script>
     -->

    <script src="../Util/js/js_util.Grilla.js" type="text/javascript"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../Util/componentes/jqgrid/js/i18n/grid.locale-en.js" type="text/javascript"></script>

    <script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmListaDocumentoDesembolso.aspx.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dv_cuerpo">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="icono">
                <img alt="" src="../Util/images/ico_mdl_contrato.gif" class="jd_menu_icono" />
            </td>
            <td class="titulos">
                <div class="css_lbl_subTitulo">
                    Listado Valor Generico</div>
                <div class="css_lbl_titulo">
                    <asp:Label ID="lblTitulo" runat="server"></asp:Label> :: Búsqueda</div>
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
            </td>
        </tr>
        </table>
        <br />
        <div class=" css_scrollPane" style="height:400px">
        <div class="dv_tabla_contenedora">
            <table id="jqGrid_lista_A">
                <tr>
                    <td />
                </tr>
            </table>
            <asp:HiddenField ID="hidCodigoContrato" runat="server" />
            <asp:HiddenField ID="hidTipoDocumento" runat="server" />
            <asp:HiddenField ID="hidNroDocumento" runat="server" />
            <asp:HiddenField ID="hidFechaEmision" runat="server" />
            <asp:HiddenField ID="hidCodProveedor" runat="server" />     
            <asp:HiddenField ID="hidCodunico" runat="server" />     
                   
            <%--<div id="jqGrid_pager_A">
            </div>--%>
        </div>
        </div>
    </div>
    </form>
</body>
</html>
