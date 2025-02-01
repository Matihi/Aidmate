using AidMate.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
//
namespace AidMate.Services;
public interface IParamedicService
{
    Task<List<ParamedicModel>> Get(string? name, string? qualification);
    Task<ParamedicModel?> GetById(string id);
    Task Add(ParamedicModel newParamedic);
    Task Update(string id, ParamedicModel updatedParamedic);
    Task Delete(string id);
}
