using System.Collections.Generic;
using Trackerman2.DomainModels;

namespace Trackerman2.ViewModels
{
    public class ShowIndexViewModel
    {
        public IEnumerable<ShowViewModel> Shows { get; set; }
    }
}