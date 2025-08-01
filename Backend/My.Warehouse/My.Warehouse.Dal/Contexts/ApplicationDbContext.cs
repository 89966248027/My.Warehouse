using Microsoft.EntityFrameworkCore;
using My.Warehouse.Dal.Entities.Dictionaries;
using My.Warehouse.Dal.Entities.Documents.Arrival;
using My.Warehouse.Dal.Entities.Documents.Shipment;

namespace My.Warehouse.Dal.Contexts;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<ResourceEntity> Resource { get; set; }

    public DbSet<MeasurementUnitEntity> MeasurementUnit { get; set; }

    public DbSet<ClientEntity> Client { get; set; }

    public DbSet<BalanceEntity> Balance { get; set; }

    public DbSet<ArrivalDocumentEntity> ArrivalDocument { get; set; }

    public DbSet<ArrivalResourceEntity> ArrivalResource { get; set; }

    public DbSet<ShipmentDocumentEntity> ShipmentDocument { get; set; }

    public DbSet<ShipmentResourceEntity> ShipmentResource { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<BalanceEntity>().HasOne(x => x.Resource).WithMany(x => x.Balances);

        builder
            .Entity<ArrivalResourceEntity>()
            .HasOne(x => x.Resource)
            .WithMany(x => x.ArrivalResources);

        builder
            .Entity<ShipmentResourceEntity>()
            .HasOne(x => x.Resource)
            .WithMany(x => x.ShipmentResources);

        builder.Entity<BalanceEntity>().HasOne(x => x.MeasurementUnit).WithMany(x => x.Balances);

        builder
            .Entity<ArrivalResourceEntity>()
            .HasOne(x => x.MeasurementUnit)
            .WithMany(x => x.ArrivalResources);

        builder
            .Entity<ShipmentResourceEntity>()
            .HasOne(x => x.MeasurementUnit)
            .WithMany(x => x.ShipmentResources);

        builder
            .Entity<ArrivalDocumentEntity>()
            .HasMany(x => x.ArrivalResources)
            .WithOne(x => x.ArrivalDocument);

        builder
            .Entity<ShipmentDocumentEntity>()
            .HasMany(x => x.ShipmentResources)
            .WithOne(x => x.ShipmentDocument);

        builder
            .Entity<ShipmentDocumentEntity>()
            .HasOne(x => x.Client)
            .WithMany(x => x.ShipmentDocuments);
    }
}
