using Microsoft.AspNetCore.Mvc;
using AidMate.Models;

namespace AidMate.Controllers{
  [ApiController]
[Route("[controller]")]
public class ParamedicsController : ControllerBase
{
    private readonly IParamedicService _service;
    public ParamedicsController(IParamedicService service) => _service = service;

    [HttpGet]
    public async Task<List<ParamedicModel>> Get() => await _service.Get();

    [HttpGet("{id}")]
    public async Task<ParamedicModel?> GetById(string id) => await _service.GetById(id);

    [HttpPost]
    public async Task<List<ParamedicModel>> Add(ParamedicModel paramedic)
        => await _service.Add(paramedic);

    [HttpPut("{id}")]
    public async Task<List<ParamedicModel>> Update(string id, ParamedicModel paramedic)
        => await _service.Update(id, paramedic);

    [HttpDelete("{id}")]
    public async Task<List<ParamedicModel>> Delete(string id)
        => await _service.Delete(id);
}
}
