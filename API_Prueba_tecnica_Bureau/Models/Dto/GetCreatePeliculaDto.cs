namespace API_Prueba_tecnica_Bureau.Models.Dto
{
    public class GetCreatePeliculaDto
    {
        public List<Categoria> Categorias { get; set; }

        public List<Idioma> Idiomas { get; set; }

        public List<Director> Directores { get; set; }
    }
}
