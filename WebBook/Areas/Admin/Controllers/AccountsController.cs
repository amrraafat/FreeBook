using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Http;
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
        
        public async Task <IActionResult> DeleteRoles(string Id)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == Id);
            if ((await _roleManager.DeleteAsync(role)).Succeeded)
            {
                return RedirectToAction("Roles");
            }
            return RedirectToAction("Roles");
        }


        public async Task<IActionResult> UpdateRole(string Id , string Name  )
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == Id );
            if ((await _roleManager.DeleteAsync(role)).Succeeded)
            {
                return RedirectToAction("Roles");
            }
            return RedirectToAction("Roles");
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

                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        HttpContext.Session.SetString("msgType", "success");
                        HttpContext.Session.SetString("titel", " تم الحفظ");
                        HttpContext.Session.SetString("msg", "تم مجموعة المستخدم ");

                        return RedirectToAction("Roles");
                    }
                    else
                    {
                        List<IdentityError> errorList = result.Errors.ToList();
                        var errors = string.Join(", ", errorList.Select(e => e.Description));
                        if (result.Errors.Any(x=>x.Code == "DuplicateRoleName"))
                        {
                            HttpContext.Session.SetString("msgType", " لم يتم الحفظ");
                            HttpContext.Session.SetString("titel", "اسم المستخدم مستخدم من قبل");
                            HttpContext.Session.SetString("msg", "لم يتم مجموعة المستخدم ");
                        }
                        else
                        {
                            HttpContext.Session.SetString("msgType", " لم يتم الحفظ");
                            HttpContext.Session.SetString("titel", errors);
                            HttpContext.Session.SetString("msg", "لم يتم مجموعة المستخدم ");
                        }                         
                        //return Content(errors);
                        return RedirectToAction("Roles");

                    }
                }
                else
                {
                    var RoleUpdate = await _roleManager.FindByIdAsync(role.Id);
                    RoleUpdate.Id = model.NewRole.RoleId;
                    RoleUpdate.Name = model.NewRole.RoleName;
                    var Result =await _roleManager.UpdateAsync(RoleUpdate);
                    if (Result.Succeeded)
                    {
                        HttpContext.Session.SetString("msgType", "success");
                        HttpContext.Session.SetString("titel", " تم الحفظ");
                        HttpContext.Session.SetString("msg", "تم مجموعة المستخدم ");

                        return RedirectToAction("Roles");
                    }
                    else
                    {
                        HttpContext.Session.SetString("msgType", " erorr");
                        HttpContext.Session.SetString("titel", "لم يتم الحفظ");
                        HttpContext.Session.SetString("msg", "لم يتم مجموعة المستخدم ");
                        return RedirectToAction("Roles");
                    }
                }
            }
            
           
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
