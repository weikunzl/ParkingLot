using System.Collections.Generic;
using System.Linq;

namespace ParkingLot
{
    public abstract class BaseParkingManager
    {
        public BaseParkingManager(List<ParkingLot> parkingLots)
        {
            this.ParkingLots = parkingLots;
        }

        protected List<ParkingLot> ParkingLots { get; set; }

        public string Parking(Car car)
        {
            var parkingLot = ParkingLots.Find(_ => !_.IsFull());
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

        public Car Pickup(string ticketNumber)
        {
            if (ticketNumber == null)
            {
                throw new TicketNoProvideException("Please provide your parking ticket.");
            }

            ParkingLot parkingLot = ParkingLots.Find(_ => _.GetParkingSpaces().ContainsKey(ticketNumber));
            if (parkingLot == null)
            {
                throw new WrongTicketException("Unrecognized parking ticket.");
            }

            return parkingLot.Pickup(ticketNumber);
        }
    }
}