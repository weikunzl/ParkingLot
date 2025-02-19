using System;
using System.Collections.Generic;

namespace ParkingLotTest
{
    using ParkingLot;
    using Xunit;

    public class ParkingManagerTest
    {
        [Fact]
        public void Should_contain_specify_parking_body_when_get_management_list_given_already_added()
        {
            // given
            ParkingManager parkingManager = new ParkingManager();
            BaseParkingBoy parkingBoy = new SequenceParkingBoy(new List<ParkingLot>());
            parkingManager.AddParkingBoy(parkingBoy);

            // when
            List<BaseParkingBoy> managedParkingBoys = parkingManager.GetManagedParkingBoys();

            // then
            Assert.Contains(parkingBoy, managedParkingBoys);
        }

        [Fact]
        public void Should_return_ticket_when_park_a_car()
        {
            // given
            Car car = new Car("AE00000");
            var parkingLots = new List<ParkingLot>() { new ParkingLot() };
            ParkingManager parkingManager = new ParkingManager();
            BaseParkingBoy parkingBoy = new SequenceParkingBoy(parkingLots);

            // when
            string ticket = parkingManager.Parking(parkingBoy, car);

            // then
            Assert.NotNull(ticket);
        }

        [Fact]
        public void Should_return_car_when_pick_up_a_car_given_ticket()
        {
            // given
            Car car = new Car("AE00000");
            var parkingLots = new List<ParkingLot>() { new ParkingLot() };
            string ticketNumber = "T000001";
            parkingLots[0].GetParkingSpaces().Add(ticketNumber, car);
            ParkingManager parkingManager = new ParkingManager();
            BaseParkingBoy parkingBoy = new SequenceParkingBoy(parkingLots);

            // when
            Car parkedCar = parkingManager.Pickup(parkingBoy, ticketNumber);

            // then
            Assert.Equal(car, parkedCar);
        }

        [Fact]
        public void Should_return_a_parking_ticket_number_when_parking_given_a_car()
        {
            List<ParkingLot> parkingLots = new List<ParkingLot>()
            {
                new ParkingLot(2),
            };
            ParkingManager sequenceParkingBoy = new ParkingManager(parkingLots);

            string ticketNumber = sequenceParkingBoy.Parking(new Car("AE00003"));

            Assert.Matches("^T\\d{18}$", ticketNumber);
        }

        [Fact]
        public void Should_return_2_parking_ticket_number_when_parking_given_2_car()
        {
            List<ParkingLot> parkingLots = new List<ParkingLot>()
            {
                new ParkingLot(2),
            };
            ParkingManager sequenceParkingBoy = new ParkingManager(parkingLots);
            var cars = new List<Car>
            {
                new Car("AE00001"),
                new Car("AE00002"),
            };

            List<string> ticketNumbers = sequenceParkingBoy.Parking(cars);

            Assert.Equal(2, ticketNumbers.Count);
        }

        [Fact]
        public void Should_parking_sequentially_to_other_parking_lot_when_parking_given_2_parkingLots()
        {
            List<ParkingLot> parkingLots = new List<ParkingLot>()
            {
                new ParkingLot(2),
                new ParkingLot(10),
            };
            ParkingManager sequenceParkingBoy = new ParkingManager(parkingLots);
            List<string> ticketNumbers = sequenceParkingBoy.Parking(new List<Car>
            {
                new Car("AE00001"),
                new Car("AE00002"),
            });

            string ticketNumber = sequenceParkingBoy.Parking(new Car("AE00003"));

            Assert.Equal(2, ticketNumbers.Count);
            Assert.Matches("^T\\d{18}$", ticketNumber);
        }

        [Fact]
        public void Should_parking_sequentially_to_other_parking_lot_when_parking_given_3_cars()
        {
            List<ParkingLot> parkingLots = new List<ParkingLot>()
            {
                new ParkingLot(2),
                new ParkingLot(10),
            };
            ParkingManager sequenceParkingBoy = new ParkingManager(parkingLots);

            List<string> ticketNumbers = sequenceParkingBoy.Parking(new List<Car>
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
            ParkingManager sequenceParkingBoy = new ParkingManager(parkingLots);
            var ticketNumber = sequenceParkingBoy.Parking(new Car("AE8888"));

            Car car = sequenceParkingBoy.Pickup(ticketNumber);

            Assert.Equal("AE8888", car.PlantNumber);
        }

        [Fact]
        public void Should_throw_parked_car_exception_when_parking_given_a_parked_car()
        {
            ParkingManager sequenceParkingBoy = new ParkingManager(new List<ParkingLot>()
            {
                new ParkingLot(2),
                new ParkingLot(10),
            });
            sequenceParkingBoy.Parking(new Car("AE8888"));

            Action parkingAction = () => sequenceParkingBoy.Parking(new Car("AE8888"));

            var parkedException = Assert.Throws<ParkedException>(parkingAction);
            Assert.Equal("Car parked exception", parkedException.Message);
        }

        [Fact]
        public void Should_throw_WrongTicketException_when_parking_given_a_wrong_ticket()
        {
            ParkingManager sequenceParkingBoy = new ParkingManager(new List<ParkingLot>()
            {
                new ParkingLot(2),
                new ParkingLot(10),
            });

            Action pickupAction = () => sequenceParkingBoy.Pickup("T11111");

            var wrongTicketException = Assert.Throws<WrongTicketException>(pickupAction);
            Assert.Equal("Unrecognized parking ticket.", wrongTicketException.Message);
        }

        [Fact]
        public void Should_throw_WrongTicketException_when_parking_given_a_ticket_has_been_used()
        {
            ParkingManager sequenceParkingBoy = new ParkingManager(new List<ParkingLot>()
            {
                new ParkingLot(2),
                new ParkingLot(10),
            });
            var ticket = sequenceParkingBoy.Parking(new Car("AE8888"));
            Car car = sequenceParkingBoy.Pickup(ticket);

            Action pickupAction = () => sequenceParkingBoy.Pickup(ticket);

            Assert.Equal("AE8888", car.PlantNumber);
            var wrongTicketException = Assert.Throws<WrongTicketException>(pickupAction);
            Assert.Equal("Unrecognized parking ticket.", wrongTicketException.Message);
        }

        [Fact]
        public void Should_throw_TicketNoProvideException_when_parking_given_NO_ticket()
        {
            ParkingManager sequenceParkingBoy = new ParkingManager(new List<ParkingLot>()
            {
                new ParkingLot(2),
                new ParkingLot(10),
            });

            Action pickupAction = () => sequenceParkingBoy.Pickup(null);

            var ticketNoProvideException = Assert.Throws<TicketNoProvideException>(pickupAction);
            Assert.Equal("Please provide your parking ticket.", ticketNoProvideException.Message);
        }

        [Fact]
        public void Should_throw_NotEnoughCapacityException_when_parking_given_parking_lot_capacity_2()
        {
            ParkingManager sequenceParkingBoy = new ParkingManager(new List<ParkingLot>()
            {
                new ParkingLot(1),
                new ParkingLot(1),
            });
            var cars = new List<Car>
            {
                new Car("AE00001"),
                new Car("AE00002"),
            };
            var ticketNumbers = sequenceParkingBoy.Parking(cars);

            Action parkingAction = () => sequenceParkingBoy.Parking(new Car("AE00003"));

            Assert.Equal(2, ticketNumbers.Count);
            var notEnoughCapacityException = Assert.Throws<NotEnoughCapacityException>(parkingAction);
            Assert.Equal("Not enough position.", notEnoughCapacityException.Message);
        }
    }
}