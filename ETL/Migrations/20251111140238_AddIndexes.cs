using ETL.Infrastructure.DataBase;
using ETL.Infrastructure.DataBase.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETL.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        private readonly string _puLocationIdIndexName = $"IX_{nameof(ETLDbContext.ETLEntities)}_{nameof(ETLEntity.PULocationId)}";
        private readonly string _timeSpentTravelingIndexName = $"IX_{nameof(ETLDbContext.ETLEntities)}_{nameof(ETLEntity.TimeSpentTraveling)}";
        private readonly string _tripDistanceIndexName = $"IX_{nameof(ETLDbContext.ETLEntities)}_{nameof(ETLEntity.TripDistance)}";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: _puLocationIdIndexName,
                table: nameof(ETLDbContext.ETLEntities),
                column: nameof(ETLEntity.PULocationId));

            migrationBuilder.CreateIndex(
                name: _timeSpentTravelingIndexName,
                table: nameof(ETLDbContext.ETLEntities),
                column: nameof(ETLEntity.TimeSpentTraveling));

            migrationBuilder.CreateIndex(
                name: _tripDistanceIndexName,
                table: nameof(ETLDbContext.ETLEntities),
                column: nameof(ETLEntity.TripDistance));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: _puLocationIdIndexName,
                table: nameof(ETLDbContext.ETLEntities));

            migrationBuilder.DropIndex(
                name: _timeSpentTravelingIndexName,
                table: nameof(ETLDbContext.ETLEntities));

            migrationBuilder.DropIndex(
                name: _tripDistanceIndexName,
                table: nameof(ETLDbContext.ETLEntities));
        }
    }
}
