namespace RailwaysUI
{
    using System;
    using RailwaysLibrary;

    public static class Program
    {
        public static void Main()
        {
            Menu();
        }

        private static void Menu()
        {
            while (true)
            {
                Console.WriteLine("Администратор Ж/Д. Выберите пункт меню:");
                Console.WriteLine("1. Управление поездами и вагонами");
                Console.WriteLine("2. Управление путями и станциями");
                Console.WriteLine("0. Выход");

                if (int.TryParse(Console.ReadLine(), out int menuSwitch))
                {
                    Console.Clear();
                    switch (menuSwitch)
                    {
                        case 0:
                            Environment.Exit(0);
                            break;
                        case 1:
                            Util.TrainMenus.TrainsMenu();
                            break;
                        case 2:
                            Util.RouteMenus.RoutesMenu();
                            break;
                        default:
                            Console.WriteLine("Вы выбрали неверный пункт меню");
                            Console.Clear();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Вы ввели неверные данные");
                }
            }
        }
    }
}
