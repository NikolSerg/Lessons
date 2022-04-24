using System;
using System.IO;

namespace Practice_6
{
    internal class Program
    {
        static string[] AddNewEmployee()
        {
            string[] empData = new string[5];
            Console.Write("\nВведите данные сотрудника:\nФИО сотрудника: ");
            empData[0] = Console.ReadLine();
            Console.Write("Возраст: ");
            empData[1] = Console.ReadLine();
            Console.Write("Рост: ");
            empData[2] = Console.ReadLine();
            Console.Write("Дата рождения(дд.мм.гггг): ");
            empData[3] = Console.ReadLine();
            Console.Write("Место рождения: ");
            empData[4] = Console.ReadLine();

            return empData;
        }
        //static string[] SplitString(string str)
        //{
        //    string[] empData = str.Split('#'); 
        //}
        static void WriteDataToFile(string path, string[] empData)
        {
            
            using (StreamWriter file = new StreamWriter(path, true))
            {
                for (int i = 0; i < empData.Length - 1; i++)
                    file.Write($"{empData[i]}#");
                file.Write($"{empData[6]}\n");
            }
        }
        static string[] ReadDataFromFile(string path)
        {
            string[] data;
            data = File.ReadAllLines(path);
            return data;
        }


        static void Main(string[] args)
        {
            string path = "Employees.txt";
            Console.Write("Сотрудники\nВывести данные на экран(1) или добавить новые(2)? ");
            ushort mode = Convert.ToUInt16(Console.ReadLine());
            
            switch (mode)
            {
                case 1:
                    if (!File.Exists(path)) File.Create(path).Close();
                    string[] empDataR = ReadDataFromFile(path);
                    foreach (string str in empDataR)
                    {
                        string[] data = str.Split('#');
                        Console.WriteLine($"\nРаботник {data[0]}:\nID: {data[0]}\nДата и время добавления записи: {data[1]}\n" +
                     $"Ф.И.О.: {data[2]}\nВозраст: {data[3]}\nРост: {data[4]}\nДата рождения: {data[5]}\nМесто рождения: {data[6]}");
                    }
                    break;
                case 2:
                    while(true)
                    {
                        int id;
                        DateTime time;
                        string[] empDataToWrite = new string[7];
                        string[] empData = AddNewEmployee();
                        if (!File.Exists(path))
                        {
                            File.Create(path).Close();
                            
                        }

                        string[] rData = ReadDataFromFile(path);
                        id = rData.Length + 1;
                        time = DateTime.Now;
                        empDataToWrite[0] = id.ToString();
                        empDataToWrite[1] = time.ToString();
                        for (int i = 2; i < 7; i++) empDataToWrite[i] = empData[i - 2];
                        WriteDataToFile(path, empDataToWrite);
                        Console.Write("Нажмите '+', чтобы добавить больше сотрудников. ");
                        if (Console.ReadKey().Key != ConsoleKey.Add) break;
                        else Console.WriteLine();
                    }
                    Console.WriteLine("Новые данные успешно добавлены!");
                    break;
                default:
                    break;
            }
        }
    }
}
