using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasky.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBoardModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Users_OwnerId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardTasks_Users_ResponsibleId",
                table: "BoardTasks");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "BoardTasks",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "ResponsibleId",
                table: "BoardTasks",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "Estimate",
                table: "BoardTasks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BoardTasks",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "UserEntityUserId",
                table: "Boards",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boards_UserEntityUserId",
                table: "Boards",
                column: "UserEntityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Users_OwnerId",
                table: "Boards",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Users_UserEntityUserId",
                table: "Boards",
                column: "UserEntityUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardTasks_Users_ResponsibleId",
                table: "BoardTasks",
                column: "ResponsibleId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Users_OwnerId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Users_UserEntityUserId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardTasks_Users_ResponsibleId",
                table: "BoardTasks");

            migrationBuilder.DropIndex(
                name: "IX_Boards_UserEntityUserId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "UserEntityUserId",
                table: "Boards");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "BoardTasks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<Guid>(
                name: "ResponsibleId",
                table: "BoardTasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Estimate",
                table: "BoardTasks",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BoardTasks",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Users_OwnerId",
                table: "Boards",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardTasks_Users_ResponsibleId",
                table: "BoardTasks",
                column: "ResponsibleId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
