using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MicrowaveQueue.DAL.Migrations
{
    public partial class CorrectingOnlineQueue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlineQueues_Microwaves_MicrowaveId",
                table: "OnlineQueues");

            migrationBuilder.DropForeignKey(
                name: "FK_OnlineQueues_AspNetUsers_UserId",
                table: "OnlineQueues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnlineQueues",
                table: "OnlineQueues");

            migrationBuilder.DropIndex(
                name: "IX_OnlineQueues_MicrowaveId",
                table: "OnlineQueues");

            migrationBuilder.DropIndex(
                name: "IX_OnlineQueues_UserId",
                table: "OnlineQueues");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OnlineQueues");

            migrationBuilder.DropColumn(
                name: "MicrowaveId",
                table: "OnlineQueues");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OnlineQueues",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "OnlineQueues",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "QueueNum",
                table: "OnlineQueues",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnlineQueues",
                table: "OnlineQueues",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineQueues_AspNetUsers_UserId",
                table: "OnlineQueues",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlineQueues_AspNetUsers_UserId",
                table: "OnlineQueues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnlineQueues",
                table: "OnlineQueues");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "OnlineQueues");

            migrationBuilder.DropColumn(
                name: "QueueNum",
                table: "OnlineQueues");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OnlineQueues",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OnlineQueues",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "MicrowaveId",
                table: "OnlineQueues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnlineQueues",
                table: "OnlineQueues",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineQueues_MicrowaveId",
                table: "OnlineQueues",
                column: "MicrowaveId");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineQueues_UserId",
                table: "OnlineQueues",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineQueues_Microwaves_MicrowaveId",
                table: "OnlineQueues",
                column: "MicrowaveId",
                principalTable: "Microwaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineQueues_AspNetUsers_UserId",
                table: "OnlineQueues",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
