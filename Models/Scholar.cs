using DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Models
{
    public abstract class Scholar<T> : Record where T : Assoc
    {
        static protected Random rand = new Random();
        public abstract Repository<T> SelectionRepository { get; }
        public abstract int TryGenerateCode();

        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }

        public virtual void SetCode(int code) => Code = code.ToString();
        public int GenerateCode()
        {
            int code;
            var students = DB.Students.ToList();

            do code = TryGenerateCode();
            while (students.Find(existing => int.Parse(existing.Code) == code) != null);

            return code;
        }

        [JsonIgnore] public string FullName => LastName + " " + FirstName;
        [JsonIgnore] public string Caption => Code + " " + FullName;
        [JsonIgnore] List<T> Assocs => SelectionRepository.ToList().Where(t => t.ScholarId == Id).ToList();
        [JsonIgnore] List<T> NextSessionAssocs
            => SelectionRepository.ToList().Where(t => t.ScholarId == Id && t.IsNextSession).ToList();

        static List<Course> GetCourses(List<T> assocs) =>
            assocs.Select(t => t.Course).OrderBy(c => c.Code).ToList();

        [JsonIgnore] public List<Course> Courses { get => GetCourses(Assocs); }
        [JsonIgnore] public List<Course> NextSessionCourses { get => GetCourses(NextSessionAssocs); }

        [JsonIgnore] public SelectList CoursesSelectList => SelectListUtilities<Course>.Convert(Courses, "Caption");
        [JsonIgnore] public SelectList NextSessionCoursesToSelectList => SelectListUtilities<Course>.Convert(NextSessionCourses, "Caption");
    }
}
