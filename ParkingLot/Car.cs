using System.Collections.Generic;

namespace ParkingLot
{
    public class Car
    {
        public Car(string plantNumber)
        {
            PlantNumber = plantNumber;
        }

        public string PlantNumber { get; }

        public override bool Equals(object obj)
        {
            return PlantNumber.Equals(((Car)obj)?.PlantNumber);
        }
    }
}