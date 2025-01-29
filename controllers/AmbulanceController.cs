using AidMate.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aidmate.Controllers{
  [ApiController]
[Route("[controller]")]
public class AmbulancesController : ControllerBase
{
    private readonly IAmbulanceService _service;
    public AmbulancesController(IAmbulanceService service) => _service = service;

    [HttpGet]
    public async Task<List<AmbulanceModel>> Get() => await _service.Get();

    [HttpGet("{id}")]
    public async Task<AmbulanceModel?> GetById(string id) => await _service.GetById(id);

    [HttpPost]
    public async Task<List<AmbulanceModel>> Add(AmbulanceModel ambulance)
        => await _service.Add(ambulance);

    [HttpPut("{id}")]
    public async Task<List<AmbulanceModel>> Update(string id, AmbulanceModel ambulance)
        => await _service.Update(id, ambulance);

    [HttpDelete("{id}")]
    public async Task<List<AmbulanceModel>> Delete(string id)
        => await _service.Delete(id);
}
}