using System;
using System.ComponentModel.DataAnnotations;

namespace AnimalesModels
{
    public class Animal
    {
        public string Id { get; set; }
        public string tipo { get; set; }
        public string nacimiento { get; set; }
        public string estatus { get; set; }
        public int corral { get; set; }
        public string envioMatadero { get; set; }
    }
    public class crearAnimal
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string tipo { get; set; }
        [Required]
        public string nacimiento { get; set; }
        [Required]
        public string estatus { get; set; }
        [Required]
        public int corral { get; set; }
        public string envioMatadero { get; set; }
    }
    public class editarAnimal
    {
        [UIHint("Hidden")]
        public string Id { get; set; }
        public string tipo { get; set; }
        public string nacimiento { get; set; }
        public string estatus { get; set; }
        public int corral { get; set; }
        public string envioMatadero { get; set; }
    }
    public class eliminarAnimal
    {
         public string Id { get; set; }
    }

}