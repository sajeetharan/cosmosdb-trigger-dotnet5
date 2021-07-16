using CosmosTriggerSample.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosTriggerSample.Services
{
    public interface ICosmosDbService
    {
        object ProcessDocuments(IReadOnlyList<Profile> documents);
    }

    public class CosmosDbService : ICosmosDbService
    {
        public CosmosDbService()
        {

        }

        public object ProcessDocuments(IReadOnlyList<Profile> documents)
        {
            return documents.Select(x => new
            {
                id = x.Id,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Age = x.Age + 10,
                Company = x.Company
            });
        }
    }
}
