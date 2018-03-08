using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class LoginController : Controller
    {
        private CommunityAssist2017Entities db = new CommunityAssist2017Entities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(
            [Bind(Include = 
            "UserName, Password, PersonKey")
            ]Login lg)
        {

            lg.PersonKey = 0;
            int result = db.usp_Login(lg.UserName, lg.Password);

            Message m = new Message();

            if (result == -1)
            {
                m.MessageText = "Validation failed, please try again or register.";
            }
            else
            {
                Session["ukey"] = (from p in db.People
                            where p.PersonEmail.Equals(lg.UserName)
                            select p.PersonKey).FirstOrDefault();
                lg.PersonKey = (int)Session["ukey"];
                m.MessageText = "Thank you for logging in. You are now free to donate or apply for assistance";
            }

            return View("LoginResult", m);
        }
    }
}