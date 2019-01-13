using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.ModelClass
{
    public class CategoriaViewModel
    {
        public virtual string  nombre { get; set; }
        public virtual string descripcion { get; set; }
        public virtual bool estado { get; set; }

    }
}
