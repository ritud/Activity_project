using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using test_project.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace test_project.Controllers
{
     
    public class UserController : Controller
    {
        private UserContext _context;
        public UserController(UserContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
            User userexist = _context.user.Where(u => u.email == model.email).SingleOrDefault();
            if(userexist == null)
            {
            User user = new User();
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.password = Hasher.HashPassword(user, model.password);
            user.first_name = model.first_name;
            user.last_name = model.last_name;
            user.email = model.email;
            _context.user.Add(user);
            _context.SaveChanges();
            int UserId = _context.user.Last().userid;
            HttpContext.Session.SetInt32("UserId", UserId);
            return Redirect("/dashboard");
            }
            else
            {
                 TempData["Error1"] = "This email is already in use.Please enter valid email";
            }
            }
            return View("Index");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                User user=_context.user.Where(u => u.email == model.email1).SingleOrDefault();  
                if (user != null && model.password1 != null)
                {
                // Console.WriteLine("********************************"+user.password);
                    var Hasher = new PasswordHasher<User>();
                
                    if(0 != Hasher.VerifyHashedPassword(user, user.password, model.password1))
                    {
                        HttpContext.Session.SetInt32("UserId", user.userid);
                        return Redirect("dashboard");
                    }
                }
                TempData["Error"] = "Invalid Email/Password Combination";
                return View("Index");
            }
            return View("Index");
        }

    }
}
