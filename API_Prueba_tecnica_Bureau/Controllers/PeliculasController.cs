using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Prueba_tecnica_Bureau.Data;
using API_Prueba_tecnica_Bureau.Models;
using API_Prueba_tecnica_Bureau.Models.Dto;
using API_Prueba_tecnica_Bureau.Repositorios;
using Microsoft.AspNetCore.Authorization;

namespace API_Prueba_tecnica_Bureau.Controllers
{
    [Authorize]
    [Route("api/Pelicula")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculaRepositorio _peliculaRepositorio;
        protected ResponseDto _response;

        public PeliculasController(IPeliculaRepositorio peliculaRepositorio)
        {
            _peliculaRepositorio = peliculaRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/Peliculas
        [HttpGet("listarpeliculas/")]
        public async Task<ActionResult<IEnumerable<Pelicula>>> GetPeliculas()
        {
            try
            {
                var lista = await _peliculaRepositorio.GetPeliculas();
                _response.Result = lista;
                _response.DisplayMessage = "Lista de Peliculas";
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        // GET: api/Peliculas/5
        [HttpGet("listarpelicula/{id}")]
        public async Task<ActionResult<Pelicula>> GetPelicula(int id)
        {
            var pelicula = await _peliculaRepositorio.GetPeliculaById(id);
            if (pelicula == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Pelicula no existe";
                return NotFound(_response);
            }
            _response.Result = pelicula;
            _response.DisplayMessage = "Informacion de la pelicula";
            return Ok(_response);
        }

        // PUT: api/Peliculas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("editarpelicula/{id}")]
        public async Task<IActionResult> PutPelicula(int id, PeliculaDto peliculaDto)
        {
            try
            {
                PeliculaDto model = await _peliculaRepositorio.CreateUpdate(peliculaDto);
                _response.Result = model;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar la pelicula";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // POST: api/Peliculas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("crearpelicula/")]
        public async Task<ActionResult<Pelicula>> PostPelicula(PeliculaDto peliculaDto)
        {
            try
            {
                PeliculaDto model = await _peliculaRepositorio.CreateUpdate(peliculaDto);
                _response.Result = model;
                return CreatedAtAction("GetPelicula", new { id = model.Id_pelicula }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al crear la pelicula";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // DELETE: api/Peliculas/5
        [HttpDelete("editarpelicula/{id}")]
        public async Task<IActionResult> DeletePelicula(int id)
        {
            try
            {
                bool estaeliminado = await _peliculaRepositorio.DeletePelicula(id);
                if (estaeliminado)
                {
                    _response.Result = estaeliminado;
                    _response.DisplayMessage = "Pelicula eliminada con exito";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error al eliminar la pelicula";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        [HttpGet("crearpelicula/")]
        public async Task<ActionResult<GetCreatePeliculaDto>> GetCreatePelicula()
        {
            try
            {
                var lista = await _peliculaRepositorio.GetCreatePeliculas();
                _response.Result = lista;
                _response.DisplayMessage = "Lista de atributos para crear una pelicula";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }
    }
}
