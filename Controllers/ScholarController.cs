using DAL;
using Models;
using Registrar.Controllers;
using System.Linq;
using System.Web.Mvc;
using static Controllers.AccessControl;

namespace Controllers
{
    public abstract class ScholarController<T, S> : CrudController<T, S> where T : Scholar<S>, new() where S : Assoc, new()    {
        public override int GetIdFromSelection(S s) => s.ScholarId;
        public override bool ValidateEdit(T old, T s)
        {
            s.Code = old.Code;
            return true;
        }

        public override S ConstructSelItem(int scholar, int course, int year) =>
            new S { CourseId=course, ScholarId=scholar, Year=year };
        public ScholarSessionLocals GetScholarLocals() => (ScholarSessionLocals)GetLocals();

        public override bool ValidateCreate(T t)
        {
            t.SetCode(GetScholarLocals().CurrentCode);
            return true;
        }

        [UserAccess(Access.Write)]
        public override ActionResult Create()
        {
            GetScholarLocals().CurrentCode = new T().GenerateCode();
            return base.Create();
        }


        [UserAccess(Access.Write)]
        public ActionResult Delete()
        {
            int id = GetLocals().CurrentId;
            Repository.Delete(id);
            foreach (S sel in SelectionRepository.ToList().Where(s => s.ScholarId == id).ToList())
                SelectionRepository.Delete(sel.Id);

            return RedirectToLocalAction("list");
        }
    }
}