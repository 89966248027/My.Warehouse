using My.Warehouse.Documents.Abstraction.Models.Balance;
using My.Warehouse.Documents.Abstraction.Repositories;
using My.Warehouse.Documents.Abstraction.Services;

namespace My.Warehouse.Documents.Services;

internal sealed class BalanceService : IBalanceService
{
    private readonly IBalanceRepository _repository;

    public BalanceService(IBalanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<BalanceData>> GetAll(
        IEnumerable<Guid> resourceIds,
        IEnumerable<Guid> measurementUnitIds
    )
    {
        return await _repository.GetAll(resourceIds, measurementUnitIds);
    }
}
