using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Proyecto_Gestion_Escolar_Horarios.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "81b3c429-effe-4b76-9b71-41e5c6e9c549", null, "Profesor", "PROFESOR" },
                    { "93ed711b-c758-447e-9fa9-d8d1ba4c3184", null, "Administrador", "ADMINISTRADOR" },
                    { "d4789d55-9204-4a20-b8af-0c4de811e16e", null, "Estudiante", "ESTUDIANTE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81b3c429-effe-4b76-9b71-41e5c6e9c549");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93ed711b-c758-447e-9fa9-d8d1ba4c3184");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4789d55-9204-4a20-b8af-0c4de811e16e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "428d9b13-0f93-478f-92f4-20c685fcbf87", null, "Profesor", "PROFESOR" },
                    { "5249f84f-a5cb-4437-8000-0ee8bd778ffe", null, "Estudiante", "ESTUDIANTE" },
                    { "5ae6e99c-0f8c-4d7c-913e-5658942bdf26", null, "Administrador", "ADMINISTRADOR" }
                });
        }
    }
}
