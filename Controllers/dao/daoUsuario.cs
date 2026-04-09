using System.Data;
using webLoginMVC.Controllers.bd;
using webLoginMVC.Models;
using static System.Net.WebRequestMethods;

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
            DataRow dr = clsBD.getDataRow();
            if (dr != null) return new Usuario(dr);
            return null;
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
        internal string obtenerToken(string correo)
        {
            clsBD.Sentencia("sp_obtenerTokenRecuperacion '" + correo + "'");
            DataRow fila = clsBD.getDataRow();
            if (fila != null)
            {
                return fila["token_recuperacion"].ToString();
            }
            return null;
        }
        internal void actualizarPassword(string correo, string nuevoHash)
        {
            clsBD.Sentencia("sp_actualizarPassword '" + correo + "','" + nuevoHash + "'");
            clsBD.getDataTable();
        }
    }
}