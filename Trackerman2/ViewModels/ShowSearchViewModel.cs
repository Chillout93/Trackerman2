using System.Collections.Generic;
using Trackerman2.DomainModels;

namespace Trackerman2.ViewModels
{
    public class ShowSearchViewModel
    {
        public IEnumerable<ShowViewModel> Shows { get; set; }
        public string SearchTerm { get; set; }
    }
}