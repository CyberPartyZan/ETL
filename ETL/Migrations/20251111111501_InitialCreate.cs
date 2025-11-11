using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ETLEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TpepPickupDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TpepDropoffDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassengerCount = table.Column<int>(type: "int", nullable: false),
                    TripDistance = table.Column<double>(type: "float", nullable: false),
                    StoreAndForwardFlag = table.Column<bool>(type: "bit", nullable: false),
                    PULocationId = table.Column<int>(type: "int", nullable: false),
                    DOLocationID = table.Column<int>(type: "int", nullable: false),
                    FareAmount = table.Column<double>(type: "float", nullable: false),
                    TipAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ETLEntities", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ETLEntities");
        }
    }
}
