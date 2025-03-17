using System.Text.RegularExpressions;

namespace Cenitu.Security.Services.Services
{
    internal static class OrderServiceHelpers
    {

        public static string RefineFilter(string filter)
        {
            var matches = Regex.Matches(filter, @"'([^']*)'");
            var filterValues = matches.Cast<Match>().Select(m => m.Groups[1].Value).ToList();
            filter = filterValues.First();
            return filter;
        }
    }
}