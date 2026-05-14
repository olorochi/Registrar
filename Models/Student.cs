using DAL;
using System;

namespace Models
{
    public class Student : Scholar<Registration>
    {
        public override Repository<Registration> SelectionRepository => DB.Registrations;

        public DateTime BirthDate { get; set; }

        public int GetYear() => int.Parse(Code.ToString().Substring(0, 4));

        public override int TryGenerateCode()
        {
            int sep = (int)Math.Pow(10, 5); // document says 6 but pre-inserted data uses 5
            return DateTime.Now.Year * sep + rand.Next() % sep;
        }
    }
}
