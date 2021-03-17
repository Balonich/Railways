using System;

namespace RailwaysLibrary
{
    public sealed class CargoTrain : Train
    {
        public CargoTrain(int id) : base(id)
        {
            _carsAllowedAmount = 7;
        }

        public override void AddCar(int volume, int amountToAdd)
        {
            if (AmountOfCars < _carsAllowedAmount)
            {
                if (volume >= amountToAdd)
                {
                    _cars.Add(new CargoCar(volume, amountToAdd));
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