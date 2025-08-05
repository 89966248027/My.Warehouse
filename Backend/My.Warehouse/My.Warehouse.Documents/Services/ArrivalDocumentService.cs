using My.Warehouse.Dal.Enums;
using My.Warehouse.Documents.Abstraction.Models.Arrival;
using My.Warehouse.Documents.Abstraction.Repositories;
using My.Warehouse.Documents.Abstraction.Services;

namespace My.Warehouse.Documents.Services;

internal sealed class ArrivalDocumentService : IArrivalDocumentService
{
    private readonly IArrivalDocumentRepository _repository;

    private readonly IBalanceService _balanceService;

    public ArrivalDocumentService(
        IArrivalDocumentRepository repository,
        IBalanceService balanceService
    )
    {
        _repository = repository;
        _balanceService = balanceService;
    }

    public async Task Save(ArrivalDocumentAddEditModel model)
    {
        if (model.Id == null)
        {
            var id = Guid.NewGuid();

            await _repository.Add(model with { Id = id, Status = DocumentStatus.Created });
        }
        else
        {
            await _repository.Update(model with { Status = DocumentStatus.Created });
        }

        await _balanceService.RecalculateBalance();
    }

    public async Task<ArrivalDocumentAddEditModel> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task Delete(Guid id)
    {
        ArrivalDocumentAddEditModel model = await _repository.GetById(id);

        await _repository.Update(model with { Resources = [], Status = DocumentStatus.Deleted });

        await _balanceService.RecalculateBalance();
    }

    public async Task<bool> CheckUnique(ArrivalDocumentAddEditModel model)
    {
        return await _repository.CheckUnique(model);
    }

    public async Task<IEnumerable<ArrivalDocumentData>> GetAll(
        DateOnly? from,
        DateOnly? to,
        int? number,
        IEnumerable<Guid> resourceIds,
        IEnumerable<Guid> measurementUnitIds
    )
    {
        return await _repository.GetAll(from, to, number, resourceIds, measurementUnitIds);
    }
}
