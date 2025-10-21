using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ADSDentalSurgeriesWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthenticationEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Street = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    State = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    ZipCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Country = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Dentists",
                columns: table => new
                {
                    DentistId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DentistNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Specialization = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    DateOfEmployment = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dentists", x => x.DentistId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MailingAddress = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Surgeries",
                columns: table => new
                {
                    SurgeryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SurgeryNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surgeries", x => x.SurgeryId);
                    table.ForeignKey(
                        name: "FK_Surgeries_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppointmentNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AppointmentTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    DentistId = table.Column<int>(type: "INTEGER", nullable: false),
                    SurgeryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Dentists_DentistId",
                        column: x => x.DentistId,
                        principalTable: "Dentists",
                        principalColumn: "DentistId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Surgeries_SurgeryId",
                        column: x => x.SurgeryId,
                        principalTable: "Surgeries",
                        principalColumn: "SurgeryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "City", "Country", "CreatedDate", "State", "Street", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Phoenix", "USA", new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3060), "AZ", "123 Main Street", "85001" },
                    { 2, "Phoenix", "USA", new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3190), "AZ", "456 Oak Avenue", "85002" },
                    { 3, "Phoenix", "USA", new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3200), "AZ", "789 Pine Road", "85003" },
                    { 4, "Phoenix", "USA", new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3200), "AZ", "321 Elm Street", "85004" },
                    { 5, "Phoenix", "USA", new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3230), "AZ", "654 Maple Drive", "85005" }
                });

            migrationBuilder.InsertData(
                table: "Dentists",
                columns: new[] { "DentistId", "CreatedDate", "DateOfEmployment", "DentistNumber", "Email", "FirstName", "IsActive", "LastName", "PhoneNumber", "Specialization" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3590), null, "D001", "tony.smith@ads.com", "Tony", true, "Smith", "(602) 555-0201", "General Dentistry" },
                    { 2, new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3600), null, "D002", "helen.pearson@ads.com", "Helen", true, "Pearson", "(602) 555-0202", "Orthodontics" },
                    { 3, new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3610), null, "D003", "robin.plevin@ads.com", "Robin", true, "Plevin", "(602) 555-0203", "Oral Surgery" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedDate", "Description", "RoleName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 21, 10, 25, 19, 236, DateTimeKind.Local).AddTicks(4530), "Administrator with full access", "ADMIN" },
                    { 2, new DateTime(2025, 10, 21, 10, 25, 19, 236, DateTimeKind.Local).AddTicks(4590), "Dentist with access to patient records and appointments", "DENTIST" },
                    { 3, new DateTime(2025, 10, 21, 10, 25, 19, 236, DateTimeKind.Local).AddTicks(4600), "Receptionist with access to appointments and basic patient info", "RECEPTIONIST" },
                    { 4, new DateTime(2025, 10, 21, 10, 25, 19, 236, DateTimeKind.Local).AddTicks(4610), "Regular user with limited access", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedDate", "Email", "FirstName", "IsActive", "LastLoginDate", "LastName", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 21, 10, 25, 19, 236, DateTimeKind.Local).AddTicks(4700), "admin@ads.com", "System", true, null, "Administrator", "$2a$11$2iV7/o35ytmkZDdeJ5/.0OoUZu9MqZcw8vtD6T2mtCtHjiklzPmpO", "admin" },
                    { 2, new DateTime(2025, 10, 21, 10, 25, 19, 466, DateTimeKind.Local).AddTicks(5180), "tony.smith@ads.com", "Tony", true, null, "Smith", "$2a$11$Y6kc6GXpP8ATmFcDDwip6.gM/d1Llzb.AcjM1COYMP8n94VowV/z2", "tony.smith" },
                    { 3, new DateTime(2025, 10, 21, 10, 25, 19, 701, DateTimeKind.Local).AddTicks(1270), "helen.pearson@ads.com", "Helen", true, null, "Pearson", "$2a$11$c2tevIpf.zV0TmG8rdrjq.r1IiW97nEl.vvcYXkQ5CJ8pxRZX7ujm", "helen.pearson" },
                    { 4, new DateTime(2025, 10, 21, 10, 25, 19, 941, DateTimeKind.Local).AddTicks(4670), "receptionist@ads.com", "Office", true, null, "Receptionist", "$2a$11$kzIoeU5wv1zVvb96hf9hmebDAxGEVbb6eSy0QwRS1iUFlwNQiFchm", "receptionist" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "AddressId", "CreatedDate", "DateOfBirth", "Email", "FirstName", "LastName", "MailingAddress", "PatientNumber", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3660), null, "gillian.white@email.com", "Gillian", "White", null, "P100", "(602) 555-0301" },
                    { 2, 5, new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3670), null, "jill.bell@email.com", "Jill", "Bell", null, "P105", "(602) 555-0302" },
                    { 3, 4, new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3820), null, "ian.mackay@email.com", "Ian", "MacKay", null, "P108", "(602) 555-0303" },
                    { 4, 5, new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3830), null, "john.walker@email.com", "John", "Walker", null, "P110", "(602) 555-0304" }
                });

            migrationBuilder.InsertData(
                table: "Surgeries",
                columns: new[] { "SurgeryId", "AddressId", "CreatedDate", "Email", "Name", "PhoneNumber", "SurgeryNumber" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3490), null, "Phoenix Central Dental", "(602) 555-0101", "S10" },
                    { 2, 2, new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3520), null, "Phoenix North Dental", "(602) 555-0102", "S13" },
                    { 3, 3, new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3530), null, "Phoenix South Dental", "(602) 555-0103", "S15" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRoleId", "AssignedDate", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 21, 10, 25, 20, 206, DateTimeKind.Local).AddTicks(1350), 1, 1 },
                    { 2, new DateTime(2025, 10, 21, 10, 25, 20, 206, DateTimeKind.Local).AddTicks(1370), 2, 2 },
                    { 3, new DateTime(2025, 10, 21, 10, 25, 20, 206, DateTimeKind.Local).AddTicks(1380), 2, 3 },
                    { 4, new DateTime(2025, 10, 21, 10, 25, 20, 206, DateTimeKind.Local).AddTicks(1430), 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "AppointmentDate", "AppointmentNumber", "AppointmentTime", "CreatedDate", "DentistId", "Notes", "PatientId", "Status", "SurgeryId", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2013, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "A001", new TimeSpan(0, 10, 0, 0, 0), new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3900), 1, null, 1, "Scheduled", 3, null },
                    { 2, new DateTime(2013, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "A002", new TimeSpan(0, 12, 0, 0, 0), new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3910), 1, null, 2, "Scheduled", 3, null },
                    { 3, new DateTime(2013, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "A003", new TimeSpan(0, 10, 0, 0, 0), new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3920), 2, null, 3, "Scheduled", 1, null },
                    { 4, new DateTime(2013, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A004", new TimeSpan(0, 14, 0, 0, 0), new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3930), 2, null, 3, "Scheduled", 1, null },
                    { 5, new DateTime(2013, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "A005", new TimeSpan(0, 16, 30, 0, 0), new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3990), 3, null, 2, "Scheduled", 3, null },
                    { 6, new DateTime(2013, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "A006", new TimeSpan(0, 18, 0, 0, 0), new DateTime(2025, 10, 21, 10, 25, 19, 235, DateTimeKind.Local).AddTicks(3990), 3, null, 4, "Scheduled", 2, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentNumber",
                table: "Appointments",
                column: "AppointmentNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DentistId",
                table: "Appointments",
                column: "DentistId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SurgeryId",
                table: "Appointments",
                column: "SurgeryId");

            migrationBuilder.CreateIndex(
                name: "IX_Dentists_DentistNumber",
                table: "Dentists",
                column: "DentistNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AddressId",
                table: "Patients",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientNumber",
                table: "Patients",
                column: "PatientNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleName",
                table: "Roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Surgeries_AddressId",
                table: "Surgeries",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Surgeries_SurgeryNumber",
                table: "Surgeries",
                column: "SurgeryNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Dentists");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Surgeries");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
