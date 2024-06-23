using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShop.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesToRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ad84814-48fe-4300-983d-a50ca9d8b498", null, "Editor", "EDITOR" },
                    { "30428aa2-79dc-4ad4-9eda-daa3e36da255", null, "Customer", "CUSTOMER" },
                    { "cd380ebc-0482-497f-9043-ce4c5f329e6f", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ad84814-48fe-4300-983d-a50ca9d8b498");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30428aa2-79dc-4ad4-9eda-daa3e36da255");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd380ebc-0482-497f-9043-ce4c5f329e6f");
        }
    }
}
