using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunMate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRunningStatsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RunningStats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FiveKmPb = table.Column<TimeSpan>(type: "interval", nullable: true),
                    TenKmPb = table.Column<TimeSpan>(type: "interval", nullable: true),
                    HalfMarathonPb = table.Column<TimeSpan>(type: "interval", nullable: true),
                    MarathonPb = table.Column<TimeSpan>(type: "interval", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunningStats", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RunningStats");
        }
    }
}
