using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.Models
{
    public class Users :IdentityUser
    {

       
        public string Role { get; set; }
        public string RoleId { get; set; }

    }
}
