using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Customer.API.Host.Middlewares.Dependencies
{
    public class DependencyResolutionsModel
    {
        public string Contract { get; set; }
        public string Implementation { get; set; }
        public ObjectScope ObjectScope { get; set; }
    }
}
