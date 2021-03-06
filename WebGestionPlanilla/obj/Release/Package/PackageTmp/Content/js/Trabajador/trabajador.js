var Trabajador = (function ($, win, doc) {

    var $btnNuevoTrabajador = $('#btnNuevoTrabajador');
    var $btnGuardar = $('#btnGuardar');

    var $tblListadoTrabajadores = $('#tblListadoTrabajadores');

    // Modal
    var $modalTrabajador = $('#modalTrabajador');  
    var $txtModalUsuario = $('#txtModalUsuario');  
    var $txtModalClave = $('#txtModalClave');  
    var $cboModalTipo = $('#cboModalTipo');  
    var $txtModalNombres = $('#txtModalNombres');  
    var $txtModalApellidoP = $('#txtModalApellidoP');  
    var $txtModalApellidoM = $('#txtModalApellidoM');    
    var $cboModalArea = $('#cboModalArea');  
    var $cboModalTurno = $('#cboModalTurno');  
    var $cboModalCargo = $('#cboModalCargo');
    var $txtModalFechaN = $('#txtModalFechaN');       
    var $cboModalSexo = $('#cboModalSexo');              
    var $cboModalEstado = $('#cboModalEstado');          
    var $titleModalTrabajador = $('#titleModalTrabajador');

    var $cboTipoBusqueda = $('#cboTipoBusqueda');
    var $tipoNombre = $('#tipoNombre');
    var $tipoEstado = $('#tipoEstado');
    var $txtNombre = $('#txtNombre');
    var $cboEstado = $('#cboEstado');
    var $btnBuscar = $('#btnBuscar');
    
                                                                    
                                                   
    var Message = {
        ObtenerTipoBusqueda: "Obteniendo los tipos de busqueda, Por favor espere...",
        GuardarSuccess: "Los datos se guardaron satisfactoriamente"
    };

    var Global = {
        Area_Id: null
    };

    // Constructor
    $(Initialize);

    // Implementacion del constructor
    function Initialize() {

        $btnNuevoTrabajador.click($btnNuevoTrabajador_click);
        $btnGuardar.click($btnGuardar_click);
        GetTrabajador();
        GetArea();
        GetTurno();
        GetCargo();
        $txtModalFechaN.datepicker({
            endDate: "today",
            todayHighlight: true
        });                 
        $cboTipoBusqueda.change($cboTipoBusqueda_change);
        $btnBuscar.click($btnBuscar_click);
    }

    function $btnNuevoTrabajador_click() {
        $titleModalTrabajador.html("Nuevo Trabajador");
        $modalTrabajador.modal();
        Global.Trabajador_Id = null;
        $txtModalUsuario.val("");
        $txtModalClave.val("");
        $txtModalNombres.val("");
        $txtModalApellidoP.val("");
        $txtModalApellidoM.val("");
        $cboModalArea.val(0);
        $cboModalTurno.val(0);
        $cboModalCargo.val(0);
        $txtModalFechaN.val("");
        $cboModalSexo.val(0);
        $cboModalTipo.val(0);
        $cboModalEstado.val(1);
        app.Event.Disabled($cboModalEstado);

    }

    function $btnGuardar_click() {
        InsertUpdateTrabajador();
    }

    function InsertUpdateTrabajador() {

        var obj = {
            "Trabajador_Id": Global.Trabajador_Id,
            "Usuario": $txtModalUsuario.val(),
            "Contraseña": $txtModalClave.val(),
            "Nombres": $txtModalNombres.val(),
            "ApellidoPaterno": $txtModalApellidoP.val(),
            "ApellidoMaterno": $txtModalApellidoM.val(),
            "FechaNacimiento": $txtModalFechaN.val(),
            "Sexo": $cboModalSexo.val(),
            "Estado": $cboModalEstado.val(),
            "Tipo": $cboModalTipo.val(),
            "Area": { "Area_Id": $cboModalArea.val() },
            "Turno": { "Turno_Id": $cboModalTurno.val() },
            "Cargo": { "Cargo_Id": $cboModalCargo.val() }           

        };

        var method = "POST";
        var data = obj;
        var url = "Trabajador/InsertUpdateTrabajador";

        var fnDoneCallback = function (data) {
            app.Message.Success("Grabar", Message.GuardarSuccess, "Aceptar", null);
            $modalTrabajador.modal('hide');
            GetTrabajador();
        };
        app.CallAjax(method, url, data, fnDoneCallback);
    }

    function GetTrabajador() {
        var parms = {
            Estado: $cboEstado.val(),
            Nombres: $txtNombre.val(),
            Trabajador_Id: null
        };

        var url = "Trabajador/GetTrabajador";

        var columns = [
            { data: "Nombres" },
            { data: "ApellidoPaterno" },       
            { data: "ApellidoMaterno" },    
            { data: "Area.Descripcion" },
            { data: "Turno.Descripcion" },
            { data: "Cargo.Descripcion" },
            { data: "Tipo" },
            { data: "Estado" }, 
            { data: "Auditoria.TipoUsuario" }
        ];
        var columnDefs = [
            {
                "targets": [6],
                'render': function (data, type, full, meta) {
                    if (data === 1) {
                        return "Administrador";
                    } else return "Trabajador";

                }
            },
            {
                "targets": [7],
                'render': function (data, type, full, meta) {
                    if (data === 1) {
                        return "Activo";
                    } else return "Inactivo";

                }
            },
            {
                "targets": [8],
                "visible": true,
                "orderable": false,
                "className": "text-center",
                'render': function (data, type, full, meta) {
                    if (data === "1") {
                        return "<center>" +
                            '<a class="btn btn-default btn-xs" style= "margin-right:0.5em" title="Editar" href="javascript:Trabajador.EditarTrabajador(' + meta.row + ');"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></a>' +
                            '<a class="btn btn-default btn-xs" style= "margin-right:0.5em" title="Eliminar" href="javascript:Trabajador.EliminarTrabajador(' + meta.row + ')"><i class="fa fa-trash" aria-hidden="true"></i></a>' +
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
        app.FillDataTableAjaxPaging($tblListadoTrabajadores, url, parms, columns, columnDefs, filters, null, null);

    }

    function EditarTrabajador(row) {
        var data = app.GetValueRowCellOfDataTable($tblListadoTrabajadores, row);
        $titleModalTrabajador.html("Editar Trabajador");

        $modalTrabajador.modal();
        Global.Trabajador_Id = data.Trabajador_Id;
        $txtModalUsuario.val(data.Usuario);
        $txtModalClave.val(data.Contraseña);
        $cboModalTipo.val(data.Auditoria.TipoUsuario).trigger('change');
        $txtModalNombres.val(data.Nombres);
        $txtModalApellidoP.val(data.ApellidoPaterno);
        $txtModalApellidoM.val(data.ApellidoMaterno);
        $cboModalArea.val(data.Area.Area_Id);
        $cboModalTurno.val(data.Turno.Turno_Id);
        $cboModalCargo.val(data.Cargo.Cargo_Id);
        console.log(data.FechaNacimiento);
        var fecha = new Date(parseInt(data.FechaNacimiento.replace("/Date(", "").replace(")/", ""), 10));
        var dia = ('0' + fecha.getDate()).slice(-2);
        var mes = ('0' + (fecha.getMonth() + 1)).slice(-2);
        var año = fecha.getFullYear();
        $txtModalFechaN.val(dia + "/" + mes + "/" + año);
        $cboModalSexo.val(data.Sexo).trigger('change');
        $cboModalEstado.val(data.Estado).trigger('change');
        app.Event.Enable($cboModalEstado);
    }

    function EliminarTrabajador(row) {
        var fnAceptarCallback = function () {
            var data = app.GetValueRowCellOfDataTable($tblListadoTrabajadores, row);

            var obj = {
                "Trabajador_Id": data.Trabajador_Id
            };

            var method = "POST";
            var url = "Trabajador/DeleteTrabajador";
            var rsdata = obj;
            var fnDoneCallback = function (data) {
                GetTrabajador();
            };
            app.CallAjax(method, url, rsdata, fnDoneCallback, null, null, null);
        };
        app.Message.Confirm("Aviso", "Esta seguro que desea eliminar el trabajador?", "Aceptar", "Cancelar", fnAceptarCallback, null);
    }

    function GetArea() {              
        var method = "POST";
        var url = "Combos/GetArea";
        var fnDoneCallback = function (data) {  
            for (var i = 0; i < data.Data.length; i++) {
                $cboModalArea.append('<option value=' + data.Data[i].Area_Id + '>' + data.Data[i].Descripcion + '</option>');
            }
          
        };
        app.CallAjax(method, url, null, fnDoneCallback, null, null, null);          
    }

    function GetTurno() {
        var method = "POST";
        var url = "Combos/GetTurno";
        var fnDoneCallback = function (data) {
            for (var i = 0; i < data.Data.length; i++) {
                $cboModalTurno.append('<option value=' + data.Data[i].Turno_Id + '>' + data.Data[i].Descripcion + '</option>');
            }
        };
        app.CallAjax(method, url, null, fnDoneCallback, null, null, null);
    }

    function GetCargo() {
        var method = "POST";
        var url = "Combos/GetCargo";
        var fnDoneCallback = function (data) {
            for (var i = 0; i < data.Data.length; i++) {
                $cboModalCargo.append('<option value=' + data.Data[i].Cargo_Id + '>' + data.Data[i].Descripcion + '</option>');
            }
        };
        app.CallAjax(method, url, null, fnDoneCallback, null, null, null);
    }

    function $cboTipoBusqueda_change() {
        var codSelec = $(this).val();
        $tipoNombre.hide();
        $tipoEstado.hide();

        $txtNombre.val("");
        $cboEstado.val(0);

        if (codSelec === "1") {
            $tipoNombre.show();
        }
        else if (codSelec === "2") {
            $tipoEstado.show();
        }

    }

    function $btnBuscar_click() {
        GetTrabajador();
    }                                        

    return {
        EditarTrabajador: EditarTrabajador,
        EliminarTrabajador: EliminarTrabajador
    };


})(window.jQuery, window, document);