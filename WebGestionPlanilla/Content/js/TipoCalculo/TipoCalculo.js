var TipoCalculo = (function ($, win, doc) {

    var $tblListadoTipoCalculo = $('#tblListadoTipoCalculo');
    var $btnNuevaTipoCalculos = $('#btnNuevaTipoCalculos');

    var $cboTipoBusqueda = $('#cboTipoBusqueda');
    var $tipoDescripcion = $('#tipoDescripcion');
    var $txtDescripcion = $('#txtDescripcion');
    var $tipoEstado = $('#tipoEstado');
    var $cboEstado = $('#cboEstado');
    var $btnBuscar = $('#btnBuscar');

    //Modal
    var $modalTipoCalculo = $('#modalTipoCalculo');
    var $titleModalTipoCalculo = $('#titleModalTipoCalculo');
    var $txtModalDescripcion = $('#txtModalDescripcion');
    var $cboModalEstado = $('#cboModalEstado');
    var $btnGuardar = $('#btnGuardar');

    var Message = {
        ObtenerTipoBusqueda: "Obteniendo los tipos de busqueda, Por favor espere...",
        GuardarSuccess: "Los datos se guardaron satisfactoriamente"
    };

    var Global = {
        Tipo_Calculo_Boleta_Id: null
    };

    // Constructor
    $(Initialize);

    // Implementacion del constructor
    function Initialize() {
        GetTipoCalculos();
        $cboTipoBusqueda.change($cboTipoBusqueda_change);
        $btnBuscar.click($btnBuscar_click);
        $btnGuardar.click($btnGuardar_click);
        $btnNuevaTipoCalculos.click($btnNuevaTipoCalculos_click);
    }

    function $btnNuevaTipoCalculos_click() {
        $titleModalTipoCalculo.html("Nuevo tipo de calculo");
        $modalTipoCalculo.modal();
        Global.Tipo_Calculo_Boleta_Id = null;
        $txtModalDescripcion.val("");
        $cboModalEstado.val(1);
        app.Event.Disabled($cboModalEstado);

    }

    function GetTipoCalculos() {

        var parms = {
            Descripcion: $txtDescripcion.val(),
            Estado: $cboEstado.val()
        };

        var url = "TipoCalculos/GetTipoCalculos";

        var columns = [
            { data: "Descripcion" },
            { data: "Estado" },
            { data: "Auditoria.TipoUsuario" }
        ];
        var columnDefs = 

        [
            {
                "targets": [1],
                'render': function (data, type, full, meta) {
                    if (data === 1) {
                        return "Activo";
                    } else return "Inactivo";

                }
            },
            {
                "targets": [2],
                "visible": true,
                "orderable": false,
                "className": "text-center",
                'render': function (data, type, full, meta) {
                    if (data === "1") {
                        return "<center>" +
                            '<a class="btn btn-default btn-xs" style= "margin-right:0.5em" title="Editar" href="javascript:TipoCalculo.EditarTipoCalculo(' + meta.row + ');"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></a>' +
                            '<a class="btn btn-default btn-xs" style= "margin-right:0.5em" title="Eliminar" href="javascript:TipoCalculo.EliminarTipoCalculo(' + meta.row + ')"><i class="fa fa-trash" aria-hidden="true"></i></a>' +
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
        app.FillDataTableAjaxPaging($tblListadoTipoCalculo, url, parms, columns, columnDefs, filters, null, null);

    }

    function $cboTipoBusqueda_change() {
        var codSelec = $(this).val();
        $tipoDescripcion.hide();
        $tipoEstado.hide();

        $txtDescripcion.val("");
        $cboEstado.val(0);


        if (codSelec === "1") {
            $tipoDescripcion.show();
        }
        else if (codSelec === "2") {
            $tipoEstado.show();
        }

    }

    function $btnGuardar_click() {
        InsertUpdateTipoCalculos();
    }

    function InsertUpdateTipoCalculos() {

        var obj = {
            "Tipo_Calculo_Boleta_Id": Global.Tipo_Calculo_Boleta_Id,
            "Descripcion": $txtModalDescripcion.val(),
            "Estado": $cboModalEstado.val()
        };

        var method = "POST";
        var data = obj;
        var url = "TipoCalculos/InsertUpdateTipoCalculos";

        var fnDoneCallback = function (data) {
            app.Message.Success("Grabar", Message.GuardarSuccess, "Aceptar", null);
            $modalTipoCalculo.modal('hide');
            GetTipoCalculos();
        };
        app.CallAjax(method, url, data, fnDoneCallback);
    }


    function $btnBuscar_click() {
        GetTipoCalculos();
    }

    function EditarTipoCalculo(row) {
        var data = app.GetValueRowCellOfDataTable($tblListadoTipoCalculo, row);
        $titleModalTipoCalculo.html("Editar Tipo Calculo");

        $modalTipoCalculo.modal();
        Global.Tipo_Calculo_Boleta_Id = data.Tipo_Calculo_Boleta_Id;
        $txtModalDescripcion.val(data.Descripcion);
        $cboModalEstado.val(data.Estado).trigger('change');
        app.Event.Enable($cboModalEstado);
    }

    function EliminarTipoCalculo(row) {
        var fnAceptarCallback = function () {
            var data = app.GetValueRowCellOfDataTable($tblListadoTipoCalculo, row);

            var obj = {
                "Tipo_Calculo_Boleta_Id": data.Tipo_Calculo_Boleta_Id
            };

            var method = "POST";
            var url = "TipoCalculos/DeleteTipoCalculos";
            var rsdata = obj;
            var fnDoneCallback = function (data) {
                GetTipoCalculos();
            };
            app.CallAjax(method, url, rsdata, fnDoneCallback, null, null, null);
        };
        app.Message.Confirm("Aviso", "Esta seguro que desea eliminar el TipoCalculo?", "Aceptar", "Cancelar", fnAceptarCallback, null);
    }

    return {
        EditarTipoCalculo: EditarTipoCalculo,
        EliminarTipoCalculo: EliminarTipoCalculo
    };


})(window.jQuery, window, document);