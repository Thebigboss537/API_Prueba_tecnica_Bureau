using System.ComponentModel.DataAnnotations;

namespace API_Prueba_tecnica_Bureau.Models
{
    public class Director
    {
        [Key]
        public int Id_director { get; set; }

        [Required]
        public string Nombre { get; set; }
    }
}
