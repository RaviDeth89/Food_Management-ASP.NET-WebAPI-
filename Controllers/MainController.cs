using Ravi_MscIT_WebAPI.Filter;
using Ravi_MscIT_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ravi_MscIT_WebAPI.Controllers
{
    public class MainController : Controller
    {
        dbFoodWebApiEntities context = new dbFoodWebApiEntities();
        // GET: Main
        [HttpGet]
        [CheckLoginFilter]
        public ActionResult HomePage()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoLogin(TBLUser user)
        {
            TBLUser checkUser = context.TBLUsers.Where(a=>a.UserName== user.UserName && a.Password==user.Password).FirstOrDefault();
            if(checkUser != null)
            {
                Session["CurrentUser"] = checkUser.Uid;
                return RedirectToAction("HomePage");
            }
            else {
                ViewData["errorMessage"] = "Enter right credentials";
                return View("Login"); 
            }
            
        }
    }
}