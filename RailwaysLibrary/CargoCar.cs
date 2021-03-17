using System;

namespace RailwaysLibrary
{
    public partial class Train
    {
        protected class CargoCar : Car
        {
            public CargoCar(int volume, int amountOfCargo) : base(volume, amountOfCargo)
            {

            }

            // public override void AddCargo(int amountToAdd)
            // {
            //     if (amountToAdd + AmountOfCargo > Volume)
            //     {
            //         // Console.WriteLine("Невозможно добавить груз. Либо вагон достиг лимита, либо уменьшите количество добавляемого груза.");
            //         Exception ex = new Exception("Невозможно добавить груз. Либо вагон достиг лимита, либо уменьшите количество добавляемого груза.");
            //         ex.HelpLink = "https://www.google.com/";
            //         ex.Data.Add("Timestamp", $"Переполнение произошло в {DateTime.Now}");
            //         ex.Data.Add("Cause", $"Слишком мало места в вагоне");
            //         throw ex;
            //     }
            //     else
            //     {
            //         AmountOfCargo += amountToAdd;
            //     }
            // }

            public override void DisplayInfo()
            {
                Console.WriteLine($"Вагон №{ID}\n\tколичество груза в вагоне: {AmountOfCargo} из {Volume}");
            }
        }
    }
}