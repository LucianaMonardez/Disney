using Disney.Data;
using Disney.Models;
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
    public class MovieController : ControllerBase
    {
        

            private readonly ApplicationDbContext _context;
            public MovieController(ApplicationDbContext context)
            {
                _context = context;
            }
        //devuelve el listado de las peliculas con todos sus atributos
            [AllowAnonymous]
            [HttpGet]
            public IEnumerable<MovieOrSerie> Get()
            {
                var movie = _context.MovieOrSeries.ToList();
                return movie;
            }
        //devuelve el detalle de una pelicula
            [AllowAnonymous]
            [HttpGet("{id}")]
            public ActionResult<MovieOrSerie> Get(int id)
            {
                return _context.MovieOrSeries.Find(id);
            }
        //creacion de una nueva pelicula
        [HttpPost("{id}")]
        public ActionResult Post(MovieOrSerie movie)
        {
            _context.MovieOrSeries.Add(movie);
            _context.SaveChanges();

            return new CreatedAtRouteResult("Getmovie", new { id = movie.IdPelicula }, movie);

        }
        
        //modificacion de pelicula
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] MovieOrSerie movie)
        {
            if (id != movie.IdPelicula)
            {
                return BadRequest();

            }

            _context.Entry(movie).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();

        }
        //eliminacion de pelicula
        [HttpDelete("{id}")]
        public ActionResult<MovieOrSerie> Delete(int id)
        {
            var movie = _context.MovieOrSeries.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            _context.MovieOrSeries.Remove(movie);
            _context.SaveChanges();

            return movie;
        }

    }
    
}

