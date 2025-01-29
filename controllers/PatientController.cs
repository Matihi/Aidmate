using Microsoft.AspNetCore.Mvc;
using AidMate.Models;

namespace AidMate.Controllers{
[ApiController]
[Route("[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _service;
    public PatientsController(IPatientService service) => _service = service;

    [HttpGet]
    public async Task<List<PatientModel>> Get() => await _service.Get();

    [HttpGet("{id}")]
    public async Task<PatientModel?> GetById(string id) => await _service.GetById(id);

    [HttpPost]
    public async Task<List<PatientModel>> Add(PatientModel patient)
        => await _service.Add(patient);

    [HttpPut("{id}")]
    public async Task<List<PatientModel>> Update(string id, PatientModel patient)
        => await _service.Update(id, patient);

    [HttpDelete("{id}")]
    public async Task<List<PatientModel>> Delete(string id)
        => await _service.Delete(id);
}
}

