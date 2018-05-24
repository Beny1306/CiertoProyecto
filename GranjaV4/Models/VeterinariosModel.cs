using System;
using System.ComponentModel.DataAnnotations;

namespace VeterinariosModel
{
    public class Veterinario
    {
        public string RFC { get; set; }
        public string nombre { get; set; }
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }
        public string especialidad { get; set; }
        public string telefono { get; set; }
    }
    public class crearVeterinario
    {
        [Required]
        public string RFC { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellidoP { get; set; }
        [Required]
        public string apellidoM { get; set; }
        [Required]
        public string especialidad { get; set; }
        [Required]
        public string telefono { get; set; }
    }
    public class editarVeterinario
    {
        [UIHint("Hidden")]
        public string RFC { get; set; }
        public string nombre { get; set; }
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }
        public string especialidad { get; set; }
        public string telefono { get; set; }
    }
    public class eliminarVeterinario
    {
        public string RFC { get; set; }
        public string nombre { get; set; }
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }
    }
}