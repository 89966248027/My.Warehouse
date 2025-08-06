using My.Warehouse.Dal.Enums;
using My.Warehouse.Documents.Abstraction.Models.Shipment;
using My.Warehouse.Documents.Abstraction.Repositories;
using My.Warehouse.Documents.Abstraction.Services;

namespace My.Warehouse.Documents.Services;

internal sealed class ShipmentDocumentService : IShipmentDocumentService
{
    private readonly IShipmentDocumentRepository _repository;

    private readonly IBalanceService _balanceService;

    public ShipmentDocumentService(
        IShipmentDocumentRepository repository,
        IBalanceService balanceService
    )
    {
        _repository = repository;
        _balanceService = balanceService;
    }

    public async Task Save(ShipmentDocumentAddEditModel model)
    {
        if (model.Id == null)
        {
            var id = Guid.NewGuid();

            await _repository.Add(model with { Id = id });
        }
        else
        {
            await _repository.Update(model);
        }

        if (model.Status == DocumentStatus.Signed)
        {
            await _balanceService.RecalculateBalance();
        }
    }

    public async Task<ShipmentDocumentAddEditModel> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task Delete(Guid id)
    {
        ShipmentDocumentAddEditModel model = await _repository.GetById(id);

        await _repository.Update(model with { Resources = [], Status = DocumentStatus.Deleted });

        await _balanceService.RecalculateBalance();
    }

    public async Task Revoke(Guid id)
    {
        ShipmentDocumentAddEditModel model = await _repository.GetById(id);

        await _repository.Update(model with { Status = DocumentStatus.Created });

        await _balanceService.RecalculateBalance();
    }

    public async Task<bool> CheckUnique(ShipmentDocumentAddEditModel model)
    {
        return await _repository.CheckUnique(model);
    }

    public async Task<IEnumerable<ShipmentDocumentData>> GetAll(
        DateOnly? from,
        DateOnly? to,
        IEnumerable<Guid> clientIds,
        int? number,
        IEnumerable<Guid> resourceIds,
        IEnumerable<Guid> measurementUnitIds
    )
    {
        return await _repository.GetAll(
            from,
            to,
            clientIds,
            number,
            resourceIds,
            measurementUnitIds
        );
    }
}
