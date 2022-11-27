using API_Prueba_tecnica_Bureau.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API_Prueba_tecnica_Bureau.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Usuario_autenticacion> Usuarios_autenticacion { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Director> Directores { get; set; }
        public DbSet<Idioma> Idiomas { get; set; }
    }
}
