using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class RegistrationController : Controller
    {
        private CommunityAssist2017Entities db = new CommunityAssist2017Entities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(
            [Bind(Include =
            "LastName, FirstName, Email, Phone, PlainPassword,"+
            "Apartment, Street, City, State, Zipcode")
            ]NewPerson np)
        {
            int result = db.usp_Register(
                np.LastName,
                np.FirstName,
                np.Email,
                np.PlainPassword,
                np.Apartment,
                np.Street,
                np.City,
                np.State,
                np.Zipcode,
                np.Phone);

            Message m = new Message();

            if (result == -1)
            {
                m.MessageText = "Sorry, but something seems to have gone wrong with the registration.";
            }
            else
            {
                m.MessageText = "Thanks for registering.";
            }

            return View("RegistrationResult", m);
        }
    }
}