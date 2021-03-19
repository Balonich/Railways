namespace RailwaysLibrary
{
    using System;

    public class PassangerStation : Station
    {
        public PassangerStation(string name)
        : base(name)
        {
        }

        public override void TakeTrain(Train arrivingTraing)
        {
            if (arrivingTraing is PassangerTrain)
            {
                Console.WriteLine($"Поезд №{arrivingTraing.ID} прибыл на пассажирскую станцию {Name}");
                TrainsOnTheStation.Enqueue(arrivingTraing);
            }
            else
            {
                Console.WriteLine("Данный поезд не может остановиться на этой станции");
            }
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine("(пассажирская).");
        }
    }
}