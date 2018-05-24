using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositorios
{
    public interface IProveedoresRepository
    {
        Task<ProveedoresEntity> LeerPorId(string id);
        Task<List<ProveedoresEntity>> LeerTodos();
        Task<bool> Guardar(ProveedoresEntity nuevo);
        Task<bool> Actualizar(ProveedoresEntity actualizado);
        Task<bool> Borrar(string id);

    }
    public class ProveedoresEntity
    {
        public string RFC { get; set; }
        public string nombre { get; set; }
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }
        public string telefono { get; set; }
    }
}