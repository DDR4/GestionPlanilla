var HorasTrabajadas = (function ($, win, doc) {

    var $tblListadoNumeroHoras = $('#tblListadoNumeroHoras');

    var $cboTipoBusqueda = $('#cboTipoBusqueda');
    var $tipoFecha = $('#tipoFecha');
    var $txtFecha = $('#txtFecha');
    var $tipoNombres = $('#tipoNombres');
    var $txtNombres = $('#txtNombres');
    var $btnBuscar = $('#btnBuscar');
    var $btnNuevoDescansoMedico = $('#btnNuevoDescansoMedico');
    var $modalDescansoMedico = $('#modalDescansoMedico');
    var $txtModalFechaDescansoInicio = $('#txtModalFechaDescansoInicio');
    var $txtModalFechaDescansoFin = $('#txtModalFechaDescansoFin');
    var $txtModalNombre = $('#txtModalNombre');
    var $btnBuscarModal = $('#btnBuscarModal');
    var $tblTrabajadores = $('#tblTrabajadores');

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
        $txtModalFechaDescansoInicio.datepicker({
            endDate: "today",
            todayHighlight: true
        }); 
        $txtModalFechaDescansoFin.datepicker({
            endDate: "today",
            todayHighlight: true
        }); 
        $btnNuevoDescansoMedico.click($btnNuevoDescansoMedico_click);
        $btnBuscarModal.click($btnBuscarModal_click);
        //var row = $tblTrabajadores.DataTable();

        //console.log(row);

        var table = $tblTrabajadores.DataTable();

        $('#tblTrabajadores tbody').on('click', 'tr', function () {
            console.log(table.row(this).data());
        });

        //table.on('select', function (e, dt, type, indexes) {
        //    var row = table.rows({ selected: true }).data();
        //    console.log(row);
        //});
    }           

    function GetHorasTrabajadas() {             

        var parms = {
            Trabajador_Id: null,
            HorasTrabajadas: { Periodo: $txtFecha.val() },
            Nombres: $txtNombres.val()
        };

        var url = "HorasTrabajadas/GetHorasTrabajadas";

        var columns = [
            { data: "Nombres" },
            { data: "HorasTrabajadas.Horas_Trabajadas" },
            { data: "HorasTrabajadas.Horas_Tardanzas" },
            { data: "HorasTrabajadas.Periodo" },
            { data: "HorasTrabajadas.Tipo" }
        ];
        var columnDefs = [                   
            {
                "targets": [4],
                'render': function (data, type, full, meta) {
                    if (data === 1) {
                        return "Horas Trabajadas";
                    } else return "Descanso Medico";

                }
            },

        ];

        var filters = {
            pageLength: app.Defaults.TablasPageLength
        };
        app.FillDataTableAjaxPaging($tblListadoNumeroHoras, url, parms, columns, columnDefs, filters, null, null);

    }     

    function $cboTipoBusqueda_change() {
        var codSelec = $(this).val();
        $tipoNombres.hide();
        $tipoFecha.hide();
 
        $txtNombres.val("");
        $txtFecha.val("");           

        if (codSelec === "1") {
            $tipoNombres.show();
        }
        else if (codSelec === "2") {
            $tipoFecha.show();
        }

    }

    function $btnBuscar_click() {
        GetHorasTrabajadas();
    }   

    function $btnNuevoDescansoMedico_click() {
        $modalDescansoMedico.modal();
        GetTrabajadores();
      
    }

    function $btnBuscarModal_click() {
        GetTrabajadores();
    }

    function GetTrabajadores() {

        var parms = {
            Trabajador_Id: null,
            Nombres: $txtModalNombre.val(),
            Estado: 1,
            TipoBusqueda: 1
        };

        var url = "HorasTrabajadas/GetTrabajador";

        var columns = [
            { data: "Nombres" }
        ];

        var filters = {
            pageLength: app.Defaults.TablasPageLength
        };
        app.FillDataTableAjaxPaging($tblTrabajadores, url, parms, columns, null, filters, null, null);

    }    

    return {

    };


})(window.jQuery, window, document);