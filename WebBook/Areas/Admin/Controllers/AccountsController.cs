using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountsController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountsController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        public IActionResult Roles()
        {
           return View(new RoleViweModel
            {
                NewRole = new NewRole(),
                Roles = _roleManager.Roles.OrderBy(x=>x.Name).ToList()
            });
            

        }


        [HttpPost]
        public async Task<IActionResult> Roles(RoleViweModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Id = model.NewRole.RoleId,
                    Name = model.NewRole.RoleName
                };
                if (role.Id == null)
                {
                    role.Id = Guid.NewGuid().ToString();

                    IdentityResult result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Roles");
                    }
                    else
                    {

                    }
                }
            }
            if (ModelState == null)
            {

            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
