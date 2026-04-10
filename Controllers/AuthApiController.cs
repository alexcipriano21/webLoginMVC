using Microsoft.AspNetCore.Mvc;
using webLoginMVC.Controllers.dao;
using webLoginMVC.Models;

namespace webLoginMVC.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AutenticacionApiController : ControllerBase
    {
        public readonly daoUsuario daoUsuario;
        public AutenticacionApiController(IConfiguration config)
        {
            daoUsuario = new daoUsuario(config);
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] Peticion datosRecibidos)
        {
            Usuario usuario = daoUsuario.getUsuarioLogin(datosRecibidos.correo);
            if (usuario != null)
            {
                bool esValida = BCrypt.Net.BCrypt.Verify(datosRecibidos.password, usuario.password_hash);
                if (esValida)
                {
                    return Ok(new
                    {
                        exito = true,
                        mensaje = "Login exitoso",
                        usuario = new
                        {
                            idUsuario = usuario.idUsuario,
                            nombre = usuario.usuario,
                            correo = usuario.correo,
                            rol = usuario.rol
                        }
                    });
                }
            }
            return BadRequest(new { exito = false, mensaje = "Correo o contraseña incorrectos" });
        }
        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] PeticionRegistro datosRecibidos)
        {
            return Ok(new { exito = true, mensaje = "¡Cuenta creada!" });
        }
        [HttpPost("recuperar")]
        public IActionResult Recuperar([FromBody] PeticionCorreo datosRecibidos)
        {
            return Ok(new { exito = true, mensaje = "Revisa tu correo" });
        }
        [HttpPost("validar-token")]
        public IActionResult ValidarToken([FromBody] PeticionNuevoPassword datosRecibidos)
        {
            return Ok(new { exito = true, mensaje = "Clave cambiada" });
        }
    }
}
