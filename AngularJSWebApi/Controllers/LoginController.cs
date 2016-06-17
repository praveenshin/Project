using AngularJSWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AngularJSWebApi.Controllers
{
    public class LoginController : Controller
    {
        private DatabaseEntities1 db = new DatabaseEntities1();
        public bool isValid(string name, string password)
        {
            var HasUser = db.User_details.Where(u => u.name == name).Where(u => u.password == password).Select(p => p).ToList();
            if (HasUser.Count == 0)
            {
                return false;
            }
            return true;
        }
        
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
       
        [HttpPost]
        public ActionResult Login(User_details user, string returnUrl)
        {
            var u = db.User_details.ToList();
            User_details u2 = new User_details();
            foreach (User_details u1 in u)
            {
                if (u1.name == user.name)
                {
                    u2.name = u1.name;
                    u2.password = u1.password;
                    u2.user_id = u1.user_id;
                }
            }
            int id = u2.user_id;
            Session["id"] = id;
            Session["name"] = u2.name;
            if (ModelState.IsValid)
            {
                if (isValid(user.name, user.password))
                {
                    if (FormsAuthentication.IsEnabled)
                    {
                        FormsAuthentication.SetAuthCookie(user.name, false);
                    }
                    if (returnUrl != null)
                    {
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
                return RedirectToAction("Error", "Login");
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult Error()
        {
            return View();
        }

         [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register([Bind(Include = "name,date_of_birth,email_id,mobile_no,password,address")] User_details model)
        {
            if (ModelState.IsValid)
            {

                db.User_details.Add(model);
                db.SaveChanges();
                FormsAuthentication.SetAuthCookie(model.name, false);
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Values");
            }
            return View(model);
        }
         [Authorize]
        public ActionResult Index()
        {
            var bus = Session["Obj"];
            string name = Convert.ToString(Session["name"]);
            var p = db.Passengers.Where(u => u.User_details.name == name).Select(p1 => p1).ToList();
            return View(p);
        }
         [Authorize]
        [HttpGet]
        public ActionResult Details()
        {
            return View();
        }
         [Authorize]
        [HttpPost]
        public ActionResult Details([Bind(Include="name,age,mobile_no")]Passenger p)
        {
            int uid=Convert.ToInt32(Session["id"]);
            Passenger p1 = new Passenger();
            p1.name = p.name;
            p1.age = p.age;
            p1.mobile_no = p.mobile_no;
            p1.user_id = uid;
            if(ModelState.IsValid)
            {
                db.Passengers.Add(p1);
                db.SaveChanges();
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
         [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passenger p = db.Passengers.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
         
            return View(p);
        }

         [Authorize]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "name,age,mobile_no")]Passenger p)
        {
            if (ModelState.IsValid)
            {
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(p);
        }
         [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passenger p = db.Passengers.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

         [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete1(int id)
        {
            Passenger book = db.Passengers.Find(id);
            db.Passengers.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

	}
}