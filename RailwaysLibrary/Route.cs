namespace RailwaysLibrary
{
    using System;
    using System.Collections.Generic;

    public class Route : BasicIdentifiedObject, IDisplayable
    {
        private static int routesCounter = 1;

        public Route(Station firstStation, Station lastStation)
            : this()
        {
            StationsOnRoute.AddFirst(firstStation);
            StationsOnRoute.AddLast(lastStation);
        }

        public Route(Station[] stations)
            : this()
        {
            foreach (Station newStation in stations)
            {
                StationsOnRoute.AddLast(newStation);
            }
        }

        // is this legal? private constructor...
        private Route()
        {
            StationsOnRoute = new LinkedList<Station>();
            ID = routesCounter;
            routesCounter++;
        }

        public override int ID { get; }

        public string Name
        {
            get
            {
                if (IsEmpty)
                {
                    throw new Exception($"Путь №{ID} без имени! Возможно, что маршрут пустой");
                }
                else
                {
                    return $"{StationsOnRoute.First.Value.Name} - {StationsOnRoute.Last.Value.Name}";
                }
            }
        }

        public bool IsEmpty
        {
            get => StationsOnRoute.Count == 0;
        }

        private LinkedList<Station> StationsOnRoute { get; set; }

        public void AddStation(Station stationToAdd, bool addAfterLast)
        {
            if (addAfterLast)
            {
                StationsOnRoute.AddLast(stationToAdd);
            }
            else
            {
                StationsOnRoute.AddFirst(stationToAdd);
            }
        }

        public void AddStation(Station stationToAdd, Station stationFromRoute)
        {
            if (StationsOnRoute.Contains(stationFromRoute))
            {
                StationsOnRoute.AddAfter(StationsOnRoute.Find(stationFromRoute), stationToAdd);
            }
            else
            {
                Console.WriteLine("В пути нет станции, после которой требуется добавить новую");
            }
        }

        public void RemoveStation(Station stationToRemove)
        {
            if (StationsOnRoute.Count > 2)
            {
                if (StationsOnRoute.Contains(stationToRemove))
                {
                    StationsOnRoute.Remove(stationToRemove);
                }
                else
                {
                    Console.WriteLine("Такой станции в пути нет");
                }
            }
            else
            {
                Console.WriteLine("Невозможно убрать станцию из пути - в пути 2 или менее станций!");
            }
        }

        public void AddTrain(Train train)
        {
            StationsOnRoute.First.Value.TakeTrain(train);
        }

        public void MoveTrains()
        {
            foreach (Station station in StationsOnRoute)
            {
                Train train = station.SendTrain();
                StationsOnRoute.Find(station).Next.Value.TakeTrain(train);
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Путь №{ID} {Name}");
        }

        public void DisplayDetailedInfo()
        {
            List<string> stations = new ();

            DisplayInfo();
            Console.Write("Станции:\n\t");
            foreach (Station station in StationsOnRoute)
            {
                stations.Add($"№{station.ID}-{station.Name}");
            }

            Console.WriteLine(string.Join(" <-> ", stations));
        }
    }
}