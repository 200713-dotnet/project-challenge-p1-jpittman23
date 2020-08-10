using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaStore.Storing.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Pizza",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Pizza",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Pizza");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Pizza");
        }
    }
}
