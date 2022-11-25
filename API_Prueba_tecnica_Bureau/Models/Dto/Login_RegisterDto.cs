using System.ComponentModel.DataAnnotations;

namespace API_Prueba_tecnica_Bureau.Models.Dto
{
    public class Login_RegisterDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Debe ser un correo electronico valido")]
        public string Correo_electronico { get; set; }

        [Required]
        public string Contraseña { get; set; }
    }
}
