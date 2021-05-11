var HorasTrabajadas = (function ($, win, doc) {

    var $tblListadoNumeroHoras = $('#tblListadoNumeroHoras');

    var $cboTipoBusqueda = $('#cboTipoBusqueda');
    var $tipoFecha = $('#tipoFecha');
    var $txtFecha = $('#txtFecha');
    var $btnBuscar = $('#btnBuscar');
                                        
    var Message = {
        ObtenerTipoBusqueda: "Obteniendo los tipos de busqueda, Por favor espere...",
        GuardarSuccess: "Los datos se guardaron satisfactoriamente"
    };

    // Constructor
    $(Initialize);                        

    // Implementacion del constructor
    function Initialize() {          
        GetHorasTrabajadas();         
        $cboTipoBusqueda.change($cboTipoBusqueda_change);
        $btnBuscar.click($btnBuscar_click);
        $txtFecha.datepicker({
            endDate: "today",
            todayHighlight: true
        });                 
    }           

    function GetHorasTrabajadas() {             

        var parms = {
            Trabajador_Id: null,
            HorasTrabajadas: { Periodo: $txtFecha.val() }
        };

        var url = "HorasTrabajadas/GetHorasTrabajadas";

        var columns = [
            { data: "Horas_Trabajadas" },
            { data: "Horas_Tardanzas" },
            { data: "Faltas" },
            { data: "Periodo" },
            //{ data: "Auditoria.TipoUsuario" }
        ];
        var columnDefs = [                   
            //{
            //    "targets": [4],
            //    "visible": true,
            //    "orderable": false,
            //    "className": "text-center",
            //    'render': function (data, type, full, meta) {
            //        if (data === "1") {
            //            return "<center>" +
            //                '<a class="btn btn-default btn-xs" style= "margin-right:0.5em" title="Editar" href="javascript:Trabajador.EditarTrabajador(' + meta.row + ');"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></a>' +
            //                '<a class="btn btn-default btn-xs" style= "margin-right:0.5em" title="Eliminar" href="javascript:Trabajador.EliminarTrabajador(' + meta.row + ')"><i class="fa fa-trash" aria-hidden="true"></i></a>' +
            //                "</center> ";
            //        } else {
            //            return "";
            //        }
            //    }
            //}

        ];

        var filters = {
            pageLength: app.Defaults.TablasPageLength
        };
        app.FillDataTableAjaxPaging($tblListadoNumeroHoras, url, parms, columns, columnDefs, filters, null, null);

    }     

    function $cboTipoBusqueda_change() {
        var codSelec = $(this).val();
        $tipoFecha.hide();
        //$tipoEstado.hide();

        $txtFecha.val("");
        //$cboEstado.val(0);

        if (codSelec === "1") {
            $tipoFecha.show();
        }
        else if (codSelec === "2") {
            //$tipoEstado.show();
        }

    }

    function $btnBuscar_click() {
        GetHorasTrabajadas();
    }   

    return {

    };


})(window.jQuery, window, document);