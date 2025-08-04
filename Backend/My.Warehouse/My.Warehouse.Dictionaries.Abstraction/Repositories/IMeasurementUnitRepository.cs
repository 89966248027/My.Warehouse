using My.Warehouse.Dal.Enums;
using My.Warehouse.Dictionaries.Abstraction.Models.MeasurementUnits;

namespace My.Warehouse.Dictionaries.Abstraction.Repositories;

public interface IMeasurementUnitRepository
{
    Task Add(MeasurementUnitAddEditModel model);

    Task Update(MeasurementUnitAddEditModel model);

    Task<MeasurementUnitAddEditModel> GetById(Guid id);

    Task<IEnumerable<MeasurementUnitData>> GetAll(CommonStatus? status);

    Task<bool> CheckUnique(MeasurementUnitAddEditModel model);

    Task<IEnumerable<MeasurementUnitDictionaryItem>> GetDictionaryItems();
}
