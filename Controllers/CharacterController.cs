using Disney.Data;
using Disney.Models;
using Disney.Response;
using Disney.Schemas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            try
            {
                var personajes = _context.Characters.ToList();

                if (personajes.Count == 0)
                {
                    throw new Exception("No hay personajes para mostrar");
                }

                var personajesList = new List<SchemaGetCharacters>();

                foreach (var personaje in personajes)
                {
                    personajesList.Add(new SchemaGetCharacters()
                    {
                        ImagenPersonaje = personaje.ImagenPersonaje,
                        NombrePersonaje = personaje.NombrePersonaje
                    });
                }

                resultado.Ok = true;
                resultado.Return = personajesList;
                return resultado;
            }
            catch (Exception error)
            {
                resultado.Ok = false;
                resultado.Error = "404 - " + error.Message;

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
                if (characters.Count == 0)
                {
                    throw new Exception("Edad: " + age.ToString());
                }

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

                if (characters.Count == 0)
                {
                    throw new Exception("idMovie: " + idMovie.ToString());
                }

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
                if (characters.Count == 0)
                {
                    throw new Exception("Name: " + name);
                }

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
        public ActionResult Put(int id, [FromBody] SchemaEditCharacter character)
        {
            var personaje = _context.Characters.Find(id);

            if (personaje is null)
            {
                return BadRequest();
            }

            personaje.NombrePersonaje = character.NombrePersonaje;
            personaje.ImagenPersonaje = character.ImagenPersonaje;
            personaje.Edad = character.Edad;
            personaje.Peso = character.Peso;
            personaje.Historia = character.Historia;
            personaje.IdPelicula = character.IdPelicula;


            _context.Entry(personaje).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }
    }
}