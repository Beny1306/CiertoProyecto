using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Repositorios
{
    public class AzureVeterinariosRepository : IVeterinariosRepository
    {
        private string conexion;

        public AzureVeterinariosRepository(string azAccountConnection)
        {
            conexion = azAccountConnection;
        }
        private CloudTable ObtenerTablaAzure(){
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(conexion);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("Veterinarios");

            // Create the table if it doesn't exist.
            //table.CreateIfNotExists();
            return table;
        }
        public async Task<bool> Actualizar(VeterinariosEntity actualizado)
        {
            var table = ObtenerTablaAzure();
            // Create a retrieve operation that expects a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<VeterinariosAzureEntity>(actualizado.RFC.Substring(0,2), actualizado.RFC);

            // Execute the operation.
            TableResult retrievedResult = await table.ExecuteAsync(retrieveOperation);

            // Assign the result to a CustomerEntity.
            var updateEntity = retrievedResult.Result as VeterinariosAzureEntity;

            // Create the Delete TableOperation.
            if (updateEntity != null)
            {
                updateEntity.nombre = actualizado.nombre;
                updateEntity.apellidoP = actualizado.apellidoP;
                updateEntity.apellidoM = actualizado.apellidoM;
                updateEntity.especialidad = actualizado.especialidad;
                updateEntity.telefono = actualizado.telefono;

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
            TableOperation retrieveOperation = TableOperation.Retrieve<VeterinariosAzureEntity>(id.Substring(0,2), id);

            // Execute the operation.
            TableResult retrievedResult = await table.ExecuteAsync(retrieveOperation);

            // Assign the result to a CustomerEntity.
            var deleteEntity = retrievedResult.Result as VeterinariosAzureEntity;

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

        public async Task<bool> Guardar(VeterinariosEntity nuevo)
        {
            var table = ObtenerTablaAzure();

            var azE = new VeterinariosAzureEntity(nuevo.nombre, nuevo.RFC);
            azE.apellidoP = nuevo.apellidoP;
            azE.apellidoM = nuevo.apellidoM;
            azE.especialidad = nuevo.especialidad;
            azE.telefono = nuevo.telefono;

            var insertOperation = TableOperation.Insert(azE);

            await table.ExecuteAsync(insertOperation);

            return true;
        }

        public async Task<VeterinariosEntity> LeerPorId(string id)
        {
            var table = ObtenerTablaAzure();
            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<VeterinariosAzureEntity>(id.Substring(0,2),id);
            
            // Execute the retrieve operation.
            TableResult retrievedResult = await table.ExecuteAsync(retrieveOperation);

            // Print the phone number of the result.
            if (retrievedResult.Result != null)
            {
                var r = retrievedResult.Result as VeterinariosAzureEntity;
                return new VeterinariosEntity(){
                    RFC = r.RFC,
                    nombre = r.nombre,
                    apellidoP = r.apellidoP,
                    apellidoM = r.apellidoM,
                    especialidad = r.especialidad,
                    telefono = r.telefono
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<List<VeterinariosEntity>> LeerTodos()
        {
            var table = ObtenerTablaAzure();
            var tk = new TableContinuationToken();
            var query = new TableQuery<VeterinariosAzureEntity>();
            var lista = new List<VeterinariosEntity>();

            do{
                //Regresa un segmento de hasta 1000 entidades
                TableQuerySegment<VeterinariosAzureEntity> tableQueryResutl = await
                table.ExecuteQuerySegmentedAsync(query, tk);

                //Asigna una nueva iteracio al token para continuar, si llega al final se agrega un null 
                tk = tableQueryResutl.ContinuationToken;
                lista.AddRange(tableQueryResutl.Results.Select( az => new VeterinariosEntity(){
                        RFC = az.RFC,
                        nombre = az.nombre,
                        apellidoP = az.apellidoP,
                        apellidoM = az.apellidoM,
                        especialidad = az.especialidad,
                        telefono = az.telefono
                    })
                );

                //ciclo hasta que llega el caracter null
            }while(tk != null);
            return lista;
        }
    }
    public class VeterinariosAzureEntity : TableEntity
    {
        public VeterinariosAzureEntity(string nombre, string RFC)
        {
            this.PartitionKey = RFC.Substring(0, 2);
            this.RowKey = RFC;
            this.nombre = nombre;
        }
        public VeterinariosAzureEntity() { }

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
        public string especialidad { get; set; }
        public string telefono { get; set; }
    }
}