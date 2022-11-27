using API_Prueba_tecnica_Bureau.Models;
using API_Prueba_tecnica_Bureau.Models.Dto;
using AutoMapper;

namespace API_Prueba_tecnica_Bureau
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<PeliculaDto, Pelicula>();
                config.CreateMap<Pelicula, PeliculaDto>();
            });
            return mappingConfig;
        }
    }
}
