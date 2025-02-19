using System;
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
                new Car("AE00003"),
            };
            var ticketNumbers = parkingLot.Parking(cars);

            Assert.Equal(3, ticketNumbers.Count);
        }

        [Fact]
        public void Should_throw_parked_car_exception_when_parking_given_a_parked_car()
        {
            var parkingLot = new ParkingLot();
            parkingLot.Parking(new Car("AE8888"));

            Action parkingAction = () => parkingLot.Parking(new Car("AE8888"));

            var parkedException = Assert.Throws<ParkedException>(parkingAction);
            Assert.Equal("Car parked exception", parkedException.Message);
        }

        [Fact]
        public void Should_throw_WrongTicketException_when_parking_given_a_wrong_ticket()
        {
            var parkingLot = new ParkingLot();

            Action pickupAction = () => parkingLot.Pickup("T11111");

            var wrongTicketException = Assert.Throws<WrongTicketException>(pickupAction);
            Assert.Equal("Unrecognized parking ticket.", wrongTicketException.Message);
        }

        [Fact]
        public void Should_return_empty_when_parking_given_a_ticket_has_been_used()
        {
            var parkingLot = new ParkingLot();
            var ticket = parkingLot.Parking(new Car("AE8888"));
            Car car = parkingLot.Pickup(ticket);

            Action pickupAction = () => parkingLot.Pickup(ticket);

            Assert.Equal("AE8888", car.PlantNumber);
            var wrongTicketException = Assert.Throws<WrongTicketException>(pickupAction);
            Assert.Equal("Unrecognized parking ticket.", wrongTicketException.Message);
        }

        [Fact]
        public void Should_throw_TicketNoProvideException_when_parking_given_NO_ticket()
        {
            var parkingLot = new ParkingLot();

            Action pickupAction = () => parkingLot.Pickup(null);

            var ticketNoProvideException = Assert.Throws<TicketNoProvideException>(pickupAction);
            Assert.Equal("Please provide your parking ticket.", ticketNoProvideException.Message);
        }

        [Fact]
        public void Should_return_empty_when_parking_given_parking_lot_capacity_2()
        {
            var parkingLot = new ParkingLot(2);
            var cars = new List<Car>
            {
                new Car("AE00001"),
                new Car("AE00002"),
            };
            var ticketNumbers = parkingLot.Parking(cars);

            Action parkingAction = () => parkingLot.Parking(new Car("AE00003"));

            Assert.Equal(2, ticketNumbers.Count);
            var notEnoughCapacityException = Assert.Throws<NotEnoughCapacityException>(parkingAction);
            Assert.Equal("Not enough position.", notEnoughCapacityException.Message);
        }
    }
}