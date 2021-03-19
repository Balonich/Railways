namespace RailwaysLibrary
{
    using System;

    public sealed class CargoTrain : Train
    {
        public CargoTrain(int id)
            : base(id)
        {
            CarsAllowedAmount = 7;
        }

        public override void AddCar(int volume, int amountToAdd)
        {
            if (AmountOfCars < CarsAllowedAmount)
            {
                if (volume >= amountToAdd)
                {
                    Cars.Add(new CargoCar(volume, amountToAdd));
                }
                else
                {
                    Console.WriteLine("Общее количество мест меньше количества людей в добавляемом вагоне!");
                }
            }
            else
            {
                Console.WriteLine("Поезд не может иметь больше вагонов.");
            }
        }

        public override void DisplayInfo()
        {
            Console.Write("Грузовой поезд ");
            base.DisplayInfo();
            Console.WriteLine();
        }
    }
}