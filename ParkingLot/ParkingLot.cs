using System.Collections.Generic;
using System.Linq;

namespace ParkingLot
{
    using System;
    public class ParkingLot
    {
        private Dictionary<string, Car> parkingLots;

        private int capacity = int.MaxValue;

        public ParkingLot()
        {
            parkingLots = new Dictionary<string, Car>();
        }

        public ParkingLot(int capacity)
        {
            this.capacity = capacity;
            parkingLots = new Dictionary<string, Car>(capacity);
        }

        public string Parking(Car car)
        {
            if (parkingLots.ContainsValue(car))
            {
                throw new ParkedException("Car parked exception");
            }

            if (parkingLots.Count >= capacity)
            {
                return null;
            }

            string ticket = TicketGenerator.CreateTicket();
            parkingLots.Add(ticket, car);
            return ticket;
        }

        public Car Pickup(string ticket)
        {
            if (parkingLots.ContainsKey(ticket))
            {
                var car = parkingLots[ticket];
                parkingLots.Remove(ticket);
                return car;
            }

            return null;
        }

        public List<string> Parking(List<Car> cars)
        {
            return cars.Select(Parking).Where(_ => _ != null).ToList();
        }
    }
}
