using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AddBranchSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "Address", "Code", "CreatedAt", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("3c9e6679-7425-40de-944b-e07fc1f90ae0"), "Rua XV de Novembro, 1500 - Centro, Curitiba - PR", "BRANCH-CWB-001", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Curitiba Branch", null },
                    { new Guid("5c9e6679-7425-40de-944b-e07fc1f90ae9"), "Av. Afonso Pena, 2000 - Centro, Belo Horizonte - MG", "BRANCH-BH-001", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Belo Horizonte Branch", null },
                    { new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"), "Av. Paulista, 1000 - Bela Vista, São Paulo - SP", "MATRIX-001", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "São Paulo Headquarters", null },
                    { new Guid("9c9e6679-7425-40de-944b-e07fc1f90ae8"), "Av. Rio Branco, 500 - Centro, Rio de Janeiro - RJ", "BRANCH-RJ-001", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Rio de Janeiro Branch", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("3c9e6679-7425-40de-944b-e07fc1f90ae0"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("5c9e6679-7425-40de-944b-e07fc1f90ae9"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("9c9e6679-7425-40de-944b-e07fc1f90ae8"));
        }
    }
}
