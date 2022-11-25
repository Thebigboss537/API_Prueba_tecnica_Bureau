using System.ComponentModel.DataAnnotations;

namespace API_Prueba_tecnica_Bureau.Models.Dto
{
    public class Login_RegisterDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Debe ser un correo electronico valido")]
        public string Correo_electronico { get; set; }

        [Required]
        [MinLength(10,ErrorMessage = "La contraseña debe tener al menos 10 caracteres")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#?\]]).+$", ErrorMessage = "La contraseña debe contener minimo una letra minúscula, una letra mayúscula y uno de los siguientes caracteres:!, @, #,? o ]")]
        public string Contraseña { get; set; }
    }
}
