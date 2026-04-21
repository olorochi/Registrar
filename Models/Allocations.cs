using DAL;
using Newtonsoft.Json;
using System;

namespace Models
{
    public class Allocation : Record
    {
        public int TeacherId;
        public int CourseId;
        public int Year;
    }
}
