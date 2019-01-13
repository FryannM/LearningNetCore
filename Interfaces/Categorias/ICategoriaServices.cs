using Microsoft.AspNetCore.Identity;
using SistemaAC.Data;
using SistemaAC.ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.Interfaces.Categorias
{
    interface ICategoriaServices
    {    
        List<IdentityError> SaveCategoria(CategoriaViewModel vm);
    }
}
