using Microsoft.AspNetCore.Identity;
using SistemaAC.Data;
using SistemaAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.ModelClass
{
    public class CategoriaModels
    {
        private ApplicationDbContext context;

        private string nombre { get; set; }
        private string descripcion { get; set; }
        private string estado { get; set; }


        public CategoriaModels(ApplicationDbContext context)
        {
            this.context = context;
        }



        public List<IdentityError> SaveCategoria(CategoriaModels vm)
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
                Code = "Save",
                Description = "Save"
            });
            return errorList;
        }
    }
}
