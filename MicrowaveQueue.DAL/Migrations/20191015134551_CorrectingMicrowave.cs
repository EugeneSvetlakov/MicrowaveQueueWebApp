using Microsoft.EntityFrameworkCore.Migrations;

namespace MicrowaveQueue.DAL.Migrations
{
    public partial class CorrectingMicrowave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Microwaves");

            migrationBuilder.AddColumn<int>(
                name: "FirstInQueue",
                table: "Microwaves",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondInQueue",
                table: "Microwaves",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstInQueue",
                table: "Microwaves");

            migrationBuilder.DropColumn(
                name: "SecondInQueue",
                table: "Microwaves");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Microwaves",
                nullable: true);
        }
    }
}
