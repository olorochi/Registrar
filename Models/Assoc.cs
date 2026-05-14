using DAL;
using Newtonsoft.Json;

namespace Models
{
    public abstract class Assoc : Record
    {
        public int CourseId;
        public int ScholarId;
        public int Year;

        [JsonIgnore] public Course Course => DB.Courses.Get(CourseId);
        [JsonIgnore] public bool IsNextSession { get => Session.Instance.NextSession.IsNextSession(Course.Session, Year); }
    }
}