using System.Collections.Generic;
using System.Linq;

namespace ParkingLot
{
    public class SequenceParkingManager : BaseParkingManager
    {
        public SequenceParkingManager(List<ParkingLot> parkingLots) : base(parkingLots)
        {
        }
    }
}