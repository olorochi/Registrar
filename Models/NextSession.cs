using System;
using System.Web;

namespace Models
{
    public enum Season
    {
        Automne = 1, // month limit for automn registrations
        Hiver = 8 // month limit for winter registrations
    }

    public class NextSession
    {
        public int Year { get; set; }
        public Season Season { get; set; }

        public NextSession()
        {
            DateTime now = DateTime.Now;
            if (now.Month < (int)Season.Hiver)
            {
                Season = Season.Automne;
                Year = now.Year;
            }
            else
            {
                Season = Season.Hiver;
                Year = now.Year + 1;
            }
        }

        public bool AvailableNextSession(int session) => (Season == Season.Automne) == ((session & 1) == 1);
        public bool IsNextSession(int session, int year) => year == Year && AvailableNextSession(session);

        public string ShortCaption => Season + " " + Year;
        public string Caption => "Session courante : " + ShortCaption;
    }
}