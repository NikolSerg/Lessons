using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_7
{
    struct Employee
    {
        int id;
        DateTime dateTime;
        string fullName;
        ushort age;
        ushort height;
        DateTime dateOfBirth;
        string placeOfBirth;
        bool fired;

        public int Id { get { return id; } }

        public DateTime DateOfBirth { get { return dateOfBirth; } set { dateOfBirth = value; } }

        public string FullName { get { return fullName; } set { fullName = value; } }

        public ushort Height { get { return height; } set { height = value; } }

        public DateTime DateTime { get { return dateTime; }}

        public string PlaceOfBirth { get { return placeOfBirth; } set { placeOfBirth = value; } }

        public ushort Age { get { return age; }}

        public bool Fired { get { return fired; } }



        public void Fire()
        {
            this.fired = true;
        }

        public string[] GetEmpInformation()
        {
            string[] data = {"ID: " + id.ToString(),"Дата и время добавления записи: " + dateTime.ToString(),
                "Ф.И.О.: " + fullName,"Возраст: " + age.ToString(),"Рост: " + height.ToString(),
                "Дата рождения: " + dateOfBirth.ToString("d"),"Место рождения: " + placeOfBirth };
            return data;
        }

        public void ChangeAge()
        {
            age = (ushort)((DateTime.Now - dateOfBirth).TotalDays / 365);
        }

        public Employee(int id, DateTime dateTime, string fullName, ushort age, ushort height, DateTime dateOfBirth, string placeOfBirth)
        {
            this.id = id;
            this.dateTime = dateTime;
            this.fullName = fullName;
            this.age = age;
            this.height = height;
            this.dateOfBirth = dateOfBirth;
            this.placeOfBirth = placeOfBirth;
            fired = false;
        }

        public Employee(int id, string fullName, ushort height, DateTime dateOfBirth, string placeOfBirth) :
            this(id, DateTime.Now, fullName, 0,  height,  dateOfBirth, placeOfBirth)
        {
            age = (ushort)((DateTime.Now - dateOfBirth).TotalDays/365);
        }
    }
}
