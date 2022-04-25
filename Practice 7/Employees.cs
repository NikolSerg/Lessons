using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_7
{
    internal class Employees
    {
        Employee[] employees;
        public Employee[] EmployeesArr { get { return employees; } }
        public Employee this[int index]
        {
            get { return employees[index]; }
            set { employees[index] = value; }
        }
        string[] ReadDataFromFile(string path)
        {
            string[] data = File.ReadAllLines(path);
            return data;
        }
        Employee[] Upload(string path)
        {
            string[] data = ReadDataFromFile(path);
            Employee[] employees = new Employee[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                string[] datas = data[i].Split('#');
                employees[i] = new Employee(Convert.ToInt32(datas[0]), Convert.ToDateTime(datas[1]), datas[2], Convert.ToUInt16(datas[3]),
                    Convert.ToUInt16(datas[4]), Convert.ToDateTime(datas[5]), datas[6]);

            }
            return employees;
        }
        public Employees(string path)
        {
            employees = Upload(path);
        }
        Employee[] RebuildData()
        {
            Employee[] newEmployees;
            int j = 0;
            newEmployees = new Employee[employees.Length - 1];
            for (int i = 0; i < employees.Length; i++)
            {
                if (employees[i].Fired) continue;
                else
                {
                    newEmployees[j] = employees[i];
                    j++;
                }
            }
                    employees = newEmployees;
            return employees;
        }
        /// <summary>
        /// Показывает данные выбранного сотрудника
        /// </summary>
        /// <param name="id"></param>
        public void ShowEmployeeByID(int id)
        {
            foreach(Employee emp in employees)
            {
                if(emp.Id == id)
                {
                    Console.WriteLine($"ID: {emp.Id}\nДата добавления: {emp.DateTime.ToString()}\n" +
                        $"Ф.И.О.: {emp.FullName}\nВозраст: {emp.Age}\nРост: {emp.Height}\n" +
                        $"Дата рождения: {emp.DateOfBirth.ToString("d")}\nМесто рождения: {emp.PlaceOfBirth}");
                }
            }
        }
        /// <summary>
        /// Добавляет сотрудника в список сотрудников
        /// </summary>
        /// <param name="employee"></param>
        public void AddEmployee(Employee employee)
        {
            Array.Resize(ref employees, employees.Length + 1);
            employees[employees.Length - 1] = employee;
        }
        /// <summary>
        /// Удаляет сотрудника из списка сотрудников
        /// </summary>
        /// <param name="employee"></param>
        public void FireEmployee(Employee employee)
        {
            for(int i = 0; i < employees.Length; i++)
            {
                if (employees[i].Id == employee.Id)
                {
                    employees[i].Fire();
                    employees = RebuildData();
                    break;
                }
            }
        }
        /// <summary>
        /// Выводит в консоль данные всех сотрудников
        /// </summary>
        public void ShowAllEmployees()
        {
            foreach (Employee emp in employees)
            {
                foreach(string str in emp.GetEmpInformation())
                {
                    Console.WriteLine(str);
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Сортирует по дате создания(type == 0), Ф.И.О.(type == 1), возрасту(type == 2), ID(type == 3)
        /// </summary>
        /// <param name="type"></param>
        public void SortData(int type)
        {
            switch (type)
            {
                case 0:
                    employees = employees.OrderBy(employee => employee.DateTime).ToArray();
                    ShowAllEmployees();
                    break;
                case 1:
                    employees = employees.OrderBy(employee => employee.FullName).ToArray();
                    ShowAllEmployees();
                    break;
                case 2:
                    employees = employees.OrderBy(employee => employee.Age).ToArray();
                    ShowAllEmployees();
                    break;
                case 3:
                    employees = employees.OrderBy(employee => employee.Id).ToArray();
                    ShowAllEmployees();
                    break;

            }
        }

        
    }
}
