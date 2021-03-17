using System;
using System.Collections.Generic;

namespace RailwaysLibrary
{
    public static partial class Util
    {
        public static class TrainMenus
        {
            public static void TrainsMenu()
            {
                bool flag = true;
                while (flag)
                {
                    Console.WriteLine("Управление поездами. Выберите пункт меню:");
                    Console.WriteLine("1. Создать поезд");
                    Console.WriteLine("2. Управление вагонами поезда");
                    Console.WriteLine("3. Отобразить все существующие поезда");
                    Console.WriteLine("4. Отобразить подробную информацию о поезде");
                    Console.WriteLine("0. Назад");

                    if (int.TryParse(Console.ReadLine(), out int menuSwitch))
                    {
                        Console.Clear();
                        switch (menuSwitch)
                        {
                            case 0:
                                flag = false;
                                break;
                            case 1:
                                TrainsCreationMenu();
                                break;
                            case 2:
                                Train foundTrain = GetObject<Train>(Util.Trains);
                                if (foundTrain != null)
                                {
                                    CarsMenu(foundTrain);
                                }
                                break;
                            case 3:
                                Util.DisplayAll<Train>(Util.Trains);
                                break;
                            case 4:
                                Util.DisplayDetailed<Train>(Util.Trains, ReadID(true));
                                break;
                            default:
                                Console.WriteLine("Вы выбрали неверный пункт меню");
                                Console.Clear();
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели неверные данные");
                    }
                }
            }

            static void TrainsCreationMenu()
            {
                bool flag = true;
                while (flag)
                {
                    Console.WriteLine("Создание поездов. Выберите тип поезда:");
                    Console.WriteLine("1. Пассажирский");
                    Console.WriteLine("2. Грузовой");
                    Console.WriteLine("0. Назад");

                    if (int.TryParse(Console.ReadLine(), out int menuSwitch))
                    {
                        Console.Clear();
                        switch (menuSwitch)
                        {
                            case 0:
                                flag = false;
                                break;
                            case 1:
                                if (CheckTrain(out int passangerID))
                                {
                                    Util.AddTrain(new PassangerTrain(passangerID));
                                    Console.WriteLine("Поезд успешно создан!");
                                }
                                break;
                            case 2:
                                if (CheckTrain(out int cargoID))
                                {
                                    Util.AddTrain(new CargoTrain(cargoID));
                                    Console.WriteLine("Поезд успешно создан!");
                                }
                                break;
                            default:
                                Console.WriteLine("Вы выбрали неверный пункт меню");
                                Console.Clear();
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели неверные данные");
                    }
                }
            }

            static void CarsMenu(Train foundTrain)
            {
                bool flag = true;
                while (flag)
                {
                    foundTrain.DisplayInfo();
                    Console.WriteLine("Выберите пункт меню:");
                    Console.WriteLine("1. Добавить вагон");
                    Console.WriteLine("2. Убрать вагон");
                    Console.WriteLine("3. Убрать все вагоны");
                    Console.WriteLine("4. Добавить груз/пассажиров");
                    Console.WriteLine("5. Убрать груз/пассажиров");
                    Console.WriteLine("6. Отобразить детальную информацию о текущем поезде");
                    Console.WriteLine("0. Назад");

                    if (int.TryParse(Console.ReadLine(), out int menuSwitch))
                    {
                        Console.Clear();
                        switch (menuSwitch)
                        {
                            case 0:
                                flag = false;
                                break;
                            case 1:
                                if (foundTrain is PassangerTrain)
                                {
                                    Console.Write("Введите общее количество мест в вагоне: ");
                                }
                                else
                                {
                                    Console.Write("Введите общее количество объёма вагона: ");
                                }

                                int volume = int.Parse(Console.ReadLine());

                                if (foundTrain is PassangerTrain)
                                {
                                    Console.Write("Введите количество пассажиров вагоне: ");
                                }
                                else
                                {
                                    Console.Write("Введите количество груза в вагоне: ");
                                }

                                int amountToAdd = int.Parse(Console.ReadLine());
                                foundTrain.AddCar(volume, amountToAdd);
                                break;
                            case 2:
                                foundTrain.RemoveCar();
                                break;
                            case 3:
                                foundTrain.RemoveAllCars();
                                break;
                            case 4:
                                if (foundTrain is PassangerTrain)
                                {
                                    Console.Write("Введите количество новых пассажиров: ");
                                }
                                else
                                {
                                    Console.Write("Введите количество добавляемого груза: ");
                                }

                                amountToAdd = int.Parse(Console.ReadLine());
                                foundTrain.AddCargo(amountToAdd);
                                break;
                            case 5:
                                if (foundTrain is PassangerTrain)
                                {
                                    Console.Write("Введите количество уходящих пассажиров: ");
                                }
                                else
                                {
                                    Console.Write("Введите количество убираемого груза: ");
                                }

                                int amountToRemove = int.Parse(Console.ReadLine());
                                foundTrain.RemoveCargo(amountToRemove);
                                break;
                            case 6:
                                Util.DisplayDetailed<Train>(Util.Trains, foundTrain.ID);
                                break;
                            default:
                                Console.WriteLine("Вы выбрали неверный пункт меню");
                                Console.Clear();
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели неверные данные");
                    }
                }
            }

            static bool CheckTrain(out int id)
            {
                id = ReadID(true);
                if (Util.IsEmpty<Train>(Util.Trains))
                {
                    return true;
                }

                if (Util.Find<Train>(Util.Trains, id) == null)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Поезд с таким ID уже существует");
                    return false;
                }
            }

            static Train CheckTrain()
            {
                Train foundTrain = Util.Find<Train>(Util.Trains, ReadID(true));

                if (foundTrain == null)
                {
                    Console.WriteLine("Поезд с таким ID не существует");
                }

                return foundTrain;
            }
        }
    }
}