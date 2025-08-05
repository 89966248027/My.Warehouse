using Microsoft.AspNetCore.Mvc;
using My.Warehouse.Dictionaries.Abstraction.Models.Clients;
using My.Warehouse.Dictionaries.Abstraction.Services;
using My.Warehouse.Documents.Abstraction.Models.Balance;
using My.Warehouse.Documents.Abstraction.Services;

namespace My.Warehouse.Web.Controllers;

[ApiController]
[Route("api/doc/balance")]
public class BalanceController : ControllerBase
{
    private readonly IBalanceService _service;

    public BalanceController(IBalanceService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<BalanceData>> GetAll(
        [FromQuery] IEnumerable<Guid> resourceIds,
        [FromQuery] IEnumerable<Guid> measurementUnitIds
    )
    {
        return await _service.GetAll(resourceIds, measurementUnitIds);
    }

    [HttpGet("funds-left")]
    public async Task<Dictionary<Guid, Dictionary<Guid, decimal>>> GetResourceFundsLeft()
    {
        return await _service.GetResourceFundsLeft();
    }

    [HttpGet("funds-left/without-document")]
    public async Task<
        Dictionary<Guid, Dictionary<Guid, decimal>>
    > GetResourceFundsLeftWithoutDocument([FromQuery] Guid? documentId)
    {
        return await _service.GetResourceFundsLeftWithoutDocument(documentId);
    }
}
