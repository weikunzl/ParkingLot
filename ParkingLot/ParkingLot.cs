﻿using System.Collections.Generic;
using System.Linq;

namespace ParkingLot
{
    using System;
    public class ParkingLot
    {
        private Dictionary<string, Car> parkingLots = new Dictionary<string, Car>();
        public string Parking(Car car)
        {
            if (parkingLots.ContainsValue(car))
            {
                throw new ParkedException("Car parked exception");
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
            return cars.Select(Parking).ToList();
        }
    }
}
