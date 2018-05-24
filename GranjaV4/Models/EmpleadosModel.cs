using System;
using System.ComponentModel.DataAnnotations;

namespace EmpleadosModels
{
    public class Empleado
    {
        public string RFC { get; set; }
        public string nombre { get; set; }
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }
        public string nacimiento { get; set; }
        public string tipo { get; set; }
        public string telefono { get; set; }
        public string sueldo { get; set; }
        public string horario { get; set; }
    }
    public class crearEmpleado
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
        public string nacimiento { get; set; }
        [Required]
        public string tipo { get; set; }
        [Required]
        public string telefono { get; set; }
        [Required]
        public string sueldo { get; set; }
        [Required]
        public string horario { get; set; }
    }
    public class editarEmpleado
    {
        [UIHint("Hidden")]
        public string RFC { get; set; }
        public string nombre { get; set; }
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }
        public string nacimiento { get; set; }
        public string tipo { get; set; }
        public string telefono { get; set; }
        public string sueldo { get; set; }
        public string horario { get; set; }
    }
    public class eliminarEmpleado
    {
        public string RFC { get; set; }
        public string nombre { get; set; }
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }
    }
}