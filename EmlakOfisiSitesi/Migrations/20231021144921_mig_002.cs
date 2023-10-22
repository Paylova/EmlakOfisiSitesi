using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmlakOfisiSitesi.Migrations
{
    /// <inheritdoc />
    public partial class mig_002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAndOver",
                table: "BuildingAges",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAndOver",
                table: "BuildingAges");
        }
    }
}
