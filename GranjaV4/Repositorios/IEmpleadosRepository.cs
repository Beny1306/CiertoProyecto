using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositorios
{
    public interface IEmpleadosRepository
    {
        Task<EmpleadosEntity> LeerPorId(string id);
        Task<List<EmpleadosEntity>> LeerTodos();
        Task<bool> Guardar(EmpleadosEntity nuevo);
        Task<bool> Actualizar(EmpleadosEntity actualizado);
        Task<bool> Borrar(string id);

    }
    public class EmpleadosEntity
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
}