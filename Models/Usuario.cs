using System.Data;

namespace webLoginMVC.Models
{
    public class Usuario
    {
        public int idUsuario {  get; set; }
        public string usuario { get; set; }
        public string correo { get; set; }
        public string password_hash { get; set; }
        public string rol { get; set; }
        public Usuario() { }
        public Usuario(DataRow dr)
        {
            if (dr != null)
            {
                idUsuario = Convert.ToInt32(dr["id"]);
                usuario = dr["usuario"].ToString();
                correo = dr["correo"].ToString();
                password_hash = dr["password_hash"].ToString();
                rol = dr["rol"].ToString();
            }
        }
    }
}
