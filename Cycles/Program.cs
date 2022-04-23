using System;

namespace Cycles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите число: ");
            int num = int.Parse(Console.ReadLine());

            if (num % 2 == 0)
                Console.WriteLine("Число четное");
            else
                Console.WriteLine("Число нечетное");
            Console.ReadKey();


                                            //For, switch/case       
            Console.Clear();
            Console.WriteLine("Приветсвуем в игре BlackJack\n\nСколько у вас карт в руке?");
            int j = int.Parse(Console.ReadLine());
            string card;
            int cardSum = 0;
            for (int i = 0; i < j; i++) 
            {
                Console.WriteLine("Введите значение карты номер " + (i + 1));
                card = Console.ReadLine();
                switch (card)
                {
                    case "6":
                        cardSum += 6;
                        break;
                    case "7":
                        cardSum += 7;
                        break;
                    case "8":
                        cardSum += 8;
                        break;
                    case "9":
                        cardSum += 9;
                        break;
                    case "10":
                        cardSum += 10;
                        break;
                    case "J":               //Насколько я помню, в "21" значения картинок не равны 10
                        cardSum += 2;
                        break;
                    case "j":
                        cardSum += 2;
                        break;
                    case "Q":
                        cardSum += 3;
                        break;
                    case "q":
                        cardSum += 3;
                        break;
                    case "K":
                        cardSum += 4;
                        break;
                    case "k":
                        cardSum += 4;
                        break;
                    case "T":
                        cardSum += 11;
                        break;
                    case "t":
                        cardSum += 11;
                        break;
                    default:
                        Console.WriteLine("Такой карты не существует, введите значение снова.");
                        i--;
                        break;
                }
            }
            Console.WriteLine(cardSum);
            Console.ReadKey();



                                            //While

            Console.Clear();
            Console.Write("Введите число: ");
            int N = Convert.ToInt32(Console.ReadLine());
            int k = 2;
            bool prime = true;
            while (k!=(N-1))
            {
                if (N % k == 0)
                {
                    prime = false;
                    break;
                }
                else k++;
            }
            if (prime) Console.WriteLine("Это число простое.");
            else Console.WriteLine("Это число не является простым");
            Console.ReadKey();
        }
    }
}
