﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using My.Warehouse.Dal.Contexts;

#nullable disable

namespace My.Warehouse.Dal.Migrations.Application
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("My.Warehouse.Dal.Entities.Dictionaries.BalanceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("MeasurementUnitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ResourceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MeasurementUnitId");

                    b.HasIndex("ResourceId");

                    b.ToTable("Balance", "dict");
                });

            modelBuilder.Entity("My.Warehouse.Dal.Entities.Dictionaries.ClientEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Client", "dict");
                });

            modelBuilder.Entity("My.Warehouse.Dal.Entities.Dictionaries.MeasurementUnitEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MeasurementUnit", "dict");
                });

            modelBuilder.Entity("My.Warehouse.Dal.Entities.Dictionaries.ResourceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Resource", "dict");
                });

            modelBuilder.Entity("My.Warehouse.Dal.Entities.Documents.Arrival.ArrivalDocumentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("DocumentDate")
                        .HasColumnType("date");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ArrivalDocument", "doc");
                });

            modelBuilder.Entity("My.Warehouse.Dal.Entities.Documents.Arrival.ArrivalResourceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ArrivalDocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MeasurementUnitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ResourceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ArrivalDocumentId");

                    b.HasIndex("MeasurementUnitId");

                    b.HasIndex("ResourceId");

                    b.ToTable("ArrivalResource", "doc");
                });

            modelBuilder.Entity("My.Warehouse.Dal.Entities.Documents.Shipment.ShipmentDocumentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("DocumentDate")
                        .HasColumnType("date");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("ShipmentDocument", "doc");
                });

            modelBuilder.Entity("My.Warehouse.Dal.Entities.Documents.Shipment.ShipmentResourceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("MeasurementUnitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ResourceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShipmentDocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MeasurementUnitId");

                    b.HasIndex("ResourceId");

                    b.HasIndex("ShipmentDocumentId");

                    b.ToTable("ShipmentResource", "doc");
                });

            modelBuilder.Entity("My.Warehouse.Dal.Entities.Dictionaries.BalanceEntity", b =>
                {
                    b.HasOne("My.Warehouse.Dal.Entities.Dictionaries.MeasurementUnitEntity", "MeasurementUnit")
                        .WithMany("Balances")
                        .HasForeignKey("MeasurementUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("My.Warehouse.Dal.Entities.Dictionaries.ResourceEntity", "Resource")
                        .WithMany("Balances")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MeasurementUnit");

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("My.Warehouse.Dal.Entities.Documents.Arrival.ArrivalResourceEntity", b =>
                {
                    b.HasOne("My.Warehouse.Dal.Entities.Documents.Arrival.ArrivalDocumentEntity", "ArrivalDocument")
                        .WithMany("ArrivalResources")
                        .HasForeignKey("ArrivalDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("My.Warehouse.Dal.Entities.Dictionaries.MeasurementUnitEntity", "MeasurementUnit")
                        .WithMany("ArrivalResources")
                        .HasForeignKey("MeasurementUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("My.Warehouse.Dal.Entities.Dictionaries.ResourceEntity", "Resource")
                        .WithMany("ArrivalResources")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArrivalDocument");

                    b.Navigation("MeasurementUnit");

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("My.Warehouse.Dal.Entities.Documents.Shipment.ShipmentDocumentEntity", b =>
                {
                    b.HasOne("My.Warehouse.Dal.Entities.Dictionaries.ClientEntity", "Client")
                        .WithMany("ShipmentDocuments")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("My.Warehouse.Dal.Entities.Documents.Shipment.ShipmentResourceEntity", b =>
                {
                    b.HasOne("My.Warehouse.Dal.Entities.Dictionaries.MeasurementUnitEntity", "MeasurementUnit")
                        .WithMany("ShipmentResources")
                        .HasForeignKey("MeasurementUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("My.Warehouse.Dal.Entities.Dictionaries.ResourceEntity", "Resource")
                        .WithMany("ShipmentResources")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("My.Warehouse.Dal.Entities.Documents.Shipment.ShipmentDocumentEntity", "ShipmentDocument")
                        .WithMany("ShipmentResources")
                        .HasForeignKey("ShipmentDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MeasurementUnit");

                    b.Navigation("Resource");

                    b.Navigation("ShipmentDocument");
                });

            modelBuilder.Entity("My.Warehouse.Dal.Entities.Dictionaries.ClientEntity", b =>
                {
                    b.Navigation("ShipmentDocuments");
                });

            modelBuilder.Entity("My.Warehouse.Dal.Entities.Dictionaries.MeasurementUnitEntity", b =>
                {
                    b.Navigation("ArrivalResources");

                    b.Navigation("Balances");

                    b.Navigation("ShipmentResources");
                });

            modelBuilder.Entity("My.Warehouse.Dal.Entities.Dictionaries.ResourceEntity", b =>
                {
                    b.Navigation("ArrivalResources");

                    b.Navigation("Balances");

                    b.Navigation("ShipmentResources");
                });

            modelBuilder.Entity("My.Warehouse.Dal.Entities.Documents.Arrival.ArrivalDocumentEntity", b =>
                {
                    b.Navigation("ArrivalResources");
                });

            modelBuilder.Entity("My.Warehouse.Dal.Entities.Documents.Shipment.ShipmentDocumentEntity", b =>
                {
                    b.Navigation("ShipmentResources");
                });
#pragma warning restore 612, 618
        }
    }
}
