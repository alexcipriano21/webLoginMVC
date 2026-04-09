using Microsoft.AspNetCore.Mvc;
using webLoginMVC.Controllers.dao;
using webLoginMVC.Models;
using BCrypt.Net;

namespace webLoginMVC.Controllers
    {
        public class LoginController : Controller
        {
            public readonly daoUsuario daoUsuario;
            public LoginController(IConfiguration config)
            {
                daoUsuario = new daoUsuario(config);
            }
            public IActionResult Index()
            {
                return View();
            }
            public IActionResult Login()
            {
                return View();
            }
            [HttpPost]
            public IActionResult Login(string correo,string password)
            {
                Usuario usuario = daoUsuario.getUsuarioLogin(correo);
                if (usuario != null && usuario.password_hash != null)
                {
                    bool esValida = BCrypt.Net.BCrypt.Verify(password, usuario.password_hash);
                    if (esValida) return RedirectToAction("Index");
                }
                ViewBag.Error = "Correo o contraseña incorrectos";
                return View();
            }
            public IActionResult Registrar()
            {
                return View();
            }
            [HttpPost]
            public IActionResult Registrar(string usuario,string correo,string password)
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
                daoUsuario.registrarUsuario(usuario, correo, passwordHash);
                TempData["MensajeRegistro"] = "¡Cuenta creada! Ya puedes iniciar sesión.";
                return RedirectToAction("Login");
        }
            public IActionResult Recuperar()
            {
                return View();
            }
            [HttpPost]
            public IActionResult Recuperar(string correo)
            {
                string token = Guid.NewGuid().ToString().Substring(0, 8);
                string expiracion = DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd HH:mm:ss");

                daoUsuario.generarTokenRecuperacion(correo, token, expiracion);

                ViewBag.TokenGenerado = token;
                ViewBag.CorreoUsuario = correo; 
                ViewBag.Mensaje = "Copia tu token de seguridad:";

                return View();
            }

            [HttpPost]
            public IActionResult ValidarToken(string correo, string tokenIngresado, string nuevaPassword)
            {
                string tokenRealDeLaBD = daoUsuario.obtenerToken(correo);

                if (tokenRealDeLaBD != null && tokenIngresado == tokenRealDeLaBD)
                {
                    string hash = BCrypt.Net.BCrypt.HashPassword(nuevaPassword);
                    daoUsuario.actualizarPassword(correo, hash);

                    TempData["MensajeRegistro"] = "Contraseña actualizada correctamente.";
                    return RedirectToAction("Login");
                }

                ViewBag.Error = "El token es incorrecto.";
                return View("Recuperar");
            }
        }
    }
