using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.Models
{
    public class UsersRole
    {
        public List<SelectListItem> UserRole;

        public UsersRole()
        {
            UserRole = new List<SelectListItem>();
        }

        public async Task<List<SelectListItem>> GetRole(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, string ID)
        {

            UserRole = new List<SelectListItem>();
            string rol;
            var user = await userManager.FindByIdAsync(ID);
            var roles = await userManager.GetRolesAsync(user);


            if (roles.Count == 0)
            {
                UserRole.Add(new SelectListItem()
                {
                    Value = "Null",
                    Text = "No Role"
                });
            }
            else
            {
                rol = Convert.ToString(roles[0]);
                var rolesid = roleManager.Roles.Where(x => x.Name == rol);
                foreach (var Data in rolesid)
                {
                    UserRole.Add(new SelectListItem()
                    {
                        Value = Data.Id,
                        Text = Data.Name
                    });
                }
            }
            return UserRole;
        }

         public List<SelectListItem>Roles(RoleManager<IdentityRole> roleManager)
        {
            var roles = roleManager.Roles.ToList();

            foreach (var Data in roles)
            {
                UserRole.Add(new SelectListItem() {
                    Value = Data.Id,
                    Text = Data.Name

                });
            }

            return UserRole;
        }
    }
}
