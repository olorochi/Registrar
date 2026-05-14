using DAL;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Models
{
    public class Course : Record
    {
        public int Session { get; set; } = 1;
        public string Code { get; set; }
        public string Title { get; set; }

        [JsonIgnore] public string Caption => string.Format("[{0}] {1} {2}", Session, Code, Title);

        [JsonIgnore] public List<Allocation> Allocations => DB.Allocations.ToList().Where(a => a.CourseId == Id).ToList();
        [JsonIgnore] public List<Registration> Registrations => DB.Registrations.ToList().Where(r => r.CourseId == Id).ToList();
        [JsonIgnore] public List<Student> Students => Registrations.Select(t => t.Student).OrderBy(c => c.Code).ToList();
        [JsonIgnore] public SelectList StudentsSelectList => SelectListUtilities<Student>.Convert(Students, "Caption");
    }
}
