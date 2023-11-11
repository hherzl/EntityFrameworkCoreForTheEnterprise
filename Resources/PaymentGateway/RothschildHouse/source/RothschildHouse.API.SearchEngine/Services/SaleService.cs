using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RothschildHouse.API.SearchEngine.Services.Documents;

namespace RothschildHouse.API.SearchEngine.Services;

public class SaleService
{
    private readonly IMongoCollection<SaleDocument> _salesCollection;

    public SaleService(IOptions<SaleServiceSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.Database);

        _salesCollection = database.GetCollection<SaleDocument>(settings.Value.CollectionName);
    }

    public async Task<SaleDocument> GetByTxnIdAsync(long? txnId)
        => await _salesCollection.Find(item => item.TxnId == txnId).FirstOrDefaultAsync();

    public async Task AddSaleAsync(SaleDocument document)
        => await _salesCollection.InsertOneAsync(document);

    public async Task UpdateAsync(long? txnId, SaleDocument document)
        => await _salesCollection.ReplaceOneAsync(item => item.TxnId == txnId, document);

    public async Task<List<SaleDocument>> GetSalesAsync(int year)
        => await _salesCollection.Find(item => item.TxnDateTime.Value.Year == year).ToListAsync();

    public async Task<List<SaleDocument>> GetSalesAsync(int year, int month)
        => await _salesCollection.Find(item => item.TxnDateTime.Value.Year == year && item.TxnDateTime.Value.Month == month).ToListAsync();
}
