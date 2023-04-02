using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedeSocial.Infra.Migrations
{
    public partial class Alterandodeletebehavioramizade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Profiles_IdProfileA",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Profiles_IdProfileB",
                table: "Friendships");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Profiles_IdProfileA",
                table: "Friendships",
                column: "IdProfileA",
                principalTable: "Profiles",
                principalColumn: "IdProfile",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Profiles_IdProfileB",
                table: "Friendships",
                column: "IdProfileB",
                principalTable: "Profiles",
                principalColumn: "IdProfile",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Profiles_IdProfileA",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Profiles_IdProfileB",
                table: "Friendships");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Profiles_IdProfileA",
                table: "Friendships",
                column: "IdProfileA",
                principalTable: "Profiles",
                principalColumn: "IdProfile",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Profiles_IdProfileB",
                table: "Friendships",
                column: "IdProfileB",
                principalTable: "Profiles",
                principalColumn: "IdProfile",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
