using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trackerman2.Core
{
    public static class HtmlHelperExtensions
    {
        public static string ImageOrDefault(this HtmlHelper helper, string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return "/Content/DefaultImage.png";

            return "http://image.tmdb.org/t/p/w500" + filename;
        }
    }
}