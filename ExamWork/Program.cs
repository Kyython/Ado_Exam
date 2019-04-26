using System;

namespace ExamWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Добавление данных - 1");
                Console.WriteLine("Вывод всех данных - 2");
                Console.WriteLine("Удалить улицу - 3");
                Console.WriteLine("Удалить город - 4");
                Console.WriteLine("Удалить страну - 5");
                Console.WriteLine("Выход - 6");

                string userNumber = Console.ReadLine();

                if (userNumber == "1")
                {
                    menu.AddData();
                }
                else if (userNumber == "2")
                {
                    menu.ShowData();
                }
                else if (userNumber == "3")
                {
                    menu.DeleteStreet();
                }
                else if (userNumber == "4")
                {
                    menu.DeleteCity();
                }
                else if (userNumber == "5")
                {
                    menu.DeleteCountry();
                }
                else if (userNumber == "6")
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Такого варианта не существует!");
                    Console.WriteLine("Нажмите Enter чтобы продолжить!");
                    Console.ReadKey();
                }
            }

        }
    }
}
