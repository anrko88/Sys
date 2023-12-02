//****************************************************************
// Funcion		:: 	fn_cargarVentana
// Descripción	::	Carga Popup
// Log			:: 	JRC - 24/04/2012
//****************************************************************
var popup = null;
function fn_cargarVentana(pUsu, pRol, pUrlAnt, pAmbiente) {
    if (popup != null) {
        popup.close();
    }
    if (navigator.appVersion.substring(21, 23) < 7) {
        ventana = window.parent.self;
        ventana.opener = window.parent.self;
        ventana.close();
    }
    else {
        window.open('', '_parent', '');
        window.close();
    }
    var x = 0, y = 10;
    var w = screen.availWidth - 30;
    var h = screen.availHeight - 60;
    strRuta = 'Default.aspx?retorno=1&urlant=' + pUrlAnt + '&usu=' + pUsu + '&rol=' + pRol;
    popup = window.open(strRuta, 'AbrirGCC' + pAmbiente, "top=" + x + ",left=" + y + ",width=" + w + ",height=" + h + ",toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbars=yes,copyhistory=no,resizable=yes,value=ventana");
}

function fn_CargarVentanaSDA(pUsu, pPin, pBD, pName, pPerfil, pUrlAnt, pAmbiente) {
    if (popup != null) {
        popup.close();
    }
    if (navigator.appVersion.substring(21, 23) < 7) {
        ventana = window.parent.self;
        ventana.opener = window.parent.self;
        ventana.close();
    }
    else {
        window.open('', '_parent', '');
        window.close();
    }
    var x = 0, y = 10;
    var w = screen.availWidth - 30;
    var h = screen.availHeight - 60;
    strRuta = 'Default.aspx?retorno=1&urlant=' + pUrlAnt + '&arg01=' + pUsu + '&arg02=' + pPin + '&arg03=' + pBD + '&arg04=' + pName + '&arg05=' + pPerfil;
    popup = window.open(strRuta, 'AbrirGCC' + pAmbiente, "top=" + x + ",left=" + y + ",width=" + w + ",height=" + h + ",toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbars=yes,copyhistory=no,resizable=yes,value=ventana");
}

function MostrarMensaje(mensaje) {
    alert(mensaje);
}

function pCerrarMaster(pUrl) {
    window.close();
    window.open(pUrl);
    return false;
}