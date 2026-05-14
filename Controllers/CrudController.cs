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
    public abstract class CrudController<T, S> : Controller where T : Record where S : Assoc, new()
    {
        const string IllegalAccessUrl = "/Accounts/Login?message=Tentative d'accès illégal!&success=false";
        public abstract Repository<T> Repository { get; }
        public abstract Repository<S> SelectionRepository { get; }
        public abstract int GetIdFromSelection(S selection);
        public abstract SessionLocals GetLocals();
        public abstract S ConstructSelItem(int self, int assoc, int year);
        public virtual bool ValidateEdit(T old, T t) => true;
        public virtual bool ValidateCreate(T t) => true;

        public static Session SessionState => Models.Session.Instance;

        public string Controller => HttpContext.Request.RequestContext.RouteData.Values["controller"].ToString().ToLower();
        public ActionResult RedirectToLocalAction(string action, object args = null) => RedirectToAction(action, Controller, args);

        [UserAccess(Access.View)]
        public ActionResult GetListContent(bool forceRefresh = false)
        {
            try
            {
                if (Repository.HasChanged || forceRefresh)
                {
                    var locals = GetLocals();
                    IEnumerable<T> result = Repository.ToList();
                    if (locals.Search)
                        result = result.Where(s => true);

                    return PartialView(result);
                }

                return null;
            }
            catch (Exception ex)
            {
                return Content("Erreur interne" + ex.Message, "text/html");
            }
        }

        public ActionResult ToggleSearch()
        {
            var locals = GetLocals();
            locals.Search = !locals.Search;
            return RedirectToLocalAction("List");
        }

        public ActionResult SetSearchString(string value)
        {
            GetLocals().SearchString = value;
            return RedirectToLocalAction("List");
        }

        [UserAccess(Access.View)]
        public ActionResult GetDetailsInfoPanel(bool forceRefresh = false)
        {
            if (Repository.HasChanged || forceRefresh)
            {
                var locals = GetLocals();
                return PartialView(Repository.Get(locals.CurrentId));
            }

            return null;
        }

        [UserAccess(Access.View)]
        public ActionResult GetDetailsSelectPanel(bool forceRefresh = false)
        {
            try
            {
                if (Repository.HasChanged || forceRefresh)
                {
                    var locals = GetLocals();
                    return PartialView(SelectionRepository.ToList().Where(s => GetIdFromSelection(s) == locals.CurrentId));
                }

                return null;
            }
            catch (Exception ex)
            {
                return Content("Erreur interne" + ex.Message, "text/html");
            }
        }

        [UserAccess(Access.View)]
        public ActionResult List()
        {
            return View();
        }

        [UserAccess(Access.View)]
        public ActionResult Details(int id)
        {
            var locals = GetLocals();
            locals.CurrentId = id;
            return View();
        }

        public ActionResult SetNextSession()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult SetNextSession(NextSession nextSession)
        {
            SessionState.NextSession = nextSession;
            return RedirectToLocalAction("List");
        }

        [UserAccess(Access.Write)]
        public virtual ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAccess(Access.Write)]
        public ActionResult Create(T t)
        {
            try
            {
                if (ValidateCreate(t))
                    Repository.Add(t);
                else
                    return RedirectToLocalAction("Create");

                return RedirectToLocalAction("List");
            }
            catch
            {
                return View();
            }
        }

        [UserAccess(Access.Write)]
        public virtual ActionResult Edit()
        {
            int id = GetLocals().CurrentId;
            if (id != 0)
            {
                T t = Repository.Get(id);
                if (t != null)
                {
                    return View(t);
                }
            }
            return Redirect(IllegalAccessUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAccess(Access.Write)]
        public ActionResult Edit(T t, List<int> selection)
        {
            int id = GetLocals().CurrentId;

            T storedT = Repository.Get(id);
            if (storedT != null)
            {
                t.Id = id;
                if (ValidateEdit(storedT, t))
                    Repository.Update(t);
                else
                    return RedirectToLocalAction("Edit");

                if (selection != null)
                {
                    foreach (S s in SelectionRepository.ToList().Where(s => id == GetIdFromSelection(s)).ToList())
                        SelectionRepository.Delete(s.Id);
                    foreach (int s in selection)
                    {
                        var it = ConstructSelItem(id, s, SessionState.NextSession.Year);
                        if (it != null)
                            SelectionRepository.Add(it);
                    }
                }
            }
            return RedirectToLocalAction("Details", new{id});
        }
    }
}