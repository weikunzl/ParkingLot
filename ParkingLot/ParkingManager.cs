using System.Collections.Generic;

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
            return parkingLots[0].Parking(car);
        }

        public List<string> Parking(List<Car> cars)
        {
            return parkingLots[0].Parking(cars);
        }
    }
}