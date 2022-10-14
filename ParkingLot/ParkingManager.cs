using System.Collections.Generic;
using System.Linq;

namespace ParkingLot
{
    public class ParkingManager
    {
        private List<ParkingLot> parkingLots;

        public ParkingManager(List<ParkingLot> parkingLots)
        {
            this.parkingLots = parkingLots;
        }

        public string Parking(Car car)
        {
            return parkingLots.First(_ => _.IsNotFull()).Parking(car);
        }

        public List<string> Parking(List<Car> cars)
        {
            return parkingLots[0].Parking(cars);
        }
    }
}