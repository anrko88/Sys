//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	VMA - 29/01/2013
//****************************************************************
$(document).ready(function() {
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));

    $('#cmbClasificacionBien').change(function() {
        var strValor = $(this).val();
        fn_CargarTipoBien(strValor);
    });

    $('#cmbTipoBien').change(function() {
        var strValor = $(this).val();
        var strText = $("#cmbTipoBien option:selected").text()

        $("#hdnTipoBeneficio").val(strValor);
        $("#hdnTipoBeneficioDesc").val(strText);
    });

    $("#hdnTipoBeneficio").val("0");
    $("#hdnTipoBeneficioDesc").val("[-Seleccione-]");
});

function fn_CargarTipoBien(strValor)
{
    var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL104", "pstrCodigoGenerico", strValor];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbTipoBien').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
}

function fn_VerReporte() {
    var strFechaInicial = $("#txtFecha").val();

    if (fn_util_trim(strFechaInicial) == "") {
        parent.fn_mdl_mensajeIco("Debe ingresar la fecha de contratos activados", "util/images/error.gif", "ADVERTENCIA");
    }
    else {
        $("#btnGenerar").click();
    }
}

//****************************************************************
// Funcion		:: 	fn_Limpiar
// Descripción	::	
// Log			:: 	VMA - 29/01/2013
//****************************************************************
function fn_Limpiar() {
    $("#txtFecha").val("");
    $('input[id*=rdMensual]').attr('checked', true);
    $("#cmbClasificacionBien").val("0");
    fn_CargarTipoBien("0")

    $("#hdnTipoBeneficio").val("0");
    $("#hdnTipoBeneficioDesc").val("[-Seleccione-]");
}