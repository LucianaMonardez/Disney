using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Disney.Models
{
    public class Genre
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGenero { get; set; }
        public string NombreGenero { get; set; }
        public string ImagenGenero { get; set; }
    }
}
