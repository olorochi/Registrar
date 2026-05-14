using System.Web;

namespace Models
{

    public class SessionLocals
    {
        public string SearchString = "";
        public int CurrentId = 0;
        public bool Search = false;
    }

    public class ScholarSessionLocals : SessionLocals
    {
        public int CurrentCode = 0;
    }

    public class StudentSessionLocals : ScholarSessionLocals
    {
        public int SearchYear = 0;
    }

    public class Session
    {
        public static Session Instance
        {
            get
            {
                var session = HttpContext.Current.Session;
                if (session["session"] == null)
                    session["session"] = new Session();
                return (Session)session["session"];
            }
            set => HttpContext.Current.Session["session"] = value;
        }

        public StudentSessionLocals Students = new StudentSessionLocals();
        public ScholarSessionLocals Teachers = new ScholarSessionLocals();
        public SessionLocals Courses = new SessionLocals();
        public NextSession NextSession = new NextSession();

        private Session() { }

        public SessionLocals GetLocals(string controller)
        {
            switch(controller)
            {
                case "students":
                    return Students;
                case "teachers":
                    return Teachers;
                case "courses":
                    return Courses;
                default:
                    return null;
            }
        }
    }
}