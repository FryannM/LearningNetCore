using SistemaAC.Models;
using System;

namespace SistemaAC.ModelClass
{
    public class UserViewModels
    {
        public string id { get; set; }
        public string userName { get; set; }
        public virtual string email { get; set; }
        public string phoneNumber { get; set; }
        public int accessFailedCount { get; set; }
        public string concurrencyStamp { get; set; }
        public bool emailConfirmed { get; set; }
        public bool lockoutEnabled { get; set; }
        public DateTimeOffset lockoutEnd { get; set; }
        public string normalizedEmail { get; set; }
        public string normalizedUserName { get; set; }
        public string passwordHash { get; set; }
        public bool phoneNumberConfirmed { get; set; }
        public string securityStamp { get; set; }
        public bool twoFactorEnabled { get; set; }
        public string selectRole { get; set; }
        public ApplicationUser applicationUse { get; set; }
    }
}
