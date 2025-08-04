using My.Warehouse.Documents.Abstraction.Models.Balance;

namespace My.Warehouse.Documents.Abstraction.Services;

public interface IBalanceService
{
    Task<IEnumerable<BalanceData>> GetAll(
        IEnumerable<Guid> resourceIds,
        IEnumerable<Guid> measurementUnitIds
    );
}
