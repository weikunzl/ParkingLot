using System.Collections.Generic;
using System.Linq;

namespace ParkingLot
{
    using System;
    public class ParkingLot
    {
        private Dictionary<string, Car> parkingSpaces;

        private int capacity = 10;

        public ParkingLot()
        {
            parkingSpaces = new Dictionary<string, Car>(capacity);
        }

        public ParkingLot(int capacity)
        {
            this.capacity = capacity;
            parkingSpaces = new Dictionary<string, Car>(capacity);
        }

        public string Parking(Car car)
        {
            if (parkingSpaces.ContainsValue(car))
            {
                throw new ParkedException("Car parked exception");
            }

            if (!IsNotFull())
            {
                throw new NotEnoughCapacityException("Not enough position.");
            }

            string ticket = TicketGenerator.CreateTicket();
            parkingSpaces.Add(ticket, car);
            return ticket;
        }

        public Car Pickup(string ticket)
        {
            if (ticket == null)
            {
                throw new TicketNoProvideException("Please provide your parking ticket.");
            }

            if (parkingSpaces.ContainsKey(ticket))
            {
                return PickUpFromParkingLot(ticket);
            }

            throw new WrongTicketException("Unrecognized parking ticket.");
        }

        public List<string> Parking(List<Car> cars)
        {
            return cars.Select(Parking).Where(_ => _ != null).ToList();
        }

        public bool IsNotFull()
        {
            return this.parkingSpaces.Count < capacity;
        }

        private Car PickUpFromParkingLot(string ticket)
        {
            var car = parkingSpaces[ticket];
            parkingSpaces.Remove(ticket);
            return car;
        }
    }
}
