using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaStore.Storing.Migrations
{
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Toppings_Pizza_PizzaModelId",
                table: "Toppings");

            migrationBuilder.DropIndex(
                name: "IX_Toppings_PizzaModelId",
                table: "Toppings");

            migrationBuilder.DropColumn(
                name: "PizzaModelId",
                table: "Toppings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PizzaModelId",
                table: "Toppings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Toppings_PizzaModelId",
                table: "Toppings",
                column: "PizzaModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Toppings_Pizza_PizzaModelId",
                table: "Toppings",
                column: "PizzaModelId",
                principalTable: "Pizza",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
