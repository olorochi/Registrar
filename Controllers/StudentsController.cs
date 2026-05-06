using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using DAL;
using static Controllers.AccessControl;

namespace Registrar.Controllers
{
    public class StudentsController : SessionController
    {
        const string IllegalAccessUrl = "/Accounts/Login?message=Tentative d'accès illégal!&success=false";

        [UserAccess(Access.View)]
        public ActionResult List()
        {
            return View();
        }

        public ActionResult ToggleSearch()
        {
            var session = GetSession();
            session.Students.Search = !session.Students.Search;
            CommitSession(session);
            return RedirectToAction("List");
        }

        [UserAccess(Access.View)]
        public ActionResult GetListContent(bool forceRefresh = false)
        {
            try
            {
                IEnumerable<Student> result = null;

                if (DB.Students.HasChanged || forceRefresh)
                {
                    var session = GetSession().Students;
                    IEnumerable<Student> students = DB.Students.ToList();
                    if (session.Search)
                        students = students.Where(s =>
                            s.FirstName.ToLower().Contains(session.SearchString)
                            || s.LastName.ToLower().Contains(session.SearchString)
                        );

                    return PartialView(students);
                }

                return null;
            }
            catch (Exception ex)
            {
                return Content("Erreur interne" + ex.Message, "text/html");
            }
        }

        [UserAccess(Access.View)]
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [UserAccess(Access.Write)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [UserAccess(Access.Write)]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [UserAccess(Access.Write)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [UserAccess(Access.Write)]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [UserAccess(Access.Write)]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
