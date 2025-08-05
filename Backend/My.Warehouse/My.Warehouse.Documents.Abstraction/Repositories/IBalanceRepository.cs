using My.Warehouse.Documents.Abstraction.Models.Balance;

namespace My.Warehouse.Documents.Abstraction.Repositories;

public interface IBalanceRepository
{
    Task<IEnumerable<BalanceData>> GetAll(
        IEnumerable<Guid> resourceIds,
        IEnumerable<Guid> measurementUnitIds
    );

    Task Add(BalanceAddEditModel balance);

    Task Update(BalanceAddEditModel balance);

    Task<Dictionary<Guid, Dictionary<Guid, decimal>>> GetResourceFundsLeft();

    Task<IEnumerable<BalanceFundsLeft>> GetDocumentAmounts(Guid documentId);

    Task<IEnumerable<BalanceFundsLeft>> GetDocumentAmounts();
}
