using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PontoSavi.src.PontoSavi.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    TradeName = table.Column<string>(type: "TEXT", nullable: false),
                    CNPJ = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DaysOff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysOff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DaysOff_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkShifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CheckIn = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    CheckInToleranceMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    CheckOut = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    CheckOutToleranceMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkShifts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ManagerId = table.Column<int>(type: "INTEGER", nullable: true),
                    CheckIn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CheckInStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    CheckInDescription = table.Column<string>(type: "TEXT", nullable: true),
                    CheckOut = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CheckOutAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CheckOutStatus = table.Column<int>(type: "INTEGER", nullable: true),
                    CheckOutDescription = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Points_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Points_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Points_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyWorkShifts",
                columns: table => new
                {
                    WorkShiftId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyWorkShifts", x => x.WorkShiftId);
                    table.ForeignKey(
                        name: "FK_CompanyWorkShifts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyWorkShifts_WorkShifts_WorkShiftId",
                        column: x => x.WorkShiftId,
                        principalTable: "WorkShifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWorkShifts",
                columns: table => new
                {
                    WorkShiftId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkShifts", x => x.WorkShiftId);
                    table.ForeignKey(
                        name: "FK_UserWorkShifts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWorkShifts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWorkShifts_WorkShifts_WorkShiftId",
                        column: x => x.WorkShiftId,
                        principalTable: "WorkShifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CNPJ", "CreatedAt", "Name", "TradeName" },
                values: new object[] { 1, "00000000000000", new DateTime(2024, 6, 19, 8, 2, 17, 444, DateTimeKind.Local).AddTicks(8185), "Ponto Savi", "Ponto Savi" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CompanyId", "ConcurrencyStamp", "CreatedAt", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, 1, "98b72587-a923-4b32-a5cd-fa07a18154fa", new DateTime(2024, 6, 19, 8, 2, 17, 694, DateTimeKind.Local).AddTicks(4483), "Desenvolvedor", "DESENVOLVEDOR" },
                    { 2, 1, "96872095-d959-441b-a6e0-8c93c1896b8a", new DateTime(2024, 6, 19, 8, 2, 17, 694, DateTimeKind.Local).AddTicks(4504), "CEO", "CEO" },
                    { 3, 1, "b32214e9-52fb-459a-aca8-25672cc0e87e", new DateTime(2024, 6, 19, 8, 2, 17, 694, DateTimeKind.Local).AddTicks(4509), "Administrador", "ADMINISTRADOR" },
                    { 4, 1, "f9fae724-5480-40aa-b8ba-35522033e666", new DateTime(2024, 6, 19, 8, 2, 17, 694, DateTimeKind.Local).AddTicks(4521), "Supervisor", "SUPERVISOR" },
                    { 5, 1, "5b82c443-bc5e-48fe-a101-b29c99a7265b", new DateTime(2024, 6, 19, 8, 2, 17, 694, DateTimeKind.Local).AddTicks(4525), "Colaborador", "COLABORADOR" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "CompanyId", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, 1, "81850961-8faa-4bbb-9f6e-f71906c62da7", new DateTime(2024, 6, 19, 8, 2, 17, 507, DateTimeKind.Local).AddTicks(1084), "dev@gmail.com", false, false, null, "Developer", "DEV@GMAIL.COM", "DEV", "AQAAAAIAAYagAAAAEPEj3NJD2yXaMq2ffUe/kE84aGccujBsXwiUPYdSbqESCQjlxkzXe1uxi4F0T1N0tg==", "(55) 85 9 9999-9999", false, "993dd97d-9d78-466f-8717-1a70fb26fcd1", false, "dev" },
                    { 2, 0, 1, "6fbcc103-607d-4e08-9187-a4e10811c2e4", new DateTime(2024, 6, 19, 8, 2, 17, 568, DateTimeKind.Local).AddTicks(9488), "admin@gmail.com", false, false, null, "Administrator", "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEDi1Kcbx6YH2mu2ydjw0jXjN+bPXvG8fsVdJ/kiFM4COJ9TkhU5Zg75rd6f2ZofJng==", "(55) 85 9 9999-9998", false, "274d3613-b0f3-451a-a085-bec0d0971acf", false, "admin" },
                    { 3, 0, 1, "0b0a7b55-6ab9-4111-88a1-40ec849ea6eb", new DateTime(2024, 6, 19, 8, 2, 17, 631, DateTimeKind.Local).AddTicks(2805), "super@gmail.com", false, false, null, "Supervisor", "SUPER@GMAIL.COM", "SUPER", "AQAAAAIAAYagAAAAEFaCaOT1/FAQFwXtO+fD36S4Y8rL2joLLVfDuoVJDuXk3hTGZ4rLLYWgjt+fh4oMxw==", "(55) 85 9 9999-9997", false, "d826436f-0b90-4ba8-b8b4-4f5246fc4985", false, "super" },
                    { 4, 0, 1, "cc8a88b4-35b4-4101-afd6-b41989a31368", new DateTime(2024, 6, 19, 8, 2, 17, 693, DateTimeKind.Local).AddTicks(2388), "base@gmail.com", false, false, null, "Base", "BASE@GMAIL.COM", "BASE", "AQAAAAIAAYagAAAAEEBylCq+cmebCjuw1hmsOsb3kICnDLOSsY+pHDLnbhLUbdCEMSunQ6LLwwklSpioxQ==", "(55) 85 9 9999-9997", false, "bde2c58b-61c0-4d18-aaee-2f6d5f3c0819", false, "base" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "CompanyId", "RoleId", "UserId", "CreatedAt" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2024, 6, 19, 8, 2, 17, 700, DateTimeKind.Local).AddTicks(8598) },
                    { 1, 2, 1, new DateTime(2024, 6, 19, 8, 2, 17, 700, DateTimeKind.Local).AddTicks(8608) },
                    { 1, 3, 1, new DateTime(2024, 6, 19, 8, 2, 17, 700, DateTimeKind.Local).AddTicks(8610) },
                    { 1, 4, 1, new DateTime(2024, 6, 19, 8, 2, 17, 700, DateTimeKind.Local).AddTicks(8612) },
                    { 1, 5, 1, new DateTime(2024, 6, 19, 8, 2, 17, 700, DateTimeKind.Local).AddTicks(8613) },
                    { 1, 3, 2, new DateTime(2024, 6, 19, 8, 2, 17, 700, DateTimeKind.Local).AddTicks(8615) },
                    { 1, 4, 3, new DateTime(2024, 6, 19, 8, 2, 17, 700, DateTimeKind.Local).AddTicks(8616) },
                    { 1, 5, 4, new DateTime(2024, 6, 19, 8, 2, 17, 700, DateTimeKind.Local).AddTicks(8618) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWorkShifts_CompanyId",
                table: "CompanyWorkShifts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWorkShifts_WorkShiftId_CompanyId",
                table: "CompanyWorkShifts",
                columns: new[] { "WorkShiftId", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DaysOff_CompanyId",
                table: "DaysOff",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DaysOff_Date_CompanyId",
                table: "DaysOff",
                columns: new[] { "Date", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Points_CompanyId",
                table: "Points",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_ManagerId",
                table: "Points",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_UserId",
                table: "Points",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CompanyId",
                table: "Roles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name_CompanyId",
                table: "Roles",
                columns: new[] { "Name", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_CompanyId",
                table: "UserRoles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkShifts_CompanyId",
                table: "UserWorkShifts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkShifts_UserId_WorkShiftId",
                table: "UserWorkShifts",
                columns: new[] { "UserId", "WorkShiftId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkShifts_CompanyId",
                table: "WorkShifts",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyWorkShifts");

            migrationBuilder.DropTable(
                name: "DaysOff");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserWorkShifts");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkShifts");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
