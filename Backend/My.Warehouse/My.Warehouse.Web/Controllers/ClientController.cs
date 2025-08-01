using Microsoft.AspNetCore.Mvc;
using My.Warehouse.Dictionaries.Abstraction.Models.Clients;
using My.Warehouse.Dictionaries.Abstraction.Services;

namespace My.Warehouse.Web.Controllers;

[ApiController]
[Route("api/dict/client")]
public class ClientController : ControllerBase
{
    private readonly IClientService _service;

    public ClientController(IClientService service)
    {
        _service = service;
    }

    [HttpGet("active")]
    public async Task<IEnumerable<ClientData>> GetActive()
    {
        return await _service.GetAllActive();
    }

    [HttpGet("archived")]
    public async Task<IEnumerable<ClientData>> GetArchived()
    {
        return await _service.GetAllArchived();
    }

    [HttpGet("{id}")]
    public async Task<ClientAddEditModel> Get([FromRoute] Guid id)
    {
        return await _service.Get(id);
    }

    [HttpPost("check-unique")]
    public async Task<bool> CheckUnique([FromBody] ClientAddEditModel model)
    {
        return await _service.CheckUnique(model);
    }

    [HttpPost]
    public async Task Save([FromBody] ClientAddEditModel model)
    {
        await _service.Save(model);
    }

    [HttpDelete("delete/{id}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await _service.Delete(id);
    }

    [HttpDelete("archive/{id}")]
    public async Task Archive([FromRoute] Guid id)
    {
        await _service.Archive(id);
    }

    [HttpDelete("active/{id}")]
    public async Task Active([FromRoute] Guid id)
    {
        await _service.Active(id);
    }
}
