using Microsoft.EntityFrameworkCore.Migrations;

namespace MicrowaveQueue.DAL.Migrations
{
    public partial class CorrectDbTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Microwaves_Rooms_RoomId",
                table: "Microwaves");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Microwaves",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Microwaves_Rooms_RoomId",
                table: "Microwaves",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Microwaves_Rooms_RoomId",
                table: "Microwaves");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Microwaves",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Microwaves_Rooms_RoomId",
                table: "Microwaves",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
