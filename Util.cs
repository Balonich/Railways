using System;
using System.Collections.Generic;
using System.Linq;

namespace Railways
{
    public static partial class Util
    {
        public static List<Station> Stations { get; private set; } = new List<Station>();
        public static List<Route> Routes { get; private set; } = new List<Route>();
        public static List<Train> Trains { get; private set; } = new List<Train>();

        public static void AddStation(Station newStation)
        {
            if (UniqueStation(newStation, out Station foundStation))
            {
                Stations.Add(newStation);
            }
            else
            {
                Console.WriteLine($"Станция с именем {newStation.Name} уже существует. Её номер - №{foundStation.ID}");
            }
        }

        public static void AddRoute(Route newRoute) => Routes.Add(newRoute);

        public static void AddTrain(Train newTrain) => Trains.Add(newTrain);

        public static void DisplayAll<T>(List<T> list) where T : IDisplayable
        {
            if (IsEmpty<T>(list))
            {
                Console.WriteLine("Список пуст");
                return;
            }
            foreach (T element in list)
            {
                element.DisplayInfo();
            }
        }

        public static void DisplayDetailed<T>(List<T> list, int id) where T : BasicIdentifiedObject, IDisplayable
        {
            list.Where(list => list.ID == id).First().DisplayDetailedInfo();
        }

        // public static T Find<T>(List<T> list, int id) where T : BasicIdentifiedObject, IDisplayable => list.Where(element => element.ID == id).First();
        public static T Find<T>(List<T> list, int id) where T : BasicIdentifiedObject, IDisplayable
        {
            foreach(T item in list)
            {
                if (item.ID == id)
                {
                    return item;
                }
            }
            return null;
        }

        private static bool IsEmpty<T>(List<T> list)
        {
            if (list.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool UniqueStation(Station stationToCheck, out Station foundStation)
        {
            foundStation = null;

            if (Stations.Count == 0)
            {
                return true;
            }
            else
            {
                foreach (Station station in Stations)
                {
                    if (station.Name == stationToCheck.Name)
                    {
                        foundStation = station;
                        return false;
                    }
                }
            }
            return true;
        }

        private static int ReadID(bool isTrain = false)
        {
            int id;

            while (true)
            {
                if (isTrain)
                {
                    Console.Write("Введите числовой идентификатор поезда в формате ХХХХ: ");
                }
                else
                {
                    Console.Write("Введите числовой идентификатор: ");
                }

                string inputString = Console.ReadLine();

                if (int.TryParse(inputString, out id))
                {
                    if (inputString.Length != 4 && isTrain)
                    {
                        Console.WriteLine("Неверный формат");
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Неверный формат");
                }
            }

            return id;
        }

        static T GetObject<T>(List<T> list) where T : BasicIdentifiedObject, IDisplayable
        {
            if (Util.IsEmpty<T>(list))
            {
                return null;
            }
            int id = typeof(T) == typeof(Train) ? ReadID(true) : ReadID();
            T foundObject = Util.Find<T>(list, id);
                
            if (foundObject == null)
            {
                Console.WriteLine("Объект с таким ID не существует");
            }

            return foundObject;
        }
    }
}