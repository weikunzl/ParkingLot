using System.Collections.Generic;
using System.Linq;

namespace ParkingLot
{
    public class AvailableRateParkingManager : BaseParkingManager
    {
        public AvailableRateParkingManager(List<ParkingLot> parkingLots) : base(parkingLots)
        {
        }

        protected override ParkingLot GetLotForParking()
        {
            return ParkingLots.OrderByDescending(_ => _.CountEmptyPosition() / _.GetCapacity()).First();
        }
    }
}