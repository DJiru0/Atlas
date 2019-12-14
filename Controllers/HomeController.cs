using Atlas.Models.AltasModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Atlas.Controllers
{
    public class HomeController : Controller
    {
        dbContext db = new dbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [Authorize]
        public ActionResult UploadExcel()
        {
            return View("Meetings");
        }

        [Authorize]
        public ActionResult Meetings()
        {

            return View(db.Meetings.ToList());
        }

        [Authorize]
        public ActionResult ReviewMeetings()
        {

            return View(db.Meetings.OrderBy(r=> r.Priority).ToList());
        }

        [Authorize]
        public ActionResult Resources()
        {
            return View(db.Resources.ToList());
        }


        [Authorize]
        public ActionResult CreateResource()
        {
            return View();
        }


        [Authorize]
        public ActionResult MeetingDetail(int id)
        {

            return View(db.Meetings.Find(id));
        }
    }
}