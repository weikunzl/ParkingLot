namespace ParkingLotTest
{
    using ParkingLot;
    using Xunit;

    public class ParkingLotTest
    {
        [Fact]
        public void Should_return_a_parking_ticket_number_when_parking_given_a_car()
        {
            var parkingLot = new ParkingLot();

            string ticketNumber = parkingLot.Parking(new Car("AE8888"));

            Assert.Matches("^T\\d{18}$", ticketNumber);
        }
    }
}
