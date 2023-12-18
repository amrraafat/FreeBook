using Domin.Entity;
using Infarstuructre.Data;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FreeBookDbContext _context;

        public AccountsController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> UserManager , FreeBookDbContext context)
        {
            _roleManager = roleManager;
            _userManager = UserManager;
            _context = context;
        }


        public IActionResult Roles()
        {
           return View(new RoleViweModel
            {
                NewRole = new NewRole(),
                Roles = _roleManager.Roles.OrderBy(x=>x.Name).ToList()
            });
            

        }
        //-------------------------------------------------------------------------- calling Users to Viwe
        public IActionResult Register()
        {
            var model = new RegisterViweModel
            {
                NewRegister = new NewRegister(),
                Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList(),
                Users = _context.VwUsers.OrderBy(x => x.Role).ToList() 
                /*_userManager.Users.OrderBy(x => x.Name).ToList() */
            };
            return View(model);


        }



        //--------------------------------------------------------------------------create New Users
        [HttpPost]
        public async Task <IActionResult> Register( RegisterViweModel model)
        {
            if (ModelState.IsValid)
            {
                var User = new ApplicationUser
                {
                    Id = model.NewRegister.Id,
                    Name = model.NewRegister.Name,
                    UserName = model.NewRegister.Email,
                    Email = model.NewRegister.Email,
                    ImageUser = model.NewRegister.ImgeUser,
                    ActiveUser = model.NewRegister.AciveUser,
                   


                };
                if (User.Id == null)
                {
                    User.Id = Guid.NewGuid().ToString();
                    var result = await _userManager.CreateAsync(User,model.NewRegister.Password);
                    if (result.Succeeded)
                    {
                        var Role = await _userManager.AddToRoleAsync(User, model.NewRegister.RoleName);
                        if (Role.Succeeded)
                        {
                            HttpContext.Session.SetString("msgType", "success");
                            HttpContext.Session.SetString("titel", " تم الحفظ");
                            HttpContext.Session.SetString("msg", "تم مجموعة المستخدم ");

                        }
                        else
                        {
                            HttpContext.Session.SetString("msgType", " لم يتم الحفظ");
                            HttpContext.Session.SetString("titel", "اسم المستخدم مستخدم من قبل");
                            HttpContext.Session.SetString("msg", "لم يتم مجموعة المستخدم ");
                        }

                    }
                    else
                    {

                    }
                }
               else
                {
                    var userUpdate = await _userManager.FindByIdAsync(User.Id);

                        userUpdate.Name = model.NewRegister.Name;
                        userUpdate.UserName = model.NewRegister.Email;
                        userUpdate.Email = model.NewRegister.Email;
                        userUpdate.ImageUser = model.NewRegister.ImgeUser;
                        userUpdate.ActiveUser = model.NewRegister.AciveUser;

                        var updateResult = await _userManager.UpdateAsync(userUpdate);

                        if (updateResult.Succeeded)
                        {
                            // Update the role if needed
                            var roles = await _userManager.GetRolesAsync(userUpdate);
                            if (roles.Any())
                            {
                                var removeRoleResult = await _userManager.RemoveFromRolesAsync(userUpdate, roles);
                                if (removeRoleResult.Succeeded)
                                {
                                    var addRoleResult = await _userManager.AddToRoleAsync(userUpdate, model.NewRegister.RoleName);

                                    if (addRoleResult.Succeeded)
                                    {
                                        HttpContext.Session.SetString("msgType", "success");
                                        HttpContext.Session.SetString("titel", "تم التحديث");
                                        HttpContext.Session.SetString("msg", "تم تحديث مستخدم بنجاح");
                                    }
                                    else
                                    {
                                        HttpContext.Session.SetString("msgType", "لم يتم التحديث");
                                        HttpContext.Session.SetString("titel", "خطأ في تحديث الدور");
                                        HttpContext.Session.SetString("msg", "لم يتم تحديث مستخدم بنجاح");
                                    }
                                }
                            }
                        }
                        else
                        {
                            HttpContext.Session.SetString("msgType", "لم يتم التحديث");
                            HttpContext.Session.SetString("titel", "خطأ في تحديث المستخدم");
                            HttpContext.Session.SetString("msg", "لم يتم تحديث مستخدم بنجاح");
                        }
                   
                }

            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);

                // Add the errors to the model state for display in the view
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }

                // Return to the same view with validation errors
                return View(errors);
            }
            return RedirectToAction("Register", "Accounts");
        }

        public async Task<IActionResult> DeleteUser(string Id)
        {
            var User = _userManager.Users.FirstOrDefault(x => x.Id == Id);
            if ((await _userManager.DeleteAsync(User)).Succeeded)
            {
                return RedirectToAction("Users");
            }
            return RedirectToAction("Users");
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
        public async Task<IActionResult> DeleteRoles(string Id)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == Id);
            if ((await _roleManager.DeleteAsync(role)).Succeeded)
            {
                return RedirectToAction("Roles");
            }
            return RedirectToAction("Roles");
        }


        public async Task<IActionResult> UpdateRole(string Id, string Name)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == Id);
            if ((await _roleManager.DeleteAsync(role)).Succeeded)
            {
                return RedirectToAction("Roles");
            }
            return RedirectToAction("Roles");
        }




        public IActionResult Login()
        {
            return View();
        }
    }
}
