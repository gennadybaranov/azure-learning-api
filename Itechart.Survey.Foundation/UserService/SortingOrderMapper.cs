using System.Collections.Generic;

namespace Itechart.Survey.Foundation.UserService
{
    internal static class SortingOrderMapper
    {
        public static readonly IReadOnlyDictionary<SortingOrder, Repositories.SortingOrder> Map;


        static SortingOrderMapper()
        {
            Map = new Dictionary<SortingOrder, Repositories.SortingOrder>
            {
                { SortingOrder.Ascending, Repositories.SortingOrder.Ascending },
                { SortingOrder.Descending, Repositories.SortingOrder.Descending }
            };
        }
    }
}
