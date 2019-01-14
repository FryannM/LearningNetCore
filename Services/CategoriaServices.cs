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
    }
}
