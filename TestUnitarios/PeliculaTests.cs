using API_Prueba_tecnica_Bureau;
using API_Prueba_tecnica_Bureau.Controllers;
using API_Prueba_tecnica_Bureau.Data;
using API_Prueba_tecnica_Bureau.Models;
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
    public class PeliculaTests
    {
        private readonly IConfiguration _configuration;
        private readonly PeliculaRepositorio _peliculaRepositorio;
        private readonly DataContext _db;
        private readonly PeliculasController _controller;
        private IMapper _mapper;

        public PeliculaTests()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            var contextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseSqlServer(_configuration.GetConnectionString("DefaultConnection")).Options;

            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

            _mapper = mapper;

            _db = new DataContext(contextOptions);
            _peliculaRepositorio = new PeliculaRepositorio(_db, _mapper);
            _controller = new PeliculasController(_peliculaRepositorio);

        }

        [Fact]
        public async void ListarPeliculaTest()
        {
            var result = await _controller.GetPeliculas();

            ObjectResult objectResult = (ObjectResult)result.Result;

            ResponseDto response = (ResponseDto)objectResult.Value;

            Assert.True(objectResult.StatusCode.Equals(StatusCodes.Status200OK) && response.DisplayMessage.Equals("Lista de Peliculas"));
        }

        [Theory]
        [InlineData("titulo pelicula", 1, 1,"2022-12-31","descripcion de la pelicula",120,1, "Pelicula creada con exito")]
        public async void CrearPeliculaTest(string titulo, int idcategoria, int iddirector, DateTime fechaestreno, string descripcion, int duracion, int ididioma, string message)
        {
            PeliculaDto model = new()
            {
                Titulo = titulo,
                Id_categoria = idcategoria,
                Id_director = iddirector,
                Fechadeestreno = fechaestreno,
                Descripcion = descripcion,
                Duracion = duracion,
                Id_idioma = ididioma
            };

            var result = await _controller.PostPelicula(model);

            ObjectResult objectResult = (ObjectResult)result.Result;

            ResponseDto response = (ResponseDto) objectResult.Value;

            Assert.True(objectResult.StatusCode.Equals(StatusCodes.Status201Created) && response.DisplayMessage.Equals(message));

        }

        [Theory]
        [InlineData(4,"titulo pelicula editado", 1, 1, "2022-12-31", "descripcion de la pelicula editada", 120, 1, "Pelicula editada con exito")]
        public async void EditarPeliculaTest(int idpelicula, string titulo, int idcategoria, int iddirector, DateTime fechaestreno, string descripcion, int duracion, int ididioma, string message)
        {
            PeliculaDto model = new()
            {
                Id_pelicula = idpelicula,
                Titulo = titulo,
                Id_categoria = idcategoria,
                Id_director = iddirector,
                Fechadeestreno = fechaestreno,
                Descripcion = descripcion,
                Duracion = duracion,
                Id_idioma = ididioma
            };

            var result = await _controller.PutPelicula(idpelicula, model);

            ObjectResult objectResult = (ObjectResult)result;

            ResponseDto response = (ResponseDto)objectResult.Value;

            Assert.True(objectResult.StatusCode.Equals(StatusCodes.Status200OK) && response.DisplayMessage.Equals(message));
        }

        [Theory]
        [InlineData(5, "Pelicula eliminada con exito")]
        public async void EliminarPeliculaTest(int id_pelicula, string message)
        {
            var result = await _controller.DeletePelicula(id_pelicula);

            ObjectResult objectResult = (ObjectResult)result;

            ResponseDto response = (ResponseDto)objectResult.Value;

            Assert.True(objectResult.StatusCode.Equals(StatusCodes.Status200OK) && response.DisplayMessage.Equals(message));

        }

    }
}
