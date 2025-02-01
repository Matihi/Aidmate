using AidMate.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AidMate.Services;
public interface IPatientService
{
    Task<List<PatientModel>> Get(string? name, bool? isCritical);
    Task<PatientModel?> GetById(string id);
    Task Add(PatientModel newPatient);
    Task Update(string id, PatientModel updatedPatient);
    Task Delete(string id);
}
