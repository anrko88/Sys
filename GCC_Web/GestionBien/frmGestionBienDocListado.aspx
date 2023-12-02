<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGestionBienDocListado.aspx.vb" Inherits="GestionBien_frmGestionBienDocListado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SGL :: Sistema de Gestión de Leasing</title>
    
    <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="../Util/images/PV16x16.ico" />
    
    <!-- Estilos --> 
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery.jscrollpane.css" media="all" />
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
    <script type='text/javascript' src="frmGestionBienDocListado.aspx.js"> </script>

</head>
<body>

    <form id="frmSiniestroListado" runat="server">
    
    <input type="hidden" name="hddCodContrato" id="hddCodContrato" value="" runat="server" />
    <input type="hidden" name="hddCodBien" id="hddCodBien" value="" runat="server" />
    <input type="hidden" name="hddCodRelacionado" id="hddCodRelacionado" value="" runat="server" />
    <input type="hidden" name="hddCodTipo" id="hddCodTipo" value="" runat="server" />
    <input type="hidden" name="hddVer" id="hddVer" value="" runat="server" />
    
    <input type="hidden" name="hddCodDocumento" id="hddCodDocumento" value="" runat="server" />
    
    
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../Util/images/ico_version.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">Gestión del Bien</div>
                    <div class="css_lbl_titulo">Documentos y Comentarios :: Listado</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    
                    <div id="dv_img_boton" class="dv_img_boton">
						<a href="javascript:parent.fn_util_CierraModal2();">
							<img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0"/><br />
							Cerrar
						</a>
					</div>
                    
                    <div id="dv_separador" class="dv_img_boton_separador">
                        :
                    </div>
                    
                    <div id="dv_GBEliminar" class="dv_img_boton">
                        <a href="javascript:fn_GBEliminar()">
                            <img alt="" src="../Util/images/ico_acc_eliminar.gif" border="0" /><br />
                            Eliminar </a>
                    </div>
                    <div id="dv_GBEditar" class="dv_img_boton">
                        <a href="javascript:fn_GBAbreEditar()">
                            <img alt="" src="../Util/images/ico_acc_editar.gif" border="0" /><br />
                            Editar </a>
                    </div>
                    <div id="dv_GBAgregar" class="dv_img_boton" style="width:60px;">
                        <a href="javascript:fn_GBAbreNuevo();">
                            <img alt="" src="../Util/images/ico_acc_agregar.gif" border="0" /><br />
                            Agregar </a>
                    </div>    
                    
                </td>
            </tr>
        </table>
        
        
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="lineas">
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                          
                        <br />
                        
                        <!-- Inicio Grilla Listado -->
                        <div class="dv_tabla_contenedoraSola">
                            <table id="jqGrid_lista_A">
                                <tr>
                                    <td />
                                </tr>
                            </table>
                            <div id="jqGrid_pager_A">
                            </div>
                        </div>
                        <!-- Fin Grilla -->
                                                
                    </td>
                </tr>
            </table>
                                    
        </div>               
        
    </div>
    <!-- Fin Cuerpo -->
     
    </form>
    
</body>
</html>
