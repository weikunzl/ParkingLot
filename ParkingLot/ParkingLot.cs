using System.Collections.Generic;

namespace ParkingLot
{
    using System;
    public class ParkingLot
    {
        private Dictionary<string, Car> parkingLots;
        public string Parking(Car car)
        {
            string ticket = TicketGenerator.CreateTicket();
            parkingLots.Add(ticket, car);
            return ticket;
        }
    }
}
