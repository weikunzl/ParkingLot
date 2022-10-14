using System.Collections.Generic;
using System.Linq;

namespace ParkingLot
{
    public class SequenceParkingBoy : BaseParkingBoy
    {
        public SequenceParkingBoy(List<ParkingLot> parkingLots) : base(parkingLots)
        {
        }

        protected override ParkingLot GetLotForParking()
        {
            return ParkingLots.Find(_ => !_.IsFull());
        }
    }
}