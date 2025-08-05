using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using My.Warehouse.Documents.Abstraction.Models.Arrival;
using My.Warehouse.Documents.Abstraction.Services;

namespace My.Warehouse.Web.Controllers;

[ApiController]
[Route("api/doc/arrival-document")]
public class ArrivalDocumentController : ControllerBase
{
    private readonly IArrivalDocumentService _service;

    public ArrivalDocumentController(IArrivalDocumentService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<ArrivalDocumentAddEditModel> Get([FromRoute] Guid id)
    {
        return await _service.GetById(id);
    }

    [HttpGet]
    public async Task<IEnumerable<ArrivalDocumentData>> GetAll(
        [FromQuery] string? from,
        [FromQuery] string? to,
        [FromQuery] int? number,
        [FromQuery] IEnumerable<Guid> resourceIds,
        [FromQuery] IEnumerable<Guid> measurementUnitIds
    )
    {
        DateOnly? dateFromParsed =
            from == null
                ? null
                : DateOnly.ParseExact(from, "dd.MM.yyyy", CultureInfo.CurrentCulture);

        DateOnly? dateToParsed =
            to == null ? null : DateOnly.ParseExact(to, "dd.MM.yyyy", CultureInfo.CurrentCulture);

        return await _service.GetAll(
            dateFromParsed,
            dateToParsed,
            number,
            resourceIds,
            measurementUnitIds
        );
    }

    [HttpPost("check-unique")]
    public async Task<bool> CheckUnique([FromBody] ArrivalDocumentAddEditModel model)
    {
        return await _service.CheckUnique(model);
    }

    [HttpPost]
    public async Task Save([FromBody] ArrivalDocumentAddEditModel model)
    {
        await _service.Save(model);
    }

    [HttpDelete("delete/{id}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await _service.Delete(id);
    }
}
