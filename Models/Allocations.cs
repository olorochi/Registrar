using DAL;
using Newtonsoft.Json;

namespace Models
{
    public class Allocation : Assoc
    {
        public int TeacherId { get => ScholarId; set => ScholarId = value; }
        [JsonIgnore] public Teacher Teacher => DB.Teachers.Get(TeacherId);
    }
}
