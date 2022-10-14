using System;

namespace ParkingLot
{
    public class TicketNoProvideException : Exception
    {
        public TicketNoProvideException(string message) : base(message)
        {
        }
    }
}