using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Disney.Schemas
{
    public class SchemaCreateMovie
    {
        [Required(ErrorMessage = "El titulo es obligatorio")]
        [MaxLength(100, ErrorMessage = "Excediste los 100 caracteres")]
        public string TituloPelicula { get; set; }

        [Required(ErrorMessage = "La fecha de creacion es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaDeCreacion { get; set; }

        [Required(ErrorMessage = "La calificacion es obligatoria")]
        [Range(0, 5, ErrorMessage = "El rango permitido es de 0 a 5")]

        public int Calificacion { get; set; }
        public string ImagenPelicula { get; set; }

        [Required(ErrorMessage = "El numero de id del genero es obligatorio")]

        public int IdGenero { get; set; }

    }
}
