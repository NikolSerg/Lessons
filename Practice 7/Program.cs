using System;

namespace Practice_7
{

    internal class Program
    {

        static void Main(string[] args)
        {
            string path = "Employees.txt";

            Employees employees = new Employees(path);

            Console.WriteLine("Справочник \"Сотрудники\"");
            Start(employees, path);


            static void Pause()
            {
                Console.ReadKey();
            }

            static void Start(Employees employees, string path)
            {
                bool cont = true;
                while (cont)
                {
                    employees.ShowEmployees();
                    Console.WriteLine("Если хотите выбрать данные из диапазона дат нажмите \"1\"\n" +
                        "Если хотите отсортировать данные нажмите \"2\"\n" +
                        "Если хотите выбрать сотрудника нажмите \"3\"\n" +
                        "Если хотите показать всех сотрудников нажмите \"4\"\n" +
                        "Если хотите добавить сотрудника нажмите \"5\"\n" +
                        "Если хотите сохранить изменения нажмите \"6\"\n" +
                        "Если хотите выйти нажмите \"Esc\"");

                    ConsoleKey mainSwitcher = Console.ReadKey().Key;
                    Console.Clear();
                    switch (mainSwitcher)
                    {
                        case ConsoleKey.NumPad1:
                            Console.Write("\nВведите дату начала отсчета: ");
                            DateTime min = Convert.ToDateTime(Console.ReadLine());

                            Console.Write("\nВведите дату окончания отсчета: ");
                            DateTime max = Convert.ToDateTime(Console.ReadLine());
                            Console.Clear();
                            employees.ChooseByDateRange(min, max);
                            
                            break;



                        case ConsoleKey.NumPad2:
                            Console.WriteLine("Если хотите отсортировать данные по ID нажмите \"1\"\n" +
                                "Если хотите отсортировать данные по дате добавления нажмите \"2\"\n" +
                                "Если хотите отсортировать данные по Ф.И.О. нажмите \"3\"\n" +
                                "Если хотите отсортировать данные по возрасту нажмите \"4\"\n" +
                                "Если хотите отсортировать данные по росту нажмите \"5\"\n" +
                                "Если хотите отсортировать данные по месту рождения нажмите \"6\"\n");

                            ConsoleKey key = Console.ReadKey().Key;
                            Console.Clear();
                            switch(key)
                            {
                                case ConsoleKey.NumPad1:
                                    employees.SortData(3);
                                    break;
                                case ConsoleKey.NumPad2:
                                    employees.SortData(0);
                                    break;
                                case ConsoleKey.NumPad3:
                                    employees.SortData(1);
                                    break;
                                case ConsoleKey.NumPad4:
                                    employees.SortData(2);
                                    break;
                                case ConsoleKey.NumPad5:
                                    employees.SortData(4);
                                    break;
                                case ConsoleKey.NumPad6:
                                    employees.SortData(5);
                                    break;
                                case ConsoleKey.NumPad7:
                                    employees.SortData(6);
                                    break;
                            }
                            break;


                        case ConsoleKey.NumPad3:
                            employees.ShowEmployees();
                            Console.Write("Введите ID сотрудника, чтобы посмотреть запись: ");
                            int id = int.Parse(Console.ReadLine());
                            Console.Clear();
                            employees.ShowEmployeeByID(id);
                            Console.Write("\nНажмите \"Esc\" чтобы вернуться, \"1\" - удалить запись и вернуться," +
                                " \"2\" - изменить запись");

                            ConsoleKey changer = Console.ReadKey().Key;
                            Console.Clear();
                            switch(changer)
                            {
                                case ConsoleKey.Escape:
                                    break;
                                case ConsoleKey.NumPad1:
                                    employees.FireEmployee(id);
                                    break;
                                case ConsoleKey.NumPad2:
                                    bool changing = true;
                                    while (changing)
                                    {
                                        employees.ShowEmployeeByID(id);
                                        Console.Write("\nНажмите \"1\" чтобы изменить Ф.И.О., \"2\" - дату рождения, " +
                                            "\"3\" - рост,\n\"4\" - место рождения, \"Esc\" - выйти из меню редактирования");
                                        switch (Console.ReadKey().Key)
                                        {
                                            case ConsoleKey.NumPad1:
                                                Console.Write("\nВведите новые Ф.И.О: ");
                                                employees.ChangeFullName(id, Console.ReadLine());
                                                Console.Clear();
                                                break;
                                            case ConsoleKey.NumPad2:
                                                Console.Write("\nВведите дату рождения: ");
                                                employees.ChangeDateOfBirth(id, Convert.ToDateTime(Console.ReadLine()));
                                                Console.Clear();
                                                break;
                                            case ConsoleKey.NumPad3:
                                                Console.Write("\nВведите новые данные роста: ");
                                                employees.ChangeHeight(id, ushort.Parse(Console.ReadLine()));
                                                Console.Clear();
                                                break;
                                            case ConsoleKey.NumPad4:
                                                Console.Write("\nВведите место рождения: ");
                                                employees.ChangePlaceOfBirth(id, Console.ReadLine());
                                                Console.Clear();
                                                break;
                                            case ConsoleKey.Escape:
                                                changing = false;
                                                break;
                                        }
                                    }
                                    break;
                            }
                            break;


                        case ConsoleKey.NumPad4:
                            Console.Clear();
                            employees.ShowAllEmployees();
                            break;


                        case ConsoleKey.NumPad5:
                            Console.Write("Введите данные нового сотрудника:\nФ.И.О.: ");
                            string fullName = Console.ReadLine();

                            Console.Write("Рост: ");
                            ushort height = ushort.Parse(Console.ReadLine());

                            Console.Write("Дата рождения: ");
                            DateTime dateOfBirth = Convert.ToDateTime(Console.ReadLine());

                            Console.Write("Место рождения: ");
                            string placeOfBirth = Console.ReadLine();

                            employees.AddEmployee(fullName, height, dateOfBirth, placeOfBirth);
                            break;


                        case ConsoleKey.NumPad6:
                            employees.SaveData(path);
                            break;


                        case ConsoleKey.Escape:
                            Console.WriteLine("Сохранить изменения?(Нажмите \"1\", если хотите сохранить)");
                            bool save = Console.ReadKey().Key == ConsoleKey.NumPad1 ? true : false;
                            if (save) employees.SaveData(path);
                            cont = false;
                            break;

                        default:
                            employees.ShowEmployees();
                            break;
                    }
                }
            }
        }
    }

}

