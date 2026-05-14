using DAL;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

public class CourseRepository : Repository<Course>
{
    NextSession NextSession => Session.Instance.NextSession;
    SelectList MakeSelectList(IEnumerable<Course> courses) => SelectListUtilities<Course>.Convert(courses, "Caption");
    public SelectList NextSessionSelectList() => MakeSelectList(ToList().Where(c => NextSession.AvailableNextSession(c.Session)).OrderBy(c => c.Caption));
    public SelectList NextSessionAllocationsSelectList() => MakeSelectList(
        ToList().Where(c => NextSession.AvailableNextSession(c.Session) && !DB.Allocations.ToList().Exists(a => a.Year == NextSession.Year && a.CourseId == c.Id)).OrderBy(c => c.Caption)
    );
}