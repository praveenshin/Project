using AngularJSWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngularJSWebApi.Controllers
{
    public class ServerRouteController : ApiController
    {
        private RouteRepository db = new RouteRepository();
        // GET api/server
        public IEnumerable<Route> Get()
        {
            return db.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var route = db.GetRouteById(id);

            if (route == null)
            {
                return NotFound();//returns 404
            }

            return Ok(route);//returns 200 with dataS
        }

        public IHttpActionResult Post(Route b)
        {

            var route = db.AddRoute(b);

            return Created(Request.RequestUri + "/" + route.route_id, route);
        }

        public void Put(int id, Route b)
        {
            b.route_id = id;

            if (!db.UpdateRoute(b))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void Delete(int id)
        {
            if (!db.RemoveRoute(id))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

    }
}
