using System.ComponentModel.DataAnnotations;

namespace API_Prueba_tecnica_Bureau.Models
{
    public class Categoria
    {
        [Key]
        public int Id_categoria { get; set; }

        [Required]
        public string Nombre { get; set; }
    }
}
