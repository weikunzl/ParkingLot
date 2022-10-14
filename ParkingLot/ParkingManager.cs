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
            var parkingLot = parkingLots.Find(_ => _.IsNotFull());
            if (parkingLot == null)
            {
                throw new NotEnoughCapacityException("Not enough position.");
            }

            return parkingLot.Parking(car);
        }

        public List<string> Parking(List<Car> cars)
        {
            return cars.Select(Parking).ToList();
        }
    }
}