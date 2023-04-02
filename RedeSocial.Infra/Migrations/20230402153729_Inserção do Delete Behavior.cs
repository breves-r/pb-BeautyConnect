using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedeSocial.Infra.Migrations
{
    public partial class InserçãodoDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Profiles_ProfileIdProfile",
                table: "Posts");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Profiles_ProfileIdProfile",
                table: "Posts",
                column: "ProfileIdProfile",
                principalTable: "Profiles",
                principalColumn: "IdProfile",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Profiles_ProfileIdProfile",
                table: "Posts");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Profiles_ProfileIdProfile",
                table: "Posts",
                column: "ProfileIdProfile",
                principalTable: "Profiles",
                principalColumn: "IdProfile");
        }
    }
}
