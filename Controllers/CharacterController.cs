using Disney.Data;
using Disney.Models;
using Disney.Response;
using Disney.Schemas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Disney.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CharacterController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Eliminacion personaje
        [HttpDelete("{id}")]
        public ActionResult<Character> Delete(int id)
        {
            var character = _context.Characters.Find(id);
            if (character == null)
            {
                return NotFound();
            }
            _context.Characters.Remove(character);
            _context.SaveChanges();

            return character;
        }

        //devuelve una lista de personajes
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<ResponseApi> Get()
        {
            var resultado = new ResponseApi();
            var personajes = _context.Characters.ToList();
            try
            {
                foreach (var personaje in _context.Characters.ToList())
                {
                    resultado.Return += "El nombre del personaje es: " + personaje.NombrePersonaje + " Imagen:" + personaje.ImagenPersonaje + "/  ";
                }

                resultado.Ok = true;
                return resultado;
            }
            catch (Exception)
            {
                resultado.Ok = false;

                return resultado;
            }
        }

        //Detalle personaje. Devuelve un personaje por id con todos sus atributos, peliculas asociadas y genero
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<Character> Get(int id)
        {
            var personaje = _context.Characters.Find(id);
            if (personaje == null)
            {
                return NotFound();
            }
            _context.Entry(personaje).Reference(x => x.MovieOrSerie).Load();
            _context.Entry(personaje.MovieOrSerie).Reference(x => x.genre).Load();

            return personaje;
        }

        //

        //Personaje por edad
        [HttpGet]
        [Route("age")]
        [AllowAnonymous]
        public ActionResult<ResponseApi> GetByAge([FromQuery] int age)
        {
            var resultado = new ResponseApi();
            try
            {
                var characters = _context.Characters.Where(k => k.Edad == age).ToList();

                foreach (var character in characters)
                {
                    _context.Entry(character).Reference(x => x.MovieOrSerie).Load();
                    _context.Entry(character.MovieOrSerie).Reference(x => x.genre).Load();
                }
                resultado.Ok = true;
                resultado.Return = characters;
                return resultado;
            }
            catch (Exception error)
            {
                resultado.Ok = false;
                resultado.Error = "404 - El personaje no existe: " + error.Message;

                return resultado;
            }
        }

        //Personaje por pelicula(idmovie)
        [HttpGet]
        [Route("movie")]
        [AllowAnonymous]
        public ActionResult<ResponseApi> GetByIdMovie([FromQuery] int idMovie)
        {
            var resultado = new ResponseApi();
            try
            {
                var characters = _context.Characters.Where(k => k.IdPelicula == idMovie).ToList();
                foreach (var character in characters)
                {
                    _context.Entry(character).Reference(x => x.MovieOrSerie).Load();

                    _context.Entry(character.MovieOrSerie).Reference(x => x.genre).Load();
                }
                resultado.Ok = true;
                resultado.Return = characters;

                return resultado;
            }
            catch (Exception error)
            {
                resultado.Ok = false;
                resultado.Error = "404 - El personaje no existe: " + error.Message;

                return resultado;
            }
        }

        //Personaje por nombre
        [HttpGet]
        [Route("name")]
        [AllowAnonymous]
        public ActionResult<ResponseApi> GetByName([FromQuery] string name)
        {
            var resultado = new ResponseApi();
            try
            {
                var characters = _context.Characters.Where(k => k.NombrePersonaje.Contains(name)).ToList();

                foreach (var character in characters)
                {
                    _context.Entry(character).Reference(x => x.MovieOrSerie).Load();
                    _context.Entry(character.MovieOrSerie).Reference(x => x.genre).Load();
                }

                resultado.Ok = true;
                resultado.Return = characters;
                return resultado;
            }
            catch (Exception error)
            {
                resultado.Ok = false;
                resultado.Error = "404 - El personaje no existe: " + error.Message;

                return resultado;
            }
        }

        [HttpPost("create")]
        public ActionResult<ResponseApi> Post([FromBody] SchemaCreateCharacter character)
        {
            var resultado = new ResponseApi();
            var personaje = new Character();
            personaje.NombrePersonaje = character.NombrePersonaje;
            personaje.ImagenPersonaje = character.ImagenPersonaje;
            personaje.Edad = character.Edad;
            personaje.Peso = character.Peso;
            personaje.Historia = character.Historia;
            personaje.IdPelicula = character.IdPelicula;

            _context.Characters.Add(personaje);
            _context.SaveChanges();
            resultado.Ok = true;
            resultado.Return = personaje;
            _context.Entry(personaje).Reference(x => x.MovieOrSerie).Load();
            return resultado;
        }

        //Educion atributos de personaje
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Character character)
        {
            if (id != character.IdPersonaje)
            {
                return BadRequest();
            }

            _context.Entry(character).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }
    }
}