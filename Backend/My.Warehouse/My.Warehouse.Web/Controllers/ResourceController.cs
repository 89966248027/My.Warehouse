using Microsoft.AspNetCore.Mvc;
using My.Warehouse.Dictionaries.Abstraction.Models.Resources;
using My.Warehouse.Dictionaries.Abstraction.Services;

namespace My.Warehouse.Web.Controllers;

[ApiController]
[Route("api/dict/resource")]
public class ResourceController : ControllerBase
{
    private readonly IResourceService _service;

    public ResourceController(IResourceService service)
    {
        _service = service;
    }

    [HttpGet("active")]
    public async Task<IEnumerable<ResourceData>> GetActive()
    {
        return await _service.GetAllActive();
    }

    [HttpGet("archived")]
    public async Task<IEnumerable<ResourceData>> GetArchived()
    {
        return await _service.GetAllArchived();
    }

    [HttpGet("{id}")]
    public async Task<ResourceAddEditModel> Get([FromRoute] Guid id)
    {
        return await _service.Get(id);
    }

    [HttpPost("check-unique")]
    public async Task<bool> CheckUnique([FromBody] ResourceAddEditModel model)
    {
        return await _service.CheckUnique(model);
    }

    [HttpPost]
    public async Task Save([FromBody] ResourceAddEditModel model)
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
