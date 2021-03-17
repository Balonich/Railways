using System;
using System.Collections.Generic;

namespace RailwaysLibrary
{
    public static partial class Util
    {
        public static class RouteMenus
        {
            public static void RoutesMenu()
            {
                bool flag = true;
                while (flag)
                {
                    Console.WriteLine("Управление путями и станциями. Выберите пункт меню:");
                    Console.WriteLine("1. Создать путь");
                    Console.WriteLine("2. Создать станцию");
                    Console.WriteLine("3. Изменить путь");
                    Console.WriteLine("4. Отобразить все пути");
                    Console.WriteLine("5. Отобразить все станции");
                    Console.WriteLine("6. Отобразить подробную информацию о пути");
                    Console.WriteLine("7. Отобразить подробную информацию о станции");
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
                                RoutesCreationMenu();
                                break;
                            case 2:
                                StationsCreationMenu();
                                break;
                            case 3:
                                Route foundRoute = GetObject<Route>(Util.Routes);
                                if (foundRoute != null)
                                {
                                    RouteEditingMenu(foundRoute);
                                }
                                break;
                            case 4:
                                Util.DisplayAll<Route>(Util.Routes);
                                break;
                            case 5:
                                Util.DisplayAll<Station>(Util.Stations);
                                break;
                            case 6:
                                Util.DisplayDetailed<Route>(Util.Routes, ReadID());
                                break;
                            case 7:
                                Util.DisplayDetailed<Station>(Util.Stations, ReadID());
                                break;
                            default:
                                Console.WriteLine("Вы выбрали неверный пункт меню");
                                Console.Clear();
                                break;
                        }
                    }
                }
            }

            private static void RoutesCreationMenu()
            {
                Console.Clear();
                if (Util.Stations.Count < 2)
                {
                    Console.WriteLine("В системе недостаточно станций для создания пути\nДля создания пути нужно минимум 2 станции");
                }
                else
                {
                    Console.WriteLine("Выберите первую станцию: ");
                    Util.DisplayAll<Station>(Util.Stations);
                    Station firstStation = Util.Find<Station>(Util.Stations, Util.ReadID());

                    Console.WriteLine("Выберите вторую станцию: ");
                    Util.DisplayAll<Station>(Util.Stations);
                    Station secondStation = Util.Find<Station>(Util.Stations, Util.ReadID());

                    Route newRoute = new Route(firstStation, secondStation);
                    Util.AddRoute(newRoute);
                    Console.Clear();
                    Console.WriteLine("Создан новый путь: ");
                    newRoute.DisplayInfo();
                }
            }

            private static void StationsCreationMenu()
            {
                Console.Write("Введите имя станции: ");
                string stationName = Console.ReadLine();
                Console.WriteLine("Выберите тип станции: ");
                Console.WriteLine("1. Общая");
                Console.WriteLine("2. Грузовая");
                Console.WriteLine("3. Пассажирская");
                if (int.TryParse(Console.ReadLine(), out int trainStationType))
                {
                    Console.Clear();
                    switch (trainStationType)
                    {
                        case 1:
                            Util.AddStation(new Station(stationName));
                            break;
                        case 2:
                            Util.AddStation(new CargoStation(stationName));
                            break;
                        case 3:
                            Util.AddStation(new PassangerStation(stationName));
                            break;
                        default:
                            Console.WriteLine("Вы выбрали неверный пункт меню");
                            Console.Clear();
                            break;
                    }
                }
            }

            private static void RouteEditingMenu(Route foundRoute)
            {
                bool flag = true;
                while (flag)
                {
                    Console.WriteLine("Изменение пути. Выберите пункт меню:");
                    Console.WriteLine("1. Добавить станцию к пути");
                    Console.WriteLine("2. Убрать станцию из пути");
                    Console.WriteLine("3. Отобразить все существующие станции");
                    Console.WriteLine("4. Отобразить подробную информацию о текущем пути");
                    Console.WriteLine("5. Добавить поезд к пути");
                    Console.WriteLine("6. Движение!");
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
                                StationToRouteAddingMenu(foundRoute);
                                break;
                            case 2:
                                Console.WriteLine("Выберите станцию, которую хотите убрать:");
                                foundRoute.DisplayDetailedInfo();
                                foundRoute.RemoveStation(Util.Find<Station>(Util.Stations, ReadID()));
                                break;
                            case 3:
                                Util.DisplayAll<Station>(Util.Stations);
                                break;
                            case 4:
                                foundRoute.DisplayDetailedInfo();
                                break;
                            case 5:
                                Console.WriteLine("Выберите поезд, который хотите добавить:");
                                Util.DisplayAll<Train>(Util.Trains);
                                if (!Util.IsEmpty<Train>(Util.Trains))
                                {
                                    foundRoute.AddTrain(Util.Find<Train>(Util.Trains, ReadID(true)));
                                }
                                break;
                            case 6:
                                foundRoute.MoveTrains();
                                break;
                            default:
                                Console.WriteLine("Вы выбрали неверный пункт меню");
                                Console.Clear();
                                break;
                        }
                    }
                }
            }

            private static void StationToRouteAddingMenu(Route foundRoute)
            {
                Console.WriteLine("Введите метод добавления станции к пути: ");
                Console.WriteLine("1. Добавить в конец");
                Console.WriteLine("2. Добавить в начало");
                Console.WriteLine("3. Добавить после конкретной станции");
                if (int.TryParse(Console.ReadLine(), out int trainStationType))
                {
                    Console.Clear();
                    Console.WriteLine("Выберите станцию, которую хотите добавить:");
                    Util.DisplayAll<Station>(Util.Stations);
                    switch (trainStationType)
                    {
                        case 1:
                            foundRoute.AddStation(Util.Find<Station>(Util.Stations, ReadID()), true);
                            break;
                        case 2:
                            foundRoute.AddStation(Util.Find<Station>(Util.Stations, ReadID()), false);
                            break;
                        case 3:
                            Station stationToAdd = Util.Find<Station>(Util.Stations, ReadID());
                            Console.WriteLine("Выберите станцию, после которой хотите добавить новую:");
                            foundRoute.DisplayDetailedInfo();
                            foundRoute.AddStation(stationToAdd, Util.Find<Station>(Util.Stations, ReadID()));
                            break;
                        default:
                            Console.WriteLine("Вы выбрали неверный пункт меню");
                            Console.Clear();
                            break;
                    }
                }
            }
        }
    }
}