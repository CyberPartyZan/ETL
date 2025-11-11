using CsvHelper;
using System.Globalization;
using System;
using ETL.Domain.CSVMap;
using ETL.Domain.Dtos;
using ETL.Infrastructure.DataBase;
using ETL.Infrastructure.DataBase.Entities;

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

            var db = new ETLDbContext();

            // TODO: Select records in stream use less memory for huge files and perform db.Save by little batches of the records
            await db.AddRangeAsync(records.Select(ETLEntity.FromDto));

            await db.SaveChangesAsync();
        }
    }
}
