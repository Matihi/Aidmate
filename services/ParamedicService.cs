using AidMate.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
//
namespace AidMate.Services;
public class ParamedicService : IParamedicService
{
    private readonly IMongoCollection<ParamedicModel> _paramedicCollection;

    public ParamedicService(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
        var database = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
        _paramedicCollection = database.GetCollection<ParamedicModel>(config["MongoDbSettings:Collections:Paramedics"]);
    }

    public async Task<List<ParamedicModel>> Get(string? name, string? qualification)
    {
        var filterBuilder = Builders<ParamedicModel>.Filter;
        var filters = new List<FilterDefinition<ParamedicModel>>();

        if (!string.IsNullOrEmpty(name))
            filters.Add(filterBuilder.Regex(p => p.Name, new BsonRegularExpression(name, "i")));

        if (!string.IsNullOrEmpty(qualification))
            filters.Add(filterBuilder.Eq(p => p.Qualification, qualification));

        var filter = filters.Count > 0 ? filterBuilder.And(filters) : filterBuilder.Empty;
        return await _paramedicCollection.Find(filter).ToListAsync();
    }

    public async Task<ParamedicModel?> GetById(string id) =>
        await _paramedicCollection.Find(p => p.Id == id).FirstOrDefaultAsync();

    public async Task Add(ParamedicModel newParamedic)
    {
        newParamedic.Id = Guid.NewGuid().ToString();
        await _paramedicCollection.InsertOneAsync(newParamedic);
    }

    public async Task Update(string id, ParamedicModel updatedParamedic) =>
        await _paramedicCollection.ReplaceOneAsync(p => p.Id == id, updatedParamedic);

    public async Task Delete(string id) =>
        await _paramedicCollection.DeleteOneAsync(p => p.Id == id);
}