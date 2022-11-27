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
    [Route("api/")]
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

        [HttpPost("Registro/")]
        public async Task<ActionResult> Registro(Login_RegisterDto usuario)
        {
            var respuesta = await _usuarioRepositorio.Registro(usuario);
                
            if (respuesta == "usuario registrado")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario ya registrado";
                return BadRequest(_response);
            }
            else if (respuesta == "error")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "error en el registro de usuario";
                return BadRequest(_response);
            }

            _response.DisplayMessage = "Usuario registrado con exito";
            return Ok(_response);
        }

        [HttpPost("Login/")]
        public async Task<ActionResult> Login(Login_RegisterDto usuario)
        {
            var respuesta = await _usuarioRepositorio.Login(usuario);

            if (respuesta == "usuario no registrado")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario no registrado";
                return BadRequest(_response);
            }
            else if (respuesta == "contraseña incorrecta")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Contraseña Incorrecta";
                return BadRequest(_response);
            }
            else if (respuesta == "error")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "error al iniciar sesion";
                return BadRequest(_response);
            }

            JwTPackage jpt = new()
            {
                UserName = usuario.Correo_electronico,
                Token = respuesta
            };
            _response.Result = jpt;
            _response.DisplayMessage = "Usuario conectado";
            return Ok(_response);
        }

        public class JwTPackage
        {
            public string UserName { get; set; }
            public string Token { get; set; }
        }
    }
}
