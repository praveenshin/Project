using AngularJSWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace AngularJSWebApi.Controllers
{
    public class ServerUserController : ApiController
    {
        private UserRepository db = new UserRepository();
        // GET api/server
        public IEnumerable<User_details> Get()
        {
            return db.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var user = db.GetUserById(id);

            if (user == null)
            {
                return NotFound();//returns 404
            }

            return Ok(user);//returns 200 with dataS
        }

        public IHttpActionResult Post(User_details b)
        {
            b.password = Encryptdata(b.password);
            var user = db.AddUser(b);

            return Created(Request.RequestUri + "/" + user.user_id, user);
        }

        public void Put(int id, User_details b)
        {
            b.user_id = id;
            b.password = Encryptdata(b.password);
            if (!db.UpdateUser(b))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void Delete(int id)
        {
            if (!db.RemoveUser(id))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        private static string Encryptdata(string password)
        {
            byte[] encode = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(encode);
        }
      
    }
}
