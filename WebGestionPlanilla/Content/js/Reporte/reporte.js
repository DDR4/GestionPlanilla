var Reporte = (function ($, win, doc) {

    var $txtFechaReporte = $('#txtFechaReporte');
    var $btnNuevaArea = $('#btnNuevaArea');

                                        
    var Message = {
        ObtenerTipoBusqueda: "Obteniendo los tipos de busqueda, Por favor espere...",
        GuardarSuccess: "Los datos se guardaron satisfactoriamente"
    };

    // Constructor
    $(Initialize);                        

    // Implementacion del constructor
    function Initialize() {          
        $txtFechaReporte.datepicker({
            endDate: "today",
            todayHighlight: true,
            format: "mm/yyyy",
            startView: "months",
            minViewMode: "months"
        });      
    }           

    function InsertUpdateArea() {

        var obj = {
            "Area_Id": Global.Area_Id,
            "Descripcion": $txtModalDescripcion.val(),       
            "Estado": $cboModalEstado.val()
        };

        var method = "POST";
        var data = obj;
        var url = "Area/InsertUpdateArea";

        var fnDoneCallback = function (data) {
            app.Message.Success("Grabar", Message.GuardarSuccess, "Aceptar", null);
            $modalArea.modal('hide');
            GetAreas();
        };
        app.CallAjax(method, url, data, fnDoneCallback);
    }



})(window.jQuery, window, document);