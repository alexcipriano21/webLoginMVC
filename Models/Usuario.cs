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
        public Usuario(string[] aRegistro) 
        {
            if (aRegistro != null)
            {
                idUsuario = int.Parse(aRegistro[0]);
                usuario = aRegistro[1];
                correo = aRegistro[2];
                password_hash = aRegistro[3];
                rol = aRegistro[4];
            }
        }   
    }
}
