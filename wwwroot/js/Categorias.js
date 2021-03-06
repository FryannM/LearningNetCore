﻿
var localStorage = window.localStorage;
class Categorias {

    constructor(nombre, descripcion, estado, action) {
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.estado = estado;
        this.action = action;
    }

    AddCategoria() {

        if (this.nombre === "") {
            document.getElementById('Nombre').focus();
        }
        else {
            if (this.descripcion === "") {
                document.getElementById('Descripcion').focus();
            }
            else {
                if (this.estado === "0") {
                    document.getElementById('Mensaje').innerHTML = "Selecione un Estado";
                } else {
                    var nombre = this.nombre;
                    var descripcion = this.descripcion;
                    var estado = this.estado;
                    var action = this.action;
                    var mensaje = '';
                    $.ajax({
                        type: "POST",
                        url: action,
                        data: {
                            nombre, descripcion, estado
                        },
                        success: (response) => {
                            $.each(response, (index, val) => {
                                mensaje = val.code;
                            });
                            if (mensaje === "Save") {
                                this.restablecer();
                            } else {
                                document.getElementById("mensaje").innerHTML = "No se puede guardar la categoria";
                            }
                        //console.log(response);
                        }
                    });
                }
            }
          
        }
    }
    filtrarDatos(numPagina) {
        var valor = this.nombre;
        var action = this.action;
        if (valor === "") {
            valor = "null";
        }
        $.ajax({
            type: "POST",
            url: action,
            data: { valor, numPagina },
            success: (response) => {
                console.log(response);
                $.each(response, (index, val) => {
                    $("#resultSearch").html(val[0]);
                    $("#paginado").html(val[1]);
                });
            }
        });
    }
    restablecer() {
        document.getElementById("Nombre").value = "";
        document.getElementById("Descripcion").value = "";
        document.getElementById("Mensaje").innerHTML = "";
        document.getElementById("Estado").selectedIndex = 0;
        $('#modalAc').modal('hide');
        $('#ModalEstado').modal('hide');
        filtrarDatos(1);
    }

    getCategorias(id) {
        var action = this.action;
        $.ajax({
            type: "POST",
            url: action,
            data: { id },
            success: (response) => {
                if (response[0].response) {
                    document.getElementById("titleCategoria").innerHTML = "Esta seguro desactivar la  " +
                        " categoria " + response[0].nombre;

                }
                else {
                    document.getElementById("titleCategoria").innerHTML = "Esta seguroo de activar la  " +
                        " categoria " + response[0].nombre;
                }
                console.log(response);
                localStorage.setItem("categoria", JSON.stringify(response));
               }
        });
    }
    editar(id, nombre, descripcion, estado, funcion) {
        debugger;
        var action = this.action;
        $.ajax({
            type: "POST",
            url: action,
            data:{ id, nombre, descripcion, estado, funcion },
            success: (response) => {
                console.log(response);
                this.restablecer();
            }
        });

}
    editarCategoria(id, funcion)
    {
        debugger;

        var nombre = null;
        var descripcion = null;
        var estado = null;
        var action = null;
        switch (funcion) {
            case "estado":
                var response = JSON.parse(localStorage.getItem("categoria"));
                nombre = response[0].nombre;
                descripcion = response[0].descripcion;
                estado = response[0].estado;
                localStorage.removeItem("categoria");
                this.editar(id, nombre, descripcion, estado, funcion);
                break;
            default:
        }

    }
}
  