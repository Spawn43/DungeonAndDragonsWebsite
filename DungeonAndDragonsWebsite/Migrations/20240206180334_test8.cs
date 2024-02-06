using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DungeonAndDragonsWebsite.Migrations
{
    /// <inheritdoc />
    public partial class test8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventPlannerID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    TableID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlayersAllowed = table.Column<int>(type: "int", nullable: false),
                    DungeonMasterID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.TableID);
                    table.ForeignKey(
                        name: "FK_Tables_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TableID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Tables_TableID",
                        column: x => x.TableID,
                        principalTable: "Tables",
                        principalColumn: "TableID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventPlannerID",
                table: "Events",
                column: "EventPlannerID");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_DungeonMasterID",
                table: "Tables",
                column: "DungeonMasterID");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_EventID",
                table: "Tables",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TableID",
                table: "Users",
                column: "TableID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_EventPlannerID",
                table: "Events",
                column: "EventPlannerID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Users_DungeonMasterID",
                table: "Tables",
                column: "DungeonMasterID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_EventPlannerID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Users_DungeonMasterID",
                table: "Tables");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
