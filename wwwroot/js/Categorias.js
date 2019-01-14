  
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
                            nombre,descripcion, estado
                        },
                        
                        success: (response) => {
                            $.each(response, (index, val) => {
                                mensaje = val.code;
                            });
                            if (mensaje === "8") {
                                this.restablecer();
                                mensaje = "Guardado con Existo";
                            } else {
                                document.getElementById("mensaje").innerHTML = "No se puede Guardar la Categoria";

                            }
                        }
                    });
                }
            }
          
        }
    }
    restablecer() {
        document.getElementById("Nombre").value = "";
        document.getElementById("Descripcion").value = "";
        document.getElementById("Mensaje").innerHTML = "";
        document.getElementById("Estado").selectedIndex = 0;
        $('#modalAc').modal('hide');
    }
}
  