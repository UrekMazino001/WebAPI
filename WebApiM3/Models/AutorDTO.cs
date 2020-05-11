using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiM3.Models
{
    public class AutorDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Error")]
        [StringLength(10, ErrorMessage = "El campo de tener un tamaño de 20 caracteres")]
        public string Nombre { get; set; } 
        public DateTime FechaNacimiento { get; set; }
        public List<LibroDTO> Books { get; set; }
    }
}
