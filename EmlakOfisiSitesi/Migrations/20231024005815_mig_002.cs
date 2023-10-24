using System;
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
            migrationBuilder.DropForeignKey(
                name: "FK_HousingAdvertisements_DeedStatuses_DeedStatusId",
                table: "HousingAdvertisements");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeedStatusId",
                table: "HousingAdvertisements",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_HousingAdvertisements_DeedStatuses_DeedStatusId",
                table: "HousingAdvertisements",
                column: "DeedStatusId",
                principalTable: "DeedStatuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HousingAdvertisements_DeedStatuses_DeedStatusId",
                table: "HousingAdvertisements");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeedStatusId",
                table: "HousingAdvertisements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HousingAdvertisements_DeedStatuses_DeedStatusId",
                table: "HousingAdvertisements",
                column: "DeedStatusId",
                principalTable: "DeedStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
