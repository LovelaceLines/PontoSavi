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
                values: new object[] { 1, "00000000000000", new DateTime(2024, 6, 18, 22, 50, 39, 716, DateTimeKind.Local).AddTicks(3102), "Ponto Savi", "Ponto Savi" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CompanyId", "ConcurrencyStamp", "CreatedAt", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, 1, "1bf10673-4d8c-41fc-b088-b9584900e56c", new DateTime(2024, 6, 18, 22, 50, 39, 957, DateTimeKind.Local).AddTicks(386), "Desenvolvedor", "DESENVOLVEDOR" },
                    { 2, 1, "04f43299-9ceb-45f2-8a31-aeca32aa6988", new DateTime(2024, 6, 18, 22, 50, 39, 957, DateTimeKind.Local).AddTicks(415), "Administrador", "ADMINISTRADOR" },
                    { 3, 1, "4563a3c8-ff91-47f8-a228-480de085c4f4", new DateTime(2024, 6, 18, 22, 50, 39, 957, DateTimeKind.Local).AddTicks(473), "Supervisor", "SUPERVISOR" },
                    { 4, 1, "b2faaf30-6e7d-44b0-9880-4d306dff7ac6", new DateTime(2024, 6, 18, 22, 50, 39, 957, DateTimeKind.Local).AddTicks(479), "Colaborador", "COLABORADOR" },
                    { 5, 1, "0be9713e-58fe-4e20-beb9-4d032e7ad14d", new DateTime(2024, 6, 18, 22, 50, 39, 957, DateTimeKind.Local).AddTicks(484), "CEO", "CEO" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "CompanyId", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, 1, "b2fa46bf-7d43-4725-9423-98c44a8fb9dc", new DateTime(2024, 6, 18, 22, 50, 39, 795, DateTimeKind.Local).AddTicks(6441), "dev@gmail.com", false, false, null, "Developer", "DEV@GMAIL.COM", "DEV", "AQAAAAIAAYagAAAAEF5XVDwXWELdalQagrzRjUqQVsULV1tgWYZRdTaqqfk8Hm2Lp+QvQWlDmBT11+X5BA==", "(55) 85 9 9999-9999", false, "3aa1e78c-c581-45e7-b512-11ccbbe574b8", false, "dev" },
                    { 2, 0, 1, "147acd0d-f27d-4cae-8aa9-278d03bd0810", new DateTime(2024, 6, 18, 22, 50, 39, 875, DateTimeKind.Local).AddTicks(5435), "admin@gmail.com", false, false, null, "Administrator", "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEAhma4TdEU4NCUuFdCqZUJb9oNtjCPeOXdC/amQO+r1e8uZw33m38VJ30UdMNdh+mg==", "(55) 85 9 9999-9998", false, "e3c68d64-6fd1-4982-bd28-f83de2360420", false, "admin" },
                    { 3, 0, 1, "e9c37d6e-c5c4-4e8b-a0db-2a15575fb430", new DateTime(2024, 6, 18, 22, 50, 39, 955, DateTimeKind.Local).AddTicks(6207), "super@gmail.com", false, false, null, "Supervisor", "SUPER@GMAIL.COM", "SUPER", "AQAAAAIAAYagAAAAEMSuJnxKwNBgnAtpFuUXoBfKlRPtM4XHi/STIRKZlMHU8zBVq5w9VRv4kSpZgCKH8w==", "(55) 85 9 9999-9997", false, "e88490cc-59c3-4b09-8f67-4e92edac2302", false, "super" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "CompanyId", "RoleId", "UserId", "CreatedAt" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2024, 6, 18, 22, 50, 39, 960, DateTimeKind.Local).AddTicks(1486) },
                    { 1, 4, 1, new DateTime(2024, 6, 18, 22, 50, 39, 960, DateTimeKind.Local).AddTicks(1507) },
                    { 1, 5, 1, new DateTime(2024, 6, 18, 22, 50, 39, 960, DateTimeKind.Local).AddTicks(1509) },
                    { 1, 2, 2, new DateTime(2024, 6, 18, 22, 50, 39, 960, DateTimeKind.Local).AddTicks(1511) },
                    { 1, 4, 2, new DateTime(2024, 6, 18, 22, 50, 39, 960, DateTimeKind.Local).AddTicks(1513) },
                    { 1, 3, 3, new DateTime(2024, 6, 18, 22, 50, 39, 960, DateTimeKind.Local).AddTicks(1515) },
                    { 1, 4, 3, new DateTime(2024, 6, 18, 22, 50, 39, 960, DateTimeKind.Local).AddTicks(1517) }
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
