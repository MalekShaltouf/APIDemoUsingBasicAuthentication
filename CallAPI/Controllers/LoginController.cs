using CallAPI.Models;
using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CallAPI.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserViewModel user)
        {
            User usr = new User()
            {
                UserName = user.USER_NAME,
                Password = user.PASSWORD
            };
            DataAccessContext context = new DataAccessContext();
            context.Users.Add(usr);
            context.SaveChanges();
            return RedirectToAction("Login", "Login");
        }
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "CallAPI");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserViewModel user)
        {
            DataAccessContext context = new DataAccessContext();
            if (context.Users.Any(u => u.UserName.ToLower() == user.USER_NAME.ToLower() && u.Password.ToLower() == user.PASSWORD.ToLower()))
            {
                FormsAuthentication.SetAuthCookie(user.USER_NAME + "|" + user.PASSWORD, false);
                return RedirectToAction("Index", "CallAPI");
            }
            ModelState.AddModelError("user-login-validation", "userName or password is wrong.");
            return View();
        }
        public ActionResult Logout() 
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}