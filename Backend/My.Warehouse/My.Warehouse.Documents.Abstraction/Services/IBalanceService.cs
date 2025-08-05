using My.Warehouse.Documents.Abstraction.Models.Arrival;
using My.Warehouse.Documents.Abstraction.Models.Balance;

namespace My.Warehouse.Documents.Abstraction.Services;

public interface IBalanceService
{
    Task<IEnumerable<BalanceData>> GetAll(
        IEnumerable<Guid> resourceIds,
        IEnumerable<Guid> measurementUnitIds
    );

    Task RecalculateBalance();

    Task<Dictionary<Guid, Dictionary<Guid, decimal>>> GetResourceFundsLeft();

    Task<Dictionary<Guid, Dictionary<Guid, decimal>>> GetResourceFundsLeftWithoutDocument(
        Guid? documentId
    );

    Task<IEnumerable<BalanceFundsLeft>> GetDocumentAmounts(Guid documentId);
}
