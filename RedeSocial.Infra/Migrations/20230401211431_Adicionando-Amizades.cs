using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedeSocial.Infra.Migrations
{
    public partial class AdicionandoAmizades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sobrenome",
                table: "Profiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Profiles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    IdProfileA = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProfileB = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => new { x.IdProfileA, x.IdProfileB });
                    table.ForeignKey(
                        name: "FK_Friendships_Profiles_IdProfileA",
                        column: x => x.IdProfileA,
                        principalTable: "Profiles",
                        principalColumn: "IdProfile",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Friendships_Profiles_IdProfileB",
                        column: x => x.IdProfileB,
                        principalTable: "Profiles",
                        principalColumn: "IdProfile",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ProfileDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Aniversario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoPele = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoCabelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorPele = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorCabelo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileDetails_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "IdProfile",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_IdProfileB",
                table: "Friendships",
                column: "IdProfileB");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileDetails_ProfileId",
                table: "ProfileDetails",
                column: "ProfileId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.DropTable(
                name: "ProfileDetails");

            migrationBuilder.DropColumn(
                name: "Sobrenome",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Profiles");
        }
    }
}
