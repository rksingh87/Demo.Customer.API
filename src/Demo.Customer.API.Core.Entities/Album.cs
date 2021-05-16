using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Customer.API.Core.Entities
{

    [Serializable]
    public class Album
    {

        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }
    }
}
