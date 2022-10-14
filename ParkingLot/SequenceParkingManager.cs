using System.Collections.Generic;
using System.Linq;

namespace ParkingLot
{
    public class SequenceParkingManager : BaseParkingManager
    {
        public SequenceParkingManager(List<ParkingLot> parkingLots) : base(parkingLots)
        {
        }

        protected override ParkingLot GetLotForParking()
        {
            return ParkingLots.Find(_ => !_.IsFull());
        }
    }
}