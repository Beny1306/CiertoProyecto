using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Repositorios
{
    public class AzureEmpleadosRepository : IEmpleadosRepository
    {
        private string conexion;

        public AzureEmpleadosRepository(string azAccountConnection)
        {
            conexion = azAccountConnection;
        }
        private CloudTable ObtenerTablaAzure(){
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(conexion);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("Empleados");

            // Create the table if it doesn't exist.
            //table.CreateIfNotExists();
            return table;
        }
        public async Task<bool> Actualizar(EmpleadosEntity actualizado)
        {
            var table = ObtenerTablaAzure();
            // Create a retrieve operation that expects a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<EmpleadosAzureEntity>(actualizado.RFC.Substring(0,2), actualizado.RFC);

            // Execute the operation.
            TableResult retrievedResult = await table.ExecuteAsync(retrieveOperation);

            // Assign the result to a CustomerEntity.
            var updateEntity = retrievedResult.Result as EmpleadosAzureEntity;

            // Create the Delete TableOperation.
            if (updateEntity != null)
            {
                updateEntity.nombre = actualizado.nombre;
                updateEntity.apellidoP = actualizado.apellidoP;
                updateEntity.apellidoM = actualizado.apellidoM;
                updateEntity.nacimiento = actualizado.nacimiento;
                updateEntity.tipo = actualizado.tipo;
                updateEntity.telefono = actualizado.telefono;
                updateEntity.sueldo = actualizado.sueldo;
                updateEntity.horario = actualizado.horario;

                TableOperation updateOperation = TableOperation.Replace(updateEntity);

                // Execute the operation.
                await table.ExecuteAsync(updateOperation);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Borrar(string id)
        {
            var table = ObtenerTablaAzure();
            // Create a retrieve operation that expects a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<EmpleadosAzureEntity>(id.Substring(0,2), id);

            // Execute the operation.
            TableResult retrievedResult = await table.ExecuteAsync(retrieveOperation);

            // Assign the result to a CustomerEntity.
            var deleteEntity = retrievedResult.Result as EmpleadosAzureEntity;

            // Create the Delete TableOperation.
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);

                // Execute the operation.
                await table.ExecuteAsync(deleteOperation);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Guardar(EmpleadosEntity nuevo)
        {
            var table = ObtenerTablaAzure();

            var azE = new EmpleadosAzureEntity(nuevo.nombre, nuevo.RFC);
            azE.apellidoP = nuevo.apellidoP;
            azE.apellidoM = nuevo.apellidoM;
            azE.nacimiento = nuevo.nacimiento;
            azE.tipo = nuevo.tipo;
            azE.telefono = nuevo.telefono;
            azE.sueldo = nuevo.sueldo;
            azE.horario = nuevo.horario;

            var insertOperation = TableOperation.Insert(azE);

            await table.ExecuteAsync(insertOperation);

            return true;
        }

        public async Task<EmpleadosEntity> LeerPorId(string id)
        {
            var table = ObtenerTablaAzure();
            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<EmpleadosAzureEntity>(id.Substring(0,2),id);
            
            // Execute the retrieve operation.
            TableResult retrievedResult = await table.ExecuteAsync(retrieveOperation);

            // Print the phone number of the result.
            if (retrievedResult.Result != null)
            {
                var r = retrievedResult.Result as EmpleadosAzureEntity;
                return new EmpleadosEntity(){
                    RFC = r.RFC,
                    nombre = r.nombre,
                    apellidoP = r.apellidoP,
                    apellidoM = r.apellidoM,
                    nacimiento = r.nacimiento,
                    tipo = r.tipo,
                    telefono = r.telefono,
                    sueldo = r.sueldo,
                    horario = r.horario
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<List<EmpleadosEntity>> LeerTodos()
        {
            var table = ObtenerTablaAzure();
            var tk = new TableContinuationToken();
            var query = new TableQuery<EmpleadosAzureEntity>();
            var lista = new List<EmpleadosEntity>();

            do{
                //Regresa un segmento de hasta 1000 entidades
                TableQuerySegment<EmpleadosAzureEntity> tableQueryResutl = await
                table.ExecuteQuerySegmentedAsync(query, tk);

                //Asigna una nueva iteracio al token para continuar, si llega al final se agrega un null 
                tk = tableQueryResutl.ContinuationToken;
                lista.AddRange(tableQueryResutl.Results.Select( az => new EmpleadosEntity(){
                        RFC = az.RFC,
                        nombre = az.nombre,
                        apellidoP = az.apellidoP,
                        apellidoM = az.apellidoM,
                        nacimiento = az.nacimiento,
                        tipo = az.tipo,
                        telefono = az.telefono,
                        sueldo = az.sueldo,
                        horario = az.horario
                    })
                );

                //ciclo hasta que llega el caracter null
            }while(tk != null);
            return lista;
        }
    }
    public class EmpleadosAzureEntity : TableEntity
    {
        public EmpleadosAzureEntity(string nombre, string RFC)
        {
            this.PartitionKey = RFC.Substring(0, 2);
            this.RowKey = RFC;
            this.nombre = nombre;
        }
        public EmpleadosAzureEntity() { }

        public string RFC { get{
            return RowKey;
            }
            set{
                RowKey = value; 
                }
        }
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