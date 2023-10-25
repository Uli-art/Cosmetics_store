using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEB_153502_Sidorova.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_CosmeticsSet_Categories_CategoryId",
                table: "CosmeticsSet",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CosmeticsSet_Categories_CategoryId",
                table: "CosmeticsSet");
        }
    }
}
