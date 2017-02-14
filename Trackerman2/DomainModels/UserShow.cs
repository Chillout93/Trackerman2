using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trackerman2.Models;

namespace Trackerman2.DomainModels
{
    public class UserShow
    {
        public string UserID { get; set; }
        public int ShowID { get; set; }
        public bool Tracking { get; set; }

        public ApplicationUser User { get; set; }
        public Show Show { get; set; }
    }
}