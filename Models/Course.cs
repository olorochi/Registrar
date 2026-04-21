using DAL;
using Newtonsoft.Json;
using System;

namespace Models
{
    public class Course : Record
    {
        // ordered to minimize padding
        public int Session;
        public string Code;
        public string Title;
    }
}
