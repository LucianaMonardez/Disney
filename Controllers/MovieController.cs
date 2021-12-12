﻿using Disney.Data;
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
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
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

        //devuelve el listado de las peliculas con los atributos titulo, fecha de creacion e imagen
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<ResponseApi> Get()
        {
            var resultado = new ResponseApi();
            var movies = _context.MovieOrSeries.ToList();

            try
            {
                foreach (var movie in movies)
                {
                    resultado.Return += "El titulo de la pelicula es: " + movie.TituloPelicula + "  -  Fecha de creacion:  " + movie.FechaDeCreacion + "   -  Imagen:" + movie.ImagenPelicula + "/  ";
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

        //devuelve el detalle de una pelicula y su genero
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<ResponseApi> Get(int id)
        {
            var resultado = new ResponseApi();

            try
            {
                var pelicula = _context.MovieOrSeries.Find(id);

                var movie = new SchemaGetMovie()
                {
                    TituloPelicula = pelicula.TituloPelicula,
                    ImagenPelicula = pelicula.ImagenPelicula,
                    Calificacion = pelicula.Calificacion,
                    FechaDeCreacion = pelicula.FechaDeCreacion,
                    IdGenero = pelicula.IdGenero
                };
                movie.Characters = _context.Characters.Where(x => x.IdPelicula == pelicula.IdPelicula).ToList();

                resultado.Ok = true;
                resultado.Return = movie;
                return resultado;
            }
            catch (Exception error)
            {
                resultado.Ok = false;
                resultado.Error = "404 - La pelicula no existe: " + error.Message;

                return resultado;
            }
        }

        //obtener pelicula por genero
        [HttpGet]
        [Route("genre")]
        [AllowAnonymous]
        public ActionResult<ResponseApi> GetByGenero([FromQuery] int genre)
        {
            var resultado = new ResponseApi();
            try
            {
                var movies = _context.MovieOrSeries.Where(k => k.genre.IdGenero == genre).ToList();

                foreach (var movie in movies)
                {
                    _context.Entry(movie).Reference(x => x.genre).Load();
                }
                resultado.Ok = true;
                resultado.Return = movies;

                return resultado;
            }
            catch (Exception error)
            {
                resultado.Ok = false;
                resultado.Error = "404 - El genero no existe: " + error.Message;

                return resultado;
            }
        }

        //obtener peliculas por nombre
        [HttpGet]
        [Route("name")]
        [AllowAnonymous]
        public ActionResult<ResponseApi> GetByName([FromQuery] string name)
        {
            var resultado = new ResponseApi();
            try
            {
                var movies = _context.MovieOrSeries.Where(k => k.TituloPelicula == name).ToList();
                foreach (var movie in movies)
                {
                    _context.Entry(movie).Reference(x => x.genre).Load();
                }
                resultado.Ok = true;
                resultado.Return = movies;
                return resultado;
            }
            catch (Exception error)
            {
                resultado.Ok = false;
                resultado.Error = "404 - El titulo de la pelicula no existe: " + error.Message;

                return resultado;
            }
        }

        [HttpGet]
        [Route("order")]
        [AllowAnonymous]
        public ActionResult<ResponseApi> GetWithOrder([FromQuery] string order)
        {
            var resultado = new ResponseApi();
            try
            {
                List<MovieOrSerie> movies = new List<MovieOrSerie>();

                if (order.ToLower() == "asc")
                {
                    movies = _context.MovieOrSeries.OrderBy(x => x.FechaDeCreacion).ToList();
                }
                else if (order.ToLower() == "desc")
                {
                    movies = _context.MovieOrSeries.OrderByDescending(x => x.FechaDeCreacion).ToList();
                }
                else
                {
                    throw new Exception("Debe utilizar ASC o DESC.");
                }

                foreach (var movie in movies)
                {
                    _context.Entry(movie).Reference(x => x.genre).Load();
                }
                resultado.Ok = true;
                resultado.Return = movies;

                return resultado;
            }
            catch (Exception error)
            {
                resultado.Ok = false;
                resultado.Error = "400: " + error.Message;

                return resultado;
            }
        }

        //creacion de una nueva pelicula
        [HttpPost("create")]
        public ActionResult<ResponseApi> Post([FromBody] SchemaCreateMovie movie)
        {
            var resultado = new ResponseApi();
            var pelicula = new MovieOrSerie();
            pelicula.TituloPelicula = movie.TituloPelicula;
            pelicula.FechaDeCreacion = movie.FechaDeCreacion;
            pelicula.Calificacion = movie.Calificacion;
            pelicula.ImagenPelicula = movie.ImagenPelicula;
            pelicula.IdGenero = movie.IdGenero;

            _context.MovieOrSeries.Add(pelicula);
            _context.SaveChanges();
            resultado.Ok = true;
            resultado.Return = pelicula;
            _context.Entry(pelicula).Reference(x => x.genre).Load();
            return resultado;
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
    }
}