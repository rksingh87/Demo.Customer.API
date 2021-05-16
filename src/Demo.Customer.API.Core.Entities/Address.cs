﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Customer.API.Core.Entities
{
    public class Address
    {
        public string Street { get; set; }

        public string Suite { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public GeoLocation Geo { get; set; }
    }
}
