using Models;
using DAL;
using System.Web.Mvc;
using Controllers;

namespace Registrar.Controllers
{
    public class StudentsController : ScholarController<Student, Registration>
    {
        public override Repository<Student> Repository => DB.Students;
        public override Repository<Registration> SelectionRepository => DB.Registrations;
        public override SessionLocals GetLocals() => SessionState.Students;

        public ActionResult GetYearsList(bool forceRefresh = false)
        {
            try
            {
                if (SessionState.Students.Search)
                    return PartialView(Repository.ToList());

                return null;
            }
            catch (System.Exception ex)
            {
                return Content("Erreur interne" + ex.Message, "text/html");
            }
        }

        public ActionResult SetSearchYear(int year)
        {
            SessionState.Students.SearchYear = year;
            return RedirectToAction("List");
        }
    }
}
