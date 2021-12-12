using System.ComponentModel.DataAnnotations;

namespace Disney.Schemas
{
    public class SchemaEditCharacter
    {

        public string NombrePersonaje { get; set; }

        public string ImagenPersonaje { get; set; }
        public int Edad { get; set; }
        public double Peso { get; set; }
        public string Historia { get; set; }

        public int IdPelicula { get; set; }
    }
}