using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Prueba_tecnica_Bureau.Models
{
    public class Usuario
    {
        [Key]
        public int Id_usuario { get; set; }

        [Required]
        public string Correo_electronico { get; set; }

        [Required]
        public string Contraseña { get; set; }

        [ForeignKey("Usuario_autenticacion")]
        public int? Id_usuario_autenticacion { get; set; }
        public Usuario_autenticacion? Usuario_autenticacion { get; set; }
    }
}
