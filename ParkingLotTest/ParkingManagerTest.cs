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
    }
}