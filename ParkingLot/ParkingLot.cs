using System.Collections.Generic;
using System.Linq;

namespace ParkingLot
{
    using System;
    public class ParkingLot
    {
        private Dictionary<string, Car> parkingLots = new Dictionary<string, Car>();
        public string Parking(Car car)
        {
            string ticket = TicketGenerator.CreateTicket();
            parkingLots.Add(ticket, car);
            return ticket;
        }

        public Car Pickup(string ticket)
        {
            return parkingLots[ticket];
        }

        public List<string> Parking(List<Car> cars)
        {
            return cars.Select(Parking).ToList();
        }
    }
}
