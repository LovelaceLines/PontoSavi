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
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysOff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DaysOff_Companies_TenantId",
                        column: x => x.TenantId,
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
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Companies_TenantId",
                        column: x => x.TenantId,
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
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false),
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
                        name: "FK_Users_Companies_TenantId",
                        column: x => x.TenantId,
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
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkShifts_Companies_TenantId",
                        column: x => x.TenantId,
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
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Points_Companies_TenantId",
                        column: x => x.TenantId,
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
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId, x.TenantId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Companies_TenantId",
                        column: x => x.TenantId,
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
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyWorkShifts", x => x.WorkShiftId);
                    table.ForeignKey(
                        name: "FK_CompanyWorkShifts_Companies_TenantId",
                        column: x => x.TenantId,
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
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkShiftId = table.Column<int>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkShifts", x => new { x.UserId, x.WorkShiftId });
                    table.ForeignKey(
                        name: "FK_UserWorkShifts_Companies_TenantId",
                        column: x => x.TenantId,
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
                values: new object[] { 1, "00000000000000", new DateTime(2024, 6, 26, 20, 47, 10, 157, DateTimeKind.Local).AddTicks(1889), "Ponto Savi", "Ponto Savi" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "Name", "NormalizedName", "TenantId" },
                values: new object[,]
                {
                    { 1, "f7f54241-ceb7-4447-a82a-919bd79ad27c", new DateTime(2024, 6, 26, 20, 47, 10, 487, DateTimeKind.Local).AddTicks(423), "Desenvolvedor", "DESENVOLVEDOR", 1 },
                    { 2, "b4739453-b334-43b5-ab84-32d38b5ef729", new DateTime(2024, 6, 26, 20, 47, 10, 487, DateTimeKind.Local).AddTicks(441), "CEO", "CEO", 1 },
                    { 3, "0ac2e163-a52f-467d-8bc7-6cb38a3ae43b", new DateTime(2024, 6, 26, 20, 47, 10, 487, DateTimeKind.Local).AddTicks(447), "Administrador", "ADMINISTRADOR", 1 },
                    { 4, "f0a9c551-e052-4250-8f99-2a66db1b274c", new DateTime(2024, 6, 26, 20, 47, 10, 487, DateTimeKind.Local).AddTicks(452), "Supervisor", "SUPERVISOR", 1 },
                    { 5, "563e651f-5a63-4af5-a1f0-eb598178133e", new DateTime(2024, 6, 26, 20, 47, 10, 487, DateTimeKind.Local).AddTicks(466), "Colaborador", "COLABORADOR", 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "3282124a-d4e1-4d11-8921-71e210ff9f2f", new DateTime(2024, 6, 26, 20, 47, 10, 241, DateTimeKind.Local).AddTicks(6238), "dev@gmail.com", false, false, null, "Developer", "DEV@GMAIL.COM", "DEV", "AQAAAAIAAYagAAAAEJp7jwqZs+FcK7ICPK+c4/CaCsaLub+CCnv2jaODzKFE4wOdOAEdba/NYFdG98D5Pg==", "(55) 85 9 9999-9999", false, "e4b3bf2f-0ab2-47d9-9eca-6dab94c9e338", 1, false, "dev" },
                    { 2, 0, "13a4d7be-9756-4226-8cee-1a35813c722e", new DateTime(2024, 6, 26, 20, 47, 10, 322, DateTimeKind.Local).AddTicks(5857), "admin@gmail.com", false, false, null, "Administrator", "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEKuNRSgGfu0lNdxPn5tSaAjP7a7DOCTNgKF60VqIK8DBvMK+FUqDp/SaDiMWPXKSZQ==", "(55) 85 9 9999-9998", false, "fc4beede-36a5-4d8b-a402-9e01680012a6", 1, false, "admin" },
                    { 3, 0, "c99b9da2-b8c7-4ce4-8a83-a9659bca23b7", new DateTime(2024, 6, 26, 20, 47, 10, 402, DateTimeKind.Local).AddTicks(6001), "super@gmail.com", false, false, null, "Supervisor", "SUPER@GMAIL.COM", "SUPER", "AQAAAAIAAYagAAAAENIo2H2QfbaI0bABDCUmg4WbwxAwdYLIGINfTcHHUpT1cCzWaijDBO7GRK/Hic4yVw==", "(55) 85 9 9999-9997", false, "36b24f13-6909-48cd-a6bd-86ed0904b519", 1, false, "super" },
                    { 4, 0, "ddfafaae-f59f-422e-9ac8-6c922a4df7e5", new DateTime(2024, 6, 26, 20, 47, 10, 486, DateTimeKind.Local).AddTicks(56), "base@gmail.com", false, false, null, "Base", "BASE@GMAIL.COM", "BASE", "AQAAAAIAAYagAAAAEOxWFPcM92IUDHtS/XI/KjMhwiPCz899+tJZU8guodnFb1WsF+qZtCv74jrQ8A67Fw==", "(55) 85 9 9999-9997", false, "43646f1e-7e45-4596-93e9-f6e2e38a9ffa", 1, false, "base" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "TenantId", "UserId", "CreatedAt" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2024, 6, 26, 20, 47, 10, 495, DateTimeKind.Local).AddTicks(9035) },
                    { 2, 1, 1, new DateTime(2024, 6, 26, 20, 47, 10, 495, DateTimeKind.Local).AddTicks(9052) },
                    { 3, 1, 1, new DateTime(2024, 6, 26, 20, 47, 10, 495, DateTimeKind.Local).AddTicks(9054) },
                    { 4, 1, 1, new DateTime(2024, 6, 26, 20, 47, 10, 495, DateTimeKind.Local).AddTicks(9056) },
                    { 5, 1, 1, new DateTime(2024, 6, 26, 20, 47, 10, 495, DateTimeKind.Local).AddTicks(9058) },
                    { 3, 1, 2, new DateTime(2024, 6, 26, 20, 47, 10, 495, DateTimeKind.Local).AddTicks(9060) },
                    { 5, 1, 2, new DateTime(2024, 6, 26, 20, 47, 10, 495, DateTimeKind.Local).AddTicks(9062) },
                    { 4, 1, 3, new DateTime(2024, 6, 26, 20, 47, 10, 495, DateTimeKind.Local).AddTicks(9081) },
                    { 5, 1, 3, new DateTime(2024, 6, 26, 20, 47, 10, 495, DateTimeKind.Local).AddTicks(9083) },
                    { 5, 1, 4, new DateTime(2024, 6, 26, 20, 47, 10, 495, DateTimeKind.Local).AddTicks(9086) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWorkShifts_TenantId",
                table: "CompanyWorkShifts",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWorkShifts_WorkShiftId_TenantId",
                table: "CompanyWorkShifts",
                columns: new[] { "WorkShiftId", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DaysOff_Date_TenantId",
                table: "DaysOff",
                columns: new[] { "Date", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DaysOff_TenantId",
                table: "DaysOff",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_ManagerId",
                table: "Points",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_TenantId",
                table: "Points",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_UserId",
                table: "Points",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name_TenantId",
                table: "Roles",
                columns: new[] { "Name", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_TenantId",
                table: "Roles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_TenantId",
                table: "UserRoles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantId",
                table: "Users",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkShifts_TenantId",
                table: "UserWorkShifts",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkShifts_WorkShiftId",
                table: "UserWorkShifts",
                column: "WorkShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShifts_TenantId",
                table: "WorkShifts",
                column: "TenantId");
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
