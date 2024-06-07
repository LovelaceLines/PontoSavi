using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontoSavi.src.PontoSavi.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CheckIn = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    CheckInToleranceMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    CheckOut = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    CheckOutToleranceMinutes = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserSettings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PublicId" },
                values: new object[] { "9f133a84-29bb-48bb-82d2-86f4be807411", "01HZR8PN7WCNG4R71A0K402QAP" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PublicId" },
                values: new object[] { "51f4a569-e764-4d4e-a6ac-f29bd3df48b1", "01HZR8PN7WXCFSWDTFQ7BNEW8F" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PublicId" },
                values: new object[] { "a3927ee6-f103-4bc9-8e05-73e1803dadac", "01HZR8PN7WCBPWS9G8ZTGW4H6K" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PublicId" },
                values: new object[] { "ddc07991-1c0e-48be-ba4d-366f865a9cb3", "01HZR8PN7WZHV7G66E090PR5AX" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PublicId", "SecurityStamp" },
                values: new object[] { "42f36196-9d6f-4878-b087-23b67fd8bc6b", "AQAAAAIAAYagAAAAEKbreTh0fPbTVprteG5K1G2pFEcB1CVJiygfPWsYBFlel2bdZaTDzsWdS2cqpK7qOg==", "01HZR8PMZ349TX1K36SXZ8VWHN", "c9b94a76-8df1-4541-bd50-832540e1bc96" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PublicId", "SecurityStamp" },
                values: new object[] { "d639f511-57c4-460d-8e6e-ad1bbdb28aae", "AQAAAAIAAYagAAAAED2hUkNIk4U66JMFMePtnU7mn9L0F1YvXZklMkgL1NUTsFCF0Yg9w8EgZjx+Vl+dkQ==", "01HZR8PN21GAJF04ZV6C69QD55", "274a897e-9255-4989-b9a5-52a96de5bfa1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PublicId", "SecurityStamp" },
                values: new object[] { "3ecc3736-c2ec-45be-973e-f8cfc300146f", "AQAAAAIAAYagAAAAEPuWv4MeoUkEyos1nfr3yIc0Vy8J8ESgLAbEa8PZCVN87LpgsFoAnvNDn2Wqbc2ZkA==", "01HZR8PN4WGQ94VBQV28QQFY4W", "bba21fde-a29e-4cf4-a65e-4339659174a3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PublicId" },
                values: new object[] { "9a65ddcf-3fcf-42d6-af4e-5c4178126332", "01HZPWGMEDJ45Z0MQ4AD0A5HPA" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PublicId" },
                values: new object[] { "d01d47eb-ecb1-4a17-970e-f14abd9b1728", "01HZPWGMEDVNTS1P8M9XHVSHDS" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PublicId" },
                values: new object[] { "1f18bce6-1443-419d-937d-c215aef2c950", "01HZPWGMEDW671X6YYWW1G6KVW" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PublicId" },
                values: new object[] { "e4f80689-8480-42b3-b70b-82b34c3fa4ee", "01HZPWGMED1NT38S7WWFH5JS55" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PublicId", "SecurityStamp" },
                values: new object[] { "3648a0f4-6bfa-4bf9-b88d-046edbe7f6d7", "AQAAAAIAAYagAAAAELq3vH+kDXbCjytbHygyCbEb+gzQoYrSnE35kimx53qsFrAYXS/0MHC4RZeqJGc3GA==", "01HZPWGM8WH7PSTSPP5ZAPM676", "1ffc1b91-416c-4645-a0da-2fe213ac268e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PublicId", "SecurityStamp" },
                values: new object[] { "88ea1d78-c7c0-4085-b0d9-b2a968efb85f", "AQAAAAIAAYagAAAAEEjMuWjHIMo0j5Br8FOVwpJfciSbGHzNypi4We2oKBXqf+HNLUIHnP3dELWnc2qUyw==", "01HZPWGMAQH5D0C2SSZR5NWC66", "c6e45f57-5c4d-445f-a3f3-9b989e6b7feb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PublicId", "SecurityStamp" },
                values: new object[] { "f905eb0a-8d62-4437-92fd-12499d27d292", "AQAAAAIAAYagAAAAEMffPL3H6EQTqXmC3WoBE/g1rkBx2phArjgVUEDYVoAiOnCk7cD1A2BVjCh+s/zVkQ==", "01HZPWGMCH8BB3KS4S2KMNAAF6", "5bc082bb-97ef-4869-a1e6-8694f22333c0" });
        }
    }
}
