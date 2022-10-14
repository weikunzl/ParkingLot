using System.Collections.Generic;
using System.Linq;

namespace ParkingLot
{
    public class MoreEmptyPositionParkingManager : BaseParkingManager
    {
        public MoreEmptyPositionParkingManager(List<ParkingLot> parkingLots) : base(parkingLots)
        {
        }

        protected override ParkingLot GetLotForParking()
        {
            return ParkingLots.OrderByDescending(_ => _.CountEmptyPosition()).First();
        }
    }
}