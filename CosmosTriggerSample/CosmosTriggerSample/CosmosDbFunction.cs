using CosmosTriggerSample.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace CosmosTriggerSample.Functions
{
    public class CosmosDbSample
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly ILogger<CosmosDbSample> _logger;

        public CosmosDbSample(ICosmosDbService cosmosDbService, ILogger<CosmosDbSample> logger)
        {
            _cosmosDbService = cosmosDbService;
            _logger = logger;
        }

        [Function(nameof(CosmosDbSample))]
        [CosmosDBOutput(databaseName: "functions-sample", collectionName: "ProfileOut", ConnectionStringSetting = "CosmosDbConnection")]
        public object Run([CosmosDBTrigger(
            databaseName: "functions-sample",
            collectionName: "Profile",
            ConnectionStringSetting = "CosmosDbConnection",
            LeaseCollectionName = "leases", CreateLeaseCollectionIfNotExists = true)] IReadOnlyList<Profile> input, FunctionContext context)
        {
            if (input != null && input.Any())
            {
                _logger.LogInformation("Item modified: " + input.Count);

                foreach (var document in input)
                {
                    _logger.LogInformation("Item Id: " + document.Id);
                }

                return _cosmosDbService.ProcessDocuments(input);
            }

            return null;
        }
    }

    public class Profile
    {
        public string Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public int Age { get; set; }

        public string Company { get; set; }

    }
}
