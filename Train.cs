using System;
using System.Collections.Generic;

namespace Railways
{
    public abstract partial class Train : BasicIdentifiedObject, ICarriable, IDisplayable
    {
        protected List<Car> _cars;
        protected int _carsAllowedAmount;
        public override int ID { get; }
        public int AmountOfCars { get => _cars.Count; }
        public int TotalCarsVolume
        {
            get
            {
                int totalVolumeSum = 0;
                foreach (Car car in _cars)
                {
                    totalVolumeSum += car.Volume;
                }
                return totalVolumeSum;
            }
        }
        public int TotalCarsCargo
        {
            get
            {
                int totalCargoSum = 0;
                foreach (Car car in _cars)
                {
                    totalCargoSum += car.AmountOfCargo;
                }
                return totalCargoSum;
            }
        }
        public static int TrainsCounter { get; private set; } = 0;

        public Train(int id)
        {
            ID = id;
            TrainsCounter++;
            _carsAllowedAmount = 5;
            _cars = new List<Car>();
        }

        public abstract void AddCar(int volume, int amountToAdd);

        public void RemoveAllCars()
        {
            if (AmountOfCars == 0)
            {
                Console.WriteLine("У поезда нет вагонов.");
            }
            else
            {
                _cars.Clear();
            }
        }

        public void RemoveCar()
        {
            if (AmountOfCars == 0)
            {
                Console.WriteLine("У поезда нет вагонов.");
            }
            else
            {
                Car carToRemove = FindCarWithLowestCargo(allowZeros: true);
                Console.Write("Был убран ");
                carToRemove.DisplayInfo();
                AddCargo(carToRemove.AmountOfCargo);
                _cars.Remove(carToRemove);
            }
        }

        public void AddCargo(int amountToAdd)
        {
            if (AmountOfCars == 0)
            {
                Console.WriteLine("У поезда нет вагонов.");
            }
            else
            {
                while (amountToAdd > 0)
                {
                    try
                    {
                        Car carToAddCargo = FindCarWithLargestCargo();
                        if (amountToAdd > carToAddCargo.Volume - carToAddCargo.AmountOfCargo)
                        {
                            amountToAdd -= (carToAddCargo.Volume - carToAddCargo.AmountOfCargo);
                            carToAddCargo.AddCargo(carToAddCargo.Volume - carToAddCargo.AmountOfCargo);
                        }
                        else
                        {
                            carToAddCargo.AddCargo(amountToAdd);
                            amountToAdd = 0;
                        }
                    }
                    catch (ArgumentNullException e)
                    {
                        Console.WriteLine("Похоже, что все вагоны уже заполнены. Остаток: " + amountToAdd);
                        break;
                    }
                    catch (NullReferenceException e)
                    {
                        Console.WriteLine("Похоже, что все вагоны уже заполнены. Остаток: " + amountToAdd);
                        break;
                    }
                }
            }
        }

        public void RemoveCargo(int amountToRemove)
        {
            if (AmountOfCars == 0)
            {
                Console.WriteLine("У поезда нет вагонов.");
            }
            else
            {
                while (amountToRemove > 0)
                {
                    try
                    {
                        Car carToRemoveCargo = FindCarWithLowestCargo();
                        if (amountToRemove > carToRemoveCargo.AmountOfCargo)
                        {
                            amountToRemove -= carToRemoveCargo.AmountOfCargo;
                            carToRemoveCargo.RemoveCargo(carToRemoveCargo.AmountOfCargo);
                        }
                        else
                        {
                            carToRemoveCargo.RemoveCargo(amountToRemove);
                            amountToRemove = 0;
                        }
                    }
                    catch (ArgumentNullException e)
                    {
                        Console.WriteLine("Похоже, что все вагоны уже пустые.");
                        break;
                    }
                }
            }
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"№{ID}");
        }

        public void DisplayDetailedInfo()
        {
            DisplayInfo();
            if (AmountOfCars == 0)
            {
                Console.WriteLine("Вагонов не имеет.");
            }
            else
            {
                Console.WriteLine($"Имеет {AmountOfCars} вагонов.");
                Console.Write($"Заполненность: {TotalCarsCargo} из {TotalCarsVolume} ");
                if (this is CargoTrain)
                {
                    Console.WriteLine("тонн");
                }
                else
                {
                    Console.WriteLine("человек");
                }

                foreach (Car car in _cars)
                {
                    car.DisplayInfo();
                }
            }
        }

        private Car FindCarWithLargestCargo()
        {
            Car carWithLargestCargo = _cars[0];

            for (int i = 1; i < AmountOfCars; i++)
            {
                if (!_cars[i].IsFull)
                {
                    if (carWithLargestCargo.IsFull)
                    {
                        carWithLargestCargo = _cars[i];
                    }
                    else
                    {
                        if ((carWithLargestCargo.Volume - carWithLargestCargo.AmountOfCargo) > (_cars[i].Volume - _cars[i].AmountOfCargo))
                        {
                            carWithLargestCargo = _cars[i];
                        }
                    }
                }
            }

            if (carWithLargestCargo.IsFull)
            {
                return null;
            }
            else
            {
                return carWithLargestCargo;
            }
        }

        private Car FindCarWithLowestCargo(bool allowZeros = false)
        {
            Car carWithLowestCargo = _cars[0];

            for (int i = 1; i < AmountOfCars; i++)
            {
                if (carWithLowestCargo.IsEmpty && allowZeros)
                {
                    return carWithLowestCargo;
                }

                if (!_cars[i].IsEmpty)
                {
                    if (carWithLowestCargo.IsEmpty)
                    {
                        carWithLowestCargo = _cars[i];
                    }
                    else
                    {
                        if (carWithLowestCargo.AmountOfCargo > _cars[i].AmountOfCargo)
                        {
                            carWithLowestCargo = _cars[i];
                        }
                    }
                }
            }
            return carWithLowestCargo;
        }
    }
}