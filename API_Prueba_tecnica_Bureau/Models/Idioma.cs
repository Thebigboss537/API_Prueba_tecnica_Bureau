using System.ComponentModel.DataAnnotations;

namespace API_Prueba_tecnica_Bureau.Models
{
    public class Idioma
    {
        [Key]
        public int Id_idioma { get; set; }

        [Required]
        public string Nombre { get; set; }

    }
}
