using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasky.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueUserEmailIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Normalize existing emails to the canonical form (see Email.Normalize) before
            // building the unique index, otherwise case-only duplicates would survive it.
            // If this exposes real duplicates, the CreateIndex below fails loudly - resolve
            // them by hand rather than letting the migration pick an arbitrary winner.
            migrationBuilder.Sql(@"UPDATE ""Users"" SET ""Email"" = lower(btrim(""Email""));");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);
        }
    }
}
