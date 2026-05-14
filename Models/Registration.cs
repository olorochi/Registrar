using DAL;
using Newtonsoft.Json;

namespace Models
{
    public class Registration : Assoc
    {
        public int StudentId { get => ScholarId; set => ScholarId = value; } 
        [JsonIgnore] public Student Student => DB.Students.Get(StudentId);
    }
}
