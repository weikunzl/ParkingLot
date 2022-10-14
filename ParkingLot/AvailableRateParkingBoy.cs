using System.Collections.Generic;
using System.Linq;

namespace ParkingLot
{
    public class AvailableRateParkingBoy : BaseParkingBoy
    {
        public AvailableRateParkingBoy(List<ParkingLot> parkingLots) : base(parkingLots)
        {
        }

        protected override ParkingLot GetLotForParking()
        {
            return ParkingLots.OrderByDescending(_ => _.CountEmptyPosition() / _.GetCapacity()).First();
        }
    }
}