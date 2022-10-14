using System.Collections.Generic;
using System.Linq;

namespace ParkingLot
{
    public class ParkingManager : SequenceParkingBoy
    {
        private List<BaseParkingBoy> parkingBoys = new List<BaseParkingBoy>();

        public ParkingManager(List<ParkingLot> parkingLots) : base(parkingLots)
        {
        }

        public ParkingManager() : base(new List<ParkingLot>())
        {
        }

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