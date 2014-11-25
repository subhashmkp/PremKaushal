using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PremKaushal.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ViewResult iMessage(FormCollection form)
        {
            PremKaushalEntities PEntity = new PremKaushalEntities();
            string name, msg, email, contact;
            name = form["fname"].Trim().ToString() + " " + form["lname"].Trim().ToString();
            msg = form["message"].Trim().ToString();
            email = form["email"].Trim().ToString();
            contact = form["phone"].Trim().ToString();
            try
            {
                PEntity.sp_insertMessage(name, msg, email, contact);
            }
            catch (Exception ex)
            {
                PEntity.sp_insertLog("Error", "Error inserting Message: " + ex.Message);
            }
            PEntity.sp_insertLog("Info", "Message Inserted: " + name + ", " + msg + ", " + email);
            return View();
        }
    }
}
