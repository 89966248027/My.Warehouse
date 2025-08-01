using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My.Warehouse.Dal.Migrations.Application
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "doc");

            migrationBuilder.EnsureSchema(
                name: "dict");

            migrationBuilder.CreateTable(
                name: "ArrivalDocument",
                schema: "doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    DocumentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArrivalDocument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                schema: "dict",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementUnit",
                schema: "dict",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                schema: "dict",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentDocument",
                schema: "doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    DocumentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentDocument_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "dict",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArrivalResource",
                schema: "doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ArrivalDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArrivalResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArrivalResource_ArrivalDocument_ArrivalDocumentId",
                        column: x => x.ArrivalDocumentId,
                        principalSchema: "doc",
                        principalTable: "ArrivalDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArrivalResource_MeasurementUnit_MeasurementUnitId",
                        column: x => x.MeasurementUnitId,
                        principalSchema: "dict",
                        principalTable: "MeasurementUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArrivalResource_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalSchema: "dict",
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Balance",
                schema: "dict",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Balance_MeasurementUnit_MeasurementUnitId",
                        column: x => x.MeasurementUnitId,
                        principalSchema: "dict",
                        principalTable: "MeasurementUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Balance_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalSchema: "dict",
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentResource",
                schema: "doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShipmentDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentResource_MeasurementUnit_MeasurementUnitId",
                        column: x => x.MeasurementUnitId,
                        principalSchema: "dict",
                        principalTable: "MeasurementUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipmentResource_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalSchema: "dict",
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipmentResource_ShipmentDocument_ShipmentDocumentId",
                        column: x => x.ShipmentDocumentId,
                        principalSchema: "doc",
                        principalTable: "ShipmentDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArrivalResource_ArrivalDocumentId",
                schema: "doc",
                table: "ArrivalResource",
                column: "ArrivalDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ArrivalResource_MeasurementUnitId",
                schema: "doc",
                table: "ArrivalResource",
                column: "MeasurementUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ArrivalResource_ResourceId",
                schema: "doc",
                table: "ArrivalResource",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Balance_MeasurementUnitId",
                schema: "dict",
                table: "Balance",
                column: "MeasurementUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Balance_ResourceId",
                schema: "dict",
                table: "Balance",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentDocument_ClientId",
                schema: "doc",
                table: "ShipmentDocument",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentResource_MeasurementUnitId",
                schema: "doc",
                table: "ShipmentResource",
                column: "MeasurementUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentResource_ResourceId",
                schema: "doc",
                table: "ShipmentResource",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentResource_ShipmentDocumentId",
                schema: "doc",
                table: "ShipmentResource",
                column: "ShipmentDocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArrivalResource",
                schema: "doc");

            migrationBuilder.DropTable(
                name: "Balance",
                schema: "dict");

            migrationBuilder.DropTable(
                name: "ShipmentResource",
                schema: "doc");

            migrationBuilder.DropTable(
                name: "ArrivalDocument",
                schema: "doc");

            migrationBuilder.DropTable(
                name: "MeasurementUnit",
                schema: "dict");

            migrationBuilder.DropTable(
                name: "Resource",
                schema: "dict");

            migrationBuilder.DropTable(
                name: "ShipmentDocument",
                schema: "doc");

            migrationBuilder.DropTable(
                name: "Client",
                schema: "dict");
        }
    }
}
