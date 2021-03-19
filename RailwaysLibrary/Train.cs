namespace RailwaysLibrary
{
    using System;
    using System.Collections.Generic;

    public abstract partial class Train : BasicIdentifiedObject, ICarriable, IDisplayable
    {
        private List<Car> cars;

        protected Train(int id)
        {
            ID = id;
            TrainsCounter++;
            CarsAllowedAmount = 5;
            Cars = new List<Car>();
        }

        public static int TrainsCounter { get; private set; } = 0;

        public override int ID { get; }

        public int AmountOfCars { get => Cars.Count; }

        public int TotalCarsVolume
        {
            get
            {
                int totalVolumeSum = 0;
                foreach (Car car in Cars)
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
                foreach (Car car in Cars)
                {
                    totalCargoSum += car.AmountOfCargo;
                }

                return totalCargoSum;
            }
        }

        protected int CarsAllowedAmount { get; set; }

        protected List<Car> Cars
        {
            get => cars;
            set => cars = value;
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
                Cars.Clear();
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
                Cars.Remove(carToRemove);
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
                            amountToAdd -= carToAddCargo.Volume - carToAddCargo.AmountOfCargo;
                            carToAddCargo.AddCargo(carToAddCargo.Volume - carToAddCargo.AmountOfCargo);
                        }
                        else
                        {
                            carToAddCargo.AddCargo(amountToAdd);
                            amountToAdd = 0;
                        }
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Похоже, что все вагоны уже заполнены. Остаток: " + amountToAdd);
                        break;
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Похоже, что все вагоны уже заполнены. Остаток: " + amountToAdd);
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Неизвестная ошибка...");
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
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Похоже, что все вагоны уже пустые.");
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Неизвестная ошибка...");
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

                foreach (Car car in Cars)
                {
                    car.DisplayInfo();
                }
            }
        }

        private Car FindCarWithLargestCargo()
        {
            Car carWithLargestCargo = Cars[0];

            for (int i = 1; i < AmountOfCars; i++)
            {
                if (!Cars[i].IsFull)
                {
                    if (carWithLargestCargo.IsFull)
                    {
                        carWithLargestCargo = Cars[i];
                    }
                    else
                    {
                        if ((carWithLargestCargo.Volume - carWithLargestCargo.AmountOfCargo) > (Cars[i].Volume - Cars[i].AmountOfCargo))
                        {
                            carWithLargestCargo = Cars[i];
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
            Car carWithLowestCargo = Cars[0];

            for (int i = 1; i < AmountOfCars; i++)
            {
                if (carWithLowestCargo.IsEmpty && allowZeros)
                {
                    return carWithLowestCargo;
                }

                if (!Cars[i].IsEmpty)
                {
                    if (carWithLowestCargo.IsEmpty)
                    {
                        carWithLowestCargo = Cars[i];
                    }
                    else
                    {
                        if (carWithLowestCargo.AmountOfCargo > Cars[i].AmountOfCargo)
                        {
                            carWithLowestCargo = Cars[i];
                        }
                    }
                }
            }

            return carWithLowestCargo;
        }
    }
}