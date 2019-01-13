using Microsoft.AspNetCore.Identity;
using SistemaAC.Data;
using SistemaAC.Interfaces.Categorias;
using SistemaAC.ModelClass;
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

            var categoria = new CategoriaViewModel
            {
               
                nombre = vm.nombre,
                descripcion = vm.descripcion,
                estado = Convert.ToBoolean(vm.estado),
            };
            context.Add(categoria);

            context.SaveChanges();

            errorList.Add(new IdentityError
            {
                Code = "120",
                Description = "No Save"
            });
            return errorList;
        }
    }
}
