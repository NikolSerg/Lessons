using System;

namespace Games
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Random random = new Random();
            Console.WriteLine("Угадай число");
            Console.Write("Установите диапазон: ");
            int range = int.Parse(Console.ReadLine());
            int goal = random.Next(0, range + 1);
            Console.WriteLine("Попробуйте угадать число");
            while (true)
            {
                string sNum = Console.ReadLine();
                if (sNum != "")
                {
                    int num = int.Parse(sNum);
                    if (num < goal) Console.WriteLine("Загаданное число больше введенного");
                    else if (num > goal) Console.WriteLine("Загаданное число меньше введенного");
                    else
                    {
                        Console.WriteLine("Поздравляем, Вы угадали!!!");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine($"Загаданное чило: {goal}");
                    
                    break;
                }
            }

        }
    }

    public class GuessTheNumber
    {
        static void Game()
        {

        }
    }
}
