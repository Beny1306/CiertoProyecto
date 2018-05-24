using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Repositorios
{
    public class AzureAnimalesRepository : IAnimalesRepository
    {
        private string conexion;

        public AzureAnimalesRepository(string azAccountConnection)
        {
            conexion = azAccountConnection;
        }
        private CloudTable ObtenerTablaAzure(){
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(conexion);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("Animales");

            // Create the table if it doesn't exist.
            //table.CreateIfNotExists();
            return table;
        }
        public async Task<bool> Actualizar(AnimalesEntity actualizado)
        {
            var table = ObtenerTablaAzure();
            // Create a retrieve operation that expects a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<AnimalesAzureEntity>(actualizado.Id.Substring(0,2), actualizado.Id);

            // Execute the operation.
            TableResult retrievedResult = await table.ExecuteAsync(retrieveOperation);

            // Assign the result to a CustomerEntity.
            var updateEntity = retrievedResult.Result as AnimalesAzureEntity;

            // Create the Delete TableOperation.
            if (updateEntity != null)
            {
                updateEntity.tipo = actualizado.tipo;
                updateEntity.nacimiento = actualizado.nacimiento;
                updateEntity.estatus = actualizado.estatus;
                updateEntity.corral = actualizado.corral;
                updateEntity.envioMatadero = actualizado.envioMatadero;

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
            TableOperation retrieveOperation = TableOperation.Retrieve<AnimalesAzureEntity>(id.Substring(0,2), id);

            // Execute the operation.
            TableResult retrievedResult = await table.ExecuteAsync(retrieveOperation);

            // Assign the result to a CustomerEntity.
            var deleteEntity = retrievedResult.Result as AnimalesAzureEntity;

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

        public async Task<bool> Guardar(AnimalesEntity nuevo)
        {
            var table = ObtenerTablaAzure();

            var azE = new AnimalesAzureEntity(nuevo.tipo, nuevo.Id);
            azE.nacimiento = nuevo.nacimiento;
            azE.estatus = nuevo.estatus;
            azE.corral = nuevo.corral;
            azE.envioMatadero = nuevo.envioMatadero;

            var insertOperation = TableOperation.Insert(azE);

            await table.ExecuteAsync(insertOperation);

            return true;
        }

        public async Task<AnimalesEntity> LeerPorId(string id)
        {
            var table = ObtenerTablaAzure();
            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<AnimalesAzureEntity>(id.Substring(0,2),id);
            
            // Execute the retrieve operation.
            TableResult retrievedResult = await table.ExecuteAsync(retrieveOperation);

            // Print the phone number of the result.
            if (retrievedResult.Result != null)
            {
                var r = retrievedResult.Result as AnimalesAzureEntity;
                return new AnimalesEntity(){
                    Id = r.Id,
                    tipo = r.tipo,
                    nacimiento = r.nacimiento,
                    estatus = r.estatus,
                    corral = r.corral,
                    envioMatadero = r.envioMatadero
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<List<AnimalesEntity>> LeerTodos()
        {
            var table = ObtenerTablaAzure();
            var tk = new TableContinuationToken();
            var query = new TableQuery<AnimalesAzureEntity>();
            var lista = new List<AnimalesEntity>();

            do{
                //Regresa un segmento de hasta 1000 entidades
                TableQuerySegment<AnimalesAzureEntity> tableQueryResutl = await
                table.ExecuteQuerySegmentedAsync(query, tk);

                //Asigna una nueva iteracio al token para continuar, si llega al final se agrega un null 
                tk = tableQueryResutl.ContinuationToken;
                lista.AddRange(tableQueryResutl.Results.Select( az => new AnimalesEntity(){
                        Id = az.Id,
                        tipo = az.tipo,
                        nacimiento = az.nacimiento,
                        estatus = az.estatus,
                        corral = az.corral,
                        envioMatadero = az.envioMatadero
                    })
                );

                //ciclo hasta que llega el caracter null
            }while(tk != null);
            return lista;
        }
    }
    public class AnimalesAzureEntity : TableEntity
    {
        public AnimalesAzureEntity(string tipo, string Id)
        {
            this.PartitionKey = Id.Substring(0, 2);
            this.RowKey = Id;
            this.tipo = tipo;
        }
        public AnimalesAzureEntity() { }

        public string Id { get{
            return RowKey;
            }
            set{
                RowKey = value; 
                }
        }
        public string tipo { get; set; }
        public string nacimiento { get; set; }
        public string estatus { get; set; }
        public int corral { get; set; }
        public string envioMatadero { get; set; }
    }
}