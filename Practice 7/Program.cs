using System;

namespace Practice_7
{

    internal class Program
    {

        static void Main(string[] args)
        {
            string path = "Employees.txt";
            Employees employees = new Employees(path);
            employees.ShowAllEmployees();
            Wait();
            employees.SortData(0);
            employees.ShowAllEmployees();
            static void Wait()
            {
                Console.ReadKey();
            }
        }
    }

}

