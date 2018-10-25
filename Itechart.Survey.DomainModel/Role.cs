using System.Collections.Generic;
using Itechart.Common;
using Microsoft.AspNet.Identity;

namespace Itechart.Survey.DomainModel
{
    public class Role : IRole<int>, IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<User> Users { get; set; }



        public static class BuiltIn
        {
            public const string Admin = "Admin";
            public const string User = "User";
        }
    }
}
