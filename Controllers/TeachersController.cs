using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Controllers.AccessControl;

namespace Registrar.Controllers
{
    public class TeachersController : Controller
    {
        [UserAccess(Models.Access.View)]
        public ActionResult List()
        {
            return View();
        }

        [UserAccess(Models.Access.View)]
        public ActionResult Details(int id)
        {
            return View();
        }

        [UserAccess(Models.Access.Write)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [UserAccess(Models.Access.Write)]
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

        [UserAccess(Models.Access.Write)]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [UserAccess(Models.Access.Write)]
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

        [UserAccess(Models.Access.Write)]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [UserAccess(Models.Access.Write)]
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
