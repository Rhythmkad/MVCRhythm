using MVCRhythm.Models;
using MVCRhythm.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCRhythm.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SaveUserDetails(UserMaster userDetails)
        {

            if (ModelState.IsValid)
            {
                using (var databaseContext = new MVCRhythmEntities())
                {
                    UserMaster objUserMaster = new UserMaster();
                    objUserMaster.FirstName = userDetails.FirstName;
                    objUserMaster.LastName = userDetails.LastName;
                    objUserMaster.Email = userDetails.Email;
                    objUserMaster.Password = userDetails.Password;
                    databaseContext.UserMasters.Add(objUserMaster);
                    databaseContext.SaveChanges();
                }

                ViewBag.Message = "User Master Save successfully";
                return View("Register");
            }
            else
            {

                return View("Register", userDetails);
            }
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {

                var isValidUser = IsValidUser(model);


                if (isValidUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Failure", "Wrong User Details!");
                    return View();
                }
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Welcome()
        {
            return View();
        }


        public UserMaster IsValidUser(Login model)
        {
            using (var dataContext = new MVCRhythmEntities())
            {
                UserMaster user = dataContext.UserMasters.Where(query => query.Email.Equals(model.Email) && query.Password.Equals(model.Password)).SingleOrDefault();
                if (user == null)
                {
                    return null;
                }
                else
                {
                    return user;
                }
            }
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); 
            return RedirectToAction("Index");
        }
    }
}