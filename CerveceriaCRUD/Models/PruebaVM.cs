namespace CerveceriaCRUD.Models
{
    public class PruebaVM
    {
        public PruebaVM() {
            Mensaje = "";
        }

        public PruebaVM(string mensaje)
        {
            Mensaje = mensaje;
        }

        public string Mensaje { get; set; }
    }
}
