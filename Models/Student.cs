using DAL;
using Newtonsoft.Json;
using System;

namespace Models
{
    public class Student : Record
    {
        // ordered to minimize padding
        public int Code;
        public int Phone;
        public DateTime BirthDate;
        public string FirstName;
        public string LastName;
        public string Email;
    }
}
