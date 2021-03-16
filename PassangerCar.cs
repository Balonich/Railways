using System;

namespace Railways
{
    public partial class Train
    {
        protected class PassangerCar : Car
        {
            public PassangerCar(int volume, int amountOfPassangers) : base(volume, amountOfPassangers)
            {

            }

            public override void DisplayInfo()
            {
                Console.WriteLine($"Вагон №{ID}\n\tколичество пассажиров в вагоне: {AmountOfCargo} из {Volume}");
            }
        }
    }
}