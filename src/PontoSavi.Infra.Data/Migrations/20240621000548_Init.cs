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
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkShiftId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkShifts", x => new { x.UserId, x.WorkShiftId });
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
                values: new object[] { 1, "00000000000000", new DateTime(2024, 6, 20, 21, 5, 47, 276, DateTimeKind.Local).AddTicks(364), "Ponto Savi", "Ponto Savi" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CompanyId", "ConcurrencyStamp", "CreatedAt", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, 1, "655ba2ee-8920-44c8-b2a0-cdfabe0ed649", new DateTime(2024, 6, 20, 21, 5, 47, 599, DateTimeKind.Local).AddTicks(9048), "Desenvolvedor", "DESENVOLVEDOR" },
                    { 2, 1, "106cd16c-fc98-407d-a8b1-c3e367631527", new DateTime(2024, 6, 20, 21, 5, 47, 599, DateTimeKind.Local).AddTicks(9065), "CEO", "CEO" },
                    { 3, 1, "87ccfd78-a345-42c0-9a1e-a8406c780891", new DateTime(2024, 6, 20, 21, 5, 47, 599, DateTimeKind.Local).AddTicks(9070), "Administrador", "ADMINISTRADOR" },
                    { 4, 1, "92bc3799-958a-4edf-9145-0efe4bdf0d1a", new DateTime(2024, 6, 20, 21, 5, 47, 599, DateTimeKind.Local).AddTicks(9084), "Supervisor", "SUPERVISOR" },
                    { 5, 1, "1c7f231a-3f32-480c-a2cf-89bff796b9f3", new DateTime(2024, 6, 20, 21, 5, 47, 599, DateTimeKind.Local).AddTicks(9088), "Colaborador", "COLABORADOR" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "CompanyId", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, 1, "d9957b7f-1d5d-4815-81eb-302e794d6607", new DateTime(2024, 6, 20, 21, 5, 47, 356, DateTimeKind.Local).AddTicks(8274), "dev@gmail.com", false, false, null, "Developer", "DEV@GMAIL.COM", "DEV", "AQAAAAIAAYagAAAAEK+VdDfM5Wk4LV6mW1cwXkrc3P8vplwVFJav7I65eggvgOq7PP1KdG2JnqwEooIMMw==", "(55) 85 9 9999-9999", false, "55a0d3f2-256d-4680-8c5d-31e2188bf27f", false, "dev" },
                    { 2, 0, 1, "99fa85cf-0ee2-41de-a522-55734c7d7cf6", new DateTime(2024, 6, 20, 21, 5, 47, 438, DateTimeKind.Local).AddTicks(6971), "admin@gmail.com", false, false, null, "Administrator", "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAENv7X5QOa1hGa+3LcGUNVI5W1tHz9sr7eEkfvVeWGnW+pc9b4wxTbB4qpt3pYz69YA==", "(55) 85 9 9999-9998", false, "95595c3f-f08c-4579-9cf3-d7d5ea8fc75a", false, "admin" },
                    { 3, 0, 1, "5a91fd52-9cf5-4709-95d5-104e6232d7db", new DateTime(2024, 6, 20, 21, 5, 47, 518, DateTimeKind.Local).AddTicks(8296), "super@gmail.com", false, false, null, "Supervisor", "SUPER@GMAIL.COM", "SUPER", "AQAAAAIAAYagAAAAEHGZdTvSkUfIAQc4WpXYeKXEkLN6BN8ccpS2QP1i39bNI5QaZuUivWguxyjlA4UwVg==", "(55) 85 9 9999-9997", false, "4c035670-a214-4fae-94a7-823e77dac5b8", false, "super" },
                    { 4, 0, 1, "91cd285f-b7cf-40dd-b812-569e99b1a3be", new DateTime(2024, 6, 20, 21, 5, 47, 598, DateTimeKind.Local).AddTicks(4717), "base@gmail.com", false, false, null, "Base", "BASE@GMAIL.COM", "BASE", "AQAAAAIAAYagAAAAEPiI74eabsVO8yZv2gs7ebl6hp6AVgtaA25YFWSKzVZdXM7WS1InvalfvMLIIin9tQ==", "(55) 85 9 9999-9997", false, "c9e960f1-0264-4481-99c4-2d20b9c94251", false, "base" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "CompanyId", "RoleId", "UserId", "CreatedAt" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2024, 6, 20, 21, 5, 47, 602, DateTimeKind.Local).AddTicks(9806) },
                    { 1, 2, 1, new DateTime(2024, 6, 20, 21, 5, 47, 602, DateTimeKind.Local).AddTicks(9814) },
                    { 1, 3, 1, new DateTime(2024, 6, 20, 21, 5, 47, 602, DateTimeKind.Local).AddTicks(9816) },
                    { 1, 4, 1, new DateTime(2024, 6, 20, 21, 5, 47, 602, DateTimeKind.Local).AddTicks(9818) },
                    { 1, 5, 1, new DateTime(2024, 6, 20, 21, 5, 47, 602, DateTimeKind.Local).AddTicks(9889) },
                    { 1, 3, 2, new DateTime(2024, 6, 20, 21, 5, 47, 602, DateTimeKind.Local).AddTicks(9892) },
                    { 1, 5, 2, new DateTime(2024, 6, 20, 21, 5, 47, 602, DateTimeKind.Local).AddTicks(9893) },
                    { 1, 4, 3, new DateTime(2024, 6, 20, 21, 5, 47, 602, DateTimeKind.Local).AddTicks(9905) },
                    { 1, 5, 3, new DateTime(2024, 6, 20, 21, 5, 47, 602, DateTimeKind.Local).AddTicks(9907) },
                    { 1, 5, 4, new DateTime(2024, 6, 20, 21, 5, 47, 602, DateTimeKind.Local).AddTicks(9909) }
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
                name: "IX_UserWorkShifts_WorkShiftId",
                table: "UserWorkShifts",
                column: "WorkShiftId");

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
