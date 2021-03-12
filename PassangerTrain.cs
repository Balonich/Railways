using System;

namespace Railways
{
    public sealed class PassangerTrain : Train
    {
        public PassangerTrain(int id) : base(id)
        {
            _carsAllowedAmount = 4;
        }

        public override void AddCar(int volume, int amountToAdd)
        {
            if (AmountOfCars < _carsAllowedAmount)
            {
                if (volume >= amountToAdd)
                {
                    _cars.Add(new PassangerCar(volume, amountToAdd));
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
            Console.Write("Пассажирский поезд ");
            base.DisplayInfo();
        }
    }
}