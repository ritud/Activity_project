using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using test_project.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace test_project.Controllers
{
     
    public class ActivityController : Controller
    {
        private UserContext _context;
        public ActivityController(UserContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            int? uId = HttpContext.Session.GetInt32("UserId");
            if(uId == null)
            {
                return Redirect("/");
            }
            ViewBag.uid=uId;
             List<Activity> activities=_context.activity.Where(a => a.date >= DateTime.Now)
                                                            .OrderBy(a => a.date)
                                                            .Include( a => a.participants )
                                                            .Include( a => a.User).ToList();
            
            ViewBag.activities = activities;
            return View();
        }

        [HttpGet]
        [Route("new")]
         public IActionResult New()
        {
            int? uId = HttpContext.Session.GetInt32("UserId");
            if(uId == null)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost]
        [Route("add")]
         public IActionResult Add(Activity model)
        {
             DateTime curr = DateTime.Now;
           
            if(ModelState.IsValid)
            {
                if(model.date <= curr)
               {
                   ModelState.AddModelError("date","You can not enter a date in future");
                   return View("New");
               }

                //Console.WriteLine("****************************************"+model.durationtype);
                model.userid = (int)HttpContext.Session.GetInt32("UserId");
                _context.activity.Add(model);
                _context.SaveChanges();
                int ActivityId = _context.activity.Last().activityid;
                return Redirect($"display/{ActivityId}");
            }
            if(model.date <= curr)
               {
                   ModelState.AddModelError("date","You can not enter a date in future");
               }
            return View("New");
        }

        [HttpGet]
        [Route("display/{id}")]
         public IActionResult Display(int id)
        {
            int? uId = HttpContext.Session.GetInt32("UserId");
            ViewBag.uid=uId;
            if(uId == null)
            {
                return Redirect("/");
            }
            //Console.WriteLine("***********************************"+id);
            Activity Activity = _context.activity.Where(a => a.activityid == id)
                                                    .Include(a => a.participants)
                                                        .ThenInclude(a => a.User)
                                                    .SingleOrDefault();
            
            ViewBag.activity= Activity;

            int ids = Activity.userid;
            //Console.WriteLine("****************************************"+userid);
            User user = _context.user.Where(u => u.userid == ids).SingleOrDefault();
            ViewBag.creator = user;


            return View();
        }

         [HttpGet]
        [Route("delete/{id}")]
         public IActionResult Delete(int id)
        {
            Activity activity = _context.activity.Where(a => a.activityid == id).SingleOrDefault();
            _context.activity.Remove(activity);
            _context.SaveChanges();
            return RedirectToAction("dashboard");
        }

        [HttpGet]
        [Route("join/{id}")]
         public IActionResult Join(int id)
        {
           Participant rs = new Participant();
            rs.activityid=id;
            rs.userid=(int)HttpContext.Session.GetInt32("UserId");
            _context.participants.Add(rs);
            _context.SaveChanges();
            return Redirect($"/display/{id}");
        }

          [HttpGet]
        [Route("joinit/{id}")]
         public IActionResult Joinit(int id)
        {
           Participant rs = new Participant();
            rs.activityid=id;
            rs.userid=(int)HttpContext.Session.GetInt32("UserId");
            _context.participants.Add(rs);
            _context.SaveChanges();
            return RedirectToAction("dashboard");
        }
        [HttpGet]
        [Route("leave/{id}")]
         public IActionResult Leave(int id)
        {
            Participant rs = new Participant();
            int userid=(int)HttpContext.Session.GetInt32("UserId");
            rs = _context.participants.Where(u => u.userid == userid && u.activityid == id).SingleOrDefault();
            _context.participants.Remove(rs);
            _context.SaveChanges();
            return Redirect($"/display/{id}");
        }
        [HttpGet]
        [Route("leaveit/{id}")]
         public IActionResult Leaveit(int id)
        {
            Participant rs = new Participant();
            int userid=(int)HttpContext.Session.GetInt32("UserId");
            rs = _context.participants.Where(u => u.userid == userid && u.activityid == id).SingleOrDefault();
            _context.participants.Remove(rs);
            _context.SaveChanges();
            return RedirectToAction("dashboard");
        }

        [HttpGet]
        [Route("logout")]
         public IActionResult Logout(int id)
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}
