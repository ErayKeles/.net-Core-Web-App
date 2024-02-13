using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreIdentityApp.Migrations
{
    /// <inheritdoc />
    public partial class beehive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beehives_AspNetUsers_UserId",
                table: "Beehives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Beehives",
                table: "Beehives");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Beehives");

            migrationBuilder.RenameTable(
                name: "Beehives",
                newName: "BeeHives");

            migrationBuilder.RenameColumn(
                name: "NumberOfFrames",
                table: "BeeHives",
                newName: "Capacity");

            migrationBuilder.RenameColumn(
                name: "BeehiveId",
                table: "BeeHives",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Beehives_UserId",
                table: "BeeHives",
                newName: "IX_BeeHives_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "BeeHives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProductionDate",
                table: "BeeHives",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BeeHives",
                table: "BeeHives",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BeeHives_AspNetUsers_UserId",
                table: "BeeHives",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BeeHives_AspNetUsers_UserId",
                table: "BeeHives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BeeHives",
                table: "BeeHives");

            migrationBuilder.DropColumn(
                name: "ProductionDate",
                table: "BeeHives");

            migrationBuilder.RenameTable(
                name: "BeeHives",
                newName: "Beehives");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Beehives",
                newName: "NumberOfFrames");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Beehives",
                newName: "BeehiveId");

            migrationBuilder.RenameIndex(
                name: "IX_BeeHives_UserId",
                table: "Beehives",
                newName: "IX_Beehives_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Beehives",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Beehives",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Beehives",
                table: "Beehives",
                column: "BeehiveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beehives_AspNetUsers_UserId",
                table: "Beehives",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
