using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RothschildHouse.Application.Core.Services.Models;
using Microsoft.Extensions.Configuration;

namespace RothschildHouse.Application.Core.Services
{
    public class ReportsService
    {
        private readonly IMongoCollection<SaleDocument> _salesCollection;

        public ReportsService() //IOptions<ReportsSettings> settings)
        {
            //options.ConnectionString = ;
            //options.Database = ;
            //options.CollectionName = ;

            //var client = new MongoClient(settings.Value.ConnectionString);
            //var database = client.GetDatabase(settings.Value.Database);

            //_salesCollection = database.GetCollection<SaleDocument>(settings.Value.CollectionName);

            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Reports");

            _salesCollection = database.GetCollection<SaleDocument>("Sales");
        }

        public async Task<List<SaleDocument>> GetSalesAsync()
            => await _salesCollection.Find(_ => true).ToListAsync();

        public async Task AddSaleAsync(SaleDocument document)
            => await _salesCollection.InsertOneAsync(document);
    }
}
