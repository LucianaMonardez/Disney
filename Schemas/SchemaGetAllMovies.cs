using System;
using System.ComponentModel.DataAnnotations;

namespace Disney.Schemas
{
    public class SchemaGetAllMovies
    {
        public string TituloPelicula { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public string ImagenPelicula { get; set; }

    }
}