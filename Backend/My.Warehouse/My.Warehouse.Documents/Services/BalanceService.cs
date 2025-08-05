using My.Warehouse.Dal.Enums;
using My.Warehouse.Documents.Abstraction.Models.Arrival;
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

    public async Task RecalculateBalance()
    {
        IEnumerable<BalanceFundsLeft> recourses = await _repository.GetDocumentAmounts();

        Dictionary<Guid, Dictionary<Guid, decimal>> newBalances = recourses
            .GroupBy(x => x.ResourceId)
            .ToDictionary(
                x => x.Key,
                x =>
                    x.GroupBy(y => y.MeasurementUnitId)
                        .ToDictionary(
                            y => y.Key,
                            y =>
                                y.Sum(balance =>
                                {
                                    switch (balance.DocumentType)
                                    {
                                        case DocumentType.Arrival:
                                        {
                                            return balance.Amount;
                                        }
                                        case DocumentType.Shipment:
                                        {
                                            return -balance.Amount;
                                        }
                                        default:
                                            return 0;
                                    }
                                })
                        )
            );

        Dictionary<Guid, Dictionary<Guid, decimal>> oldBalances =
            await _repository.GetResourceFundsLeft();

        foreach (var newResource in newBalances)
        {
            Guid resourceId = newResource.Key;
            Dictionary<Guid, decimal> newMeasurementUnits = newResource.Value;

            oldBalances.TryGetValue(resourceId, out var oldMeasurementUnits);
            oldMeasurementUnits ??= new Dictionary<Guid, decimal>();

            foreach (var newMeasurementUnit in newMeasurementUnits)
            {
                Guid measurementUnitId = newMeasurementUnit.Key;
                decimal newAmount = newMeasurementUnit.Value;

                if (!oldMeasurementUnits.TryGetValue(measurementUnitId, out decimal oldAmount))
                {
                    await _repository.Add(
                        new BalanceAddEditModel()
                        {
                            ResourceId = resourceId,
                            MeasurementUnitId = measurementUnitId,
                            Amount = newAmount,
                        }
                    );
                }
                else if (oldAmount != newAmount)
                {
                    await _repository.Update(
                        new BalanceAddEditModel()
                        {
                            ResourceId = resourceId,
                            MeasurementUnitId = measurementUnitId,
                            Amount = newAmount,
                        }
                    );

                    oldMeasurementUnits.Remove(measurementUnitId);
                }
                else
                {
                    oldMeasurementUnits.Remove(measurementUnitId);
                }
            }

            foreach (var oldMeasurementUnitId in oldMeasurementUnits.Keys)
            {
                await _repository.Update(
                    new BalanceAddEditModel()
                    {
                        ResourceId = resourceId,
                        MeasurementUnitId = oldMeasurementUnitId,
                        Amount = 0,
                    }
                );
            }

            oldBalances.Remove(resourceId);
        }

        foreach (var oldResourceId in oldBalances.Keys)
        {
            foreach (var oldMeasurementUnitId in oldBalances[oldResourceId].Keys)
            {
                await _repository.Update(
                    new BalanceAddEditModel()
                    {
                        ResourceId = oldResourceId,
                        MeasurementUnitId = oldMeasurementUnitId,
                        Amount = 0,
                    }
                );
            }
        }
    }

    // public async Task AddResource(IEnumerable<ArrivalResourceAddEditModel> resources)
    // {
    //     foreach (var resource in resources)
    //     {
    //         var balance = new BalanceAddEditModel()
    //         {
    //             ResourceId = resource.ResourceId,
    //             MeasurementUnitId = resource.MeasurementUnitId,
    //             Amount = resource.Amount,
    //         };
    //
    //         await _repository.Save(balance);
    //     }
    // }

    public async Task<Dictionary<Guid, Dictionary<Guid, decimal>>> GetResourceFundsLeft()
    {
        return await _repository.GetResourceFundsLeft();
    }

    public async Task<
        Dictionary<Guid, Dictionary<Guid, decimal>>
    > GetResourceFundsLeftWithoutDocument(Guid? documentId)
    {
        Dictionary<Guid, Dictionary<Guid, decimal>> fundsLeft =
            await _repository.GetResourceFundsLeft();

        if (documentId.HasValue)
        {
            IEnumerable<BalanceFundsLeft> amounts = await GetDocumentAmounts(documentId.Value);

            foreach (var amount in amounts)
            {
                switch (amount.DocumentType)
                {
                    case DocumentType.Arrival:
                        {
                            fundsLeft[amount.ResourceId][amount.MeasurementUnitId] -= amount.Amount;
                        }

                        break;
                    case DocumentType.Shipment:
                        {
                            fundsLeft[amount.ResourceId][amount.MeasurementUnitId] += amount.Amount;
                        }

                        break;
                }
            }
        }

        return fundsLeft;
    }

    public async Task<IEnumerable<BalanceFundsLeft>> GetDocumentAmounts(Guid documentId)
    {
        return await _repository.GetDocumentAmounts(documentId);
    }
}
