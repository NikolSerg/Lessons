using System;
using System.Collections.Generic;

namespace Methods
{
    internal class Program
    {
        //static string[] StringToWords(string str)
        //{
        //    return str.Split(' ');
        //}


        /// <summary>
        /// Возвращает массив подстрок данной строки отделенных разделителем
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static string[] SplitStringToWords(string Str)
        {
            List<string> stroke = new List<string>();
            string word = null;
            foreach (char ch in Str)
            {
                if (ch != ' ') word += ch;
                else
                {
                    stroke.Add(word);
                    word = null;
                }
            }
            stroke.Add(word);
            return stroke.ToArray();
        }
        /// <summary>
        /// Выводит в консоль элементы массива строк
        /// </summary>
        /// <param name="str"></param>
        static void PrintStrings(string[] Str)
        {
            Console.WriteLine("Предложение разбитое на слова: ");
            foreach (string word in Str)
            {
                Console.WriteLine(word);
            }
        }

        /// <summary>
        /// Выводит в консоль строку обратную данной
        /// </summary>
        /// <param name="str"></param>
        static void PrintReverseWords(string Str)
        {
            string[] words = SplitStringToWords(Str);
            string reverseString = null;
            for (int i = words.Length - 1; i >= 0; i--)
            {
                reverseString += words[i] + " ";
            }
            Console.WriteLine($"Ваше предложение наоборот: {reverseString}");
        }

        static void Main(string[] args)
        {

            Console.Write("Введите предложение: ");

            string[] words = SplitStringToWords(Console.ReadLine());
            PrintStrings(words);
            Console.ReadKey();




            Console.Clear();

            Console.Write("Введите предложение: ");
            PrintReverseWords(Console.ReadLine());
            Console.ReadKey();
        }
        
    }
}
