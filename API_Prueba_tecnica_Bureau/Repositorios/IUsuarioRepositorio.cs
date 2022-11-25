using API_Prueba_tecnica_Bureau.Models;
using API_Prueba_tecnica_Bureau.Models.Dto;

namespace API_Prueba_tecnica_Bureau.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Task<string> Registro(Login_RegisterDto Usuario);
        Task<string> Login(Login_RegisterDto usuario);
    }
}
