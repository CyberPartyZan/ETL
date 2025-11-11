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
using CsvHelper.Configuration;

namespace ETL
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var uniqueRecords = GetUniqueCSVRecords();
            var duplicates = GetDuplicatedCSVRecords();
            SaveDuplicatedCSVRecords(duplicates);
            await SaveUniqueCSVRecordsToDb(uniqueRecords);
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
                csvEntityDto.StoreAndForwardFlag == "Yes" ? true : false,
                csvEntityDto.PULocationId,
                csvEntityDto.DOLocationID,
                csvEntityDto.FareAmount,
                csvEntityDto.TipAmount);
        }

        public static void SaveDuplicatedCSVRecords(IEnumerable<CSVEntityDto> csvEntityDtos)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };

            using (var writer = new StreamWriter("Data/sample-cab-data-duplicates.csv"))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.Context.RegisterClassMap<EntityMap>();
                csv.WriteRecords(csvEntityDtos);
            }
        }

        public static IEnumerable<CSVEntityDto> GetUniqueCSVRecords()
        {
            using var reader = new StreamReader("Data/sample-cab-data.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<EntityMap>();
            var records = csv.GetRecords<CSVEntityDto>();

            var uniqueRecords = records
                .GroupBy(r => new { r.TpepPickupDateTime, r.TpepDropoffDateTime, r.PassengerCount })
                .Select(g => g.First())
                .ToList();

            return uniqueRecords;
        }

        public static IEnumerable<CSVEntityDto> GetDuplicatedCSVRecords()
        {
            using var reader = new StreamReader("Data/sample-cab-data.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<EntityMap>();
            var records = csv.GetRecords<CSVEntityDto>();

            var duplicates = records
                .GroupBy(r => new { r.TpepPickupDateTime, r.TpepDropoffDateTime, r.PassengerCount })
                .Where(g => g.Count() > 1)
                .SelectMany(g => g)
                .ToList();

            return duplicates;
        }

        public static async Task SaveUniqueCSVRecordsToDb(IEnumerable<CSVEntityDto> uniqueRecords)
        {
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

            foreach (var csvEntityDto in uniqueRecords)
            {
                AddCSVEntityDtoToETLEntityTable(table, csvEntityDto);
            }

            // TODO: Make it write records in some kind of stream to use less memory for storing data in case we will perform a 10Gb CSV file
            await sqlBulk.WriteToServerAsync(table);

        }
    }
}
