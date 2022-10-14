using System;

namespace ParkingLot
{
    public class TicketGenerator
    {
        public static string CreateTicket()
        {
            return $"T{Timestamp()}";
        }

        private static string Timestamp()
        {
            return DateTime.Now.ToFileTimeUtc().ToString();
        }
    }
}