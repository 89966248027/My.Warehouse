using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using My.Warehouse.Documents.Abstraction.Models.Shipment;
using My.Warehouse.Documents.Abstraction.Services;

namespace My.Warehouse.Web.Controllers;

[ApiController]
[Route("api/doc/shipment-document")]
public class ShipmentDocumentController : ControllerBase
{
    private readonly IShipmentDocumentService _service;

    public ShipmentDocumentController(IShipmentDocumentService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<ShipmentDocumentAddEditModel> Get([FromRoute] Guid id)
    {
        return await _service.GetById(id);
    }

    [HttpGet]
    public async Task<IEnumerable<ShipmentDocumentData>> GetAll(
        [FromQuery] string? from,
        [FromQuery] string? to,
        [FromQuery] IEnumerable<Guid> clientIds,
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
            clientIds,
            number,
            resourceIds,
            measurementUnitIds
        );
    }

    [HttpPost("check-unique")]
    public async Task<bool> CheckUnique([FromBody] ShipmentDocumentAddEditModel model)
    {
        return await _service.CheckUnique(model);
    }

    [HttpPost]
    public async Task Save([FromBody] ShipmentDocumentAddEditModel model)
    {
        await _service.Save(model);
    }

    [HttpDelete("delete/{id}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await _service.Delete(id);
    }

    [HttpDelete("revoke/{id}")]
    public async Task Revoke([FromRoute] Guid id)
    {
        await _service.Revoke(id);
    }
}
