﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trackerman2.Models;

namespace Trackerman2.DomainModels
{
    public class WatchedSeason
    {
        public string UserID { get; set; }
        public int SeasonID { get; set; }
        public bool Watched { get; set; }

        public ApplicationUser User { get; set; }
        public Season Season { get; set; }
    }
}