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

        public int TripTimeMinutes { get; private set; }

        public static ETLEntity FromDto(CSVEntityDto dto)
        {
            var entity = new ETLEntity();

            entity.TpepDropoffDateTime = dto.TpepDropoffDateTime.ToUniversalTime();
            entity.TpepPickupDateTime = dto.TpepPickupDateTime.ToUniversalTime();
            entity.PassengerCount = dto.PassengerCount;
            entity.TripDistance = dto.TripDistance;
            entity.StoreAndForwardFlag = dto.StoreAndForwardFlag == "Yes" ? true : false;
            entity.PULocationId = dto.PULocationId;
            entity.DOLocationID = dto.DOLocationID;
            entity.FareAmount = dto.FareAmount;
            entity.TipAmount = dto.TipAmount;

            return entity;
        }

        public CSVEntityDto ToDto()
        {
            var dto = new CSVEntityDto();

            dto.TpepDropoffDateTime = this.TpepDropoffDateTime;

            dto.TpepPickupDateTime = this.TpepPickupDateTime;
            dto.PassengerCount = this.PassengerCount;
            dto.TripDistance = this.TripDistance;
            dto.StoreAndForwardFlag = this.StoreAndForwardFlag ? "Yes" : "No";
            dto.PULocationId = this.PULocationId;
            dto.DOLocationID = this.DOLocationID;
            dto.FareAmount = this.FareAmount;
            dto.TipAmount = this.TipAmount;

            return dto;
        }
    }
}
