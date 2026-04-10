namespace webLoginMVC.Models
{
    public class Peticion
    {
        public string correo { get; set; }
        public string password { get; set; }
    }
    public class PeticionRegistro
    {
        public string usuario { get; set; }
        public string correo { get; set; }
        public string password { get; set; }
    }
    public class PeticionCorreo
    {
        public string correo { get; set; }
    }
    public class PeticionNuevoPassword
    {
        public string correo { get; set; }
        public string tokenIngresado { get; set; }
        public string nuevaPassword { get; set; }
    }
}
