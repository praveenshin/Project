using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSWebApi.Models
{
    interface IUserRepository
    {
        IEnumerable<User_details> GetAll();
        User_details GetUserById(int id);
        User_details AddUser(User_details b);
        bool UpdateUser(User_details b);
        bool RemoveUser(int id);
    }
}
