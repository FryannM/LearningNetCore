using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.Models
{
    public class Categoria
    {
        [Key]
        public int CatagoriaID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Boolean Estado { get; set; } = true;
        public ICollection<Curso> Cursos { get; set; }
    }
}                      
