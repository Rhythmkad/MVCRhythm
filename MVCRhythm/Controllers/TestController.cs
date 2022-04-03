using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCRhythm.ViewModel
{
    public class TestController : Controller
    {
        
        public ActionResult Index()
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            else
            {
                ModelState.AddModelError("Failure", "Please Login !");
                return View();
            }
        }
        
    }
}
