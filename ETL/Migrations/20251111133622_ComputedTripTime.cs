using ETL.Infrastructure.DataBase.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETL.Migrations
{
    /// <inheritdoc />
    public partial class ComputedTripTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TripTimeMinutes",
                table: "ETLEntities",
                type: "int",
                nullable: false,
                computedColumnSql: $"DATEDIFF(MINUTE, {nameof(ETLEntity.TpepPickupDateTime)}, {nameof(ETLEntity.TpepDropoffDateTime)})",
                stored: true,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TripTimeMinutes",
                table: "ETLEntities");
        }
    }
}
