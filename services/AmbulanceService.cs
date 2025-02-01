using AidMate.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace AidMate.Services;
public class AmbulanceService : IAmbulanceService
{
    private readonly IMongoCollection<AmbulanceModel> _ambulanceCollection;

    public AmbulanceService(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
        var database = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
        _ambulanceCollection = database.GetCollection<AmbulanceModel>(config["MongoDbSettings:Collections:Ambulances"]);
    }

    public async Task<List<AmbulanceModel>> Get(string? type, bool? isAvailable)
    {
        var filterBuilder = Builders<AmbulanceModel>.Filter;
        var filters = new List<FilterDefinition<AmbulanceModel>>();

        if (!string.IsNullOrEmpty(type))
            filters.Add(filterBuilder.Eq(a => a.Type, type));

        if (isAvailable.HasValue)
            filters.Add(filterBuilder.Eq(a => a.IsAvailable, isAvailable.Value));

        var filter = filters.Count > 0 ? filterBuilder.And(filters) : filterBuilder.Empty;
        return await _ambulanceCollection.Find(filter).ToListAsync();
    }

    public async Task<AmbulanceModel?> GetById(string id) =>
        await _ambulanceCollection.Find(a => a.Id == id).FirstOrDefaultAsync();

    public async Task Add(AmbulanceModel newAmbulance)
    {
        newAmbulance.Id = Guid.NewGuid().ToString();
        await _ambulanceCollection.InsertOneAsync(newAmbulance);
    }

    public async Task Update(string id, AmbulanceModel updatedAmbulance) =>
        await _ambulanceCollection.ReplaceOneAsync(a => a.Id == id, updatedAmbulance);

    public async Task Delete(string id) =>
        await _ambulanceCollection.DeleteOneAsync(a => a.Id == id);
}
