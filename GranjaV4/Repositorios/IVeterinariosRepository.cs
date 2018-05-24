using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositorios
{
    public interface IVeterinariosRepository
    {
        Task<VeterinariosEntity> LeerPorId(string id);
        Task<List<VeterinariosEntity>> LeerTodos();
        Task<bool> Guardar(VeterinariosEntity nuevo);
        Task<bool> Actualizar(VeterinariosEntity actualizado);
        Task<bool> Borrar(string id);

    }
    public class VeterinariosEntity
    {
        public string RFC { get; set; }
        public string nombre { get; set; }
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }
        public string especialidad { get; set; }
        public string telefono { get; set; }
    }
}