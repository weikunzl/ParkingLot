using System.Collections.Generic;
using System.Linq;

namespace ParkingLot
{
    public class ParkingManager
    {
        private List<BaseParkingBoy> parkingBoys = new List<BaseParkingBoy>();

        public void AddParkingBoy(BaseParkingBoy parkingBoy)
        {
            parkingBoys.Add(parkingBoy);
        }

        public string Parking(BaseParkingBoy parkingBoy, Car car)
        {
            return parkingBoy.Parking(car);
        }

        public Car Pickup(BaseParkingBoy parkingBoy, string ticketNumber)
        {
            return parkingBoy.Pickup(ticketNumber);
        }

        public List<BaseParkingBoy> GetManagedParkingBoys()
        {
            return parkingBoys;
        }
    }
}