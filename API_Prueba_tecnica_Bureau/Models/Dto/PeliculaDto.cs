using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Prueba_tecnica_Bureau.Models.Dto
{
    public class PeliculaDto
    {
        [Key]
        public int Id_pelicula { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public int Id_categoria { get; set; }
        public Categoria? Categoria { get; set; }

        [Required]
        public int Id_director { get; set; }
        public Director? Director { get; set; }

        [Required]
        public int Id_idioma { get; set; }
        public Idioma? Idioma { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime Fechadeestreno { get; set; }

        [Required]
        [MaxLength(250)]
        public string Descripcion { get; set; }

        [Required]
        public int Duracion { get; set; }
    }
}
