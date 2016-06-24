using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularJSWebApi.Controllers
{
    [Authorize(Users="Praveen")]
    public class AdminController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
	}
}