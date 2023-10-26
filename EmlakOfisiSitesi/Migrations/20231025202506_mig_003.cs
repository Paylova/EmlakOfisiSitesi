using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmlakOfisiSitesi.Migrations
{
    /// <inheritdoc />
    public partial class mig_003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agent_Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Agent_Surname",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Agent_Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Agent_Surname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
