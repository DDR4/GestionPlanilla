

var Calculos = (function ($, win, doc) {

    var $tblListadoCalculos = $('#tblListadoCalculos');
    var $btnNuevaCalculos = $('#btnNuevaCalculos');

    var $cboTipoBusqueda = $('#cboTipoBusqueda');
    var $tipoDescripcion = $('#tipoDescripcion');
    var $txtDescripcion = $('#txtDescripcion');
    var $tipoEstado = $('#tipoEstado');
    var $cboEstado = $('#cboEstado');
    var $btnBuscar = $('#btnBuscar');

    //Modal
    var $modalCalculos = $('#modalCalculos');  
    var $titleModalCalculos = $('#titleModalCalculos');
    var $txtModalDescripcion = $('#txtModalDescripcion');  
    var $txtModalSueldo = $('#txtModalSueldo'); 
    var $cboModalEstado = $('#cboModalEstado'); 
    var $btnGuardar = $('#btnGuardar');
                                        
    var Message = {
        ObtenerTipoBusqueda: "Obteniendo los tipos de busqueda, Por favor espere...",
        GuardarSuccess: "Los datos se guardaron satisfactoriamente"
    };

    var Global = {
        Calculos_Id: null
    };

    // Constructor
    $(Initialize);                        

    // Implementacion del constructor
    function Initialize() {          
        GetCalculo();         
        $cboTipoBusqueda.change($cboTipoBusqueda_change);
        $btnBuscar.click($btnBuscar_click);         
        $btnGuardar.click($btnGuardar_click);
        $btnNuevaCalculos.click($btnNuevaCalculos_click);
    }           

    function $btnNuevaCalculos_click() {
        $titleModalCalculos.html("Nuevo Calculo");
        $modalCalculos.modal();
        Global.Calculos_Id = null;
        $txtModalDescripcion.val("");
        $txtModalSueldo.val("");
        $cboModalEstado.val(1);
        app.Event.Disabled($cboModalEstado);            
    }

    function GetCalculo() {             

        var parms = {
            Descripcion: $txtDescripcion.val(),
            Estado: $cboEstado.val()
        };

        var url = "Calculos/GetCalculos";

        var columns = [
            { data: "Descripcion" },
            { data: "Monto"},
            { data: "Estado" },
            { data: "Auditoria.TipoUsuario" }
        ];
        var columnDefs = [ 
            //{
            //    "targets": [1],
            //    "className": "text-right",
            //    'render': function (data, type, full, meta) {
            //        return '' + app.FormatNumber(data) + '';
            //    }
            //},

            {
                "targets": [2],
                'render': function (data, type, full, meta) {
                    if (data === 1) {
                        return "Activo";
                    } else return "Inactivo";

                }
            },
            {
                "targets": [3],
                "visible": true,
                "orderable": false,
                "className": "text-center",
                'render': function (data, type, full, meta) {
                    if (data === "1") {
                        return "<center>" +
                            '<a class="btn btn-default btn-xs" style= "margin-right:0.5em" title="Editar" href="javascript:Calculos.EditarCalculos(' + meta.row + ');"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></a>' +
                            '<a class="btn btn-default btn-xs" style= "margin-right:0.5em" title="Eliminar" href="javascript:Calculos.EliminarCalculos(' + meta.row + ')"><i class="fa fa-trash" aria-hidden="true"></i></a>' +
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
        app.FillDataTableAjaxPaging($tblListadoCalculos, url, parms, columns, columnDefs, filters, null, null);

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
        InsertUpdateCalculos();
    }

    function InsertUpdateCalculos() {

        var obj = {
            "Calculos_Id": Global.Calculos_Id,    
            "Descripcion": $txtModalDescripcion.val(),   
            "Sueldo": $txtModalSueldo.val(),
            "Estado": $cboModalEstado.val()
        };

        var method = "POST";
        var data = obj;
        var url = "Calculos/InsertUpdateCalculos";

        var fnDoneCallback = function (data) {
            app.Message.Success("Grabar", Message.GuardarSuccess, "Aceptar", null);
            $modalCalculos.modal('hide');
            GetCalculoss();
        };
        app.CallAjax(method, url, data, fnDoneCallback);
    }


    function $btnBuscar_click() {
        GetCalculo();
    }             

    function EditarCalculos(row) {
        var data = app.GetValueRowCellOfDataTable($tblListadoCalculos, row);
        $titleModalCalculos.html("Editar calculos");

        $modalCalculos.modal();
        Global.Calculos_Id = data.Calculos_Id;
        $txtModalDescripcion.val(data.Descripcion);
        $txtModalSueldo.val(data.Sueldo);
        $cboModalEstado.val(data.Estado).trigger('change');
        app.Event.Enable($cboModalEstado);
    }

    function EliminarCalculos(row) {
        var fnAceptarCallback = function () {
            var data = app.GetValueRowCellOfDataTable($tblListadoCalculos, row);

            var obj = {
                "Calculos_Id": data.Calculos_Id
            };

            var method = "POST";
            var url = "Calculos/DeleteCalculos";
            var rsdata = obj;
            var fnDoneCallback = function (data) {
                GetCalculoss();
            };
            app.CallAjax(method, url, rsdata, fnDoneCallback, null, null, null);
        };
        app.Message.Confirm("Aviso", "Esta seguro que desea eliminar el Calculos?", "Aceptar", "Cancelar", fnAceptarCallback, null);
    }

    return {
        EditarCalculos: EditarCalculos,
        EliminarCalculos: EliminarCalculos
    };


})(window.jQuery, window, document);