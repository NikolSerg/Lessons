using System;

namespace Games
{
    internal class Program
    {
        static void Main(string[] args)
        {

            GuessTheGame game = new GuessTheGame();
            game.Game();


        }
    }
}

public class GuessTheGame
{
    private Random random = new Random();
    private int range;
    private int goal;
    public int Range
    {
        get { return range; }
        set { range = value; }
    }
    public void Game()
    {
        Console.Write("Угадай Число\nВведите диапазон: ");
        Range = int.Parse(Console.ReadLine());
        goal = random.Next(0, range + 1);
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
        Console.Write("Хотите начать заного?");

        if (Console.ReadLine() == "")
        {
            Console.Clear();
            Game();
        }

    }
}

