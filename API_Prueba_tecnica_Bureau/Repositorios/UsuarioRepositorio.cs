using API_Prueba_tecnica_Bureau.Data;
using API_Prueba_tecnica_Bureau.Models;
using API_Prueba_tecnica_Bureau.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API_Prueba_tecnica_Bureau.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DataContext _db;
        private readonly IConfiguration _configuration ;

        public UsuarioRepositorio(DataContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<string> Login(Login_RegisterDto Usuario)
        {
            try
            {
                var user = await _db.Usuarios_autenticacion.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(Usuario.Correo_electronico.ToLower()));

                if (await IsRegister(Usuario.Correo_electronico))
                {
                    if (VerificarPasswordHash(Usuario.Contraseña, user.PasswordHash, user.PasswordSalt))
                    {
                        return CrearToken(user);
                    }
                    else
                    {
                        return "contraseña incorrecta";
                    }
                }
                else
                {
                    return "usuario no registrado";
                }
            }
            catch (Exception e)
            {
                return "error";
            }
            


            /*if (user == null)
            {
                return "nouser";
            }
            else if (user.PasswordHash == null)
            {
                return "usuario no registrado";
            }
            else if (!VerificarPasswordHash(Usuario.Contraseña, user.PasswordHash, user.PasswordSalt))
            {
                return "wrongpassword";
            }
            else
            {
                return CrearToken(user);
            }*/
        }


        public async Task<string> Registro(Login_RegisterDto usuario)
        {
            try
            {
                Usuario_autenticacion usuario_Autenticacion = new Usuario_autenticacion();
                Usuario usuario2 = new Usuario();
                if (await IsRegister(usuario.Correo_electronico.ToString()))
                {
                    return "usuario registrado";
                }
                else
                {
                    usuario2.Correo_electronico = usuario.Correo_electronico;

                    CrearPasswordHash(usuario.Contraseña, out byte[] passwordHash, out byte[] passwordSalt);

                    usuario_Autenticacion.PasswordHash = passwordHash;
                    usuario_Autenticacion.PasswordSalt = passwordSalt;
                    usuario_Autenticacion.Username = usuario.Correo_electronico;
                    usuario_Autenticacion.Id_rol = 1;
                        
                    //usuario_Autenticacion.Id_usuario_autenticacion = _db.Usuarios_autenticacion.AsNoTracking().Where(e => e.Username == usuario.Correo_electronico).FirstOrDefault().Id_usuario_autenticacion;
                    await _db.Usuarios_autenticacion.AddAsync(usuario_Autenticacion);
                    await _db.SaveChangesAsync();
                    usuario2.Id_usuario_autenticacion = usuario_Autenticacion.Id_usuario_autenticacion;
                    await _db.Usuarios.AddAsync(usuario2);
                    //_db.Usuarios_autenticacion.Update(usuario_Autenticacion);
                    await _db.SaveChangesAsync();
                    return "ok";
                }
                
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        /*public async Task<bool> UserExiste(string Nombre_usuario)
        {
            if (await _db.Usuarios_autenticacion.AnyAsync(x => x.Username.ToString().ToLower().Equals(Nombre_usuario.ToLower())))
            {
                return true;
            }
            return false;
        }*/


        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwrodSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwrodSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string CrearToken(Usuario_autenticacion usuario)
        {
            int id = _db.Usuarios.FirstOrDefault(e => e.Id_usuario_autenticacion == usuario.Id_usuario_autenticacion).Id_usuario;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username.ToString()),
                new Claim(ClaimTypes.Role, usuario.Id_rol.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.
                GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> IsRegister(string correo_electronico)
        {
            return await _db.Usuarios_autenticacion.AnyAsync(x => correo_electronico.ToLower().Equals(x.Username.ToLower())/*.Equals(correo_electronico.ToLower()*/);


            
            /*if (true)
            {
                return true;
            }
            return false;


            var a = await _db.Usuarios_autenticacion.AsNoTracking().Where(e => e.Username.ToString() == cedula).FirstOrDefaultAsync();

            if (a.PasswordHash == null)
            {
                return true;
            }
            return false;*/
        }












    }
}
