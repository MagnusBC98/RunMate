using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunMate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRunningStatsRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RunningStats_UserId",
                table: "RunningStats",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RunningStats_AspNetUsers_UserId",
                table: "RunningStats",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RunningStats_AspNetUsers_UserId",
                table: "RunningStats");

            migrationBuilder.DropIndex(
                name: "IX_RunningStats_UserId",
                table: "RunningStats");
        }
    }
}
