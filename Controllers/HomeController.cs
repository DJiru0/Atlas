using Atlas.Models.AltasModel;
using Atlas.Models.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Atlas.Filter.RoleFilter;

namespace Atlas.Controllers
{
    public class HomeController : Controller
    {
        dbContext db = new dbContext();
        LoggingService logs = new LoggingService();
        SMSservice sms = new SMSservice();
        public ActionResult Index()
        {
            // logs.WriteLog("HomePage reached");
            //sms.CallSMSService();
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


        [UserAuthorize]
        public ActionResult UploadExcel()
        {
            return View("Meetings");
        }

        [RoleAuthorize]
        public ActionResult Meetings()
        {
            // logs.WriteLog("Meetings is being viewed");
            return View(db.Meetings.ToList());
        }
        [RoleAuthorize]
        public ActionResult Reservation()
        {

            IQueryable<Meeting> items = db.Meetings;
            var ids = db.Resources.Where(s => s.StatusId == 2 || s.StatusId == 3).Select(s => s.MeetingId).ToList();

            var Mids = db.Resources.Where(s => s.StatusId == 3).Select(s => s.MeetingId).ToList();
            var meetings = (from a in items
                            where
                              ids.Contains(a.MeetingId)
                            select a);
            //meetings = (from a in meetings
            //            where
            //             !Mids.Contains(a.MeetingId)
            //            select a);
            return View(meetings.OrderByDescending(r => r.Priority).ToList());
        }

        [UserAuthorize]
        public ActionResult ReviewMeetings()
        {
            // logs.WriteLog("Reviewer is viewing meetings for approval");
            IQueryable<Meeting> items = db.Meetings;
            var ids = db.Resources.Where(s => s.StatusId == 2 || s.StatusId == 3).Select(s=> s.MeetingId).ToList();

            var Mids = db.Resources.Where(s => s.StatusId == 3).Select(s => s.MeetingId).ToList();
            var meetings = (from a in items
                                    where
                                      ids.Contains(a.MeetingId)  
                                    select a);
            //meetings = (from a in meetings
            //            where
            //             !Mids.Contains(a.MeetingId)
            //            select a);
            return View(meetings.OrderByDescending(r=> r.Priority).ToList());
        }



        [UserAuthorize]
        public ActionResult DenyResource(int id)
        {
            var resource = db.Resources.Find(id);
            if (resource != null)
            {
                try
                {
                    resource.StatusId = 1;
                    resource.MeetingId = null;
                    resource.AttendeeId = null;
                    db.SaveChanges();

                    // logs.WriteLog("Resource "+ resource.ResourceType.Name + " was denied for meeting " + resource.Meeting.Name + "!");
                }
                catch (Exception ex)
                {

                }
            }

            return RedirectToAction("ReviewMeetings");
        }

        [UserAuthorize]
        public ActionResult ApproveResource(int id)
        {
            var resource = db.Resources.Find(id);
            if (resource != null)
            {
                try
                {
                    resource.StatusId = 3;
                    db.SaveChanges();
                    //sms.CallSMSService();
                    // logs.WriteLog("Resource " + resource.ResourceType.Name + " was approved for meeting " + resource.Meeting.Name + "!");
                }
                catch (Exception ex)
                {

                }
            }
            
            return RedirectToAction("ReviewMeetings");
        }


        [UserAuthorize]
        public ActionResult Resources()
        {
            return View(db.Resources.ToList());
        }



        [UserAuthorize]
        public ActionResult CreateResource()
        {
            var resourceTypes = db.ResourceTypes;
            ViewBag.Resources = new SelectList(resourceTypes, "ResourceTypeId", "Name");
            var statuses = db.Status;
            ViewBag.Statuses = new SelectList(statuses, "StatusId", "Name");
            var attendees = db.Status;
            ViewBag.Attendees = new SelectList(attendees, "AttendeeId");
            var meetings = db.Status;
            ViewBag.Meetings = new SelectList(meetings, "MeetingId");
            // logs.WriteLog("New Resource is created");
            return View();
        }


        [UserAuthorize]
        public ActionResult EditResource(int id)
        {
            return View();
        }

        [UserAuthorize]
        public ActionResult ResourceDetails(int id)
        {
            return View();
        }


        [UserAuthorize]
        public ActionResult RemoveResource(int id)
        {
            return View();
        }

        [RoleAuthorize]
        public ActionResult RequestResource(List<int> RequestedItems, int MeetingId)
        {
            string uniqueId = Session["user_id"].ToString();
            User user = db.Users.Where(r => r.ApplicationUserId == uniqueId).FirstOrDefault();
            if (user != null)
            {
                var attendeCheck = db.Attendees.Where(r => r.MeetingId == MeetingId && r.UserId == user.UserId).FirstOrDefault();

                Attendee attendee = new Attendee { MeetingId = MeetingId, UserId = user.UserId };
                if (attendeCheck == null)
                {
                    db.Attendees.Add(attendee);
                    db.SaveChanges();
                }
                else
                {
                    attendeCheck.MeetingId = MeetingId;
                }
                   
                foreach (int i in RequestedItems)
                {
                    Resource res = db.Resources.Where(r => r.ResourceTypeId == i).Where(r => r.StatusId == 1).FirstOrDefault();
                    // there are no resource that are avialable so return pending ones
                    if(res == null)
                    {
                        res = db.Resources.Where(r => r.ResourceTypeId == i).Where(r => r.StatusId == 2).FirstOrDefault();
                    }
                    //no pending
                    if (res != null)
                    {
                        res.StatusId = 2;
                        res.MeetingId = MeetingId;
                        res.AttendeeId = user.Attendees.Where(r => r.MeetingId == MeetingId && r.UserId == user.UserId).FirstOrDefault().AttendeeId;
                        // logs.WriteLog(res.ResourceType.Name + " Resource has been requested by " + user.Name);
                        db.SaveChanges();
                    }
                    else
                    {
                       // return to error page nosresultfound
                    }
                }
            }
            return RedirectToAction("Meetings");
        }


        [RoleAuthorize]
        public ActionResult MeetingDetail(int id)
        {

            return View(db.Meetings.Find(id));
        }
    }
}