using System.Collections.Generic;

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

        [Fact]
        public void Should_pickup_a_parked_car_when_pickup_at_parking_lot_given_a_ticket()
        {
            var parkingLot = new ParkingLot();
            var ticketNumber = parkingLot.Parking(new Car("AE8888"));

            Car car = parkingLot.Pickup(ticketNumber);

            Assert.Equal("AE8888", car.PlantNumber);
        }

        [Fact]
        public void Should_return_three_parking_ticket_number_when_parking_given_three_car()
        {
            var parkingLot = new ParkingLot();

            var cars = new List<Car>
            {
                new Car("AE00001"),
                new Car("AE00002"),
                new Car("AE00003")
            };
            var ticketNumbers = parkingLot.Parking(cars);

            Assert.Equal(3, ticketNumbers.Count);
        }
    }
}
