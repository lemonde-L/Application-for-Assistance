using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class GrantApplicationController : Controller
    {
        private CommunityAssist2017Entities db = new CommunityAssist2017Entities();

        public ActionResult Index()
        {
            if (Session["ukey"] == null)
            {
                Message m = new Message();
                m.MessageText = "You must be logged in to proceed";
                return View("GrantApplicationResult", m);
            }
            var list = db.GrantTypes.ToList();
            SelectList dropDowonList = new SelectList(list, "GrantTypeKey", "GrantTypeName");
            ViewBag.listName = dropDowonList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "GrantType" +
            "GrantApplicationRequestAmount, GrantApplicationReason")] GrantApplication ga)
        {
            GrantApplication g = new GrantApplication();
            g.PersonKey = (int)Session["ukey"];
            g.GrantApplicationStatusKey = 1;
            g.GrantAppicationDate = DateTime.Now;
            g.GrantApplicationRequestAmount = ga.GrantApplicationRequestAmount;
            g.GrantApplicationReason = ga.GrantApplicationReason;
            db.GrantApplications.Add(g);
            db.SaveChanges();

            Message m = new Message();
            m.MessageText = "Thank you for your application";

            return View("GrantApplicationResult", m);
        }
    }
}