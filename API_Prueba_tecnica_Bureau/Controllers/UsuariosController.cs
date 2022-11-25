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
using Azure;
using API_Prueba_tecnica_Bureau.Repositorios;

namespace API_Prueba_tecnica_Bureau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly DataContext _context;
        protected ResponseDto _response;

        public UsuariosController(DataContext context, IUsuarioRepositorio usuarioRepositorio)
        {
            _context = context;
            _usuarioRepositorio = usuarioRepositorio;
            _response = new ResponseDto();
        }

        

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id_usuario)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
          if (_context.Usuarios == null)
          {
              return Problem("Entity set 'DataContext.Usuarios'  is null.");
          }
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id_usuario }, usuario);
        }*/

        /*[HttpPost]
        public async Task<ActionResult<Autor>> PostAutor(AutorDto autorDto)
        {
            try
            {
                AutorDto model = await _autorRepositorio.CreateUpdate(autorDto);
                _response.Result = model;
                return CreatedAtAction("GetAutor", new { id = model.Id_autor }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al crear el autor";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }*/

        [HttpPost]
        public async Task<ActionResult> Registro(Login_RegisterDto usuario)
        {
            
            var respuesta = await _usuarioRepositorio.Registro(usuario);
                
            if (respuesta == "usuario registrado")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario ya registrado";
                return BadRequest(_response);
            }

            JwTPackage jpt = new JwTPackage();
            jpt.UserName = usuario.Correo_electronico.ToString();
            jpt.Token = respuesta;
            _response.Result = jpt;
            _response.DisplayMessage = "Usuario conectado";
            return Ok(_response);


            /*_response.Result = model;
            return CreatedAtAction("GetAutor", new { id = model.Id_autor }, _response);*/

            /*_response.IsSuccess = false;
            _response.DisplayMessage = "Error al crear el autor";
            _response.ErrorMessages = new List<string> { ex.ToString() };
            return BadRequest(_response);*/

        }



        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.Id_usuario == id)).GetValueOrDefault();
        }

        public class JwTPackage
        {
            public string UserName { get; set; }
            public string Token { get; set; }
        }
    }
}
