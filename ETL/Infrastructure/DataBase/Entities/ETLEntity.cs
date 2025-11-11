using ETL.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Infrastructure.DataBase.Entities
{
    internal class ETLEntity
    {
        public Guid Id { get; set; }
        public DateTime TpepPickupDateTime { get; set; }
        public DateTime TpepDropoffDateTime { get; set; }
        public int PassengerCount { get; set; }
        public double TripDistance { get; set; }
        public bool StoreAndForwardFlag { get; set; }
        public int PULocationId { get; set; }
        public int DOLocationID { get; set; }
        public double FareAmount { get; set; }
        public double TipAmount { get; set; }

        public static ETLEntity FromDto(CSVEntityDto dto)
        {
            var entity = new ETLEntity();

            entity.TpepDropoffDateTime = dto.TpepDropoffDateTime;
            entity.TpepPickupDateTime = dto.TpepPickupDateTime;
            entity.PassengerCount = dto.PassengerCount;
            entity.TripDistance = dto.TripDistance;
            entity.StoreAndForwardFlag = dto.StoreAndForwardFlag;
            entity.PULocationId = dto.PULocationId;
            entity.DOLocationID = dto.DOLocationID;
            entity.FareAmount = dto.FareAmount;
            entity.TipAmount = dto.TipAmount;

            return entity;
        }
    }
}
