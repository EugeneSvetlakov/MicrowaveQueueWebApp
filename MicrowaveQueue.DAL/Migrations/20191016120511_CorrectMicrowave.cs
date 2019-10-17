using Microsoft.EntityFrameworkCore.Migrations;

namespace MicrowaveQueue.DAL.Migrations
{
    public partial class CorrectMicrowave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InPause",
                table: "OnlineQueues");

            migrationBuilder.DropColumn(
                name: "FirstInQueue",
                table: "Microwaves");

            migrationBuilder.RenameColumn(
                name: "SecondInQueue",
                table: "Microwaves",
                newName: "NowQueue");

            migrationBuilder.AlterColumn<int>(
                name: "QueueNum",
                table: "OnlineQueues",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "OnlineQueues",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "OnlineQueues");

            migrationBuilder.RenameColumn(
                name: "NowQueue",
                table: "Microwaves",
                newName: "SecondInQueue");

            migrationBuilder.AlterColumn<int>(
                name: "QueueNum",
                table: "OnlineQueues",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "InPause",
                table: "OnlineQueues",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "FirstInQueue",
                table: "Microwaves",
                nullable: true);
        }
    }
}
