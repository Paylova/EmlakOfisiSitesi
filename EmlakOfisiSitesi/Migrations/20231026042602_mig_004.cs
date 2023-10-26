using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmlakOfisiSitesi.Migrations
{
    /// <inheritdoc />
    public partial class mig_004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HousingAdvertisements_Facades_FacadeId",
                table: "HousingAdvertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_HousingAdvertisements_UsageStatuses_UsageStatusId",
                table: "HousingAdvertisements");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsageStatusId",
                table: "HousingAdvertisements",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "RentalIncome",
                table: "HousingAdvertisements",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSuitableForTrade",
                table: "HousingAdvertisements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsFurnished",
                table: "HousingAdvertisements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<Guid>(
                name: "FacadeId",
                table: "HousingAdvertisements",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "Dues",
                table: "HousingAdvertisements",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_HousingAdvertisements_Facades_FacadeId",
                table: "HousingAdvertisements",
                column: "FacadeId",
                principalTable: "Facades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HousingAdvertisements_UsageStatuses_UsageStatusId",
                table: "HousingAdvertisements",
                column: "UsageStatusId",
                principalTable: "UsageStatuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HousingAdvertisements_Facades_FacadeId",
                table: "HousingAdvertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_HousingAdvertisements_UsageStatuses_UsageStatusId",
                table: "HousingAdvertisements");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsageStatusId",
                table: "HousingAdvertisements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "RentalIncome",
                table: "HousingAdvertisements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSuitableForTrade",
                table: "HousingAdvertisements",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsFurnished",
                table: "HousingAdvertisements",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FacadeId",
                table: "HousingAdvertisements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Dues",
                table: "HousingAdvertisements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HousingAdvertisements_Facades_FacadeId",
                table: "HousingAdvertisements",
                column: "FacadeId",
                principalTable: "Facades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HousingAdvertisements_UsageStatuses_UsageStatusId",
                table: "HousingAdvertisements",
                column: "UsageStatusId",
                principalTable: "UsageStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
