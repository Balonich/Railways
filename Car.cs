using System;

namespace Railways
{
    public partial class Train
    {
        protected abstract class Car : ICarriable
        {
            protected readonly int ID;
            public int Volume { get; private set; }
            public int AmountOfCargo { get; protected set; }
            public bool IsEmpty
            {
                get
                {
                    if (AmountOfCargo == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            public bool IsFull
            {
                get
                {
                    if (AmountOfCargo == Volume)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            Random randomID = new Random();

            public Car(int volume, int amount)
            {
                Volume = volume;
                AmountOfCargo = amount;
                ID = randomID.Next();
            }

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