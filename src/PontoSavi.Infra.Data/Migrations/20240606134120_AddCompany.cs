using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontoSavi.src.PontoSavi.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCompany : Migration
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
                    PublicId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    TradeName = table.Column<string>(type: "TEXT", nullable: false),
                    CNPJ = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PublicId" },
                values: new object[] { "acb5b17a-62bc-4b3c-bfb7-361eb91b4545", "01HZNHAB8A6EC769T61YD44732" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PublicId" },
                values: new object[] { "800b28c7-f132-44b0-9b5f-48d909c92aeb", "01HZNHAB8AT0BJZ2V7PFJ7YP4J" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PublicId" },
                values: new object[] { "469ebb4d-fd8e-4ea7-83ba-c2f44c74f490", "01HZNHAB8AQFFFNBZV7T2Z4D10" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PublicId" },
                values: new object[] { "8489012e-2777-4d02-88e9-d06338c5de38", "01HZNHAB8A955VX2E936MKY5NA" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PublicId", "SecurityStamp" },
                values: new object[] { "4ea1afe3-d91f-42ed-9d7e-b260e5dcfc3d", "AQAAAAIAAYagAAAAEPDUbYIFtzeEoZa5wb90U7L5wdYWlXN77Kmp+plI7eOg/XJFJk7Kjt9qQ0bdEZNyhw==", "01HZNHAB0WKTX69JGCSGJFJVDY", "6982f193-eeb5-404d-8cfb-4428d0a376a1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PublicId", "SecurityStamp" },
                values: new object[] { "57f462da-1a51-4a54-83e7-84c5cc76c79f", "AQAAAAIAAYagAAAAEEzXdNqpwBFXuZ4C5q2vQOuZTbPZdmRt6GlFPoEMcNo6JiO25pIXmzqc5/7l3iyHvw==", "01HZNHAB3AK36DGD8WSGQFMDF2", "6468925a-514e-4e1e-9265-4c5896f25f98" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PublicId", "SecurityStamp" },
                values: new object[] { "e3f10df4-209d-41f4-882e-927f4fb862e5", "AQAAAAIAAYagAAAAEB71Vp9FBWTn38Io6Tv4YYhf/RgM1ZgcuxmKks3/tLcRp9IPsWMzMoYN4vF36JFXgg==", "01HZNHAB5SPKDF8H90AC36X8D2", "4a214c08-1f66-4e96-b75e-1eaeff7ea591" });
        }
    }
}
