using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Customer.API.Host.Middlewares.Dependencies
{
    public enum ObjectScope
    {
        Singleton = 1,
        Transient = 2,
        Scoped = 3
    }
}
