using System;

namespace ParkingLot
{
    public class NotEnoughCapacityException : Exception
    {
        public NotEnoughCapacityException(string message) : base(message)
        {
        }
    }
}