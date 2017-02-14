using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trackerman2.DomainModels;

namespace Trackerman2.ViewModels
{
    public class TrackingShowsViewModel
    {
        public string UserID { get; set; }
        public Episode LatestEpisode { get; set; }
    }
}