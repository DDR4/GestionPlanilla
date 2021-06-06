var BoletaPago = (function ($, win, doc) {

    var $txtFechaBoleta = $('#txtFechaBoleta');
    var $btnGenerarBoleta = $('#btnGenerarBoleta');
    var $btnBuscar = $('#btnBuscar');
    var $tblListadoBoletas = $('#tblListadoBoletas');
                                        
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
        GetBoletaPago();
        $btnBuscar.click($btnBuscar_click);
    }           

    function $btnBuscar_click() {
        GetBoletaPago();
    }    

    function GetBoletaPago() {

        var parms = {
            "HorasTrabajadas": {
                "Periodo": $txtFechaBoleta.val()
            }
        };

        var url = "BoletaPago/GetBoletaPago";

        var columns = [
            { data: "Trabajador.Nombres" },
            { data: "Trabajador.Area.Descripcion" },
            { data: "Trabajador.Turno.Descripcion" },
            { data: "Trabajador.Cargo.Descripcion" },
            { data: "HorasTrabajadas.Periodo" },                       
            { data: "Auditoria.TipoUsuario" }
        ];
        var columnDefs = [
            {
                "targets": [5],
                "visible": true,
                "orderable": false,
                "className": "text-center",
                'render': function (data, type, full, meta) {
                    if (data === "1") {
                        return "<center>" +
                            '<a class="btn btn-default btn-xs" style= "margin-right:0.5em" title="Editar" href="javascript:BoletaPago.DescargarBoleta(' + meta.row + ');"><i class="fa fa-download" aria-hidden="true"></i></a>' +
                            "</center> ";
                    } else {
                        return "";
                    }
                }
            }

        ];

        var filters = {
            pageLength: app.Defaults.TablasPageLength
        };
        app.FillDataTableAjaxPaging($tblListadoBoletas, url, parms, columns, columnDefs, filters, null, null);

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

    function DescargarBoleta(row) {

        var data = app.GetValueRowCellOfDataTable($tblListadoBoletas, row);

        var obj = {
            "Trabajador": { "Trabajador_Id": data.Trabajador.Trabajador_Id },
            "HorasTrabajadas": { "Periodo": data.HorasTrabajadas.Periodo }
        };

        var method = "POST";
        var url = "BoletaPago/DescargarBoletaPago";
        var rsdata = obj;
        var fnDoneCallback = function (data) {
            GetCargos();
        };
        app.CallAjax(method, url, rsdata, fnDoneCallback, null, null, null);

    }



    return {
        DescargarBoleta: DescargarBoleta
    };


})(window.jQuery, window, document);