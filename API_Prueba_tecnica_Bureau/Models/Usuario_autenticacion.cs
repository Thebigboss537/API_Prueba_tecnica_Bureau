using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Prueba_tecnica_Bureau.Models
{
    public class Usuario_autenticacion
    {
        [Key]
        public int Id_usuario_autenticacion { get; set; }

        public string Username { get; set; }

        public Byte[] PasswordHash { get; set; }

        public Byte[] PasswordSalt { get; set; }

        [ForeignKey("Rol")]
        public int Id_rol { get; set; }
        public Rol? Rol { get; set; }
    }
}
