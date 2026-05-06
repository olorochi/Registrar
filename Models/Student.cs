using DAL;
using Newtonsoft.Json;
using System;

namespace Models
{
    public class Student : Record
    {
        // ordered to minimize padding
        public int Code;
        public DateTime BirthDate;
        public string Phone;
        public string FirstName;
        public string LastName;
        public string Email;

        public int GetYear() => int.Parse(Code.ToString().Substring(0, 4));
    }
}
