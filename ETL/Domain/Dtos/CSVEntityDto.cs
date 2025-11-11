using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Domain.Dtos
{
    internal class CSVEntityDto
    {
        private string _storeAndForwardFlag;

        public DateTime TpepPickupDateTime { get; set; }
        public DateTime TpepDropoffDateTime { get; set; }
        public int PassengerCount { get; set; }
        public double TripDistance { get; set; }
        public string StoreAndForwardFlag 
        { 
            get { return _storeAndForwardFlag; }
            set { _storeAndForwardFlag = value.Trim() == "Y" ? "Yes" : "No"; } 
        }
        public int PULocationId { get; set; }
        public int DOLocationID { get; set; }
        public double FareAmount { get; set; }
        public double TipAmount { get; set; }
    }
}
