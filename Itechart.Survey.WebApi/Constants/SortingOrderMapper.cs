using System.Collections.Generic;
using Itechart.Survey.Foundation.UserService;

namespace Itechart.Survey.WebApi.Constants
{
    internal static class SortingOrderMapper
    {
        public const string Ascending = "asc";
        public const string Descending = "desc";

        public static readonly IReadOnlyDictionary<string, SortingOrder> Map;


        static SortingOrderMapper()
        {
            Map = new Dictionary<string, SortingOrder>
            {
                { Ascending, SortingOrder.Ascending },
                { Descending, SortingOrder.Descending }
            };
        }
    }
}
