using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Prueba_tecnica_Bureau.Models
{
    public class Pelicula
    {
        [Key]
        public int Id_pelicula { get; set; }

        [Required]
        public string Titulo { get; set; }

        [ForeignKey("Categoria")]
        public int Id_categoria { get; set; }
        public Categoria? Categoria { get; set; }

        [ForeignKey("Director")]
        public int Id_director { get; set; }
        public Director? Director { get; set; }

        [ForeignKey("Idioma")]
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
