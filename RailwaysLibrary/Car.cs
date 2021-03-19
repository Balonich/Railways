namespace RailwaysLibrary
{
    using System;

    public partial class Train
    {
        protected abstract class Car : ICarriable
        {
            private readonly Random randomID = new ();

            protected Car(int volume, int amount)
            {
                Volume = volume;
                AmountOfCargo = amount;
                ID = randomID.Next();
            }

            public int Volume { get; }

            public int AmountOfCargo { get; protected set; }

            public bool IsEmpty
            {
                get => AmountOfCargo == 0;
            }

            public bool IsFull
            {
                get => AmountOfCargo == Volume;
            }

            protected int ID { get; }

            public void AddCargo(int amountToAdd)
            {
                AmountOfCargo += amountToAdd;
                Console.WriteLine($"В вагон №{ID} добавлено {amountToAdd} ед.");
                DisplayInfo();
            }

            public void RemoveCargo(int amountToRemove)
            {
                AmountOfCargo -= amountToRemove;
                Console.WriteLine($"Из вагона №{ID} убрано {amountToRemove} ед.");
                DisplayInfo();
            }

            public abstract void DisplayInfo();
        }
    }
}