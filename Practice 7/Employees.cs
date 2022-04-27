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

        List<Employee> fullList = new List<Employee>();

        public Employee[] EmployeesArr { get { return employees; } }

        public Employee this[int index]
        {
            get { return employees[index]; }
            set { employees[index] = value; }
        }

        int maxID;

        List<Employee> firedEmps = new List<Employee>();

        string[] ReadDataFromFile(string path)
        {
            if (!File.Exists(path)) File.Create(path).Close();
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
            foreach(Employee emp in employees)
            {
                fullList.Add(emp);
            }
            GetMaxId();
        }

        void GetMaxId()
        {
            int[] ids = new int[employees.Length];
            for (int i = 0; i < employees.Length; i++)
            {
                ids[i] = employees[i].Id;
            }
            maxID = ids.Max();
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
            Employee emp;
            int i = FindEmployeeById(id);
            emp = employees[i];
            Console.WriteLine($"ID: {emp.Id}\nДата добавления: {emp.DateTime.ToString()}\n" +
               $"Ф.И.О.: {emp.FullName}\nВозраст: {emp.Age}\nРост: {emp.Height}\n" +
               $"Дата рождения: {emp.DateOfBirth.ToString("d")}\nМесто рождения: {emp.PlaceOfBirth}");             
        }

        int FindEmployeeById(int id)
        {
            int j = 0;
            for(int i = 0; i < employees.Length; i++)
            {
                if (employees[i].Id == id)
                {
                    j = i;
                }
            }
            return j;
        }

        /// <summary>
        /// Добавляет сотрудника в список сотрудников
        /// </summary>
        /// <param name="employee"></param>
        public void AddEmployee(string fullName, ushort height, DateTime dateOfBirth, string placeOfBirth)
        {
            Employee emp = new Employee(maxID + 1, fullName, height, dateOfBirth, placeOfBirth);
            Array.Resize(ref employees, employees.Length + 1);
            employees[employees.Length - 1] = emp;
            fullList.Add(emp);
        }

        /// <summary>
        /// Удаляет сотрудника из списка сотрудников
        /// </summary>
        /// <param name="employee"></param>
        public void FireEmployee(int id)
        {
            int i = FindEmployeeById(id);
            if (employees[i].Id > maxID) maxID = employees[i].Id;
            employees[i].Fire();
            firedEmps.Add(employees[i]);
            int k = 0;
            for(int j = 0; j < fullList.Count; j++)
            {
                if (fullList[j].Id == employees[i].Id) k = j;
            }
            fullList.RemoveAt(k);
            employees = RebuildData();
        }

        /// <summary>
        /// Выводит в консоль данные всех сотрудников
        /// </summary>
        public void ShowAllEmployees()
        {
            employees = fullList.ToArray();
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
        /// Выводит в консоль данные сотрудников
        /// </summary>
        public void ShowEmployees()
        {
            foreach (Employee emp in employees)
            {
                foreach (string str in emp.GetEmpInformation())
                {
                    Console.WriteLine(str);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Сортирует по дате создания(type == 0), Ф.И.О.(type == 1), возрасту(type == 2), ID(type == 3), росту(type == 4),
        /// дате рождения(type == 5), месту рождения(type == 6)
        /// </summary>
        /// <param name="type"></param>
        public void SortData(int type)
        {
            switch (type)
            {
                case 0:
                    employees = employees.OrderBy(employee => employee.DateTime).ToArray();
                    ShowEmployees();
                    break;
                case 1:
                    employees = employees.OrderBy(employee => employee.FullName).ToArray();
                    ShowEmployees();
                    break;
                case 2:
                    employees = employees.OrderBy(employee => employee.Age).ToArray();
                    ShowEmployees();
                    break;
                case 3:
                    employees = employees.OrderBy(employee => employee.Id).ToArray();
                    ShowEmployees();
                    break;
                case 4:
                    employees = employees.OrderBy(employee => employee.Height).ToArray();
                    ShowEmployees();
                    break;
                case 5:
                    employees = employees.OrderBy(employee => employee.DateOfBirth).ToArray();
                    ShowEmployees();
                    break;
                case 6:
                    employees = employees.OrderBy(employee => employee.PlaceOfBirth).ToArray();
                    ShowEmployees();
                    break;

            }
        }

        /// <summary>
        /// Изменяет имя выбранного сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newFullName"></param>
        public void ChangeFullName(int id, string newFullName)
        {
            int i = FindEmployeeById(id);
            employees[i].FullName = newFullName;
        }

        /// <summary>
        /// Изменяет дату рождения и возраст выбранного сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dateTime"></param>
        public void ChangeDateOfBirth(int id, DateTime dateTime)
        {
            int i = FindEmployeeById(id);
            employees[i].DateOfBirth = dateTime;
            employees[i].ChangeAge();
        }

        /// <summary>
        /// Изменяет рост выбранного сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newHeight"></param>
        public void ChangeHeight(int id, ushort newHeight)
        {
            int i = FindEmployeeById(id);
            employees[i].Height = newHeight;
        }

        /// <summary>
        /// Изменяет место рождения выбранного сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPlaceOfBirth"></param>
        public void ChangePlaceOfBirth(int id, string newPlaceOfBirth)
        {
            int i = FindEmployeeById(id);
            employees[i].PlaceOfBirth = newPlaceOfBirth;
        }

        /// <summary>
        /// Отбирает сотрудников добавленных в диапазоне дат
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void ChooseByDateRange(DateTime min, DateTime max)
        {
            List<Employee> newEmployees = new List<Employee>();
            foreach (Employee employee in employees)
            {
                if (employee.DateTime >= min && employee.DateTime <= max) newEmployees.Add(employee);
                else continue;
            }
            employees = newEmployees.ToArray();
            ShowEmployees();
        }

        /// <summary>
        /// Сохраняет список в файл
        /// </summary>
        /// <param name="path"></param>
        public void SaveData(string path)
        {
            /*List<Employee> employeesToWrite = new List<Employee>();*/
            /*Employee[] lateEmps = Upload(path);*/
            employees = fullList.ToArray();
            File.Delete(path);
            File.Create(path).Close();
            foreach(Employee emp in employees)
            {
               
                string[] empData = new string[7];
                for (int i = 0; i < 7; i++)
                {
                    switch(i)
                    {
                        case 0:
                            empData[i] = emp.Id.ToString();
                            break;
                        case 1:
                            empData[i] = emp.DateTime.ToString();
                            break;
                        case 2:
                            empData[i] = emp.FullName;
                            break;
                        case 3:
                            empData[i] = emp.Age.ToString();
                            break;
                        case 4:
                            empData[i] = emp.Height.ToString();
                            break;
                        case 5:
                            empData[i] = emp.DateOfBirth.ToString("d");
                            break;
                        case 6:
                            empData[i] = emp.PlaceOfBirth;
                            break;
                    }

                }
                using (StreamWriter file = new StreamWriter(path, true))
                {
                    for (int j = 0; j < empData.Length - 1; j++)
                        file.Write($"{empData[j]}#");
                    file.Write($"{empData[6]}\n");
                }

            }

            if (!File.Exists("fired_" + path)) File.Create("fired_" + path).Close();
            foreach (Employee emp in firedEmps)
            {

                string[] firedEmpData = new string[7];
                for (int i = 0; i < 7; i++)
                {
                    switch (i)
                    {
                        case 0:
                            firedEmpData[i] = emp.Id.ToString();
                            break;
                        case 1:
                            firedEmpData[i] = emp.DateTime.ToString();
                            break;
                        case 2:
                            firedEmpData[i] = emp.FullName;
                            break;
                        case 3:
                            firedEmpData[i] = emp.Age.ToString();
                            break;
                        case 4:
                            firedEmpData[i] = emp.Height.ToString();
                            break;
                        case 5:
                            firedEmpData[i] = emp.DateOfBirth.ToString("d");
                            break;
                        case 6:
                            firedEmpData[i] = emp.PlaceOfBirth;
                            break;
                    }

                }
                using (StreamWriter file = new StreamWriter("fired_" + path, true))
                {
                    for (int j = 0; j < firedEmpData.Length - 1; j++)
                        file.Write($"{firedEmpData[j]}#");
                    file.Write($"{firedEmpData[6]}\n");
                }

            }

        }
    }
}
