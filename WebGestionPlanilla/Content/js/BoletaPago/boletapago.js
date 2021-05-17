var BoletaPago = (function ($, win, doc) {

    var $txtFechaBoleta = $('#txtFechaBoleta');
    var $btnGenerarBoleta = $('#btnGenerarBoleta');
                                        
    var Message = {
        ObtenerTipoBusqueda: "Obteniendo los tipos de busqueda, Por favor espere...",
        GuardarSuccess: "Los datos se guardaron satisfactoriamente"
    };

    // Constructor
    $(Initialize);                        

    // Implementacion del constructor
    function Initialize() {          
        $txtFechaBoleta.datepicker({
            endDate: "today",
            todayHighlight: true,
            format: "mm/yyyy",
            startView: "months",
            minViewMode: "months"                
        });      
        $btnGenerarBoleta.click($btnGenerarBoleta_click);
    }           

    function $btnGenerarBoleta_click() {

        var obj = {
            "HorasTrabajadas": {
                "Periodo": $txtFechaBoleta.val()
            }
        };
        var method = "POST";
        var data = obj;
        var url = "BoletaPago/GetTrabajadores";
        var fnDoneCallback = function (data) {
            app.Message.Success("Grabar", Message.GuardarSuccess, "Aceptar", null);
        };
        app.CallAjax(method, url, data, fnDoneCallback, null, null, null);
    }



    return {
      
    };


})(window.jQuery, window, document);