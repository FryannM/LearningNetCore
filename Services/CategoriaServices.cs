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

        public CategoriaServices(ApplicationDbContext context)
           {
               this.context = context;
            FilterData(1, "Fryann");
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

         public List<object[]> FilterData(int numPage,string valor)
        {
            int count = 0, cant, numRegisters = 0, inicio = 0, reg_per_page = 2;
            int cant_page, page;
            string dataFilter = "", paginator = "", Estado = null;

            List<object[]> data = new List<object[]>();
            IEnumerable<Categoria> query;

            var categorias = context.Categoria.OrderBy(f => f.Nombre).ToList();
            numRegisters = categorias.Count;
            inicio = (numPage - 1) * reg_per_page;
            cant_page = (numRegisters / reg_per_page);

             if (valor == "null")   {
                query = categorias.Skip(inicio).Take(reg_per_page);
            } else {
                query = categorias
                    .Where(d => 
                    d.Nombre.StartsWith(valor) ||
                    d.Descripcion.StartsWith(valor))
                    .Skip(inicio)
                    .Take(reg_per_page);
            }
            cant = query.Count();

            return data;

        }
    }
}
