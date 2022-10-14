using System;

namespace ParkingLot
{
    public class ParkedException : Exception
    {
        public ParkedException(string message) : base(message)
        {
        }
    }
}