using System;
using System.Collections.Generic;

namespace ParkingLotTest
{
    using ParkingLot;
    using Xunit;

    public class MoreEmptyPositionParkingBoyTest
    {
        [Fact]
        public void Should_return_a_parking_ticket_number_when_parking_given_a_car()
        {
            List<ParkingLot> parkingLots = new List<ParkingLot>()
            {
                new ParkingLot(2),
            };
            MoreEmptyPositionParkingBoy parkingBoy = new MoreEmptyPositionParkingBoy(parkingLots);

            string ticketNumber = parkingBoy.Parking(new Car("AE00003"));

            Assert.Matches("^T\\d{18}$", ticketNumber);
        }

        [Fact]
        public void Should_return_2_parking_ticket_number_when_parking_given_2_car()
        {
            List<ParkingLot> parkingLots = new List<ParkingLot>()
            {
                new ParkingLot(2),
            };
            MoreEmptyPositionParkingBoy parkingBoy = new MoreEmptyPositionParkingBoy(parkingLots);
            var cars = new List<Car>
            {
                new Car("AE00001"),
                new Car("AE00002"),
            };

            List<string> ticketNumbers = parkingBoy.Parking(cars);

            Assert.Equal(2, ticketNumbers.Count);
        }

        [Fact]
        public void Should_parking_more_empty_position_lot_when_parking_given_2_parkingLots_and_a_car()
        {
            List<ParkingLot> parkingLots = new List<ParkingLot>()
            {
                new ParkingLot(2),
                new ParkingLot(10),
            };
            MoreEmptyPositionParkingBoy parkingBoy = new MoreEmptyPositionParkingBoy(parkingLots);

            string ticketNumber = parkingBoy.Parking(new Car("AE00003"));

            Assert.Equal(2, parkingLots[0].CountEmptyPosition());
            Assert.Equal(9, parkingLots[1].CountEmptyPosition());
        }

        [Fact]
        public void Should_parking_sequentially_to_other_parking_lot_when_parking_given_3_cars()
        {
            List<ParkingLot> parkingLots = new List<ParkingLot>()
            {
                new ParkingLot(2),
                new ParkingLot(10),
            };
            MoreEmptyPositionParkingBoy parkingBoy = new MoreEmptyPositionParkingBoy(parkingLots);

            List<string> ticketNumbers = parkingBoy.Parking(new List<Car>
            {
                new Car("AE00001"),
                new Car("AE00002"),
                new Car("AE00003"),
            });

            Assert.Equal(3, ticketNumbers.Count);
        }

        [Fact]
        public void Should_pickup_a_parked_car_when_pickup_at_parking_lot_given_a_ticket()
        {
            List<ParkingLot> parkingLots = new List<ParkingLot>()
            {
                new ParkingLot(2),
                new ParkingLot(10),
            };
            MoreEmptyPositionParkingBoy parkingBoy = new MoreEmptyPositionParkingBoy(parkingLots);
            var ticketNumber = parkingBoy.Parking(new Car("AE8888"));

            Car car = parkingBoy.Pickup(ticketNumber);

            Assert.Equal("AE8888", car.PlantNumber);
        }

        [Fact]
        public void Should_throw_parked_car_exception_when_parking_given_a_parked_car()
        {
            MoreEmptyPositionParkingBoy parkingBoy = new MoreEmptyPositionParkingBoy(new List<ParkingLot>()
            {
                new ParkingLot(2),
                new ParkingLot(10),
            });
            parkingBoy.Parking(new Car("AE8888"));

            Action parkingAction = () => parkingBoy.Parking(new Car("AE8888"));

            var parkedException = Assert.Throws<ParkedException>(parkingAction);
            Assert.Equal("Car parked exception", parkedException.Message);
        }

        [Fact]
        public void Should_throw_WrongTicketException_when_parking_given_a_wrong_ticket()
        {
            MoreEmptyPositionParkingBoy parkingBoy = new MoreEmptyPositionParkingBoy(new List<ParkingLot>()
            {
                new ParkingLot(2),
                new ParkingLot(10),
            });

            Action pickupAction = () => parkingBoy.Pickup("T11111");

            var wrongTicketException = Assert.Throws<WrongTicketException>(pickupAction);
            Assert.Equal("Unrecognized parking ticket.", wrongTicketException.Message);
        }

        [Fact]
        public void Should_throw_WrongTicketException_when_parking_given_a_ticket_has_been_used()
        {
            MoreEmptyPositionParkingBoy parkingBoy = new MoreEmptyPositionParkingBoy(new List<ParkingLot>()
            {
                new ParkingLot(2),
                new ParkingLot(10),
            });
            var ticket = parkingBoy.Parking(new Car("AE8888"));
            Car car = parkingBoy.Pickup(ticket);

            Action pickupAction = () => parkingBoy.Pickup(ticket);

            Assert.Equal("AE8888", car.PlantNumber);
            var wrongTicketException = Assert.Throws<WrongTicketException>(pickupAction);
            Assert.Equal("Unrecognized parking ticket.", wrongTicketException.Message);
        }

        [Fact]
        public void Should_throw_TicketNoProvideException_when_parking_given_NO_ticket()
        {
            MoreEmptyPositionParkingBoy parkingBoy = new MoreEmptyPositionParkingBoy(new List<ParkingLot>()
            {
                new ParkingLot(2),
                new ParkingLot(10),
            });

            Action pickupAction = () => parkingBoy.Pickup(null);

            var ticketNoProvideException = Assert.Throws<TicketNoProvideException>(pickupAction);
            Assert.Equal("Please provide your parking ticket.", ticketNoProvideException.Message);
        }

        [Fact]
        public void Should_throw_NotEnoughCapacityException_when_parking_given_parking_lot_capacity_2()
        {
            MoreEmptyPositionParkingBoy parkingBoy = new MoreEmptyPositionParkingBoy(new List<ParkingLot>()
            {
                new ParkingLot(1),
                new ParkingLot(1),
            });
            var cars = new List<Car>
            {
                new Car("AE00001"),
                new Car("AE00002"),
            };
            var ticketNumbers = parkingBoy.Parking(cars);

            Action parkingAction = () => parkingBoy.Parking(new Car("AE00003"));

            Assert.Equal(2, ticketNumbers.Count);
            var notEnoughCapacityException = Assert.Throws<NotEnoughCapacityException>(parkingAction);
            Assert.Equal("Not enough position.", notEnoughCapacityException.Message);
        }
    }
}