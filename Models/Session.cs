using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public enum Season
    {
        Automne,
        Hiver
    }

    public class ControllerLocals
    {
        public string SearchString = "";
        public int CurrentId = 0;
        public bool Search = false;
    }

    public class Session
    {
        public ControllerLocals Students = new ControllerLocals();
        public ControllerLocals Teachers = new ControllerLocals();
        public ControllerLocals Courses = new ControllerLocals();
        public int AllocationYear = DateTime.Now.Year;
        public Season AllocationSeason = Season.Automne;

        public ControllerLocals GetLocals(string controller)
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