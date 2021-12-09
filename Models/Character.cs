using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Disney.Models
{
    public class Character
    {
        [Key]
        public int IdPersonaje { get; set; }
        public string ImagenPersonaje { get; set; }
        [Required(ErrorMessage = "El campo Nombre no puede estar vacio")]
        [StringLength(30)]
        public string NombrePersonaje { get; set; }
        public int Edad { get; set; }
        public double Peso { get; set; }
        public string Historia { get; set; }
        public ICollection<MovieOrSerie> MovieOrSeries { get; set; }
        public int IdPelicula { get; set; }
        [ForeignKey("IdPelicula")]
        public MovieOrSerie MovieOrSerie { get; set; }
        public Character() { }

    }
}
