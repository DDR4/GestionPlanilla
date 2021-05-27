var Vacaciones = (function ($, win, doc) {

    var $tblListadoVacaciones = $('#tblListadoVacaciones');
    var $btnNuevaCargo = $('#btnNuevaCargo');

    var $cboTipoBusqueda = $('#cboTipoBusqueda');
    var $tipoNombres = $('#tipoNombres ');
    var $txtNombres = $('#txtNombres');
    var $btnBuscar = $('#btnBuscar');

    var $btnActualizacionMasiva = $('#btnActualizacionMasiva');

    //Modal
    //var $modalCargo = $('#modalCargo');  
    //var $titleModalCargo = $('#titleModalCargo');
    //var $txtModalDescripcion = $('#txtModalDescripcion');  
    //var $txtModalSueldo = $('#txtModalSueldo'); 
    //var $cboModalEstado = $('#cboModalEstado'); 
    //var $btnGuardar = $('#btnGuardar');
                                        
    var Message = {
        ObtenerTipoBusqueda: "Obteniendo los tipos de busqueda, Por favor espere...",
        GuardarSuccess: "Los datos se guardaron satisfactoriamente"
    };

    var Global = {
        Cargo_Id: null
    };

    // Constructor
    $(Initialize);                        

    // Implementacion del constructor
    function Initialize() {          
        GetVacaciones();         
        $cboTipoBusqueda.change($cboTipoBusqueda_change);
        $btnBuscar.click($btnBuscar_click);         
        $btnActualizacionMasiva.click($btnActualizacionMasiva_click);
    }           

    function $btnActualizacionMasiva_click() {

        var method = "POST";
        var url = "Vacaciones/GenerarVacacionesMasivo";

        var fnDoneCallback = function (data) {
            app.Message.Success("Grabar", Message.GuardarSuccess, "Aceptar", null);
            GetVacaciones();
        };
        app.CallAjax(method, url, null, fnDoneCallback);

    }

    function $btnNuevaCargo_click() {
        $titleModalCargo.html("Nueva Cargo");
        $modalCargo.modal();
        Global.Cargo_Id = null;
        $txtModalDescripcion.val("");
        app.Event.Disabled($cboModalEstado);            
    }

    function GetVacaciones() {             

        var parms = {
            NombreApellido: $txtNombres.val()
        };

        var url = "Vacaciones/GetVacaciones";

        var columns = [
            { data: "NombreApellido" },
            { data: "DiasDisponibles" },
            { data: "DiasTotales" }
            //{ data: "Auditoria.TipoUsuario" }
        ];
        var columnDefs = [ 

            //{
            //    "targets": [2],
            //    'render': function (data, type, full, meta) {
            //        if (data === 1) {
            //            return "Activo";
            //        } else return "Inactivo";

            //    }
            //},

        ];

        var filters = {
            pageLength: app.Defaults.TablasPageLength
        };
        app.FillDataTableAjaxPaging($tblListadoVacaciones, url, parms, columns, columnDefs, filters, null, null);

    }     

    function $cboTipoBusqueda_change() {
        var codSelec = $(this).val();
        $tipoNombres.hide();

        $txtNombres.val("");         

        if (codSelec === "1") {
            $tipoNombres.show();
        }                

    }

    function $btnGuardar_click() {
        InsertUpdateCargo();
    }

    function InsertUpdateCargo() {

        var obj = {
            "Cargo_Id": Global.Cargo_Id,    
            "Descripcion": $txtModalDescripcion.val(),   
            "Sueldo": $txtModalSueldo.val(),
            "Estado": $cboModalEstado.val()
        };

        var method = "POST";
        var data = obj;
        var url = "Cargo/InsertUpdateCargo";

        var fnDoneCallback = function (data) {
            app.Message.Success("Grabar", Message.GuardarSuccess, "Aceptar", null);
            $modalCargo.modal('hide');
            GetCargos();
        };
        app.CallAjax(method, url, data, fnDoneCallback);
    }


    function $btnBuscar_click() {
        GetVacaciones();
    }             

    return {
        //EditarCargo: EditarCargo,
        //EliminarCargo: EliminarCargo
    };


})(window.jQuery, window, document);