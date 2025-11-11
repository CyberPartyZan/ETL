using CsvHelper.Configuration;
using ETL.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Domain.CSVMap
{
    internal class EntityMap : ClassMap<CSVEntityDto>
    {
        public EntityMap() 
        {
            Map(m => m.TpepPickupDateTime).Name("tpep_pickup_datetime");
            Map(m => m.TpepDropoffDateTime).Name("tpep_dropoff_datetime");
            Map(m => m.PassengerCount)
                .Name("passenger_count")
                .Convert(args =>
                    {
                        var passangerCountString = args.Row.GetField("passenger_count").Trim();
                        return string.IsNullOrEmpty(passangerCountString) ? 0 : int.Parse(passangerCountString);
                    });
            Map(m => m.TripDistance).Name("trip_distance");
            Map(m => m.StoreAndForwardFlag)
                .Name("store_and_fwd_flag");
            Map(m => m.PULocationId).Name("PULocationID");
            Map(m => m.DOLocationID).Name("DOLocationID");
            Map(m => m.FareAmount).Name("fare_amount");
            Map(m => m.TipAmount).Name("tip_amount");
        }
    }
}
