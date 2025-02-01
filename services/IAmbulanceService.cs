using AidMate.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AidMate.Services;
public interface IAmbulanceService
{
    Task<List<AmbulanceModel>> Get(string? type, bool? isAvailable);
    Task<AmbulanceModel?> GetById(string id);
    Task Add(AmbulanceModel newAmbulance);
    Task Update(string id, AmbulanceModel updatedAmbulance);
    Task Delete(string id);
}
