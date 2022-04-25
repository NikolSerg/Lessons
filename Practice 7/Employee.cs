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

        public int Id { get { return id; } set { id = value; } }
        public DateTime DateOfBirth { get { return dateOfBirth; } set { dateOfBirth = value; } }
        public string FullName { get { return fullName; } set { fullName = value; } }
        public ushort Height { get { return height; } set { height = value; } }
        public DateTime DateTime { get { return dateTime; } set { dateTime = value;} }
        public string PlaceOfBirth { get { return placeOfBirth; } set { placeOfBirth = value; } }
        public ushort Age { get { return age; } set { age = value; } }
        public bool Fired { get { return fired; } }



        public void Fire()
        {
            this.fired = true;
        }
        public string[] GetEmpInformation()
        {
            string[] data = {id.ToString(), dateTime.ToString(), fullName, age.ToString(),
                height.ToString(), dateOfBirth.ToString("d"), placeOfBirth };
            return data;
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


    }
}
