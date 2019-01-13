namespace SistemaAC.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using SistemaAC.Data;
    using SistemaAC.Data_Transfer_Object.UserDto;
    using SistemaAC.ModelClass;
    using SistemaAC.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="UsersController" />
    /// </summary>
    public class UsersController : Controller
    {
        /// <summary>
        /// Defines the _context
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Defines the _userManager
        /// </summary>
        internal UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Defines the _roleManager
        /// </summary>
        internal RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Defines the _usersRole
        /// </summary>
        internal UsersRole _usersRole;

        /// <summary>
        /// Defines the usuarioRole
        /// </summary>
        public List<SelectListItem> usuarioRole;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="ApplicationDbContext"/></param>
        /// <param name="userManager">The userManager<see cref="UserManager{ApplicationUser}"/></param>
        /// <param name="roleManager">The roleManager<see cref="RoleManager{IdentityRole}"/></param>
        public UsersController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _usersRole = new UsersRole();
            usuarioRole = new List<SelectListItem>();
        }

        // GET: Users
        /// <summary>
        /// The Index
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        public async Task<IActionResult> Index()
        {
            var ID = "";

            //declarando un obj list de la clase Usuario
            List<Users> user = new List<Users>();

            //Obteniendo todos los registro de la tabla donde almaceno los usuarios
            // y lo almaceno en el obj
            var appUser = await _context.ApplicationUser.ToListAsync();

            foreach (var Data in appUser)
            {
                ID = Data.Id;
                usuarioRole = await _usersRole.GetRole(_userManager, _roleManager, ID);

                user.Add(new Users()
                {
                    Id = Data.Id,
                    UserName = Data.UserName,
                    PhoneNumber = Data.PhoneNumber,
                    Email = Data.Email,
                    Role = usuarioRole[0].Text

                });

            }

            return View(user.ToList());
        }

        /// <summary>
        /// The getUser
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{Users}}"/></returns>
        public async Task<List<Users>> getUser(string id)
        {
            List<Users> user = new List<Users>();
            var appUsuario = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);

            var appUser = await _context.ApplicationUser.SingleOrDefaultAsync(x => x.Id == id);
            usuarioRole = await _usersRole.GetRole(_userManager, _roleManager, id);
            user.Add(new Users()
            {
                Id = appUser.Id,
                UserName = appUser.UserName,
                Email = appUser.Email,
                Role = usuarioRole[0].Text,
                RoleId = usuarioRole[0].Value,
                PhoneNumber = appUser.PhoneNumber,
                AccessFailedCount = appUser.AccessFailedCount,
                ConcurrencyStamp = appUser.ConcurrencyStamp,
                EmailConfirmed = appUser.EmailConfirmed,
                LockoutEnabled = appUser.LockoutEnabled,
                LockoutEnd = appUser.LockoutEnd,
                NormalizedEmail = appUser.NormalizedEmail,
                NormalizedUserName = appUser.NormalizedUserName,
                PasswordHash = appUser.PasswordHash,
                PhoneNumberConfirmed = appUser.PhoneNumberConfirmed,
                SecurityStamp = appUser.SecurityStamp,
                TwoFactorEnabled = appUser.TwoFactorEnabled
            });

            return user;
        }

        /// <summary>
        /// The GetRoles
        /// </summary>
        /// <returns>The <see cref="Task{List{SelectListItem}}"/></returns>
        public async Task<List<SelectListItem>> GetRoles()
        {
            //creamos un obj llamado rolesLista
            List<SelectListItem> rolesLista = new List<SelectListItem>();

            return rolesLista;
        }

        /// <summary>
        /// The EditUsers
        /// </summary>
        /// <param name="vm">The vm<see cref="UserViewModels"/></param>
        /// <returns>The <see cref="Task{string}"/></returns>
        public async Task<string> EditUsers(UserViewModels vm)
        {

            var resp = "";
            try {
                var applicationUser = new ApplicationUser
                {
                    Id = vm.id,
                    UserName = vm.userName,
                    Email = vm.email,
                    PhoneNumber = vm.phoneNumber,
                    AccessFailedCount = vm.accessFailedCount,
                    ConcurrencyStamp = vm.concurrencyStamp,
                    EmailConfirmed = vm.emailConfirmed,
                    LockoutEnabled = vm.lockoutEnabled,
                    LockoutEnd = vm.lockoutEnd,
                    NormalizedEmail = vm.normalizedEmail,
                    NormalizedUserName = vm.normalizedUserName,
                    PasswordHash = vm.passwordHash,
                    PhoneNumberConfirmed = vm.phoneNumberConfirmed,
                    SecurityStamp = vm.securityStamp,
                    TwoFactorEnabled = vm.twoFactorEnabled

                    //Actualizando datos
                };
                _context.Update(applicationUser);
                await _context.SaveChangesAsync();
                var user = await _userManager.FindByIdAsync(vm.id);
                usuarioRole = await _usersRole.GetRole(_userManager, _roleManager, vm.id);

                if (usuarioRole[0].Text != "No Role") {
                    await _userManager.RemoveFromRoleAsync(user, usuarioRole[0].Text);
                }
                if (vm.selectRole == "No Role") {
                    vm.selectRole = "Usuario";
                }
                var resultado = await _userManager.AddToRoleAsync(user, vm.selectRole);

                resp = "Save";
            }
            catch (Exception) {
                resp = "No Save";
            }
            return resp;
        }

        /// <summary>
        /// The DeleteUsers
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Task{String}"/></returns>
        public async Task<String> DeleteUsers(string id)
        {
            var message = "";
            try {
                var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
                _context.ApplicationUser.Remove(applicationUser);
                await _context.SaveChangesAsync();
                message = "Delete";
            } catch (Exception) {
                message = "Fail";
            }
            return message;
        }

        /// <summary>
        /// The CreateUser
        /// </summary>
        /// <param name="user">The user<see cref="UserDto"/></param>
        /// <returns>The <see cref="Task{String}"/></returns>
        public async Task<String> CreateUser(UserDto user) {
            var res = "";
            user.selectRole = "ROl";
            var applicationUser = new ApplicationUser
            {
                UserName = user.email,
                Email = user.email,
                PhoneNumber = user.phoneNumber,
            };
            var result = await _userManager.CreateAsync(user.applicationUser, user.passwordHash);

            if (result.Succeeded) {
                await _userManager.AddToRoleAsync(user.applicationUser, user.selectRole);
                res = "Save";
            }
            else {
                result.Errors.ToString();
            }
            return res;
        }

        /// <summary>
        /// The ApplicationUserExists
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool ApplicationUserExists(string id) {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}
