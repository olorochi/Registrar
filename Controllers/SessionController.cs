using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Models;

namespace Registrar.Controllers
{

    public class SessionController : Controller
    {
        public Session GetSession() => GetSession(Session);
        public static Session GetSession(HttpSessionStateBase session) => // for use in base Controller contexts (views) (Actually that crashes. Thx asp)
            session["session"] == null ? new Session() : (Session)session["session"];
        public void CommitSession(Session session) => Session["session"] = session;
    }
}