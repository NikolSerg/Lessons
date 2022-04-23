using System;

namespace Array
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество строк матрицы: ");
            int rows = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите количество столбцов матрицы: ");
            int columns = Convert.ToInt32(Console.ReadLine());

            Random random = new Random();
            int[,] matrix = new int[rows,columns];
            long sum = 0;

            Console.WriteLine("Полученная матрица");

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = random.Next();
                    sum+=matrix[i, j];
                    Console.Write($"{matrix[i, j]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Сумма элементов матрицы равна: {sum}");



            Console.ReadKey();
            Console.Clear();
            Console.Write("Введите длину последовательности: ");
            int[] arr = new int[int.Parse(Console.ReadLine())];
            Console.WriteLine("Введите элементы последовательности: ");
            int min = int.MaxValue;
            for (int i = 0; i < arr.Length; i++)
            {
                try {
                    arr[i] = int.Parse(Console.ReadLine());
                }
                catch {
                    Console.WriteLine("Введите корректное число из диапазона integer");
                    i--; 
                }
            }

            //System.Array.Sort(arr);       Заметил, что без "Sysem." не видит этот класс
            //Console.WriteLine(arr[0]);

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < min) min = arr[i];
            }
            Console.WriteLine($"Наименьший член последовательности равен: {min}");
            Console.ReadKey();



            Console.Clear();
            Console.WriteLine("Угадай число");
            Console.Write("Установите диапазон: ");
            int range = int.Parse(Console.ReadLine());
            int goal = random.Next(0, range + 1);
            Console.WriteLine("Попробуйте угадать число");
            while(true)
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
            Console.ReadKey();
        }
    }
}
