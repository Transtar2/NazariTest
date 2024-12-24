using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NazariTest.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removeTitleUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FinancialYears_Title",
                table: "FinancialYears");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FinancialYears_Title",
                table: "FinancialYears",
                column: "Title",
                unique: true);
        }
    }
}
