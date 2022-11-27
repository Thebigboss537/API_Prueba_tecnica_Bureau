using API_Prueba_tecnica_Bureau.Data;
using API_Prueba_tecnica_Bureau.Models;
using API_Prueba_tecnica_Bureau.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_Prueba_tecnica_Bureau.Repositorios
{
    public class PeliculaRepositorio : IPeliculaRepositorio
    {
        private readonly DataContext _db;
        private IMapper _mapper;
        public PeliculaRepositorio(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PeliculaDto> CreateUpdate(PeliculaDto peliculaDto)
        {
            Pelicula pelicula = _mapper.Map<PeliculaDto, Pelicula>(peliculaDto);
            if (pelicula.Id_pelicula > 0)
            {
                _db.Peliculas.Update(pelicula);
            }
            else
            {
                await _db.Peliculas.AddAsync(pelicula);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Pelicula, PeliculaDto>(pelicula);
        }

        public async Task<bool> DeletePelicula(int id)
        {
            try
            {
                Pelicula pelicula = await _db.Peliculas.FindAsync(id);
                if (pelicula == null)
                {
                    return false;
                }
                _db.Peliculas.Remove(pelicula);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<PeliculaDto> GetPeliculaById(int id)
        {
            Pelicula pelicula = await _db.Peliculas.Include(e => e.Categoria).Include(e => e.Director).Include(e => e.Idioma).FirstOrDefaultAsync(e => e.Id_pelicula == id);

            return _mapper.Map<PeliculaDto>(pelicula);
        }

        public async Task<List<PeliculaDto>> GetPeliculas()
        {
            List<Pelicula> lista = await _db.Peliculas.Include(e => e.Categoria).Include(e => e.Director).Include(e => e.Idioma).ToListAsync();

            return _mapper.Map<List<PeliculaDto>>(lista);
        }

        public async Task<GetCreatePeliculaDto> GetCreatePeliculas()
        {
            GetCreatePeliculaDto model = new() 
            { 
                Categorias = await _db.Categorias.ToListAsync(),
                Idiomas = await _db.Idiomas.ToListAsync(),
                Directores = await _db.Directores.ToListAsync()
            };

            return model;
        }

    }
}
