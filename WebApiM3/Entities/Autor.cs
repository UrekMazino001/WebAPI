using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiM3.Helpers; //Importada

namespace WebApiM3.Entities
{
    public class Autor
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Error")]
        [StringLength (10, ErrorMessage ="El campo de tener un tamaño de 20 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public List<Libro> Libros { get; set; }
    }
}
