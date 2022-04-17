using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web2.Controllers
{
    public class AboutUsController : Controller
    {

        public ActionResult Index()
        {
            try
            {
                Models.Database db = new Models.Database();
                Models.User u = new Models.User();
                List<Models.Image> images = new List<Models.Image>();

                u.FirstName = "Maison";
                u.LastName = "Arroyo";
                u.Email = "mcarroyo@cincinnatistate.edu";
                images = db.GetUserImages(2, 0, true);
                u.UserImage = new Models.Image();
                if (images.Count > 0)
                {
                    u.UserImage = images[0];
                }

                return View(u);
            
            }

            catch (Exception) 
            {
                Models.User u = new Models.User();
                return View(u);
            }            
        }

        [HttpPost]
        public ActionResult Index(FormCollection col)
        {
            try
            {
                Models.User u = new Models.User();

                if (col["btnSubmit"] == "more")
                {
                    return RedirectToAction("More","AboutUs");
                }

                if (col["btnSubmit"] == "close")
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception)
            {
                Models.User u = new Models.User();
                return View(u);
            }
            return View();
        }
        
        public ActionResult More()
        {
            try
            {
                Models.User u = new Models.User();
                u.FirstName = "Maison";
                u.LastName = "Arroyo";

                return View(u);

            }
            catch (Exception)
            {
                Models.User u = new Models.User();
                return View(u);
            }
            return View();
        }

        [HttpPost]
        public ActionResult More(FormCollection col)
        {
            try
            {

                if (col["btnSubmit"] == "back")
                {
                    return RedirectToAction("Index", "AboutUs");
                }

            }
            catch (Exception)
            {
                Models.User u = new Models.User();
                return View(u);
            }
            return View();
        }
    
    
    
    
    }
}