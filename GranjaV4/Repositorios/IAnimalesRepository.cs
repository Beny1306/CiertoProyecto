using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositorios
{
    public interface IAnimalesRepository
    {
        Task<AnimalesEntity> LeerPorId(string id);
        Task<List<AnimalesEntity>> LeerTodos();
        Task<bool> Guardar(AnimalesEntity nuevo);
        Task<bool> Actualizar(AnimalesEntity actualizado);
        Task<bool> Borrar(string id);

    }
    public class AnimalesEntity
    {
        public string Id { get; set; }
        public string tipo { get; set; }
        public string nacimiento { get; set; }
        public string estatus { get; set; }
        public int corral { get; set; }
        public string envioMatadero { get; set; }
    }
}