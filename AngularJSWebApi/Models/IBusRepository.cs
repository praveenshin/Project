using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSWebApi.Models
{
    interface IBusRepository
    {
        IEnumerable<Bus> GetAll();
        Bus GetBusById(int id);
        Bus AddBus(Bus b);
        bool UpdateBus(Bus b);
        bool RemoveBus(int id);
    }
}
