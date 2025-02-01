using AidMate.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
//
namespace AidMate.Services;
public class PatientService : IPatientService
{
    private readonly IMongoCollection<PatientModel> _patientCollection;

    public PatientService(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
        var database = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
        _patientCollection = database.GetCollection<PatientModel>(config["MongoDbSettings:Collections:Patients"]);
    }

    public async Task<List<PatientModel>> Get(string? name, bool? isCritical)
    {
        var filterBuilder = Builders<PatientModel>.Filter;
        var filters = new List<FilterDefinition<PatientModel>>();

        if (!string.IsNullOrEmpty(name))
            filters.Add(filterBuilder.Regex(p => p.Name, new BsonRegularExpression(name, "i")));

        if (isCritical.HasValue)
            filters.Add(filterBuilder.Eq(p => p.IsCritical, isCritical.Value));

        var filter = filters.Count > 0 ? filterBuilder.And(filters) : filterBuilder.Empty;
        return await _patientCollection.Find(filter).ToListAsync();
    }

    public async Task<PatientModel?> GetById(string id) =>
        await _patientCollection.Find(p => p.Id == id).FirstOrDefaultAsync();

    public async Task Add(PatientModel newPatient)
    {
        newPatient.Id = Guid.NewGuid().ToString();
        await _patientCollection.InsertOneAsync(newPatient);
    }

    public async Task Update(string id, PatientModel updatedPatient) =>
        await _patientCollection.ReplaceOneAsync(p => p.Id == id, updatedPatient);

    public async Task Delete(string id) =>
        await _patientCollection.DeleteOneAsync(p => p.Id == id);
}
