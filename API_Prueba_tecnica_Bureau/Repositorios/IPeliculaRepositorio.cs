using API_Prueba_tecnica_Bureau.Models.Dto;

namespace API_Prueba_tecnica_Bureau.Repositorios
{
    public interface IPeliculaRepositorio
    {
        Task<List<PeliculaDto>> GetPeliculas();
        Task<PeliculaDto> GetPeliculaById(int id);
        Task<PeliculaDto> CreateUpdate(PeliculaDto peliculaDto);
        Task<bool> DeletePelicula(int id);
        Task<GetCreatePeliculaDto> GetCreatePeliculas();
    }
}
