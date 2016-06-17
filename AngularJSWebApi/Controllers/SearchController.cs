using AngularJSWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularJSWebApi.Controllers
{
    public class SearchController : Controller
    {
        private DatabaseEntities1 db = new DatabaseEntities1();
        [HttpGet]
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Search(string source,string destination,string jdate,string cid)
        {
            ViewBag.src = source;
            ViewBag.dest = destination;
            DateTime d = Convert.ToDateTime(jdate);
            var routes = db.Routes.ToList();
            Route r = new Route();
           foreach(var route in routes)
           {
               if(route.source==source && route.destination==destination)
               {
                   r.route_id = route.route_id;
               }
           }
            int id=Convert.ToInt32(cid);
            var bus = db.Buses.Where(b=>b.category_id==id).Where(b=>b.route_id==r.route_id).Where(b=>b.start_date==d).Select(p=>p).ToList();
            ViewBag.message = bus.Count;
            return View(bus.ToList());
        }
        public ActionResult Book(int? id)
        {
            var bus = db.Buses.Find(id);
            Session["Obj"] = bus;
            return RedirectToAction("Login", "Login");
        }
	}
}