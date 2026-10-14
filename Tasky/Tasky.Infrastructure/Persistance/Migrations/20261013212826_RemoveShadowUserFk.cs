using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasky.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveShadowUserFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Users_UserEntityUserId",
                table: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Boards_UserEntityUserId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "UserEntityUserId",
                table: "Boards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Boards_Users_UserEntityUserId",
                table: "Boards",
                column: "UserEntityUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
