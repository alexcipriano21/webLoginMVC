using webLoginMVC.Controllers.bd;
using webLoginMVC.Models;

namespace webLoginMVC.Controllers.dao
{
    public class daoUsuario
    {
            clsBD clsBD;

        public daoUsuario(IConfiguration config)
        {
            clsBD = new clsBD(config, "cnLogin");
        }

        internal Usuario getUsuarioLogin(string correo)
        {
            clsBD.Sentencia("sp_getUsuarioLogin '" + correo + "'");
            return new Usuario(clsBD.getRegistro());
        }
        internal void registrarUsuario(string usuario,string correo,string hash)
        {
            clsBD.Sentencia("sp_registrarUsuario '" +usuario + "','" +correo + "','" +hash + "'");
            clsBD.getDataTable();
        }
        internal void generarTokenRecuperacion(string correo,string token,string expiracion)
        {
            clsBD.Sentencia("sp_generarTokenRecuperacion '" +correo + "','" +token + "','" +expiracion + "'");
            clsBD.getDataTable();
        }
        internal void actualizarPassword(string correo, string nuevoHash)
        {
            clsBD.Sentencia("sp_actualizarPassword '" + correo + "','" + nuevoHash + "'");
            clsBD.getDataTable();
        }
    }
}