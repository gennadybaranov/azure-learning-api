using System;
using System.Collections.Generic;
using Itechart.Common;
using Microsoft.AspNet.Identity;

namespace Itechart.Survey.DomainModel
{
    public class User : IUser<int>, IEntity
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }

        public DateTime SignUpDate { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
