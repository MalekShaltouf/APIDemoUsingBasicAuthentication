using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CallAPI.Controllers
{
    [Authorize]
    public class CallAPIController : Controller
    {
        // GET: CallAPI
        public ActionResult Index()
        {
            string[] userName_Password = User.Identity.Name.Split('|');
            ViewBag.UserName = userName_Password[0];
            ViewBag.Password = userName_Password[1];
            return View();
        }
        public ActionResult Create() 
        {
            ViewBag.ProcessType = "ADD";
            string[] userName_Password = User.Identity.Name.Split('|');
            ViewBag.UserName = userName_Password[0];
            ViewBag.Password = userName_Password[1];
            return View();
        }
        public ActionResult Edit(int Id)
        {
            string[] userName_Password = User.Identity.Name.Split('|');
            ViewBag.UserName = userName_Password[0];
            ViewBag.Password = userName_Password[1];

            ViewBag.ProcessType = "EDIT";
            return View("Create");
        }
    }
}