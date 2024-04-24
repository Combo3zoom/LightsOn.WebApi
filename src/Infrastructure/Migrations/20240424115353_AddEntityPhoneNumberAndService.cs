using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LightsOn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityPhoneNumberAndService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescribeProblem",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CompanyPhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPhoneNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LowerPriceLimit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDescriptions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyPhoneNumbers");

            migrationBuilder.DropTable(
                name: "ServiceDescriptions");

            migrationBuilder.DropColumn(
                name: "DescribeProblem",
                table: "Customers");
        }
    }
}
