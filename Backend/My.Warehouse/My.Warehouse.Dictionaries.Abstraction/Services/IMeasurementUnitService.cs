using My.Warehouse.Dictionaries.Abstraction.Models.MeasurementUnits;

namespace My.Warehouse.Dictionaries.Abstraction.Services;

public interface IMeasurementUnitService
{
    Task Save(MeasurementUnitAddEditModel model);

    Task<MeasurementUnitAddEditModel> Get(Guid id);

    Task Delete(Guid id);

    Task Archive(Guid id);

    Task Active(Guid id);

    Task<IEnumerable<MeasurementUnitData>> GetAllActive();

    Task<IEnumerable<MeasurementUnitData>> GetAllArchived();

    Task<bool> CheckUnique(MeasurementUnitAddEditModel model);
}
