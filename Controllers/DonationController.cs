using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class DonationController : Controller
    {
        private CommunityAssist2017Entities db = new CommunityAssist2017Entities();

        public ActionResult Index()
        {
            if (Session["ukey"] == null)
            {
                Message m = new Message();
                m.MessageText = "You must be logged in to proceed";
                return View("DonationResult", m);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Amount")] Donate dn)
        {
            Donation d = new Donation();
            d.DonationAmount = dn.Amount;
            d.DonationDate = DateTime.Now;
            d.PersonKey = (int)Session["ukey"];
            db.Donations.Add(d);
            db.SaveChanges();
            d.DonationConfirmationCode = Guid.NewGuid();

            Message m = new Message();
            m.MessageText = "Thank you for your donation";

            return View("DonationResult", m);
        }
    }
}