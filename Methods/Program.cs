using System;
using System.Collections.Generic;

namespace Methods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //static string[] StringToWords(string str)
            //{
            //    return str.Split(' ');
            //}
            
            
            static string[] SplitStringToWords(string str)
            {
                List<string> stroke = new List<string>();
                string word = null;
                foreach(char ch in str)
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
            static void PrintStrings(string[] str)
            {
                Console.WriteLine("Предложение разбитое на слова: ");
                foreach (string word in str)
                {
                    Console.WriteLine(word);
                }
            }

            Console.Write("Введите предложение: ");

            string[] words = SplitStringToWords(Console.ReadLine());
            PrintStrings(words);
            Console.ReadKey();




            Console.Clear();
            static void PrintReverseWords(string str)
            {
                string[] words = SplitStringToWords(str);
                string reverseString = null;
                for (int i = words.Length - 1; i >= 0; i--)
                {
                    reverseString += words[i] + " ";
                }
                Console.WriteLine($"Ваше предложение наоборот: {reverseString}");
            }

            Console.Write("Введите предложение: ");
            PrintReverseWords(Console.ReadLine());
            Console.ReadKey();
        }
        
    }
}
