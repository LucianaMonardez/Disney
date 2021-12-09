using Disney.Data;
using Disney.Models;
using Disney.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CharacterController(ApplicationDbContext context)
        {
            _context = context;
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
                    resultado.Return += "El nombre del personaje es: " + personaje.NombrePersonaje + " Imagen:" + personaje.ImagenPersonaje;
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

        //Detalle personaje. Devuelve un personaje por id con todos sus atributos
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<Character> Get(int id)
        {
            return _context.Characters.Find(id);
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



    }
}
