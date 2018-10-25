using System;
using Itechart.Common;

namespace Itechart.Survey.DomainModel
{
    public class RefreshToken : IEntity
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public DateTime IssuedUtc { get; set; }

        public DateTime ExpiresUtc { get; set; }

        public string ProtectedTicket { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
