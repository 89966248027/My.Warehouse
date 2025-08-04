using My.Warehouse.Documents.Abstraction.Models.Balance;

namespace My.Warehouse.Documents.Abstraction.Repositories;

public interface IBalanceRepository
{
    Task<IEnumerable<BalanceData>> GetAll(
        IEnumerable<Guid> resourceIds,
        IEnumerable<Guid> measurementUnitIds
    );
}
