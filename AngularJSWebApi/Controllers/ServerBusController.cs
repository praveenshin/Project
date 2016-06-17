using AngularJSWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngularJSWebApi.Controllers
{
    public class ServerBusController : ApiController
    {
        private BusRepository db = new BusRepository();
        // GET api/server
        public IEnumerable<Bus> Get()
        {
            return db.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var bus = db.GetBusById(id);

            if (bus == null)
            {
                return NotFound();//returns 404
            }

            return Ok(bus);//returns 200 with dataS
        }
       
        public IHttpActionResult Post(Bus b)
        {
            
            var bus = db.AddBus(b);

            return Created(Request.RequestUri + "/" + bus.bus_id, bus);
        }

        public void Put(int id, Bus b)
        {
            b.bus_id = id;

            if (!db.UpdateBus(b))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void Delete(int id)
        {
            if (!db.RemoveBus(id))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}
