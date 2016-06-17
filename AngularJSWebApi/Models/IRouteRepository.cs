using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSWebApi.Models
{
    interface IRouteRepository
    {
        IEnumerable<Route> GetAll();
        Route GetRouteById(int id);
        Route AddRoute(Route b);
        bool UpdateRoute(Route b);
        bool RemoveRoute(int id);
    }
}
