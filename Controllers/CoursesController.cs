using Models;
using DAL;
using System.Web.Mvc;
using static Controllers.AccessControl;

namespace Registrar.Controllers
{
    public class CoursesController : CrudController<Course, Registration>
    {
        public override Repository<Course> Repository => DB.Courses;
        public override Repository<Registration> SelectionRepository => DB.Registrations;
        public override int GetIdFromSelection(Registration registration) => registration.CourseId;
        public override SessionLocals GetLocals() => SessionState.Courses;
        public override Registration ConstructSelItem(int course, int student, int year) =>
            new Registration { CourseId=course, StudentId=student, Year=year };

        static bool Validate(Course course) => course.Session >= 1 && course.Session <= 6 && !DB.Courses.ToList().Exists(c => c.Code == course.Code && c.Id != course.Id);
        public override bool ValidateCreate(Course course) => Validate(course);
        public override bool ValidateEdit(Course old, Course course) => Validate(course);

        [UserAccess(Access.Write)]
        public ActionResult Delete()
        {
            int id = SessionState.Courses.CurrentId;
            Course course = DB.Courses.Get(id);
            foreach (Registration r in course.Registrations)
                DB.Registrations.Delete(r.Id);
            foreach (Allocation a in course.Allocations)
                DB.Allocations.Delete(a.Id);

            Repository.Delete(id);
            return RedirectToLocalAction("list");
        }
    }
}
