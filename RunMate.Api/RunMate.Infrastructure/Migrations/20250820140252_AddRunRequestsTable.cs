using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunMate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRunRequestsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RunRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RunId = table.Column<Guid>(type: "uuid", nullable: false),
                    RunOwnerUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequesterUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RunRequests_AspNetUsers_RequesterUserId",
                        column: x => x.RequesterUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RunRequests_AspNetUsers_RunOwnerUserId",
                        column: x => x.RunOwnerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RunRequests_Runs_RunId",
                        column: x => x.RunId,
                        principalTable: "Runs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RunRequests_RequesterUserId",
                table: "RunRequests",
                column: "RequesterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RunRequests_RunId",
                table: "RunRequests",
                column: "RunId");

            migrationBuilder.CreateIndex(
                name: "IX_RunRequests_RunOwnerUserId",
                table: "RunRequests",
                column: "RunOwnerUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RunRequests");
        }
    }
}
