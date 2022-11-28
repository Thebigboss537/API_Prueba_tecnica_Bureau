using API_Prueba_tecnica_Bureau;
using API_Prueba_tecnica_Bureau.Controllers;
using API_Prueba_tecnica_Bureau.Data;
using API_Prueba_tecnica_Bureau.Models.Dto;
using API_Prueba_tecnica_Bureau.Repositorios;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.tests
{
    public class LoginTests
    {
        private readonly IConfiguration _configuration;
        private readonly UsuarioRepositorio _usuarioRepositorio;
        private readonly DataContext _db;
        private readonly UsuariosController _controller;

        public LoginTests()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            var contextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseSqlServer(_configuration.GetConnectionString("DefaultConnection")).Options;

            _db = new DataContext(contextOptions);
            _usuarioRepositorio = new UsuarioRepositorio(_db, _configuration);
            _controller = new UsuariosController(_usuarioRepositorio);

            
        }


        [Theory]
        [InlineData("eudesgmail.com", "Eudes2022?", "Debe ser un correo electronico valido")]
        //[InlineData("eudes@gmail.com", "Eudes2022?", null)]//Cambiar correo que no este registrado para nuevos tests
        public async void CorreoValidoTest(string correo_electronico, string contraseña, string message)
        {
            /*var a = Guid.NewGuid().ToString("n").Substring(0, 8);
            correo_electronico = a + correo_electronico;*/

            Login_RegisterDto model = new()
            {
                Correo_electronico = correo_electronico,
                Contraseña = contraseña
            };

            // Verificar validacion del modelo
            var validatemodel = ValidateModel(model);

            if (validatemodel.Count != 0)
            {
                Assert.True(validatemodel.Any(v => v.ErrorMessage.Contains(message)));
            }
            else
            {
                ObjectResult result = (ObjectResult)await _controller.Login(model);

                ResponseDto response = (ResponseDto)result.Value;

                Assert.True(result.StatusCode.Equals(StatusCodes.Status400BadRequest) && response.ErrorMessages.IsNullOrEmpty());
            }
        }

        [Theory]
        [InlineData("eudes1@gmail.com", "Eudes2022?", "Correo electronico no registrado")]
        public async void CorreoNoregistradoTest(string correo_electronico, string contraseña, string message)
        {
            Login_RegisterDto model = new()
            {
                Correo_electronico = correo_electronico,
                Contraseña = contraseña
            };

            ObjectResult result = (ObjectResult)await _controller.Login(model);

            ResponseDto response = (ResponseDto)result.Value;

            Assert.True(result.StatusCode.Equals(StatusCodes.Status400BadRequest) && response.DisplayMessage.Equals(message));
        }

        [Theory]
        [InlineData("eudes@gmail.com", "Eudes202?", "La contraseña debe tener al menos 10 caracteres")]
        [InlineData("eudes@gmail.com", "Eudes20200", "La contraseña debe contener minimo una letra minúscula, una letra mayúscula y uno de los siguientes caracteres:!, @, #,? o ]")]
        [InlineData("eudes@gmail.com", "eudes20200", "La contraseña debe contener minimo una letra minúscula, una letra mayúscula y uno de los siguientes caracteres:!, @, #,? o ]")]
        [InlineData("eudes@gmail.com", "EUDES20200", "La contraseña debe contener minimo una letra minúscula, una letra mayúscula y uno de los siguientes caracteres:!, @, #,? o ]")]
        [InlineData("eudes@gmail.com", "EudesDavid", "La contraseña debe contener minimo una letra minúscula, una letra mayúscula y uno de los siguientes caracteres:!, @, #,? o ]")]
        public async void ContraseñaValidaTest(string correo_electronico, string contraseña, string message)
        {
            Login_RegisterDto model = new()
            {
                Correo_electronico = correo_electronico,
                Contraseña = contraseña
            };

            // Verificar validacion del modelo
            var validatemodel = ValidateModel(model);

            if (validatemodel.Count != 0)
            {
                Assert.True(validatemodel.Any(v => v.ErrorMessage.Contains(message)));
            }
            else
            {
                ObjectResult result = (ObjectResult)await _controller.Registro(model);

                ResponseDto response = (ResponseDto)result.Value;

                Assert.True(result.StatusCode.Equals(StatusCodes.Status400BadRequest) && response.ErrorMessages.IsNullOrEmpty());
            }
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

    }
}
