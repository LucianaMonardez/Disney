using Disney.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Disney.Schemas
{
    public class SchemaGetMovie
    {
        public string TituloPelicula { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaDeCreacion { get; set; }

        public int Calificacion { get; set; }
        public string ImagenPelicula { get; set; }

        public int IdGenero { get; set; }

        public List<Character> Characters { get; set; }

    }
}
