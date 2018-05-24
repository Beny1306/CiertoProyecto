using System;
using System.ComponentModel.DataAnnotations;

namespace ProveedoresModel
{
    public class Proveedor
    {
        public string RFC { get; set; }
        public string nombre { get; set; }
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }
        public string telefono { get; set; }
    }
    public class crearProveedor
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
        public string telefono { get; set; }
    }
    public class editarProveedor
    {
        [UIHint("Hidden")]
        public string RFC { get; set; }
        public string nombre { get; set; }
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }
        public string telefono { get; set; }
    }
    public class eliminarProveedor
    {
        public string RFC { get; set; }
        public string nombre { get; set; }
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }
    }
}