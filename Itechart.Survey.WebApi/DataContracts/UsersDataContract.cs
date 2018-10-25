using System.Collections.Generic;

namespace Itechart.Survey.WebApi.DataContracts
{
    public class UsersDataContract
    {
        public IReadOnlyCollection<UserDataContract> Users { get; set; }

        public int UsersCount { get; set; }
    }
}
