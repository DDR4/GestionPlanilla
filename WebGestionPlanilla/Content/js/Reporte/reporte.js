var Reporte = (function ($, win, doc) {

    var $txtFechaReporte = $('#txtFechaReporte');
    var $btnGenerarReporteTurno = $('#btnGenerarReporteTurno');
    var $btnGenerarReporteArea = $('#btnGenerarReporteArea');    
                                        
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
        $btnGenerarReporteTurno.click($btnGenerarReporteTurno_click);
        $btnGenerarReporteArea.click($btnGenerarReporteArea_click);
    }           

    function $btnGenerarReporteTurno_click() {
        if (ValidaGenerarReporte()) {
            GenerarReporteTurno();
        }            
    }

    function GenerarReporteTurno() {
        app.RedirectTo("Reporte/GenerarReporteTurno?Periodo=" + $txtFechaReporte.val());
    }

    function $btnGenerarReporteArea_click() {
        if (ValidaGenerarReporte()) {
            GenerarReporteArea();
        }
    }

    function GenerarReporteArea() {
        app.RedirectTo("Reporte/GenerarReporteArea?Periodo=" + $txtFechaReporte.val());
    }    

    function ValidaGenerarReporte() {
        var val = false;

        if ($txtFechaReporte.val() !== "") {
            val = true;
        } else {
            app.Message.Info("Aviso", "Seleccionar la fecha del Reporte", "Aceptar", null);
        }

        return val;
    }


})(window.jQuery, window, document);