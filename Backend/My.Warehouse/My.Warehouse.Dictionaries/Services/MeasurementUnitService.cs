using My.Warehouse.Dal.Enums;
using My.Warehouse.Dictionaries.Abstraction.Models.MeasurementUnits;
using My.Warehouse.Dictionaries.Abstraction.Repositories;
using My.Warehouse.Dictionaries.Abstraction.Services;

namespace My.Warehouse.Dictionaries.Services;

internal sealed class MeasurementUnitService : IMeasurementUnitService
{
    private readonly IMeasurementUnitRepository _repository;

    public MeasurementUnitService(IMeasurementUnitRepository repository)
    {
        _repository = repository;
    }

    public async Task Save(MeasurementUnitAddEditModel model)
    {
        if (model.Id == null)
        {
            await _repository.Add(model with { Status = CommonStatus.Active });
        }
        else
        {
            await _repository.Update(model with { Status = CommonStatus.Active });
        }
    }

    public async Task<MeasurementUnitAddEditModel> Get(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task Delete(Guid id)
    {
        MeasurementUnitAddEditModel model = await _repository.GetById(id);

        await _repository.Update(model with { Status = CommonStatus.Deleted });
    }

    public async Task Archive(Guid id)
    {
        MeasurementUnitAddEditModel model = await _repository.GetById(id);

        await _repository.Update(model with { Status = CommonStatus.Archived });
    }

    public async Task Active(Guid id)
    {
        MeasurementUnitAddEditModel model = await _repository.GetById(id);

        await _repository.Update(model with { Status = CommonStatus.Active });
    }

    public async Task<IEnumerable<MeasurementUnitData>> GetAllActive()
    {
        return await _repository.GetAll(CommonStatus.Active);
    }

    public async Task<IEnumerable<MeasurementUnitData>> GetAllArchived()
    {
        return await _repository.GetAll(CommonStatus.Archived);
    }

    public async Task<bool> CheckUnique(MeasurementUnitAddEditModel model)
    {
        return await _repository.CheckUnique(model);
    }

    public async Task<IEnumerable<MeasurementUnitDictionaryItem>> GetDictionaryItems()
    {
        return await _repository.GetDictionaryItems();
    }
}
