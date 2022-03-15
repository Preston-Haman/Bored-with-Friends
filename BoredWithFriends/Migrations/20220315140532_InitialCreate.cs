using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoredWithFriends.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerStatistics",
                columns: table => new
                {
                    PlayerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastPlayedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPlayTime = table.Column<long>(type: "bigint", nullable: false),
                    RoundsPlayed = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatistics", x => x.PlayerID);
                });

            migrationBuilder.CreateTable(
                name: "PlayerLogins",
                columns: table => new
                {
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastLoginTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerLogins", x => x.PlayerID);
                    table.ForeignKey(
                        name: "FK_PlayerLogins_PlayerStatistics_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "PlayerStatistics",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLogins_UserName",
                table: "PlayerLogins",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerLogins");

            migrationBuilder.DropTable(
                name: "PlayerStatistics");
        }
    }
}
