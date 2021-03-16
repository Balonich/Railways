using System;
using System.Collections.Generic;

namespace Railways
{
    public class Station : BasicIdentifiedObject, IDisplayable
    {
        public override int ID { get; }
        public readonly string Name;
        public bool IsEmpty
        {
            get
            {
                if (_trainsOnTheStation.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        protected Queue<Train> _trainsOnTheStation;
        private static int _stationsCounter = 1; // how does it work with inheritance?

        public Station(string name)
        {
            ID = _stationsCounter;
            _stationsCounter++;
            Name = name;
            _trainsOnTheStation = new Queue<Train>();
        }

        public virtual void TakeTrain(Train arrivingTraing)
        {
            Console.WriteLine($"Поезд №{arrivingTraing.ID} прибыл на станцию {Name}");
            _trainsOnTheStation.Enqueue(arrivingTraing);
        }

        public Train SendTrain()
        {
            return _trainsOnTheStation.Dequeue();
        }

        public virtual void DisplayInfo()
        {
            Console.Write($"Станция №{ID} - {Name} ");
        }

        public void DisplayDetailedInfo()
        {
            DisplayInfo();
            if (IsEmpty)
            {
                Console.WriteLine("На станции нет поездов");
            }
            else
            {
                Console.WriteLine($"На станции находится {_trainsOnTheStation.Count} поездов\nИнформация о поездах на станции:");
                foreach (Train train in _trainsOnTheStation)
                {
                    train.DisplayInfo();
                }
            }
        }
    }
}