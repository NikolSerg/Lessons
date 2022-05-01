using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Practice_8
{
    internal class Program
    {
        static void FillList(List<int> list)
        {
            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                list.Add(r.Next(101));
            }
        }
        static void ShowList(List<int> list)
        {
            int j = 0;
            foreach (int i in list)
            {
                j++;
                Console.Write($"{i, 3} ");
                if (j % 10 == 0) Console.WriteLine();
            }
        }
        static void DeleteRange(List<int> list)
        {
            List<int> newList = new List<int>();
            foreach(int i in list)
            {
                if (i > 25 && i < 50) continue;
                else newList.Add(i);
            }
            list.Clear();
            foreach(int i in newList)
            {
                list.Add(i);
            }
        }
        static void Pause()
        {
            Console.WriteLine("\n\n");
            Console.ReadKey();
        }


        static bool AddContact(Dictionary<string, string> dict)
        {
            bool cont = true;
            List<string> phoneNumbers = new List<string>();
            string fullName;
            Console.Write("Введите номера телефонов: ");
            while(true)
            {
                string str = Console.ReadLine();
                if (str == "") break;
                phoneNumbers.Add(str);
            }
            Console.WriteLine("Введите Ф.И.О.: ");
            fullName = Console.ReadLine();
            foreach(string i in phoneNumbers)
            {
                dict.Add(i, fullName);
            }
            if (EscapeProgramm())
                cont = false;
            Console.Clear();
            return cont;
        }
        static bool FindContact(Dictionary<string, string> dict)
        {
            bool cont = true;
            Console.Clear();
            Console.Write("Введите номер телефона: ");
            string phone = Console.ReadLine();
            string value = "";
            if (dict.TryGetValue(phone, out value)) Console.WriteLine($"Владелец: {value}");
            else Console.WriteLine("Владелец не найден");
            if (EscapeProgramm())
                cont = false;
            return cont;
        }
        static void PhoneBook()
        {
            Dictionary<string, string> phoneBook = new Dictionary<string, string>();

            while (true)
            {
                bool cont = AddContact(phoneBook);
                if (cont) continue;
                else break;
            }
            while (true)
            {
                bool cont = FindContact(phoneBook);
                if (cont) continue;
                else break;
            }
        }


        static void HashSetProgramm()
        {
            HashSet<int> numbers = new HashSet<int>();
            while (true)
            {
                Console.Clear();
                Console.Write("Введите число: ");
                int number;
                try {  number = Convert.ToInt32(Console.ReadLine());  }
                catch { continue; }
                if (numbers.Add(number)) Console.WriteLine("Число успешно добавлено");
                else Console.WriteLine("Число уже вводилось ранее");
                if (EscapeProgramm()) break;
                }
        }
        static bool EscapeProgramm()
        {
            bool esc = false;
            Console.WriteLine("Хотите продолжить?(Esc, если нет)");
            if (Console.ReadKey().Key == ConsoleKey.Escape) esc = true;
            return esc;
        }


        static void XelementProgramm()
        {
            XElement person = CreatePerson();
            SerializePerson(person);
        }

        static XElement CreatePerson()
        {
            XElement person = new XElement("Person");
            XElement adress = new XElement("Adress");
            XElement street = new XElement("Street");
            XElement houseNumber = new XElement("HouseNumber");
            XElement appartmentNumber = new XElement("AppartmentNumber");
            XElement phones = new XElement("Phones");
            XElement mPhone = new XElement("MobilePhone");
            XElement aPhone = new XElement("AppartmentPhone");

            Console.WriteLine("Введите данные.");
            Console.Write("Ф.И.О.: ");
            person.SetAttributeValue("name", Console.ReadLine());
            Console.Write("Название улицы: ");
            street.SetValue(Console.ReadLine());
            Console.Write("Номер дома: ");
            houseNumber.SetValue(Console.ReadLine());
            Console.Write("Номер квартиры: ");
            appartmentNumber.SetValue(Console.ReadLine());
            Console.Write("Номер мобильного телефона: ");
            mPhone.SetValue(Console.ReadLine());
            Console.Write("Номер домашнего телефона: ");
            aPhone.SetValue(Console.ReadLine());

            person.Add(adress);
            person.Add(phones);

            adress.Add(street);
            adress.Add(houseNumber);
            adress.Add(appartmentNumber);

            phones.Add(mPhone);
            phones.Add(aPhone);
            return person;
        }
        static void SerializePerson(XElement person)
        {
            string path = "person.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(XElement));
            using (Stream fStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                serializer.Serialize(fStream, person);
            }
        }


        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();

            FillList(numbers);
            ShowList(numbers);
            Pause();
            DeleteRange(numbers);
            ShowList(numbers);
            Pause();
            Console.Clear();




            PhoneBook();
            Pause();
            Console.Clear();



            HashSetProgramm();
            Console.Clear();


            XelementProgramm();
            Pause();
        }
    }
}
