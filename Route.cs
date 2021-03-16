using System;
using System.Collections.Generic;

namespace Railways
{
    public class Route : BasicIdentifiedObject, IDisplayable
    {
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
                    return $"{_stationsOnRoute.First.Value.Name} - {_stationsOnRoute.Last.Value.Name}";
                }
            }
        }

        public bool IsEmpty
        {
            get
            {
                if (_stationsOnRoute.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private LinkedList<Station> _stationsOnRoute;
        private static int _routesCounter = 1;

        // is this legal?
        private Route()
        {
            _stationsOnRoute = new LinkedList<Station>();
            ID = _routesCounter;
            _routesCounter++;
        }

        public Route(Station firstStation, Station lastStation) : this()
        {
            _stationsOnRoute.AddFirst(firstStation);
            _stationsOnRoute.AddLast(lastStation);
        }

        public Route(Station[] stations) : this()
        {
            foreach (Station newStation in stations)
            {
                _stationsOnRoute.AddLast(newStation);
            }
        }

        public void AddStation(Station stationToAdd, bool addAfterLast)
        {
            if (addAfterLast)
            {
                _stationsOnRoute.AddLast(stationToAdd);
            }
            else
            {
                _stationsOnRoute.AddFirst(stationToAdd);
            }
        }

        public void AddStation(Station stationToAdd, Station stationFromRoute)
        {
            if (_stationsOnRoute.Contains(stationFromRoute))
            {
                _stationsOnRoute.AddAfter(_stationsOnRoute.Find(stationFromRoute), stationToAdd);
            }
            else
            {
                Console.WriteLine("В пути нет станции, после которой требуется добавить новую");
            }
        }

        public void RemoveStation(Station stationToRemove)
        {
            if (_stationsOnRoute.Count > 2)
            {
                if (_stationsOnRoute.Contains(stationToRemove))
                {
                    _stationsOnRoute.Remove(stationToRemove);
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
            _stationsOnRoute.First.Value.TakeTrain(train);
        }

        public void MoveTrains()
        {
            foreach (Station station in _stationsOnRoute)
            {
                Train train = station.SendTrain();
                _stationsOnRoute.Find(station).Next.Value.TakeTrain(train);
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Путь №{ID} {Name}");
        }

        public void DisplayDetailedInfo()
        {
            List<string> stations = new List<string>();

            DisplayInfo();
            Console.Write("Станции:\n\t");
            foreach (Station station in _stationsOnRoute)
            {
                stations.Add($"№{station.ID}-{station.Name}");
            }
            Console.WriteLine(String.Join(" <-> ", stations));
        }
    }
}