using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RothschildHouse.Library.Common.NoSql.Documents;

namespace RothschildHouse.Library.Common.NoSql
{
    public class SaleService
    {
        private readonly IMongoCollection<SaleDocument> _salesCollection;

        public SaleService(IOptions<SaleServiceSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.Database);

            _salesCollection = database.GetCollection<SaleDocument>(settings.Value.CollectionName);
        }

        public async Task<List<SaleDocument>> GetSalesAsync(int year)
            => await _salesCollection.Find(item => item.TxnDateTime.Value.Year == year).ToListAsync();

        public async Task<List<SaleDocument>> GetSalesAsync(int year, int month)
            => await _salesCollection.Find(item => item.TxnDateTime.Value.Year == year && item.TxnDateTime.Value.Month == month).ToListAsync();

        public async Task AddSaleAsync(SaleDocument document)
            => await _salesCollection.InsertOneAsync(document);
    }
}
