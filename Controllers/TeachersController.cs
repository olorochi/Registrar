using Models;
using System.Web.Mvc;
using DAL;
using Controllers;

namespace Registrar.Controllers
{
    public class TeachersController : ScholarController<Teacher, Allocation>
    {
        public override Repository<Teacher> Repository => DB.Teachers;
        public override Repository<Allocation> SelectionRepository => DB.Allocations;
        public override SessionLocals GetLocals() => SessionState.Teachers;

        public override Allocation ConstructSelItem(int scholar, int course, int year)
        {
            if (DB.Allocations.ToList().Find(a => a.CourseId == course && a.Year == year) != null)
                return null;
            return base.ConstructSelItem(scholar, course, year);
        }
    }
}
