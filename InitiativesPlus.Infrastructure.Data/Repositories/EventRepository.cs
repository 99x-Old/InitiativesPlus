using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using InitiativesPlus.Domain.Interfaces;
using InitiativesPlus.Domain.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace InitiativesPlus.Infrastructure.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private IConfiguration _configuration;

        public EventRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // The Cosmos client instance
        private CosmosClient cosmosClient;

        // The database we will create
        private Database database;

        // The container we will create.
        private Container container;

        // The name of the database and container we will create
        private string databaseId = "Events";
        private string containerId = "Initiatives";

        public async Task<Event> CreateEventAsync(Event appEvent)
        {
            await GetStartedAsync();
            ItemResponse<Event> andersenFamilyResponse;
            try
            {
                // Read the item to see if it exists.  
                andersenFamilyResponse = await this.container.ReadItemAsync<Event>(appEvent.Id, new PartitionKey(appEvent.Month));
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"
                andersenFamilyResponse = await this.container.CreateItemAsync<Event>(appEvent, new PartitionKey(appEvent.Month));

                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
            }

            return andersenFamilyResponse;
        }

        public async Task<List<Event>> QueryEventsAsync()
        {
            var month = DateTime.Now.AddMonths(-1).ToString("MM-yyyy");
            await GetStartedAsync();
            var sqlQueryText = "SELECT * FROM c WHERE c.Month = '" + month + "'";

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Event> queryResultSetIterator = this.container.GetItemQueryIterator<Event>(queryDefinition);

            List<Event> events = new List<Event>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Event> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Event appEvent in currentResultSet)
                {
                    events.Add(appEvent);
                }
            }

            return events;
        }

        public async Task GetStartedAsync()
        {
            // Create a new instance of the Cosmos Client
            this.cosmosClient = new CosmosClient(
                _configuration.GetSection("CosmosSettings:AccountUri").Value, 
                _configuration.GetSection("CosmosSettings:AccountKey").Value, 
                new CosmosClientOptions() { ApplicationName = "initiativesplus" });

            await this.CreateDatabaseAsync();
            await this.CreateContainerAsync();
        }

        // <CreateDatabaseAsync>
        /// <summary>
        /// Create the database if it does not exist
        /// </summary>
        private async Task CreateDatabaseAsync()
        {
            // Create a new database
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        }
        // </CreateDatabaseAsync>

        // <CreateContainerAsync>
        /// <summary>
        /// Create the container if it does not exist. 
        /// Specify "/LastName" as the partition key since we're storing family information, to ensure good distribution of requests and storage.
        /// </summary>
        /// <returns></returns>
        private async Task CreateContainerAsync()
        {
            // Create a new container
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/Month", 400);
        }
        // </CreateContainerAsync>
    }
}
