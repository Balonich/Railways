namespace RailwaysLibrary
{
    using System;
    using System.Collections.Generic;

    public class Station : BasicIdentifiedObject, IDisplayable
    {
        private static int stationsCounter = 1; // how does it work with inheritance?

        public Station(string stationName)
        {
            ID = stationsCounter;
            stationsCounter++;
            Name = stationName;
            TrainsOnTheStation = new Queue<Train>();
        }

        public override int ID { get; }

        public bool IsEmpty
        {
            get => TrainsOnTheStation.Count == 0;
        }

        protected string Name { get; }

        protected Queue<Train> TrainsOnTheStation { get; set; }

        public virtual void TakeTrain(Train arrivingTraing)
        {
            Console.WriteLine($"Поезд №{arrivingTraing.ID} прибыл на станцию {Name}");
            TrainsOnTheStation.Enqueue(arrivingTraing);
        }

        public Train SendTrain()
        {
            return TrainsOnTheStation.Dequeue();
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
                Console.WriteLine($"На станции находится {TrainsOnTheStation.Count} поездов\nИнформация о поездах на станции:");
                foreach (Train train in TrainsOnTheStation)
                {
                    train.DisplayInfo();
                }
            }
        }
    }
}