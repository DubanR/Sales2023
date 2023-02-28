using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.API.Migrations
{
    /// <inheritdoc />
    public partial class addIdStateAndCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_States_Stateid",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Countries_Countryid",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_Countryid_name",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Stateid_name",
                table: "Cities");

            migrationBuilder.AlterColumn<int>(
                name: "Countryid",
                table: "States",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Stateid",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_Countryid_name",
                table: "States",
                columns: new[] { "Countryid", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Stateid_name",
                table: "Cities",
                columns: new[] { "Stateid", "name" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_States_Stateid",
                table: "Cities",
                column: "Stateid",
                principalTable: "States",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_States_Countries_Countryid",
                table: "States",
                column: "Countryid",
                principalTable: "Countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_States_Stateid",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Countries_Countryid",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_Countryid_name",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Stateid_name",
                table: "Cities");

            migrationBuilder.AlterColumn<int>(
                name: "Countryid",
                table: "States",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Stateid",
                table: "Cities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_States_Countryid_name",
                table: "States",
                columns: new[] { "Countryid", "name" },
                unique: true,
                filter: "[Countryid] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Stateid_name",
                table: "Cities",
                columns: new[] { "Stateid", "name" },
                unique: true,
                filter: "[Stateid] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_States_Stateid",
                table: "Cities",
                column: "Stateid",
                principalTable: "States",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_States_Countries_Countryid",
                table: "States",
                column: "Countryid",
                principalTable: "Countries",
                principalColumn: "id");
        }
    }
}
