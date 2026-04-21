using DAL;
using Newtonsoft.Json;
using System;

namespace Models
{
    public class Registration : Record
    {
        public int StudentId;
        public int CourseId;
        public int Year;
    }
}
