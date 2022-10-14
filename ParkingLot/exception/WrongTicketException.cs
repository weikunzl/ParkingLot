using System;

namespace ParkingLot
{
    public class WrongTicketException : Exception
    {
        public WrongTicketException(string message) : base(message)
        {
        }
    }
}