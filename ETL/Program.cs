using CsvHelper;
using System.Globalization;
using System;
using ETL.Domain.CSVMap;
using ETL.Domain.Dtos;
using ETL.Infrastructure.DataBase;
using ETL.Infrastructure.DataBase.Entities;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace ETL
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var reader = new StreamReader("Data/sample-cab-data.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<EntityMap>();
            var records = csv.GetRecords<CSVEntityDto>();


            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            using var sqlBulk = new SqlBulkCopy(connectionString)
            {
                DestinationTableName = nameof(ETLDbContext.ETLEntities)
            };

            // Convert your list to DataTable
            var table = new DataTable();
            SetupETLEntityTableColumns(table);

            foreach (var csvEntityDto in records)
            {
                AddCSVEntityDtoToETLEntityTable(table, csvEntityDto);
            }

            await sqlBulk.WriteToServerAsync(table);
        }

        public static void SetupETLEntityTableColumns(DataTable table)
        {
            table.Columns.Add(nameof(ETLEntity.Id), typeof(Guid));
            table.Columns.Add(nameof(ETLEntity.TpepPickupDateTime), typeof(DateTime));
            table.Columns.Add(nameof(ETLEntity.TpepDropoffDateTime), typeof(DateTime));
            table.Columns.Add(nameof(ETLEntity.PassengerCount), typeof(int));
            table.Columns.Add(nameof(ETLEntity.TripDistance), typeof(double));
            table.Columns.Add(nameof(ETLEntity.StoreAndForwardFlag), typeof(bool));
            table.Columns.Add(nameof(ETLEntity.PULocationId), typeof(int));
            table.Columns.Add(nameof(ETLEntity.DOLocationID), typeof(int));
            table.Columns.Add(nameof(ETLEntity.FareAmount), typeof(double));
            table.Columns.Add(nameof(ETLEntity.TipAmount), typeof(double));
        }

        public static void AddCSVEntityDtoToETLEntityTable(DataTable table, CSVEntityDto csvEntityDto)
        {
            table.Rows.Add(Guid.NewGuid(),
                csvEntityDto.TpepPickupDateTime,
                csvEntityDto.TpepDropoffDateTime,
                csvEntityDto.PassengerCount,
                csvEntityDto.TripDistance,
                csvEntityDto.StoreAndForwardFlag,
                csvEntityDto.PULocationId,
                csvEntityDto.DOLocationID,
                csvEntityDto.FareAmount,
                csvEntityDto.TipAmount);
        }
    }
}
