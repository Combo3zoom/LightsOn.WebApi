using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LightsOn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCustomerEntityAndRenameRestEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estimate_CategoryExpense_CategoryExpenseId",
                table: "Estimate");

            migrationBuilder.DropForeignKey(
                name: "FK_Estimate_Material_MaterialId",
                table: "Estimate");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_UnitMeasurement_UnitMeasurementId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPerformanceDescription_Client_ClientId",
                table: "WorkPerformanceDescription");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPerformanceDescription_Engine_EngineId",
                table: "WorkPerformanceDescription");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPerformanceDescription_PowerEquipment_PowerEquipmentId",
                table: "WorkPerformanceDescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkPerformanceDescription",
                table: "WorkPerformanceDescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitMeasurement",
                table: "UnitMeasurement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PowerEquipment",
                table: "PowerEquipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Material",
                table: "Material");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estimate",
                table: "Estimate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Engine",
                table: "Engine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client",
                table: "Client");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryExpense",
                table: "CategoryExpense");

            migrationBuilder.RenameTable(
                name: "WorkPerformanceDescription",
                newName: "WorkPerformanceDescriptions");

            migrationBuilder.RenameTable(
                name: "UnitMeasurement",
                newName: "UnitMeasurements");

            migrationBuilder.RenameTable(
                name: "PowerEquipment",
                newName: "PowerEquipments");

            migrationBuilder.RenameTable(
                name: "Material",
                newName: "Materials");

            migrationBuilder.RenameTable(
                name: "Estimate",
                newName: "Estimates");

            migrationBuilder.RenameTable(
                name: "Engine",
                newName: "Engines");

            migrationBuilder.RenameTable(
                name: "Client",
                newName: "Clients");

            migrationBuilder.RenameTable(
                name: "CategoryExpense",
                newName: "CategoryExpenses");

            migrationBuilder.RenameIndex(
                name: "IX_WorkPerformanceDescription_PowerEquipmentId",
                table: "WorkPerformanceDescriptions",
                newName: "IX_WorkPerformanceDescriptions_PowerEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkPerformanceDescription_EngineId",
                table: "WorkPerformanceDescriptions",
                newName: "IX_WorkPerformanceDescriptions_EngineId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkPerformanceDescription_ClientId",
                table: "WorkPerformanceDescriptions",
                newName: "IX_WorkPerformanceDescriptions_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Material_UnitMeasurementId",
                table: "Materials",
                newName: "IX_Materials_UnitMeasurementId");

            migrationBuilder.RenameIndex(
                name: "IX_Estimate_MaterialId",
                table: "Estimates",
                newName: "IX_Estimates_MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_Estimate_CategoryExpenseId",
                table: "Estimates",
                newName: "IX_Estimates_CategoryExpenseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkPerformanceDescriptions",
                table: "WorkPerformanceDescriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitMeasurements",
                table: "UnitMeasurements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PowerEquipments",
                table: "PowerEquipments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materials",
                table: "Materials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estimates",
                table: "Estimates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Engines",
                table: "Engines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryExpenses",
                table: "CategoryExpenses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Estimates_CategoryExpenses_CategoryExpenseId",
                table: "Estimates",
                column: "CategoryExpenseId",
                principalTable: "CategoryExpenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estimates_Materials_MaterialId",
                table: "Estimates",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_UnitMeasurements_UnitMeasurementId",
                table: "Materials",
                column: "UnitMeasurementId",
                principalTable: "UnitMeasurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPerformanceDescriptions_Clients_ClientId",
                table: "WorkPerformanceDescriptions",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPerformanceDescriptions_Engines_EngineId",
                table: "WorkPerformanceDescriptions",
                column: "EngineId",
                principalTable: "Engines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPerformanceDescriptions_PowerEquipments_PowerEquipmentId",
                table: "WorkPerformanceDescriptions",
                column: "PowerEquipmentId",
                principalTable: "PowerEquipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estimates_CategoryExpenses_CategoryExpenseId",
                table: "Estimates");

            migrationBuilder.DropForeignKey(
                name: "FK_Estimates_Materials_MaterialId",
                table: "Estimates");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_UnitMeasurements_UnitMeasurementId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPerformanceDescriptions_Clients_ClientId",
                table: "WorkPerformanceDescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPerformanceDescriptions_Engines_EngineId",
                table: "WorkPerformanceDescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPerformanceDescriptions_PowerEquipments_PowerEquipmentId",
                table: "WorkPerformanceDescriptions");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkPerformanceDescriptions",
                table: "WorkPerformanceDescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitMeasurements",
                table: "UnitMeasurements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PowerEquipments",
                table: "PowerEquipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materials",
                table: "Materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estimates",
                table: "Estimates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Engines",
                table: "Engines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryExpenses",
                table: "CategoryExpenses");

            migrationBuilder.RenameTable(
                name: "WorkPerformanceDescriptions",
                newName: "WorkPerformanceDescription");

            migrationBuilder.RenameTable(
                name: "UnitMeasurements",
                newName: "UnitMeasurement");

            migrationBuilder.RenameTable(
                name: "PowerEquipments",
                newName: "PowerEquipment");

            migrationBuilder.RenameTable(
                name: "Materials",
                newName: "Material");

            migrationBuilder.RenameTable(
                name: "Estimates",
                newName: "Estimate");

            migrationBuilder.RenameTable(
                name: "Engines",
                newName: "Engine");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Client");

            migrationBuilder.RenameTable(
                name: "CategoryExpenses",
                newName: "CategoryExpense");

            migrationBuilder.RenameIndex(
                name: "IX_WorkPerformanceDescriptions_PowerEquipmentId",
                table: "WorkPerformanceDescription",
                newName: "IX_WorkPerformanceDescription_PowerEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkPerformanceDescriptions_EngineId",
                table: "WorkPerformanceDescription",
                newName: "IX_WorkPerformanceDescription_EngineId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkPerformanceDescriptions_ClientId",
                table: "WorkPerformanceDescription",
                newName: "IX_WorkPerformanceDescription_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Materials_UnitMeasurementId",
                table: "Material",
                newName: "IX_Material_UnitMeasurementId");

            migrationBuilder.RenameIndex(
                name: "IX_Estimates_MaterialId",
                table: "Estimate",
                newName: "IX_Estimate_MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_Estimates_CategoryExpenseId",
                table: "Estimate",
                newName: "IX_Estimate_CategoryExpenseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkPerformanceDescription",
                table: "WorkPerformanceDescription",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitMeasurement",
                table: "UnitMeasurement",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PowerEquipment",
                table: "PowerEquipment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Material",
                table: "Material",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estimate",
                table: "Estimate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Engine",
                table: "Engine",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client",
                table: "Client",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryExpense",
                table: "CategoryExpense",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Estimate_CategoryExpense_CategoryExpenseId",
                table: "Estimate",
                column: "CategoryExpenseId",
                principalTable: "CategoryExpense",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estimate_Material_MaterialId",
                table: "Estimate",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_UnitMeasurement_UnitMeasurementId",
                table: "Material",
                column: "UnitMeasurementId",
                principalTable: "UnitMeasurement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPerformanceDescription_Client_ClientId",
                table: "WorkPerformanceDescription",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPerformanceDescription_Engine_EngineId",
                table: "WorkPerformanceDescription",
                column: "EngineId",
                principalTable: "Engine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPerformanceDescription_PowerEquipment_PowerEquipmentId",
                table: "WorkPerformanceDescription",
                column: "PowerEquipmentId",
                principalTable: "PowerEquipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
