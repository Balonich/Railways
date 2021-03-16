using System;

namespace Railways
{
    public class CargoStation : Station
    {
        public CargoStation(string name) : base(name)
        {

        }

        public override void TakeTrain(Train arrivingTraing)
        {
            if (arrivingTraing is CargoTrain)
            {
                Console.WriteLine($"Поезд №{arrivingTraing.ID} прибыл на грузовую станцию {Name}");
                _trainsOnTheStation.Enqueue(arrivingTraing);
            }
            else
            {
                Console.WriteLine("Данный поезд не может остановиться на этой станции");
            }
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine("(грузовая).");
        }
    }
}