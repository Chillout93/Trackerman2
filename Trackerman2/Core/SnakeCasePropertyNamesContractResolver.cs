using Newtonsoft.Json.Serialization;
using System.Text.RegularExpressions;

namespace Trackerman2.Core
{
    public class SnakeCasePropertyNamesContractResolver : DefaultContractResolver
    {
        public SnakeCasePropertyNamesContractResolver()
        {

        }

        protected override string ResolvePropertyName(string propertyName)
        {
            return string.Join("_", Regex.Split(propertyName, @"(?<=[a-z])(?=[A-Z])")).ToLower();
        }
    }
}