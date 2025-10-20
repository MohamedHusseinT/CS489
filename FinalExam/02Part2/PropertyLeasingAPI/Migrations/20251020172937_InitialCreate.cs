using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropertyLeasingAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PropertyRefName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyId);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.TenantId);
                });

            migrationBuilder.CreateTable(
                name: "Leases",
                columns: table => new
                {
                    LeaseId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LeaseNumber = table.Column<long>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    MonthlyRentalRate = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PropertyId = table.Column<int>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leases", x => x.LeaseId);
                    table.ForeignKey(
                        name: "FK_Leases_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leases_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "City", "PropertyRefName", "State" },
                values: new object[,]
                {
                    { 1, "Denver", "Kilo Drive Palace", "CO" },
                    { 2, "Dallas", "Galeria Court", "TX" },
                    { 3, "Houston", "Bells Tower", "TX" }
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "TenantId", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, null, "Bradley", "Koopa", "(480) 123-1355" },
                    { 2, "jwein@mail.com", "Julie", "Weinthrope", "(414) 998-0112" }
                });

            migrationBuilder.InsertData(
                table: "Leases",
                columns: new[] { "LeaseId", "EndDate", "LeaseNumber", "MonthlyRentalRate", "PropertyId", "StartDate", "TenantId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 5121543109L, 950.00m, 1, new DateTime(2023, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 2L, new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 7000511568L, 1075.99m, 1, new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3L, new DateTime(2023, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 6927458265L, 3945.50m, 2, new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leases_LeaseNumber",
                table: "Leases",
                column: "LeaseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leases_PropertyId",
                table: "Leases",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Leases_TenantId",
                table: "Leases",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leases");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
