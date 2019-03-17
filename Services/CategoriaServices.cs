using Microsoft.AspNetCore.Identity;
using SistemaAC.Data;
using SistemaAC.Interfaces.Categorias;
using SistemaAC.ModelClass;
using SistemaAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.Services
{
    public class CategoriaServices : ICategoriaServices
    {
        private IEnumerable<Categoria> query;
        private Boolean estados;
        public CategoriaServices(ApplicationDbContext context)
        {
            this.context = context;
          
        }
        private ApplicationDbContext context;

        public List<IdentityError> SaveCategoria(CategoriaViewModel vm)
        {
            var errorList = new List<IdentityError>();
            var categoria = new Categoria
            {
                Nombre = vm.nombre,
                Descripcion = vm.descripcion,
                Estado = Convert.ToBoolean(vm.estado),
            };
            context.Add(categoria);

            context.SaveChanges();

            errorList.Add(new IdentityError
            {
                Code = "8",
                Description = "Save"
            });
            return errorList;
        }

        public List<object[]> filtrarDatos(int numPagina, string valor)
        {
            int count = 0, cant, numRegistros = 0, inicio = 0, reg_por_pagina = 2;
            int can_paginas, pagina;
            string dataFilter = "", paginador = "", Estado = null;
            List<object[]> data = new List<object[]>();

            var categorias = context.Categoria.OrderBy(c => c.Nombre).ToList();
            numRegistros = categorias.Count;
            inicio = (numPagina - 1) * reg_por_pagina;
            can_paginas = (numRegistros / reg_por_pagina);
            if (valor == "null")
            {
                query = categorias.Skip(inicio).Take(reg_por_pagina);
            }
            else
            {
                query = categorias.Where(c => c.Nombre.StartsWith(valor) || c.Descripcion.StartsWith(valor)).Skip(inicio).Take(reg_por_pagina);
            }
            cant = query.Count();
            foreach (var item in query)
            {
                if (item.Estado == true)
                {
                    Estado = "<a data-toggle='modal' data-target='#ModalEstado'" +
                        " onclick='ChangeEstatus(" + item.CatagoriaID + ")' class='label label-success'>Activo</a>";
                }
                else
                {
                    Estado = "<a data-toggle='modal' data-target='#ModalEstado'" +
                          " onclick='ChangeEstatus(" + item.CatagoriaID + ")' class='label label-danger'>No activo</a>";

                }
                dataFilter += "<tr>" +
                "<td>" + item.Nombre + "</td>" +
                "<td>" + item.Descripcion + "</td>" +
                "<td>" + Estado + " </td>" +
                "<td>" +
               "<a data-toggle='modal' data-target='#myModal' class='btn btn-success'>Edit</a>|" +
               "<a data-toggle='modal' data-target='#myModal3' class='btn btn-danger' >Delete</a>" +
               "</td>" +
                "</tr>";

                //var button = '<a href="Edit/' + data + '" id ="btnEditar"  class="btn btn-primary" alt="Editar"> <i class="fa fa-pencil"></i></a> ' +
                //       '<a href="javascript:;" data-id=' + data + ' id="btnEliminar" class="btn btn-danger" alt="Eliminar"> <i class="fa fa-trash"></i></a>';
                //return button;
            }
            object[] dataObj = { dataFilter, paginador };
            data.Add(dataObj);
            return data;

        }

        public List<Categoria> getCategorias(int id)
        {
            return context.Categoria.Where(c => c.CatagoriaID == id).ToList();
        }

        public List<IdentityError> editarCategoria(int idCategoria, string nombre, string descripcion,
            Boolean estado, string funcion)
        {

            var errorlist = new List<IdentityError>();

            switch (funcion)
            {
                case "estado":
                     if (estado)
                    {
                        estados = false;
                    }
                     else
                    {

                        estados = true;
                    }

                    var categoria = new Categoria()
                    {
                        CatagoriaID = idCategoria,
                        Nombre = nombre,
                        Descripcion = descripcion,
                        Estado = estados,
                                              
                    };
                    context.Update(categoria);
                    context.SaveChanges();
                    break;

            }
            errorlist.Add(new IdentityError
            {
                Code = "2",
                Description = "Save"

            });

            return errorlist;
        }
    }
}