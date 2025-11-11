using ETL.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Application.Interfaces
{
    internal interface IUserService
    {
        double GetHighestTipAmmountForLocation(int pickUpLocationId);
        IEnumerable<double> GetTop100LongestFaresByTipDistance();
        IEnumerable<double> GetTop100LongestFaresByTimeSpentTraveling();
        IEnumerable<CSVEntityDto> Search(int pickUpLocationId);
    }
}
