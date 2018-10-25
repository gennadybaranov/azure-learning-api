using System;
using System.Collections.Generic;

namespace Itechart.Survey.WebApi.DataContracts
{
    public class UserDataContract
    {
        public string DisplayName { get; set; }

        public string UserName { get; set; }

        public IReadOnlyCollection<string> Roles { get; set; }

        public DateTime SignUpDate { get; set; }
    }
}
